using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;


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

        public AnimatedTimerRenderer(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
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

                canvas.DrawText(timerSegment.SegmentNumber.ToString(), centerX, centerY, font, paint);
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


        public void DrawHighlight(SKCanvas canvas, float x, float y, float width, float height)
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                var colors = new SKColor[] {new SKColor(100, 100, 255, 80),  new SKColor(50, 50, 200, 100)};
                paint.Shader = SKShader.CreateLinearGradient(new SKPoint(x, y),new SKPoint(x, y + height),colors,null, SKShaderTileMode.Clamp);

                var rect = new SKRoundRect(new SKRect(x - 5, y - 5, x + width + 5, y + height + 5), 10, 10);
                canvas.DrawRoundRect(rect, paint);
            }
        }
    }
}
