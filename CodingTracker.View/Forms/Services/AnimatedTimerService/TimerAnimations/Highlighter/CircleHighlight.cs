using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
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


        public CircleHighlight(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }




        public void Draw(SKCanvas canvas, AnimatedTimerColumn column)
        {
            int currentValue = column.CurrentValue;
            SKPaint circlePaint = DefineCirclePaint();

            // Find the index of the current value in the segments.
            int segmentIndex = column.TimerSegments.FindIndex(s => s.Value == currentValue);
            if (segmentIndex == -1) return;

            // Calculate the screen position where segment is drawn.
            float startY = column.Location.Y - column.ScrollOffset;
            float segmentY = startY + (segmentIndex * AnimatedColumnSettings.SegmentHeight);

            // Calculate center position.
            float centerX = column.Location.X + (AnimatedColumnSettings.SegmentWidth / 2f);
            float centerY = segmentY + (AnimatedColumnSettings.SegmentHeight / 2f);

            canvas.DrawCircle(centerX, centerY, 20, circlePaint);

        }


        public SKPaint DefineCirclePaint()
        {
            return new SKPaint()
            {
                Color = SKColors.Blue,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 3f
            };
        }

    }
}
