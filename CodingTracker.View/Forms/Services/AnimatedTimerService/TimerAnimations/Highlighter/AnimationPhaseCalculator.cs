namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IAnimationPhaseCalculator
    {
        /*

        float GetPhaseForColumn(TimeSpan elapsed, ColumnUnitType columnType);
        int GetCurrentValue(TimeSpan elapsed, ColumnUnitType columnType);
        */
    }

    public class AnimationPhaseCalculator : IAnimationPhaseCalculator
    {
        /*
        public float GetPhaseForColumn(TimeSpan elapsed, ColumnUnitType columnType)
        {
            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return (float)(elapsed.TotalSeconds % 1.0);
                case ColumnUnitType.SecondsLeadingDigit:
                    return (float)((elapsed.TotalSeconds % 10.0) / 10.0);
                case ColumnUnitType.MinutesSingleDigits:
                    return (float)((elapsed.TotalSeconds % 60.0) / 60.0);
                case ColumnUnitType.MinutesLeadingDigits:
                    return (float)((elapsed.TotalSeconds % 600.0) / 600.0);
                case ColumnUnitType.HoursSinglesDigits:
                    return (float)((elapsed.TotalSeconds % 3600.0) / 3600.0);
                case ColumnUnitType.HoursLeadingDigits:
                    return (float)((elapsed.TotalSeconds % 36000.0) / 36000.0);
                default:
                    return 0f;
            }
        }

        public int GetCurrentValue(TimeSpan elapsed, ColumnUnitType columnType)
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


        


        public void OldDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(SKColors.Black);

            foreach (var column in columns)
            {
                var phase = _phaseCalculator.GetPhaseForColumn(elapsed, column.ColumnType);
                var currentValue = _phaseCalculator.GetCurrentValue(elapsed, column.ColumnType);
                var scrollOffset = CalculateVerticalOffset(phase, currentValue, column);

                column.YTranslation = scrollOffset;
                column.TargetDigit = currentValue;

                DrawSegments(canvas, column);
            }
        }



        */
    }
}
