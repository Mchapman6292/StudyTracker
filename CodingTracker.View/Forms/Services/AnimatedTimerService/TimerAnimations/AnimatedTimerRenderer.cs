using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y);
        void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas);
    }

    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IHighlightManager _highlightManager;
        private readonly IAnimationPhaseCalculator _phaseCalculator;

        public AnimatedTimerRenderer(IApplicationLogger appLogger, IHighlightManager highlightManager, IAnimationPhaseCalculator phaseCalculator)
        {
            _appLogger = appLogger;
            _highlightManager = highlightManager;
            _phaseCalculator = phaseCalculator;
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

                // Calculate center positions
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.Value.ToString(), centerX, centerY, font, paint);
            }
        }


        public void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas)
        {
            for (int currentSegmentIndex = 0; currentSegmentIndex < timerColumn.SegmentCount; currentSegmentIndex++)
            {
                AnimatedTimerSegment targetSegment = timerColumn.timerSegments[currentSegmentIndex];

                float segmentY = timerColumn.TimerLocation.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight) - timerColumn.ScrollOffset;

                DrawNumber(canvas, targetSegment, timerColumn.TimerLocation.X, segmentY);
            }
        }

        public void DrawAnimatedTimerSegment(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, int segmentIndex)
        {

        }





















        public void Draw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(SKColors.Black);

            foreach (var column in columns)
            {
                var phase = _phaseCalculator.GetPhaseForColumn(elapsed, column.ColumnType);
                var currentValue = _phaseCalculator.GetCurrentValue(elapsed, column.ColumnType);
                var scrollOffset = CalculateScrollOffset(phase, currentValue, column);

                column.ScrollOffset = scrollOffset;
                column.CurrentValue = currentValue;

                DrawColumn(canvas, column);
            }

            _highlightManager.DrawHighlights(canvas, columns, bounds.Height);
        }



        private void DrawColumn(SKCanvas canvas, AnimatedTimerColumn column)
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.TextSize = AnimatedColumnSettings.TextSize;
                paint.Color = SKColors.White;
                paint.Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold);

                var textBounds = new SKRect();
                paint.MeasureText("0", ref textBounds);

                float digitHeight = AnimatedColumnSettings.SegmentHeight;
                float startY = column.TimerLocation.Y - column.ScrollOffset;

                for (int i = 0; i < column.SegmentCount; i++)
                {
                    var segment = column.timerSegments[i];
                    float y = startY + (i * digitHeight) + textBounds.Height;

                    canvas.DrawText(segment.Value.ToString(), column.TimerLocation.X, y, paint);
                }
            }
        }

        private float CalculateScrollOffset(float phase, int currentValue, AnimatedTimerColumn column)
        {
            float baseOffset = currentValue * AnimatedColumnSettings.SegmentHeight;

            if (phase < 0.25f)
            {
                float animationProgress = phase / 0.25f;
                float easedProgress = EaseInOutCubic(animationProgress);
                return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
            }

            return baseOffset;
        }

        private float EaseInOutCubic(float t)
        {
            return t < 0.5f
                ? 4f * t * t * t
                : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;
        }


    }
}
