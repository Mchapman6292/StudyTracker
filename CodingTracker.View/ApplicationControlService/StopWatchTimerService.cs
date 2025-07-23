using System.Diagnostics;

namespace CodingTracker.View.ApplicationControlService
{
    public interface IStopWatchTimerService
    {
        Stopwatch ReturnStopWatch();
        TimeSpan ReturnElapsedTimeSpan();
        double ReturnElapsedSeconds();
        double ReturnElapsedMilliseconds();
        void StopTimer();
        void StartTimer();
        void RestartTimer();
        void StartRestartTimer();


        void ResetRestartTimer();
        void StopRestartTimer();

        TimeSpan GetRestartElapsedTimeCappedAtOneSecond();
        double GetRestartStopwatchSeconds();
    }

    /// <summary>
    /// This is needed so other classes can stop the timer for the active coding session.
    /// This can happen if a coding session is active and the user goes to exit etc.
    public class StopWatchTimerService : IStopWatchTimerService
    {
        private Stopwatch sessionTimer { get; } = new Stopwatch();

        private Stopwatch restartStopwatch { get; set; } = new Stopwatch();

        private TimeSpan maxRestartStopwatchDuration { get; } = TimeSpan.FromSeconds(1); 


        public Stopwatch ReturnStopWatch()
        {
            return sessionTimer;
        }

        public TimeSpan ReturnElapsedTimeSpan()
        {
            return sessionTimer.Elapsed;
        }

        public double ReturnElapsedSeconds()
        {
            return sessionTimer.Elapsed.TotalSeconds;
        }

        public double ReturnElapsedMilliseconds()
        {
            return sessionTimer.Elapsed.TotalMilliseconds;
        }

        public void StartTimer()
        {
            sessionTimer.Start();
        }

        public void StopTimer()
        {
            sessionTimer.Stop();
        }

        public void RestartTimer()
        {
            sessionTimer.Restart();
        }






        public void ResetRestartTimer()
        {
            restartStopwatch.Restart();

        }

        public void StopRestartTimer()
        {
            restartStopwatch.Stop();
        }

        public void StartRestartTimer()
        {
            if(restartStopwatch == null)
            {
                restartStopwatch = new Stopwatch();
            }

            restartStopwatch.Start();
        }

        public TimeSpan GetRestartElapsedTimeCappedAtOneSecond()
        {
            TimeSpan elapsed = TimeSpan.FromMicroseconds(restartStopwatch.Elapsed.TotalMilliseconds);

            if (elapsed > maxRestartStopwatchDuration)
            {
                return maxRestartStopwatchDuration;
            }

            return elapsed; 
        }


        public double GetRestartStopwatchSeconds()
        {
            double elpased = restartStopwatch.Elapsed.TotalSeconds;

            if (elpased > maxRestartStopwatchDuration.Seconds)
            {
                return maxRestartStopwatchDuration.Seconds;
            }
            return elpased;
        }
 



    }
}



 /*         public void TESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                SKRect leftShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);
                SKRect rightShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);

                if (column.IsRestarting)
                {
                    double restartElpasedSeconds = _stopWatchTimerService.GetRestartStopwatchSeconds();

                    float restartProgress = Math.Min(1f, (float)restartElpasedSeconds / 0.5f);
                    float easedProgress = _animationCalculator.CalculateEasingValue(restartProgress);
                    column.YTranslation = column.YLocationAtRestart * (1f - easedProgress);

                    if (restartProgress >= 1f)
                    {
                        column.IsRestarting = false;
                        column.YTranslation = 0;
                        column.CurrentValue = 0;
                        column.TargetSegmentValue = 1;
                        column.PassedFirstTransition = false;

                        if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                        {
                            column.IsColumnActive = true;
                            column.IsNumberBlurringActive = true;
                        }
                    }

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    continue;
                }

                int liveTargetValue = _animationCalculator.CalculateTargetValue(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);

                if (liveTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = liveTargetValue;
                    column.PassedFirstTransition = true;
                    WORKINGSetFocusedSegmentByValue(column, liveTargetValue);
                }
                else
                {
                    WORKINGSetFocusedSegmentByValue(column, liveTargetValue);
                }

                column.IsAnimating = WORKINGShouldColumnAnimate(elapsed, column);

                if (column.IsAnimating)
                {
                    if (column.IsColumnActive == false)
                    {
                        column.IsColumnActive = true;
                        _columnStateManager.UpdatedNumberBlurringStartAnimationActive(column, true);
                        _appLogger.Debug($"IsCOlumnACtive changed from false to: {column.IsColumnActive} at {FormatElapsedTimeSpan(elapsed)}");
                    }

                    if (column.IsNumberBlurringActive && elapsed >= column.AnimationInterval)
                    {
                        _columnStateManager.UpdatedNumberBlurringStartAnimationActive(column, false);
                    }

                    isCircleStatic = false;

                    float animationProgress = _animationCalculator.CalculateAnimationProgress(elapsed);
                    float normalizedProgress = _animationCalculator.CalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float animatingYTranslation = _animationCalculator.CalculateYTranslation(column, elapsed, column.BaseAnimationProgress);

                    column.YTranslation = animatingYTranslation;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                }
                else
                {
                    column.YTranslation = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
                    isCircleStatic = true;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                }
            }
        }
 */