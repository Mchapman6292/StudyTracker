using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Runtime.CompilerServices;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);



        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue);
        void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet);

        void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating);
 



        AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit);
        void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment);



        float TESTCalculateCircleAnimationProgress(AnimatedTimerColumn column);

        //
        float CalculateCircleAnimationProgress(AnimatedTimerColumn column);
        void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress);

        //




        void UpdateBaseAnimationProgress(AnimatedTimerColumn column, float animationProgress);

        void UpdateColumnScrollProgress(AnimatedTimerColumn column, float normalizedProgress);

        int WORKINGCalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType);
        float WORKINGCalculateEasingValue(AnimatedTimerColumn column, TimerAnimationType animationType);

        int GetSkControlStartingY(Form targetForm);

        void UpdateIsRestarting(AnimatedTimerColumn column, bool isRestarting);
        void SetColumnStateAndStartRestartTimerForRestartBeginning(List<AnimatedTimerColumn> columns);
        void UpdateColumnsWhenRestartComplete(List<AnimatedTimerColumn> columns);
        void UpdateIsRestartingForAllColumns(List<AnimatedTimerColumn> columns, bool isRestarting);
        bool CheckAllColumnsFinishedRestart(List<AnimatedTimerColumn> columns);
        void UpdateRestartAnimationProgress(AnimatedTimerColumn column, float restartAnimationProgress);
        void UpdateStateAndTimerWhenRestartComplete();
        void UpdateIsColumnActive(AnimatedTimerColumn column, bool isColumnActive);
        bool CheckAllColumnsOutOfRestartState(List<AnimatedTimerColumn> columns);
        bool ReturnAreColumnsInRestartState();
        bool ShouldColumnNumbersBlur(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateIsNumberBlurringActive(AnimatedTimerColumn column, bool numberBlurringActive);
        void UpdateTargetSegmentValue(AnimatedTimerColumn column, int targetSegmentValue);
        void UpdateYLocationAtRestart(AnimatedTimerColumn column, float yLocationAtRestart);

        void UpdateHasRestartedForAllColumns(List<AnimatedTimerColumn> columns, bool isRestarting);

        void TESTUpdateColumnStateWhenRestartComplete(List<AnimatedTimerColumn> columns);

        void NEWTESTUpdateColumnStateWhenRestartComplete(List<AnimatedTimerColumn> columns);
        void UpdateTargetDigit(AnimatedTimerColumn column, int newTargetValue, TimeSpan? elapsed, [CallerMemberName] string callerMethod = "");
        void UpdateColumnActiveDigit(AnimatedTimerColumn column, int newCurrentSegmentValue, TimeSpan? elapsed, [CallerMemberName] string callerMethod = "");
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





  
        public void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue)
        {
            column.ActiveDigit = previousValue;
        }


        public void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet)
        {
            column.YTranslation = scrollOffSet;
        }







        public void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating)
        {
            column.IsStandardAnimationOccurring = isAnimating;
 
        }

        public AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit)
        {
            return column.TimerSegments.FirstOrDefault(x => x.Value == timeDigit);

          
        }


        public void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment)
        {
            column.FocusedSegment = segment;
    
        }


        public void UpdateColumnLocation(AnimatedTimerColumn column, SKPoint newLocation)
        {
            column.Location = newLocation;
        }






 


        public void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress)
        {
            column.CircleAnimationProgress = circleAnimationProgress;
        }



















        #region WorkingMethods

        public int WORKINGCalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType)
        {
            int totalSeconds = (int)elapsed.TotalSeconds;
            int minutes = totalSeconds / 60;
            int hours = totalSeconds / 3600;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return totalSeconds % 10;
                case ColumnUnitType.SecondsLeadingDigit:
                    return (totalSeconds / 10) % 6;
                case ColumnUnitType.MinutesSingleDigits:
                    return minutes % 10;
                case ColumnUnitType.MinutesLeadingDigits:
                    return (minutes / 10) % 6;
                case ColumnUnitType.HoursSinglesDigits:
                    return hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return (hours / 10) % 10;
                default:
                    return 0;
            }
        }



        private bool WORKINGShouldColumnAnimate(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            float animationDurationFloat = AnimatedColumnSettings.ScrollAnimationDurationRatio;

            switch (column.ColumnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    double secondsCycle = elapsed.TotalSeconds % 1.0;
                    return secondsCycle >= (1.0 - animationDurationFloat);
                case ColumnUnitType.SecondsLeadingDigit:
                    double tenSecondsCycle = elapsed.TotalSeconds % 10.0;
                    return tenSecondsCycle >= (10.0 - animationDurationFloat);
                case ColumnUnitType.MinutesSingleDigits:
                    double minutesCycle = elapsed.TotalSeconds % 60.0;
                    return minutesCycle >= (60.0 - animationDurationFloat);
                case ColumnUnitType.MinutesLeadingDigits:
                    double tenMinutesCycle = elapsed.TotalSeconds % 600.0;
                    return tenMinutesCycle >= (600.0 - animationDurationFloat);
                case ColumnUnitType.HoursSinglesDigits:
                    double hoursCycle = elapsed.TotalSeconds % 3600.0;
                    return hoursCycle >= (3600.0 - animationDurationFloat);
                case ColumnUnitType.HoursLeadingDigits:
                    double tenHoursCycle = elapsed.TotalSeconds % 36000.0;
                    return tenHoursCycle >= (36000.0 - animationDurationFloat);
                default:
                    return false;
            }
        }




        


        public void WORKINGSetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue)
        {
            var focusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);
        }



        
        public float WORKINGCalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }



        public void UpdateBaseAnimationProgress(AnimatedTimerColumn column, float baseAnimationProgress)
        {
            column.BaseAnimationProgress = baseAnimationProgress;
        }


        public void UpdateColumnScrollProgress(AnimatedTimerColumn column, float scolumnScrollProgress)
        {
            column.ColumnScrollProgress = scolumnScrollProgress;
        }


   




        public float WORKINGCalculateEasingValue(AnimatedTimerColumn column, TimerAnimationType animationType)
        {
            // Use different progress values to calculate the easing.
            float animationProgress = animationType switch
            {
                TimerAnimationType.CircleAnimation => column.CircleAnimationProgress,
                TimerAnimationType.ColumnScroll => column.ColumnScrollProgress,
                _ => throw new ArgumentException($"Unsupported animation type: {animationType}")
            };

   
            const float midpoint = 0.5f;
            if (animationProgress < midpoint)
            {
                float scaledProgress = animationProgress;
                float easeInValue = 4f * scaledProgress * scaledProgress * scaledProgress;
                return easeInValue;
            }
            else
            {
                float adjustedProgress = -2f * animationProgress + 2f; // Maps 0.5→1 to 1→0
                                                                       // Cubic ease-out: 1 - ((1-t)^3)
                float powerOfThree = MathF.Pow(adjustedProgress, 3f);
                float normalizedAnimationProgress = 1f - (powerOfThree / 2f);
                return normalizedAnimationProgress;
            }
        }


        /*
        public float CalculateEasingValue(AnimatedTimerColumn column, TimerAnimationType animationType)
        {
            const float midpoint = 0.5f;
            float animationProgress = column.BaseAnimationProgress; 

            if(animationProgress < midpoint)
            {
                // If progress less than 0.5 scale it to get a value between 0-1 and then apply cubic easing.
                float scaledAnimationProgress = animationProgress * 2;
                return (scaledAnimationProgress * scaledAnimationProgress * scaledAnimationProgress) / 2;
            }
            else
            {
                // Again we need to transform the animation value between 0.5-0.1 to a value between 0 & 1.
                // Subtract 0.5 then scale it.

                float scaledAnimationProgress = (animationProgress - 0.5f ) * 2;
                
                // If we use the same approach as we did with animation < midpoint then 

            }

        }
        */




        public int GetSkControlStartingY(Form targetForm)
        {
            return targetForm.Height / 2 - 100;
        }


        public void UpdateIsRestarting(AnimatedTimerColumn column, bool isRestarting)
        {
            column.IsRestarting = isRestarting;
        }






        public void SetColumnStateAndStartRestartTimerForRestartBeginning(List<AnimatedTimerColumn> columns)
        {
            UpdateAreColumnsInRestartState(true);
            _stopWatchTimerService.ResetRestartTimer();

            TimeSpan elapsedSessionTimer = _stopWatchTimerService.ReturnElapsedTimeSpan();

            string formatedTimeSpan = _animatedLogHelper.FormatElapsedTimeSpan(elapsedSessionTimer);
          
        

            foreach (var column in columns)
            {

                column.IsRestarting = true;
                UpdateYLocationAtRestart(column, column.YTranslation);
                UpdateTargetSegmentValue(column, 0);
                column.IsStandardAnimationOccurring = false;
                column.IsColumnActive = false;
                column.PassedFirstTransition = false;
                


                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    string message = "SetColumnStateAndStartRestartTimerForRestartBeginning CALL";
                    _appLogger.Debug($"TIMER RESTARTED AT {formatedTimeSpan}----- YLocationAtRestart{column.YLocationAtRestart}, . ");
                    _animatedLogHelper.LogColumn(column, elapsedSessionTimer, ColumnAnimationType.Restart, null, null, message);
                 
                }
            }
        }



        public void UpdateColumnsWhenRestartComplete(List<AnimatedTimerColumn> columns)
        {
            foreach (var column in columns)
            {
                if (column.ColumnType != ColumnUnitType.SecondsSingleDigits)
                {
                    column.IsRestarting = false;
                    column.ActiveDigit = 0;
                    column.TargetDigit = 1;
                    column.PassedFirstTransition = false;
                    column.IsColumnActive = false;
                    column.IsStandardAnimationOccurring = false;
                    column.IsNumberBlurringActive = true;
                }

                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    {
                        column.IsRestarting = false;
                        column.IsColumnActive = true;
                        column.IsStandardAnimationOccurring = true;
                        column.IsNumberBlurringActive = false;


                        if(column.ActiveDigit != 0)
                        {
                            UpdateColumnActiveDigit(column, 0, null);
                        }

                        if(column.TargetDigit != 1)
                        {
                            UpdateTargetDigit(column, 1, null);
                        }
    
                    }
                }
            }
        }


        public void NEWTESTUpdateColumnStateWhenRestartComplete(List<AnimatedTimerColumn> columns)
        {
            foreach (var column in columns)
            {
                if (column.ColumnType != ColumnUnitType.SecondsSingleDigits)
                {
                    column.IsRestarting = false;
                    column.PassedFirstTransition = false;
                    column.IsColumnActive = false;
                    column.IsStandardAnimationOccurring = false;
                    column.IsNumberBlurringActive = true;

                }

                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    {
                        column.IsRestarting = false;
                        column.IsColumnActive = true;
                        column.IsStandardAnimationOccurring = true;
                        column.IsNumberBlurringActive = false;


                        if (column.ActiveDigit != 0)
                        {
                            UpdateColumnActiveDigit(column, 0, null);
                        }

                        if (column.TargetDigit != 1)
                        {
                            UpdateTargetDigit(column, 1, null);
                        }


                    }
                }
            }
        }
        






        public void TESTUpdateColumnStateWhenRestartComplete(List<AnimatedTimerColumn> columns)
        {
            foreach (var column in columns)
            {
                if (column.ColumnType != ColumnUnitType.SecondsSingleDigits)
                {
                    column.IsRestarting = false;
                    column.YTranslation = 0;
                    column.ActiveDigit = 0;
                    column.TargetDigit = 1;
                    column.PassedFirstTransition = false;
                    column.IsColumnActive = false;
                    column.IsStandardAnimationOccurring = false;
                    column.IsNumberBlurringActive = true;

                }

                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    {
                        column.IsRestarting = false;
                        column.IsColumnActive = true;
                        column.IsStandardAnimationOccurring = true;
                        column.IsNumberBlurringActive = false;
                        column.PassedFirstTransition = false;
                        column.ActiveDigit = 0;
                        column.TargetDigit = 1;

                        column.YTranslation = 0;

                        column.Location = column.InitialLocation;

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




        public bool CanStartNewAnimation()
        {
            return !IsTimerRestarting;
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


        public void UpdateAndLogIsColumnActiveTrue(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            if (column.IsColumnActive == false)
            {
                column.IsColumnActive = true;
                _appLogger.Debug($"IsClumnActive changed from false to: {column.IsColumnActive} at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
            }
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


        public void UpdateColumnActiveDigit(AnimatedTimerColumn column, int newActiveDigit, TimeSpan? elapsed, [CallerMemberName] string callerMethod = "")
        {
            int oldValue = column.ActiveDigit;
            int newValue = newActiveDigit;


            if(!elapsed.HasValue)
            {
                elapsed = TimeSpan.FromHours(12);
            }


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



#endregion