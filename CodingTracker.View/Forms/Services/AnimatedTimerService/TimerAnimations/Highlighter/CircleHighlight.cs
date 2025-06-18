using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public class CircleHighlight : ITimerHighlight
    {
        public bool IsEnabled { get; set; }


        public void Draw(SKCanvas canvas, HighlightContext highlightContext)
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                var colors = new SKColor[] { new SKColor(100, 100, 255, 80), new SKColor(50, 50, 200, 100) };
                paint.Shader = SKShader.CreateLinearGradient(new SKPoint(highlightContext.X, highlightContext.Y), new SKPoint(highlightContext.X, highlightContext.Y + highlightContext.Height), colors, null, SKShaderTileMode.Clamp);

                var rect = new SKRoundRect(new SKRect(highlightContext.X - 5, highlightContext.Y - 5, highlightContext.X + highlightContext.Width + 5, highlightContext.Y + highlightContext.Height + 5), 10, 10);
                canvas.DrawRoundRect(rect, paint);
            }
        }



    }
}
