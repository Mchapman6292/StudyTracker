using CodingTracker.Common.IErrorHandlers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.CodingSessionDTOs;
using System.Diagnostics;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.IdGenerators;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using System.Linq.Expressions;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionTimers;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;

namespace CodingTracker.Business.CodingSessionManagers
{



    public class CodingSessionManager : ICodingSessionManager
    {
        public CodingSession _currentCodingSession;

        private readonly IErrorHandler _errorHandler;
        private readonly IApplicationLogger _appLogger;
        private readonly IInputValidator _inputValidator;
        private readonly IIdGenerators _idGenerators;
        private readonly ICodingSessionRepository _codingSessionRepo;
        private readonly ICodingSessionTimer _sessionTimer;
        private readonly IUserCredentialRepository _userCredentialRepository;

        private bool IsCodingSessionActive = false;

        public CodingSessionManager(IErrorHandler errorHandler, IApplicationLogger appLogger, IInputValidator inputValidator, IIdGenerators idGenerators, ICodingSessionRepository codingSessionRepo, ICodingSessionTimer sessionTimer, IUserCredentialRepository userCredentialRepository)
        {
            _errorHandler = errorHandler;
            _appLogger = appLogger;
            _inputValidator = inputValidator;
            _idGenerators = idGenerators;
            _codingSessionRepo = codingSessionRepo;
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

        // This is called upon successful login, If a user never starts the timer the Session is never saved to the database. 
        public void StartCodingSession()
        {
            _appLogger.Info($"Starting {nameof(StartCodingSession)}");
            
            int userId = _currentCodingSession.UserId;
            CodingSession newSession = CreateNewCodingSession(userId);

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



        public void EndCodingSession()
        {
            _appLogger.Info($"Starting {nameof(EndCodingSession)}");
            DateTime currentEndTime = DateTime.UtcNow;
            DateOnly currentendDate = DateOnly.FromDateTime(currentEndTime);
            int currentDurationSeconds = CalculateDurationSeconds(_currentCodingSession.StartTime, currentEndTime);
            string currentDurationHHMM = ConvertDurationSecondsToStringHHMM(currentDurationSeconds);

            _currentCodingSession.EndTime = currentEndTime;
            _currentCodingSession.EndDate = currentendDate;
            _currentCodingSession.DurationSeconds = currentDurationSeconds;
            _currentCodingSession.DurationHHMM = currentDurationHHMM;
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
