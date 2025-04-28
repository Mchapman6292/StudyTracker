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
        #region Properties

        private CodingSession _currentCodingSession { get; set; }
        private int _currentUserIdPlaceholder { get; set; }

        private readonly IApplicationLogger _appLogger;
        private readonly IInputValidator _inputValidator;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IUtilityService _utilityService;

        private bool IsCodingSessionActive { get; set; } = false;
        private bool IsSessionTimerActive { get; set; } = false;

        // This is used to store the GoalTime that is passed to the TimerDisplayForm, not relevant to CodingSessionEntity goalTime.
        private string _formGoalTimeHHMM { get; set; }
        private bool _isFormGoalSet { get; set; }

        #endregion

        #region Constructor

        public CodingSessionManager(IApplicationLogger appLogger, IInputValidator inputValidator, ICodingSessionRepository codingSessionRepo, IUserCredentialRepository userCredentialRepository, IUtilityService utilityService)
        {
            _appLogger = appLogger;
            _inputValidator = inputValidator;
            _codingSessionRepository = codingSessionRepo;
            _userCredentialRepository = userCredentialRepository;
            _utilityService = utilityService;
        }

        #endregion

        #region Session Management

        public void StartCodingSession(DateTime startTime, double? sessionGoalSeconds, bool codingGoalSet)
        {
            CodingSession session = CreateCurrentCodingSession();
            SetCurrentCodingSession(session);
            SetCodingSessionStartTimeAndDate(startTime);
            UpdateISCodingSessionActive(true);
            SetCurrentSessionGoalSeconds(sessionGoalSeconds);
            SetCurrentSessionGoalSet(codingGoalSet);
        }

        /// <summary>
        /// Controller method that calls other methods to established the final values for the CodingSession class before it is converted to a CodingSessionEntity and added to the database. All checks for valid CodingSession values are done within the child methods.
        /// </summary>
        public async Task EndCodingSessionAsync()
        {
            // Set EndTime & EndDate.
            DateTime endTime = DateTime.Now;
            DateOnly currentendDate = DateOnly.FromDateTime(endTime);

            // Calculate DurationSeconds & format to DurationHHMM
            int currentDurationSeconds = CalculateDurationSeconds(_currentCodingSession.StartTime, endTime);
            string currentDurationHHMM = ConvertDurationSecondsToStringHHMM(currentDurationSeconds);
            SetDurationHHMM(currentDurationHHMM);
            SetDurationSeconds(currentDurationSeconds);
            SetCodingSessionEndTimeAndDate(endTime);

            UpdateGoalCompletionStatus();

            CheckAllRequiredCodingSessionDetailsNotNull();

            //Dates are stored as local time in CodingSession as these are the values the user will see.
            CodingSessionEntity currentCodingSessionEntity = ConvertCodingSessionToCodingSessionEntity();
            _utilityService.ConvertCodingSessionDatesToUTC(currentCodingSessionEntity);

            bool sessionAddedToDb = await _codingSessionRepository.AddCodingSessionEntityAsync(currentCodingSessionEntity);

            UpdateISCodingSessionActive(false);
        }

        public async Task EndCodingSessionWithTimerFinished()
        {
            // Duration, EndTime & EndDate are all set once timer completes or exit buttons are actioned. 
            string currentDurationHHMM = ConvertIntToHHMMStringWitSemiColon(_currentCodingSession.DurationSeconds);

            UpdateGoalCompletionStatus();

            //Dates are stored as local time in CodingSession as these are the values the user will see.
            CodingSessionEntity currentCodingSessionEntity = ConvertCodingSessionToCodingSessionEntity();

            bool sessionAddedToDb = await _codingSessionRepository.AddCodingSessionEntityAsync(currentCodingSessionEntity);

            UpdateISCodingSessionActive(false);
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

        public void Initialize_CurrentCodingSession(int userId)
        {
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

        public void UpdateCodingSessionNoGoalSet()
        {
            UpdateIsSessionTimerActive(true);
            UpdateISCodingSessionActive(true);
            SetCurrentSessionGoalSet(false);
            SetCurrentSessionGoalReached(false);
        }

        public void UpdateCodingSessionGoalSet(int sessionGoalSeconds)
        {
            SetCurrentSessionGoalSet(true);
            UpdateISCodingSessionActive(true);
            SetCurrentSessionGoalSeconds(sessionGoalSeconds);
            UpdateISCodingSessionActive(true);
        }

        public void UpdateCodingSessionEndTimes()
        {
            _appLogger.Info($"Starting {nameof(UpdateCodingSessionEndTimes)}");
            DateTime currentDateTime = DateTime.UtcNow;
            _currentCodingSession.EndDate = DateOnly.FromDateTime(currentDateTime);
            _currentCodingSession.EndTime = currentDateTime;
            _appLogger.Info($"currentCodingSession endDate & endTime set to {_currentCodingSession.EndDate}, {_currentCodingSession.EndTime}");
        }

 



        #endregion

        #region Session State Management

        private CodingSession CreateCurrentCodingSession()
        {
            return new CodingSession
            {
                UserId = _currentUserIdPlaceholder
            };
        }

        private void SetCurrentCodingSession(CodingSession currentSession)
        {
            _currentCodingSession = currentSession;
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

        public void SetCurrentUserIdPlaceholder(int userId)
        {
            _currentUserIdPlaceholder = userId;
        }

        public int ReturnCurrentUserIdPlaceholder()
        {
            return _currentUserIdPlaceholder;
        }

        #endregion

        #region Time and Duration Methods

        public void SetCodingSessionStartTimeAndDate(DateTime startTime)
        {
            _currentCodingSession.StartTime = startTime;
            _currentCodingSession.StartDate = DateOnly.FromDateTime(startTime);
        }

        public DateTime? ReturnCurrentSessionStartTime()
        {
            if (_currentCodingSession.StartTime != null)
            {
                return _currentCodingSession.StartTime;
            }
            throw new InvalidOperationException($"Tried to return _currentSession.StartTime before it is initialized, this should never occur.");
        }

        public void SetCodingSessionEndTimeAndDate(DateTime endTime)
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

        public void SetDurationSeconds(double? durationSeconds)
        {
            durationSeconds = _currentCodingSession.DurationSeconds;
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

        public string ConvertIntToHHMMStringWitSemiColon(double? durationSeconds)
        {
            if (!durationSeconds.HasValue)
            {
                _appLogger.Error("durationSeconds cannot be null when ending codingSession.");
                throw new InvalidOperationException("durationSeconds cannot be null when ending codingSession.");
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(durationSeconds.Value);

            int totalHours = (int)timeSpan.TotalHours;
            int minutes = timeSpan.Minutes;
            return $"{totalHours:D2}:{minutes:D2}";
        }

        #endregion

        #region Goal Management

        public double? ReturnGoalSeconds()
        {
            return _currentCodingSession.GoalSeconds;
        }

        public void SetCurrentSessionGoalSet(bool goalSet)
        {
            _currentCodingSession.GoalSet = goalSet;
            _appLogger.Debug($"GoalSet updated to {goalSet}.");
        }

        public void SetCurrentSessionGoalReached(bool? goalReached)
        {
            _currentCodingSession.GoalReached = goalReached;
        }

        public bool? ReturnCurrentSessionGoalReached()
        {
            return _currentCodingSession.GoalReached;
        }

        public void SetCurrentSessionGoalSeconds(double? goalSeconds)
        {
            _currentCodingSession.GoalSeconds = goalSeconds;
            _appLogger.Debug($"GoalSeconds set to {goalSeconds}");
        }

        public void SetGoalHoursAndGoalMins(int goalMins, bool goalSet)
        {
            if (goalSet)
            {
                _currentCodingSession.GoalSeconds = goalMins;
                _appLogger.Debug($"CurrentCodingSession GoalMins set to {_currentCodingSession.GoalSeconds}");
                return;
            }
            _currentCodingSession.GoalSeconds = 0;
            _appLogger.Debug($"CurrentCdoingSession GoalMins set to {_currentCodingSession.GoalSeconds}, default for not set.");
        }

        public void UpdateGoalCompletionStatus()
        {
            try
            {
                _appLogger.Debug($"Starting UpdateGoalCompletionStatus method");

                // Default to false unless conditions are met.
                _currentCodingSession.GoalReached = false;
                _appLogger.Debug($"Default GoalReached set to false");

                // Log the current session values we'll be checking
                _appLogger.Debug($"Session values - GoalSet: {(_currentCodingSession.GoalSet.HasValue ? _currentCodingSession.GoalSet.ToString() : "null")}, GoalSeconds: {(_currentCodingSession.GoalSeconds.HasValue ? _currentCodingSession.GoalSeconds.ToString() : "null")}, DurationSeconds: {_currentCodingSession.DurationSeconds}");

                // .Value checks if GoalSet = true
                if (_currentCodingSession.GoalSet.HasValue && _currentCodingSession.GoalSet.Value)
                {
                    _appLogger.Debug($"GoalSet is true, continuing evaluation");

                    if (_currentCodingSession.GoalSeconds.HasValue)
                    {
                        double? goalSeconds = _currentCodingSession.GoalSeconds * 60;
                        _appLogger.Debug($"GoalSeconds has value: {_currentCodingSession.GoalSeconds}, calculated goalSeconds: {goalSeconds}");

                        _appLogger.Debug($"Comparing DurationSeconds: {_currentCodingSession.DurationSeconds} to goalSeconds: {goalSeconds}");
                        if (_currentCodingSession.DurationSeconds >= goalSeconds)
                        {
                            _appLogger.Debug($"Duration meets or exceeds goal time, setting GoalReached to true");
                            _currentCodingSession.GoalReached = true;
                            return;
                        }
                        else
                        {
                            _appLogger.Debug($"Duration is less than goal time, leaving GoalReached as false");
                        }
                    }
                    else
                    {
                        _appLogger.Debug($"GoalSeconds is null, cannot evaluate goal completion");
                    }
                }
                else
                {
                    _appLogger.Debug($"GoalSet is not true, goal completion not evaluated");
                }

                _appLogger.Debug($"Completed UpdateGoalCompletionStatus, final GoalReached value: {_currentCodingSession.GoalReached}");
            }
            catch (NullReferenceException ex)
            {
                _appLogger.Error($"Null value detected when updating completion status during {nameof(UpdateGoalCompletionStatus)}: " + ex.Message);
                throw new InvalidOperationException("Cannot evaluate goal status because one or more required values are null.", ex);
            }
        }

        #endregion

        #region Validation and Conversion

        public void CheckAllRequiredCodingSessionDetailsNotNull()
        {
            var nullProperties = typeof(CodingSession)
                .GetProperties()
                .Where(p => p.Name != "SessionId")
                .Where(p =>
                {
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
                DurationHHMM = _currentCodingSession.DurationHHMM ?? throw new ArgumentNullException($"DurationHHMM cannot be null when creating CdoingSesisonEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                GoalSeconds = _currentCodingSession.GoalSeconds ?? throw new ArgumentNullException($"GoalSeconds cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                GoalReached = _currentCodingSession.GoalReached ?? throw new ArgumentNullException($"GoalReached cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}")
            };
        }

        #endregion
    }
}