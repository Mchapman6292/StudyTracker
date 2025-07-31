using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System.Runtime.CompilerServices;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);
        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        float TESTCalculateCircleAnimationProgress(AnimatedTimerColumn column);
        float CalculateCircleAnimationProgress(AnimatedTimerColumn column);
        void UpdateBaseAnimationProgress(AnimatedTimerColumn column, float baseAnimationProgress);
        void UpdateColumnScrollProgress(AnimatedTimerColumn column, float scolumnScrollProgress);
        void UpdateColumnIsRestarting(AnimatedTimerColumn column, bool isRestarting);
        void SetColumnStateAndStartRestartTimerForRestartBeginning(List<AnimatedTimerColumn> columns);
        void UpdateColumnsWhenRestartComplete(List<AnimatedTimerColumn> columns);

        void UpdateIsRestartingForAllColumns(List<AnimatedTimerColumn> columns, bool isRestarting);
        void UpdateHasRestartedForAllColumns(List<AnimatedTimerColumn> columns, bool isRestarting);
        bool CheckAllColumnsFinishedRestart(List<AnimatedTimerColumn> columns);
        bool CheckAllColumnsOutOfRestartState(List<AnimatedTimerColumn> columns);
        void UpdateStateAndTimerWhenRestartComplete();
        void UpdateRestartAnimationProgress(AnimatedTimerColumn column, float restartAnimationProgress);
        bool ReturnAreColumnsInRestartState();
        void UpdateIsColumnActive(AnimatedTimerColumn column, bool isColumnActive);
        bool ShouldColumnNumbersBlur(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateIsNumberBlurringActive(AnimatedTimerColumn column, bool numberBlurringActive);
        void UpdateTargetSegmentValue(AnimatedTimerColumn column, int targetSegmentValue);
        void UpdateYLocationAtRestart(AnimatedTimerColumn column, float yLocationAtRestart);
        void UpdateTargetDigit(AnimatedTimerColumn column, int newTargetDigit, TimeSpan? elapsed, [CallerMemberName] string callerMethod = "");
        void UpdateColumnActiveDigit(AnimatedTimerColumn column, int newActiveDigit, [CallerMemberName] string callerMethod = "");
        void SetFocusedSegmentByColumnTargetValue(AnimatedTimerColumn column, int newValue);
        void UpdatePassedFirstAnimationTick(AnimatedTimerColumn column, bool passedFirstAnimationTick);
        void UpdatePassedFirstTransition(AnimatedTimerColumn column, bool passedFirstTransition);
    }

    public class AnimatedColumnStateManager : IAnimatedColumnStateManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedLogHelper _animatedLogHelper;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IAnimationCalculator _animationCalculator;

        public bool IsTimerRestarting { get; set; }


        public AnimatedColumnStateManager(IApplicationLogger appLogger, IAnimatedLogHelper aniamtedLogerHelper, IStopWatchTimerService stopWatchTimerService, IAnimationCalculator animationCalculator)
        {
            _appLogger = appLogger;
            _animatedLogHelper = aniamtedLogerHelper;
            _stopWatchTimerService = stopWatchTimerService;
            _animationCalculator = animationCalculator;
        }




    



        public int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            int totalSeconds = (int)elapsed.TotalSeconds;
            int minutes = totalSeconds / 60;
            int hours = totalSeconds / 3600;

            int segmentCount = column.TotalSegmentCount;

            switch (column.ColumnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return totalSeconds % segmentCount;
                case ColumnUnitType.SecondsLeadingDigit:
                    var result = (totalSeconds / 10) % segmentCount;
                    return result;
                case ColumnUnitType.MinutesSingleDigits:
                    return minutes % 10;
                case ColumnUnitType.MinutesLeadingDigits:
                    return (minutes / 10) % segmentCount;
                case ColumnUnitType.HoursSinglesDigits:
                    return hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return (hours / 10) % segmentCount;
                default:
                    return 0;
            }
        }






   
        

        public float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed)

        {
            return Single.Lerp(AnimatedColumnSettings.MaxRadius, AnimatedColumnSettings.MinRadius, column.CircleAnimationProgress);
        }



        public float TESTCalculateCircleAnimationProgress(AnimatedTimerColumn column)
        {
            return column.BaseAnimationProgress / AnimatedColumnSettings.CircleAnimationDurationRatio;
        }


        public float CalculateCircleAnimationProgress(AnimatedTimerColumn column)
        {
            float circleAnimationRatio = AnimatedColumnSettings.CircleAnimationDurationRatio;
            float result;

            if (column.BaseAnimationProgress > circleAnimationRatio)
            {
                result = 1f;
            }
            else
            {
                result = column.BaseAnimationProgress / circleAnimationRatio;
            }
            return Math.Min(1.0f, result);
        }




        public void UpdateBaseAnimationProgress(AnimatedTimerColumn column, float baseAnimationProgress)
        {
            column.BaseAnimationProgress = baseAnimationProgress;
        }


        public void UpdateColumnScrollProgress(AnimatedTimerColumn column, float scolumnScrollProgress)
        {
            column.ColumnScrollProgress = scolumnScrollProgress;
        }









        public void UpdateColumnIsRestarting(AnimatedTimerColumn column, bool isRestarting)
        {
            column.IsRestarting = isRestarting;
        }

        public void UpdateColumnIsStandardAnimationOccurring(AnimatedTimerColumn column, bool isStandardAnimationOccurring)
        {
            column.IsStandardAnimationOccurring = isStandardAnimationOccurring;
        }




        public void SetColumnStateAndStartRestartTimerForRestartBeginning(List<AnimatedTimerColumn> columns)
        {
            UpdateAreColumnsInRestartState(true);
            _stopWatchTimerService.ResetRestartTimer();

            TimeSpan elapsedSessionTimer = _stopWatchTimerService.ReturnElapsedTimeSpan();

            string formatedTimeSpan = _animatedLogHelper.FormatElapsedTimeSpan(elapsedSessionTimer);
          

            foreach (var column in columns)
            {

                UpdateColumnIsRestarting(column, true);
                UpdateYLocationAtRestart(column, column.YTranslation);
                UpdateTargetSegmentValue(column, 0);
                UpdateColumnIsStandardAnimationOccurring(column, false);
                UpdateIsColumnActive(column, false);
                UpdatePassedFirstTransition(column, false);
                
            }
        }



        public void UpdateColumnsWhenRestartComplete(List<AnimatedTimerColumn> columns)
        {
            foreach (var column in columns)
            {
                if (column.ColumnType != ColumnUnitType.SecondsSingleDigits)
                {

                    UpdateColumnIsRestarting(column, false);
                    UpdateIsColumnActive(column, false); 
                    UpdatePassedFirstTransition(column, false);
                    UpdateIsNumberBlurringActive(column, true);
                    UpdateColumnIsStandardAnimationOccurring(column, false);

                    if (column.ActiveDigit != 0)
                    {
                        UpdateColumnActiveDigit(column, 0);
                    }

                    if (column.TargetDigit != 1)
                    {
                        UpdateTargetDigit(column, 1, null);
                    }
                }

                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    {
                        UpdateColumnIsRestarting(column, false);
                        UpdateIsColumnActive(column, true);
                        UpdatePassedFirstTransition(column, false);
                        UpdateColumnIsStandardAnimationOccurring(column, true);
                        UpdateIsNumberBlurringActive(column, false);

                        if (column.ActiveDigit != 0)
                        {
                            UpdateColumnActiveDigit(column, 0);
                        }

                        if (column.TargetDigit != 1)
                        {
                            UpdateTargetDigit(column, 1, null);
                        }
                    }
                }
            }
        }


       


        public void UpdateIsRestartingForAllColumns(List<AnimatedTimerColumn> columns ,bool isRestarting)
        {
            foreach (var column in columns)
            {
                column.IsRestarting = isRestarting;
            }
            _appLogger.Debug($"IsRestarting for all columns in {nameof(columns)} updated to {isRestarting}");
        }



        public void UpdateHasRestartedForAllColumns(List<AnimatedTimerColumn> columns, bool isRestarting)
        {
            foreach (var column in columns)
            {
                column.IsRestarting = isRestarting;
            }
            _appLogger.Debug($"IsRestarting for all columns in {nameof(columns)} updated to {isRestarting}");
        }




        public bool CheckAllColumnsFinishedRestart(List<AnimatedTimerColumn> columns)
        {
            // Check if controller restart state method is true.
            if (!IsTimerRestarting)
                return false;

            // Check if all columns have finished restarting
            bool allColumnsFinished = columns.All(c => !c.IsRestarting);

            if (allColumnsFinished)
            {
                _appLogger.Debug("All columns finished restart animation");

                _stopWatchTimerService.RestartSessionTimer();
                _stopWatchTimerService.StopRestartTimer();
                UpdateAreColumnsInRestartState(false);

                return true; // Indicates restart just completed
            }

            return false; // Still restarting
        }


        public bool CheckAllColumnsOutOfRestartState(List<AnimatedTimerColumn> columns)
        {
            bool allColumnsFinished = columns.All(c => !c.IsRestarting);

            if (allColumnsFinished)
            {
                _appLogger.Debug("IsRestarting for each column is false no columns currently in isRestarting");
                return true;
            }
            return false;
        }


        public void UpdateStateAndTimerWhenRestartComplete()
        {
            UpdateAreColumnsInRestartState(false);
            _stopWatchTimerService.RestartSessionTimer();
            _stopWatchTimerService.StopRestartTimer();

            var currentSessionElapsed = _stopWatchTimerService.ReturnElapsedTimeSpan();

            _appLogger.Info($"Restart complete currentsessionElapsed time : {_animatedLogHelper.FormatElapsedTimeSpan(currentSessionElapsed)}.");
        }





        public void UpdateRestartAnimationProgress(AnimatedTimerColumn column, float restartAnimationProgress)
        {
            column.RestartAnimationProgress = restartAnimationProgress;
        }


        public void UpdateAreColumnsInRestartState(bool inRestartState)
        {
            IsTimerRestarting = inRestartState;
        }

        public bool ReturnAreColumnsInRestartState()
        {
            return IsTimerRestarting;
        }


        public void UpdateIsColumnActive(AnimatedTimerColumn column, bool isColumnActive)
        {
            column.IsColumnActive = isColumnActive; 
        }



        public bool ShouldColumnNumbersBlur(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            if(elapsed >= column.AnimationInterval)
            {
                return false;
            }
            return true;
        }

        public void UpdateIsNumberBlurringActive(AnimatedTimerColumn column, bool numberBlurringActive)
        {
            column.IsNumberBlurringActive = numberBlurringActive;
        }

        public void UpdateTargetSegmentValue(AnimatedTimerColumn column, int targetSegmentValue)
        {
            column.TargetDigit = targetSegmentValue;
        }

        public void UpdateYLocationAtRestart(AnimatedTimerColumn column, float yLocationAtRestart)
        {
            column.YLocationAtRestart = yLocationAtRestart;
        }

        public void UpdateTargetDigit(AnimatedTimerColumn column, int newTargetDigit, TimeSpan? elapsed, [CallerMemberName] string callerMethod = "")
        {
            var oldValue = column.TargetDigit;

            if (!elapsed.HasValue)
            {
                elapsed = TimeSpan.FromHours(12);
            }


            column.TargetDigit = newTargetDigit;

            
            _appLogger.Debug($"UpdateTargetDigit called by '{callerMethod}' at {elapsed}: " +
                             $"TargetDigit changed from {oldValue} to {newTargetDigit}.");
            

        }


        public void UpdateColumnActiveDigit(AnimatedTimerColumn column, int newActiveDigit,  [CallerMemberName] string callerMethod = "")
        {
            int oldValue = column.ActiveDigit;
            int newValue = newActiveDigit;

            if (oldValue != newValue)
            {
                column.ActiveDigit = newActiveDigit;
            }

 
        }





        public void SetFocusedSegmentByColumnTargetValue(AnimatedTimerColumn column, int newValue)
        {
            var newFocusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);

            column.FocusedSegment = newFocusedSegment;

            if (newFocusedSegment == null)
            {
                _appLogger.Error($"Unable to locate new Focused segment for column {column.ColumnType} ({newValue})");
            }

            if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                _appLogger.Debug($"Column Focused segment updated to {column.FocusedSegment.Value}");
            }

        }

        public void UpdatePassedFirstAnimationTick(AnimatedTimerColumn column, bool passedFirstAnimationTick)
        {
            column.PassedFirstAnimationTick = passedFirstAnimationTick;
        }

        public void UpdatePassedFirstTransition(AnimatedTimerColumn column, bool passedFirstTransition)
        {
            column.PassedFirstTransition = passedFirstTransition;
        }
    }
}


