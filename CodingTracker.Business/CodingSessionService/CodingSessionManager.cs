using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionTimers;
using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IdGenerators;
using CodingTracker.Common.IErrorHandlers;
using CodingTracker.Common.IInputValidators;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace CodingTracker.Business.CodingSessionManagers
{



    public class CodingSessionManager : ICodingSessionManager
    {
        private CodingSession _currentCodingSession {  get; set; }

        private readonly IErrorHandler _errorHandler;
        private readonly IApplicationLogger _appLogger;
        private readonly IInputValidator _inputValidator;
        private readonly IIdGenerators _idGenerators;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ICodingSessionTimer _sessionTimer;
        private readonly IUserCredentialRepository _userCredentialRepository;

        private bool IsCodingSessionActive = false;

        public CodingSessionManager(IErrorHandler errorHandler, IApplicationLogger appLogger, IInputValidator inputValidator, IIdGenerators idGenerators, ICodingSessionRepository codingSessionRepo, ICodingSessionTimer sessionTimer, IUserCredentialRepository userCredentialRepository)
        {
            _errorHandler = errorHandler;
            _appLogger = appLogger;
            _inputValidator = inputValidator;
            _idGenerators = idGenerators;
            _codingSessionRepository = codingSessionRepo;
            _sessionTimer = sessionTimer;
            _userCredentialRepository = userCredentialRepository;
        }

        public CodingSession ReturnCurrentCodingSession(Activity activity)
        {
            if(activity == null)
            {
                _appLogger.Error($"Error during {nameof(ReturnCurrentCodingSession)} activity = null");
            }
            _appLogger.Info($"Starting {nameof(ReturnCurrentCodingSession)} traceId: {activity.TraceId}.");
            return _currentCodingSession;
        }



        public CodingSession CreateNewCodingSession(int userId)
        {
            if (userId <= 0)
            {
                _appLogger.Error($"Invalid UserId: {userId} for {nameof(CreateNewCodingSession)}. UserId must be positive.");
                throw new ArgumentException($"UserId must be positive. Provided: {userId}", nameof(CreateNewCodingSession));
            }

            if (IsCodingSessionActive)
            {
                _appLogger.Error($"Cannot start a new coding session while another one is active. IsCodingSessionActive = {IsCodingSessionActive}.");
                throw new InvalidOperationException("Cannot start a new coding session while another one is active.");
            }

            _appLogger.Info($"Starting {nameof(CreateNewCodingSession)}");
            int sessionId = _idGenerators.GenerateSessionId();
            DateTime currentDateTime = DateTime.UtcNow;

            CodingSession codingSession = new CodingSession
            {
                UserId = userId,
                SessionId = sessionId,
                StartDate = DateOnly.FromDateTime(currentDateTime),
                StartTime = currentDateTime,
            };

            _appLogger.Info($"New coding session created. UserId: {codingSession.UserId}, SessionId: {codingSession.SessionId}, StartDate: {codingSession.StartDate}, StartTime: {codingSession.StartTime}");
            return codingSession;
        }

        public async Task SetCurrentSessionUserIdAsync(string username)
        {
            UserCredentialEntity credential = await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);

            _appLogger.Debug($"UserId {credential.UserId} retrieved for username{username} for {nameof(SetCurrentSessionUserIdAsync)}");

            int userId = credential.UserId;

            _currentCodingSession.UserId = userId;

            _appLogger.Debug($"_currentCodingSession.UserId set to {_currentCodingSession.UserId}");
  
        }

        // This is called upon successful login, If a user never starts the timer the Session is never saved to the database. 
        public async Task StartCodingSession(string username)
        {
            _appLogger.Info($"Starting {nameof(StartCodingSession)}");

            UserCredentialEntity userCredential = await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);
            CodingSession newSession = CreateNewCodingSession(userCredential.UserId);

            IsCodingSessionActive = true;
            SetCurrentCodingSession(newSession);

            _appLogger.Info($"Coding session started CurrentCodingSession user id = {_currentCodingSession.UserId}.");
        }

        public void StartCodingSessionTimer()
        {
            DateTime currentDateTimeUtc = DateTime.UtcNow;

            _sessionTimer.StartCodingSessionTimer();
            _appLogger.Debug($"Session timer started at {currentDateTimeUtc}.");
        }

        public void EndCodingSessionTimer()
        {
            DateTime currentDateTimeUtc = DateTime.UtcNow;

            _sessionTimer.EndCodingSessionTimer();
            _appLogger.Debug($"Session timer started at {currentDateTimeUtc}.");
        }


        public void SetCodingSessionStartTimeAndDate(DateTime currentDateTimeUtc)
        {
            _currentCodingSession.StartDate = DateOnly.FromDateTime(currentDateTimeUtc);
            _currentCodingSession.StartTime = currentDateTimeUtc;

            _appLogger.Debug($"Session StartDate: {_currentCodingSession.StartDate}, StartTime: {_currentCodingSession.StartTime}.");
        }



        // This is also called inEndCodingSession in ApplicationControl,
        public async Task EndCodingSessionAsync()
        {
            _appLogger.Info($"Starting {nameof(EndCodingSessionAsync)}");
            DateTime currentEndTime = DateTime.UtcNow;
            DateOnly currentendDate = DateOnly.FromDateTime(currentEndTime);
            int currentDurationSeconds = CalculateDurationSeconds(_currentCodingSession.StartTime, currentEndTime);
            string currentDurationHHMM = ConvertDurationSecondsToStringHHMM(currentDurationSeconds);

            _currentCodingSession.EndTime = currentEndTime;
            _currentCodingSession.EndDate = currentendDate;
            _currentCodingSession.DurationSeconds = currentDurationSeconds;
            _currentCodingSession.DurationHHMM = currentDurationHHMM;

            CheckAllRequiredCodingSessionDetailsNotNull();

            CodingSessionEntity currentCodingSessionEntity = ConvertCodingSessionToCodingSessionEntity();

            await _codingSessionRepository.AddCodingSessionEntityAsync(currentCodingSessionEntity);

        }

        public void CheckAllRequiredCodingSessionDetailsNotNull()
        {
            StringBuilder nullProperties = new StringBuilder();

            foreach (var property in typeof(CodingSession).GetProperties())
            {
                var value = property.GetValue(_currentCodingSession);
                if (value == null)
                {
                    nullProperties.Append($"{property.Name}, ");
                }
            }
            if (nullProperties.Length > 0)
            {
                throw new InvalidOperationException($"The following properties are null: {nullProperties.ToString().TrimEnd(',', ' ')}");
            }
        }



        public CodingSessionEntity ConvertCodingSessionToCodingSessionEntity()
        {
            CodingSessionEntity codingSessionEntity = new CodingSessionEntity
            {
                SessionId = _currentCodingSession.SessionId,
                UserId = _currentCodingSession.UserId,
                StartDate = _currentCodingSession.StartDate ?? throw new ArgumentNullException($"StartDate cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                StartTime = _currentCodingSession.StartTime ?? throw new ArgumentNullException($"StartTime cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                EndDate = _currentCodingSession.EndDate ?? throw new ArgumentNullException($"EndDate cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                EndTime = _currentCodingSession.EndTime ?? throw new ArgumentNullException($"EndTime cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                DurationSeconds = _currentCodingSession.DurationSeconds ?? throw new ArgumentNullException($"DurationSeconds cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                DurationHHMM = _currentCodingSession.DurationHHMM,
                GoalMinutes = _currentCodingSession.GoalMinutes ?? throw new ArgumentNullException($"GoalMinutes cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                GoalReached = _currentCodingSession.GoalReached ?? throw new ArgumentNullException($"GoalReached cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}")
            };
            return codingSessionEntity;
        }


        public string ConvertDurationSecondsToStringHHMM(int durationSeconds)
        {
            if (durationSeconds < 0)
            {
                _appLogger.Error($"Negative duration provided: {durationSeconds} seconds.");
                throw new ArgumentOutOfRangeException(nameof(durationSeconds), $"DurationSeconds cannot be negative for {nameof(ConvertDurationSecondsToStringHHMM)}");
            }

            _appLogger.Info($"Starting {nameof(ConvertDurationSecondsToStringHHMM)}");
            int hours = durationSeconds / 3600;
            int minutes = (durationSeconds % 3600) / 60;
            string formattedTime = $"{hours:D2}:{minutes:D2}";
            _appLogger.Info($"Converted {durationSeconds} seconds to {formattedTime}");
            return formattedTime;
        }



        public void UpdateCodingSessionEndTimes()
        {
            _appLogger.Info($"Starting {nameof(UpdateCodingSessionEndTimes)}");
            DateTime currentDateTime = DateTime.UtcNow;
            _currentCodingSession.EndDate = DateOnly.FromDateTime(currentDateTime);
            _currentCodingSession.EndTime = currentDateTime;
            _appLogger.Info($"currentCodingSession endDate & endTime set to {_currentCodingSession.EndDate}, {_currentCodingSession.EndTime}");
        }


        public async Task SetUserIdForCurrentSessionAsync(string username, string password)
        {
            if (_currentCodingSession == null)
                throw new ArgumentNullException(nameof(SetUserIdForCurrentSessionAsync), $"_codingSession cannot be null for {nameof(SetCurrentCodingSession)}");

            UserCredentialEntity userCredential =  await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);

            _appLogger.Debug($"UserId {userCredential.UserId} retrieved for username{username} for {nameof(SetCurrentSessionUserIdAsync)}");

            _currentCodingSession.UserId = userCredential.UserId;

            _appLogger.Debug($"UserId set to: {_currentCodingSession.UserId}");
        }



        public void SetCurrentCodingSession(CodingSession codingSession)
        {
            if (codingSession == null)
                throw new ArgumentNullException(nameof(codingSession), $"codingSession cannot be null for {nameof(SetCurrentCodingSession)}");

            _appLogger.Info($"Starting {nameof(SetCurrentCodingSession)}");
            _currentCodingSession = codingSession;

            if (_currentCodingSession == codingSession)
            {
                _appLogger.Info($"_currentCodingSession updated successfully");
            }
            else
            {
                _appLogger.Error($"Failed to set current coding session");
                throw new InvalidOperationException("Failed to set current coding session");
            }
        }


        public int CalculateDurationSeconds(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null)
            {
                _appLogger.Error($"StartDate is null for {nameof(CalculateDurationSeconds)}");
                throw new ArgumentNullException(nameof(startDate), $"StartDate cannot be null for {nameof(CalculateDurationSeconds)}");
            }
            if (endDate == null)
            {
                _appLogger.Error($"EndDate is null for {nameof(CalculateDurationSeconds)}");
                throw new ArgumentNullException(nameof(endDate), $"EndDate cannot be null for {nameof(CalculateDurationSeconds)}");
            }
            if (startDate >= endDate)
            {
                _appLogger.Error($"StartDate ({startDate:yyyy-MM-dd HH:mm:ss}) must be earlier than EndDate ({endDate:yyyy-MM-dd HH:mm:ss}) for {nameof(CalculateDurationSeconds)}.");
                throw new InvalidOperationException($"StartDate ({startDate:yyyy-MM-dd HH:mm:ss}) must be earlier than EndDate ({endDate:yyyy-MM-dd HH:mm:ss}) for {nameof(CalculateDurationSeconds)}.");
            }

            _appLogger.Info($"Starting {nameof(CalculateDurationSeconds)}");
            int durationSeconds = (int)(endDate.Value - startDate.Value).TotalSeconds;
            _appLogger.Info($"durationSeconds calculated: {durationSeconds}");
            return durationSeconds;
        }

        public bool CheckIfCodingSessionActive()
        {
            using (var activity = new Activity(nameof(CheckIfCodingSessionActive)).Start())
            {
                var stopwatch = Stopwatch.StartNew();
                _appLogger.Debug($"Checking if session is active. TraceID: {activity.TraceId}");

                bool isActive = IsCodingSessionActive;

                stopwatch.Stop();
                _appLogger.Info($"Coding session active status: {isActive}, Execution Time: {stopwatch.ElapsedMilliseconds}ms, Trace ID: {activity.TraceId}");

                return isActive;
            }
        }


    }
}
