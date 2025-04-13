using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;

namespace CodingTracker.Business.CodingSessionManagers
{



    public class CodingSessionManager : ICodingSessionManager
    {
        private CodingSession _currentCodingSession {  get; set; }
        private int CurrentUserId { get; set; }

        private readonly IApplicationLogger _appLogger;
        private readonly IInputValidator _inputValidator;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IUserIdService _userIdService;
        private readonly IUtilityService _utilityService;

        private bool IsCodingSessionActive { get; set; } = false;
        private bool IsSessionTimerActive { get; set; } = false;

        // This is used to store the GoalTime that is passed to the TimerDisplayForm, not relevant to CodingSessionEntity goalTime.
        private string _formGoalTimeHHMM { get; set; }   
        private bool _isFormGoalSet { get; set; }

        public CodingSessionManager( IApplicationLogger appLogger, IInputValidator inputValidator, ICodingSessionRepository codingSessionRepo,IUserCredentialRepository userCredentialRepository, IUserIdService userIdService, IUtilityService utilityService)
        {
            _appLogger = appLogger;
            _inputValidator = inputValidator;
            _codingSessionRepository = codingSessionRepo;
            _userCredentialRepository = userCredentialRepository;
            _userIdService = userIdService;
            _utilityService = utilityService;
        }


        public void StartCodingSession(DateTime startTime, int sessionGoalMins, bool codingGoalSet)
        {
            SetCodingSessionStartTimeAndDate(startTime);
            UpdateISCodingSessionActive(true);
            SetGoalMinutes(sessionGoalMins);
            SetCurrentSessionGoalSet(codingGoalSet);
        }


        
        /// <summary>
        /// Controller method that calls other methods to established the final values for the CodingSession class before it is converted to a CodingSessionEntity and added to the database. All checks for valid CodingSession values are done within the child methods.
        public async Task EndCodingSessionAsync(bool? goalReached)
        {
            if(_currentCodingSession.GoalSet == false)
            {
                goalReached = false;
            }

            // This is set in case the timer ends but the user does not close the form straight away.
            SetCurrentSessionGoalReached(goalReached);
            // Set EndTime & EndDate.
            DateTime endTime = DateTime.Now;
            DateOnly currentendDate = DateOnly.FromDateTime(endTime);

            // Calculate DurationSeconds & format to DurationHHMM
            int currentDurationSeconds = CalculateDurationSeconds(_currentCodingSession.StartTime, endTime);
            string currentDurationHHMM = ConvertDurationSecondsToStringHHMM(currentDurationSeconds);
            SetDurationHHMM(currentDurationHHMM);
            SetDurationSeconds(currentDurationSeconds);
            SetCodingSessionEndTimeAndDate(endTime);
            CheckAllRequiredCodingSessionDetailsNotNull();

            //Dates are stored as local time in CodingSession as these are the values the user will see.
            CodingSessionEntity currentCodingSessionEntity = ConvertCodingSessionToCodingSessionEntity();
            _utilityService.ConvertCodingSessionDatesToUTC(currentCodingSessionEntity);

            bool sessionAddedToDb = await _codingSessionRepository.AddCodingSessionEntityAsync(currentCodingSessionEntity);

            UpdateISCodingSessionActive(false);
        }

        public async Task EndCodingSessionWithoutGoalSet()
        {

        }



        public CodingSession ReturnCurrentCodingSession()
        {
            return _currentCodingSession;
        }

        public void UpdateISCodingSessionActive(bool active) 
        {
            IsCodingSessionActive = active; 
        }

        public void UpdateIsSessionTimerActive(bool active)
        {
            IsSessionTimerActive = active;
        }

        public bool ReturnIsCodingSessionActive()
        {
            return IsCodingSessionActive;
        }

        public void SetCodingSessionStartTimeAndDate(DateTime startTime)
        {
            _currentCodingSession.StartTime = startTime;
            _currentCodingSession.StartDate = DateOnly.FromDateTime(startTime);
        }

        public void SetCodingSessionEndTimeAndDate( DateTime endTime)
        {
            _currentCodingSession.EndTime = endTime;
            _currentCodingSession.EndDate = DateOnly.FromDateTime(endTime);
        }

        public void SetDurationSeconds(int durationSeconds)
        {
            _currentCodingSession.DurationSeconds = durationSeconds;
        }

        public void SetDurationHHMM(string durationHHMM) 
        {
            _currentCodingSession.DurationHHMM = durationHHMM;  
        }

        public void SetGoalMinutes(int goalMinutes)
        {
            _currentCodingSession.GoalMinutes = goalMinutes;
        }

        public void SetCurrentSessionGoalSet(bool goalSet)
        {
            _currentCodingSession.GoalSet = goalSet;
        }

        public void SetCurrentSessionGoalReached(bool? goalReached) 
        {
            _currentCodingSession.GoalReached = goalReached;
        }

        public bool? ReturnCurrentSessionGoalReached()
        {
            return _currentCodingSession.GoalReached;
        }









        public void Initialize_CurrentCodingSession(int userId)
        {
            if (userId <= 0)
            {
                _appLogger.Error($"Invalid UserId: {userId} for {nameof(Initialize_CurrentCodingSession)}. UserId must be positive.");
                throw new ArgumentException($"UserId must be positive. Provided: {userId}", nameof(Initialize_CurrentCodingSession));
            }


            _appLogger.Info($"Starting {nameof(Initialize_CurrentCodingSession)}");
            DateTime currentDateTime = DateTime.UtcNow;

            CodingSession codingSession = new CodingSession
            {
                UserId = userId,
            };

            _appLogger.Info($"New coding session created. UserId: {codingSession.UserId}, SessionId: {codingSession.SessionId}, StartDate: {codingSession.StartDate}, StartTime: {codingSession.StartTime}");
            _currentCodingSession = codingSession;

            UpdateISCodingSessionActive(true);
        }

        public async Task SetCurrentSessionUserIdAsync(string username)
        {
            UserCredentialEntity credential = await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);

            _appLogger.Debug($"UserId {credential.UserId} retrieved for username{username} for {nameof(SetCurrentSessionUserIdAsync)}");

            int userId = credential.UserId;

            _currentCodingSession.UserId = userId;

            CurrentUserId = userId;

            _appLogger.Debug($"_currentCodingSession.UserId set to {_currentCodingSession.UserId}");
        }

        public int GetCurrentUserId() 
        {
            return CurrentUserId;
        }

        // This is called upon successful login, If a user never starts the timer the Session is never saved to the database. 
        public async Task OldStartCodingSession(string username)
        {
            _appLogger.Info($"Starting {nameof(OldStartCodingSession)}");

            UserCredentialEntity userCredential = await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);

            Initialize_CurrentCodingSession(userCredential.UserId);

            UpdateISCodingSessionActive(true); // This is when have start a second/third session etc, a new CodingSession object is created, only when the a user completes a timed study session is this converted to a CodingSesisonEntity and added to the database. 

            _appLogger.Info($"Coding session started CurrentCodingSession user id = {_currentCodingSession.UserId}.");
        }





        public void SetGoalHoursAndGoalMins(int goalMins, bool goalSet)
        {
            if(goalSet) 
            {
                _currentCodingSession.GoalMinutes = goalMins;
                _appLogger.Debug($"CurrentCodingSession GoalMins set to {_currentCodingSession.GoalMinutes}");
                return;
            }
            _currentCodingSession.GoalMinutes = 0;
            _appLogger.Debug($"CurrentCdoingSession GoalMins set to {_currentCodingSession.GoalMinutes}, default for not set.");

        }




        public void CheckAllRequiredCodingSessionDetailsNotNull()
        {
            var nullProperties = typeof(CodingSession)
                .GetProperties()
                .Where(p => p.Name != "SessionId")  
                .Where(p => {
                    var value = p.GetValue(_currentCodingSession);
                    return value == null ||
                           (p.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value)) ||
                           (p.PropertyType == typeof(DateTime?) && value == null) ||
                           (p.PropertyType == typeof(DateOnly?) && value == null) ||
                           (p.PropertyType == typeof(int?) && value == null) ||
                           (p.PropertyType == typeof(bool?) && value == null);
                })
                .Select(p => p.Name);

            if (nullProperties.Any())
            {
                throw new InvalidOperationException(
                    $"The following properties are null: {string.Join(", ", nullProperties)}");
            }
        }



        public CodingSessionEntity ConvertCodingSessionToCodingSessionEntity()
        {
            return new CodingSessionEntity
            {
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
            _appLogger.Debug($"Values for {nameof(CalculateDurationSeconds)} startDate: {startDate}, endDate: {endDate}");

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

 


    }
}
