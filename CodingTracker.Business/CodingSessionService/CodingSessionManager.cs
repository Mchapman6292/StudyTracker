using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.Utilities;

namespace CodingTracker.Business.CodingSessionManagers
{
    /// <summary>
    /// Important design points
    /// - There are two classes used in the management/saving of the coding session: CodingSession & CodingSessionEntity
    /// - CodingSession is used while the coding session is active, with all current session values being saved to the _currentCodingSession property.
    /// - When the CodingSession is complete it is converted to a CodingSessionEntity & all DateTimes are converted to utc.

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

        #endregion

        #region Constructor

        public CodingSessionManager(IApplicationLogger appLogger, IInputValidator inputValidator,ICodingSessionRepository codingSessionRepository, IUserCredentialRepository userCredentialRepository,IUtilityService utilityService)
        {
            _appLogger = appLogger;
            _inputValidator = inputValidator;
            _codingSessionRepository = codingSessionRepository;
            _userCredentialRepository = userCredentialRepository;
            _utilityService = utilityService;
        }

        #endregion

        #region Controller Methods

        /// <summary>
        ///  Creates the CodingSession object and sets it to _currentCodingSession.
        ///  This is called in SessionGoalPage from the start or skip button.
        ///  If the user wants to set a session goal the start button is used, if skip is used then the SessionGoal is set to 0.


        public void InitializeCodingSessionAndSetGoal(int sessionGoalSeconds, bool codingGoalSet)
        {
            CodingSession session = CreateCurrentCodingSession();
            SetCurrentCodingSession(session);
            SetCurrentSessionGoalSeconds(sessionGoalSeconds);
            SetCurrentSessionGoalSet(codingGoalSet);
        }

        /// <summary>
        /// This is called in the load method of the CountdownTimerForm, sets the StartTime & Date.
        ///  Is also responsible for Updating bools IsCodingSessionActive & IsSessionTimerActive.

        public void UpdateSessionStartTimeAndActiveBoolsToTrue()
        {
            SetCodingSessionStartTimeAndDate(DateTime.UtcNow);
            UpdateISCodingSessionActive(true);
            UpdateIsSessionTimerActive(true);
        }

        /// <summary>
        /// Updates the session when the timer has ended, setting duration and end time.
        public void UpdateCodingSessionTimerEnded(TimeSpan? stopWatchTimerDuration)
        {
            UpdateIsSessionTimerActive(false);
            int durationSeconds = (int)stopWatchTimerDuration.Value.TotalSeconds;
            string durationHHMM = _utilityService.ConvertDurationSecondsToHHMMStringWithSpace(durationSeconds);
            SetDurationHHMM(durationHHMM);
            SetDurationSeconds(durationSeconds);
            SetCodingSessionEndTimeAndDate(DateTime.UtcNow);
            UpdateGoalCompletionStatus();
        }

        /// <summary>
        /// Updates the session with study notes and project, this is only called from the SessionNotesForm which is the end point when a coding session is being saved. 
        /// 
        public async Task<bool> NEWUpdateCodingSessionStudyNotesAndSaveCodingSession(string studyProject, string studyNotes)
        {
            SetStudyProject(studyProject);
            SetStudyNotes(studyNotes);
            CheckAllRequiredCurrentCodingSessionDetailNotNull();

            //Dates are stored as local time in CodingSession as these are the values the user will see.
            CodingSessionEntity currentCodingSessionEntity = ConvertCodingSessionToCodingSessionEntity();
            ConvertCodingSessionDatesToUTC(currentCodingSessionEntity);

            bool sessionAddedToDb = await _codingSessionRepository.AddCodingSessionEntityAsync(currentCodingSessionEntity);

            return sessionAddedToDb;
        }

        public void ResetCurrentCodingSession()
        {
            CodingSession blankCodingSession = CreateCurrentCodingSession();
            _currentCodingSession = blankCodingSession;

            _appLogger.LogCodingSession(blankCodingSession);


        }

        public bool IsCurrentCodingSessionNull()
        {
            return _currentCodingSession == null;
        }


        public void RestartCodingSession()
        {
            ResetCurrentCodingSession();

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

        public bool ReturnIsSessionTimerActive()
        {
            return IsSessionTimerActive;
        }

        public bool ReturnIsCodingSessionActive()
        {
            return IsCodingSessionActive;
        }

        public void UpdateGoalCompletionStatus()
        {
            try
            {
                _appLogger.Debug($"Starting UpdateGoalCompletionStatus method");

                _currentCodingSession.GoalReached = false;
                _appLogger.Debug($"Default GoalReached set to false");

                _appLogger.Debug($"Session values - GoalSet: {(_currentCodingSession.GoalSet.HasValue ? _currentCodingSession.GoalSet.ToString() : "null")}, GoalSeconds: {(_currentCodingSession.GoalSeconds.HasValue ? _currentCodingSession.GoalSeconds.ToString() : "null")}, DurationSeconds: {_currentCodingSession.DurationSeconds}");

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

        #region Property Setters

        public void SetCurrentUserIdPlaceholder(int userId)
        {
            _currentUserIdPlaceholder = userId;
        }

        public void SetStudyProject(string studyProject)
        {
            _currentCodingSession.StudyProject = studyProject;
        }

        public void SetStudyNotes(string studyNotes)
        {
            _currentCodingSession.StudyNotes = studyNotes;
        }

        public void SetCodingSessionStartTimeAndDate(DateTime startTime)
        {
            _currentCodingSession.StartTimeLocal = startTime;
            _currentCodingSession.StartDateLocal = DateOnly.FromDateTime(startTime);
        }

        public void SetCodingSessionEndTimeAndDate(DateTime endTime)
        {
            _currentCodingSession.EndTimeLocal = endTime;
            _currentCodingSession.EndDateLocal = DateOnly.FromDateTime(endTime);
        }

        public void SetDurationSeconds(int durationSeconds)
        {
            _currentCodingSession.DurationSeconds = durationSeconds;
        }

        public void SetDurationHHMM(string durationHHMM)
        {
            _currentCodingSession.DurationHHMM = durationHHMM;
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

        public void SetCurrentSessionGoalSeconds(int? goalSeconds)
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
            _appLogger.Debug($"CurrentCodingSession GoalMins set to {_currentCodingSession.GoalSeconds}, default for not set.");
        }

        public void SetSessionStarRating(int rating)
        {
            if (rating < 1 || rating > 5) 
            {
                throw new InvalidOperationException($"Star rating must be between 1-5.");
            }

            _currentCodingSession.SessionStarRating = rating;
        }

        public void UpdateSessionTimerActiveBooleansToFalse()
        {
            UpdateISCodingSessionActive(false);
            UpdateIsSessionTimerActive(false); ;
        }

        #endregion

        #region Property Getters

        public int ReturnCurrentUserIdPlaceholder()
        {
            return _currentUserIdPlaceholder;
        }

        public DateTime? ReturnCurrentSessionStartTime()
        {
            if (_currentCodingSession.StartTimeLocal != null)
            {
                return _currentCodingSession.StartTimeLocal;
            }
            throw new InvalidOperationException($"Tried to return _currentSession.StartTimeLocal before it is initialized, this should never occur.");
        }

        public int? ReturnGoalSeconds()
        {
            return _currentCodingSession.GoalSeconds;
        }

        public bool? ReturnCurrentSessionGoalReached()
        {
            return _currentCodingSession.GoalReached;
        }

        #endregion

        #region Validation and Conversion

        public void CheckAllRequiredCurrentCodingSessionDetailNotNull()
        {
            var nullProperties = typeof(CodingSession)
                .GetProperties()
                .Where(p => p.Name != "SessionId")
                .Where(p =>
                {
                    var value = p.GetValue(_currentCodingSession);
                    return value == null ||
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

        // Ensures all session date and time values are explicitly converted to UTC for PostgreSQL compatibility.
        public void ConvertCodingSessionDatesToUTC(CodingSessionEntity codingSession)
        {
            codingSession.StartDateUTC = DateOnly.FromDateTime(DateTime.SpecifyKind(codingSession.StartDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc));
            codingSession.StartTimeUTC = codingSession.StartTimeUTC.ToUniversalTime();
            codingSession.EndDateUTC = DateOnly.FromDateTime(DateTime.SpecifyKind(codingSession.EndDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc));
            codingSession.EndTimeUTC = codingSession.EndTimeUTC.ToUniversalTime();
        }

        public void ConvertCodingSessionListDatesToLocal(List<CodingSessionEntity> codingSessions)
        {
            if (!codingSessions.Any())
            {
                _appLogger.Info($"CodingSession list empty for {nameof(ConvertCodingSessionListDatesToLocal)}");
                return;
            }
            foreach (var codingSession in codingSessions)
            {
                codingSession.StartDateUTC = DateOnly.FromDateTime(DateTime.SpecifyKind(codingSession.StartDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToLocalTime());
                codingSession.StartTimeUTC = codingSession.StartTimeUTC.ToLocalTime();
                codingSession.EndDateUTC = DateOnly.FromDateTime(DateTime.SpecifyKind(codingSession.EndDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToLocalTime());
                codingSession.EndTimeUTC = codingSession.EndTimeUTC.ToLocalTime();
            }
        }

        public CodingSessionEntity ConvertCodingSessionToCodingSessionEntity()
        {
            return new CodingSessionEntity
            {
                UserId = _currentCodingSession.UserId,
                StartDateUTC = _currentCodingSession.StartDateLocal ?? throw new ArgumentNullException($"StartDateLocal cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                StartTimeUTC = _currentCodingSession.StartTimeLocal ?? throw new ArgumentNullException($"StartTimeLocal cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                EndDateUTC = _currentCodingSession.EndDateLocal ?? throw new ArgumentNullException($"EndDateLocal cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                EndTimeUTC = _currentCodingSession.EndTimeLocal ?? throw new ArgumentNullException($"EndTimeLocal cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                DurationSeconds = _currentCodingSession.DurationSeconds ?? throw new ArgumentNullException($"DurationSeconds cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                DurationHHMM = _currentCodingSession.DurationHHMM ?? throw new ArgumentNullException($"DurationHHMM cannot be null when creating CdoingSesisonEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                GoalSeconds = _currentCodingSession.GoalSeconds ?? throw new ArgumentNullException($"GoalSeconds cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                GoalReached = _currentCodingSession.GoalReached ?? throw new ArgumentNullException($"GoalReached cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                StudyProject = _currentCodingSession.StudyProject ?? throw new ArgumentNullException($"StudyProject cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                StudyNotes = _currentCodingSession.StudyNotes ?? throw new ArgumentNullException($"StudyNotes cannot be null when creating CodingSessionEntity for {nameof(ConvertCodingSessionToCodingSessionEntity)}"),
                SessionStarRating = _currentCodingSession.SessionStarRating ?? throw new ArgumentNullException($"SessionStarRating cannot be null when creating SessionStarRating for {nameof(ConvertCodingSessionToCodingSessionEntity)}")


            };
        }

        #endregion
    }
}