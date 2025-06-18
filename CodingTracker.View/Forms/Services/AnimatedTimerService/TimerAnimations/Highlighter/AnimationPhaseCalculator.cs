namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IAnimationPhaseCalculator
    {
        float GetPhaseForColumn(TimeSpan elapsed, TimeUnit columnType);
        int GetCurrentValue(TimeSpan elapsed, TimeUnit columnType);
    }

    public class AnimationPhaseCalculator : IAnimationPhaseCalculator
    {
        public float GetPhaseForColumn(TimeSpan elapsed, TimeUnit columnType)
        {
            switch (columnType)
            {
                case TimeUnit.SecondsOnes:
                    return (float)(elapsed.TotalSeconds % 1.0);
                case TimeUnit.SecondsTens:
                    return (float)((elapsed.TotalSeconds % 10.0) / 10.0);
                case TimeUnit.MinutesOnes:
                    return (float)((elapsed.TotalSeconds % 60.0) / 60.0);
                case TimeUnit.MinutesTens:
                    return (float)((elapsed.TotalSeconds % 600.0) / 600.0);
                case TimeUnit.HoursOnes:
                    return (float)((elapsed.TotalSeconds % 3600.0) / 3600.0);
                case TimeUnit.HoursTens:
                    return (float)((elapsed.TotalSeconds % 36000.0) / 36000.0);
                default:
                    return 0f;
            }
        }

        public int GetCurrentValue(TimeSpan elapsed, TimeUnit columnType)
        {
            int totalSeconds = (int)elapsed.TotalSeconds;
            int minutes = totalSeconds / 60;
            int hours = totalSeconds / 3600;

            switch (columnType)
            {
                case TimeUnit.SecondsOnes:
                    return totalSeconds % 10;
                case TimeUnit.SecondsTens:
                    return (totalSeconds / 10) % 6;
                case TimeUnit.MinutesOnes:
                    return minutes % 10;
                case TimeUnit.MinutesTens:
                    return (minutes / 10) % 6;
                case TimeUnit.HoursOnes:
                    return hours % 10;
                case TimeUnit.HoursTens:
                    return (hours / 10) % 10;
                default:
                    return 0;
            }
        }
    }
}
