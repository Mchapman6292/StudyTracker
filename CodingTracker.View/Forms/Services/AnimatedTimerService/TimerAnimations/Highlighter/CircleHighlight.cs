using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface ICircleHighLight
    {
        void Draw(SKCanvas canvas, AnimatedTimerColumn column);
    
    }

    public class CircleHighlight : ITimerHighlight, ICircleHighLight
    {
        public bool IsEnabled { get; set; }

        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmwentOverlayCalculator;


        public CircleHighlight(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverlayCalculator)
        {
            _appLogger = appLogger;
            _segmwentOverlayCalculator = segmentOverlayCalculator;
        }




        public void Draw(SKCanvas canvas, AnimatedTimerColumn column)
        {
            int currentValue = column.CurrentValue;
            var animationProgress = column.AnimationProgress;


            SKPaint outerCirclePaint = CreateOuterCirclePaint(column);
            SKPaint innerCirclePaint = CreateInnerCirclePaint(column);

            float normalizedRadius = _segmwentOverlayCalculator.CalculateNormalizedRadius(column);

            _appLogger.Debug($"NormalizedRadius: {normalizedRadius}.");

            // Find the index of the current value in the segments.
            int segmentIndex = column.TimerSegments.FindIndex(s => s.Value == currentValue);
            if (segmentIndex == -1) return;

            // Calculate the screen position where segment is drawn.
            float startY = column.Location.Y - column.ScrollOffset;
            float segmentY = startY + (segmentIndex * AnimatedColumnSettings.SegmentHeight);

            // Calculate center position.
            float centerX = column.Location.X + (AnimatedColumnSettings.SegmentWidth / 2f);
            float centerY = segmentY + (AnimatedColumnSettings.SegmentHeight / 2f);

            using (var outerPaint = CreateOuterCirclePaint(column))
            {
                canvas.DrawCircle(centerX, centerY, normalizedRadius, outerPaint);
            }

            using (var innterPaint = CreateInnerCirclePaint(column))
            {
                canvas.DrawCircle()
            }
        }




        private SKPaint CreateOuterCirclePaint(AnimatedTimerColumn column)
        {

            byte alpha = (byte)(column.NormalizedProgress * 255);

            return new SKPaint()
            {

                Color = SKColors.Blue.WithAlpha(alpha),
                Style = SKPaintStyle.StrokeAndFill,
                StrokeWidth = 4f,
                
     

            };
        }

        private SKPaint CreateInnerCirclePaint(AnimatedTimerColumn column)
        {
            byte alpha = (byte)(column.NormalizedProgress * 255);
            return new SKPaint()
            {
                Color = SKColors.LightBlue.WithAlpha(alpha), // Inside color
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
        }


    }
}
