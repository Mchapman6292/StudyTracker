using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawAnimatedTimerColumn(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, int width, int height);
        void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y);
        void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas);
    }

    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {

        public void DrawAnimatedTimerColumn(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, int width, int height)
        {
            var rect = SKRect.Create(timerColumn.TimerLocation, new SkiaSharp.SKSize(width, height));
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Black);

            using (var paint = new SKPaint())
            {
                paint.Color = SKColors.White; 
                timerCanvas.DrawRect(rect, paint);
            }

            
        }


        public void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y)
        {
            using (var paint = new SKPaint())
            using (var font = new SKFont())
            {
                paint.Color = timerSegment.SegmentColor;
                paint.IsAntialias = true;

                font.Size = timerSegment.TextSize;

                // Calculate center positions
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.SegmentNumber.ToString(), centerX, centerY, font, paint);
            }
        }


        public void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas)
        {
            for (int currentSegmentIndex = 0; currentSegmentIndex < timerColumn.SegmentCount; currentSegmentIndex++)
            {
                AnimatedTimerSegment targetSegment = timerColumn.timerSegments[currentSegmentIndex];

                float segmentY = timerColumn.TimerLocation.Y + (currentSegmentIndex * TimerMeasurements.SegmentHeight);
                DrawNumber(canvas, targetSegment, timerColumn.TimerLocation.X, segmentY);
            }
        }



        public void DrawAnimatedTimerSegment(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, int segmentIndex)
        {

        }
    }
}
