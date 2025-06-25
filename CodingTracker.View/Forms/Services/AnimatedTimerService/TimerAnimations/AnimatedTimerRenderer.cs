using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using static Guna.UI2.Material.Animation.AnimationManager;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y);
        void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas);

        void Draw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
    }

    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IAnimationPhaseCalculator _phaseCalculator;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly ICircleHighLight _circleHighlight;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalculator;

        public AnimatedTimerRenderer(IApplicationLogger appLogger, IAnimationPhaseCalculator phaseCalculator, IStopWatchTimerService stopwWatchTimerService, ICircleHighLight circleHighlight, ISegmentOverlayCalculator segmentOverlayCalculator)
        {
            _appLogger = appLogger;
            _phaseCalculator = phaseCalculator;
            _stopWatchTimerService = stopwWatchTimerService;
            _circleHighlight = circleHighlight;
            _segmentOverlayCalculator = segmentOverlayCalculator;
        }

  


        // Old method, remove once working.
   
        public void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas)
        {
            for (int currentSegmentIndex = 0; currentSegmentIndex < timerColumn.SegmentCount; currentSegmentIndex++)
            {
                AnimatedTimerSegment targetSegment = timerColumn.TimerSegments[currentSegmentIndex];

                float segmentY = timerColumn.Location.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight) - timerColumn.ScrollOffset;

                DrawNumber(canvas, targetSegment, timerColumn.Location.X, segmentY);
            }
        }

        
















        public void Draw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            SKColor formBackgroundColor = new SKColor(35, 34, 50);

            canvas.Clear(formBackgroundColor);

            foreach (var column in columns)
            {
               
                int newValue = CalculateColumnValue(elapsed, column.ColumnType);
                column.CurrentValue = newValue;

                // 2. Determine if this column should animate right now.
                column.IsAnimating = ShouldColumnAnimate(elapsed, column);

                // 3. Calculate scroll offset.
                if (column.IsAnimating)
                {
                    float animationProgress = _segmentOverlayCalculator.CalculateAnimationProgress(elapsed);
                    float normalizedProgress = _segmentOverlayCalculator.CalculateNormalizedProgress(column.AnimationProgress);

                    column.UpdateAnimationState(animationProgress, normalizedProgress);

                    column.ScrollOffset = CalculateScrollOffset(column, animationProgress);
                }
                else
                {
           
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                }
                DrawColumn(canvas, column);

                _circleHighlight.Draw(canvas, column);
            }
        }





        private bool ShouldColumnAnimate(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            // Check if we're within 1 second of when this column changes.
            double secondsUntilChange = column.AnimationInterval.TotalSeconds -
                                       (elapsed.TotalSeconds % column.AnimationInterval.TotalSeconds);

            return secondsUntilChange <= 1.0;  // Animate during last second before change.
        }





        private int CalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType)
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




        private void DrawColumn(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float digitHeight = AnimatedColumnSettings.SegmentHeight;
            float startY = column.Location.Y - column.ScrollOffset;


            for (int i = 0; i < column.SegmentCount; i++)
            {


                var segment = column.TimerSegments[i];
                float YNew = startY + (i * digitHeight);

                segment.UpdatePosition(column.Location.X, YNew);

                DrawNumber(canvas, segment, column.Location.X, YNew);
            }
        }

        // Value to determine the progress between one Segment and the next. 
        private float CalculateScrollOffset(AnimatedTimerColumn column,float animationProgress)
        {
            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = CalculateEasingValue(animationProgress);
            return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }

        public void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y)
        {
            using (var paint = new SKPaint())
            using (var font = new SKFont())
            {
                paint.Color = timerSegment.SegmentColor;
                paint.IsAntialias = true;
                paint.TextAlign = SKTextAlign.Center;

                font.Size = timerSegment.TextSize;

                // Calculate center positions.
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.Value.ToString(), centerX, centerY, font, paint);



   

            }
        }


        private float CalculateEasingValue(float t)
        {
            return t < 0.5f
                ? 4f * t * t * t
                : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

            
        }

        private float CaclulatePhaseValue(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }











   

    }
}
