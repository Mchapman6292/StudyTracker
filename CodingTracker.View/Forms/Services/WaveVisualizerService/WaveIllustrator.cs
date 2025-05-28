using SkiaSharp;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveIllustrator
    {
        void RenderWaveVisualization(SKCanvas canvas, int width, int height, IWaveAnimator animator);
    }

    public class WaveIllustrator : IWaveIllustrator
    {
        private readonly IWaveAppearanceProvider appearanceProvider;

        public WaveIllustrator(IWaveAppearanceProvider appearanceProvider)
        {
            this.appearanceProvider = appearanceProvider;
        }

        public void RenderWaveVisualization(SKCanvas canvas, int width, int height, IWaveAnimator animator)
        {
            ClearCanvas(canvas);
            DrawAllBars(canvas, width, height, animator);
        }

        private void ClearCanvas(SKCanvas canvas)
        {
            canvas.Clear(appearanceProvider.BackgroundColor);
        }

        private void DrawAllBars(SKCanvas canvas, int width, int height, IWaveAnimator animator)
        {
            float barWidth = CalculateBarWidth(width, animator.BarCount);
            float centerY = CalculateCenterPosition(height);

            for (int i = 0; i < animator.BarCount; i++)
            {
                DrawSingleBar(canvas, i, barWidth, centerY, height, animator);
            }
        }

        private void DrawSingleBar(SKCanvas canvas, int barIndex, float barWidth, float centerY, int maxHeight, IWaveAnimator animator)
        {
            float normalizedHeight = animator.GetBarHeight(barIndex);
            float pixelHeight = ConvertToPixelHeight(normalizedHeight, maxHeight);

            float x = CalculateBarXPosition(barIndex, barWidth);
            float y = CalculateBarYPosition(centerY, pixelHeight);

            SKColor barColor = GetBarColor(barIndex, animator.BarCount, animator.CurrentIntensity);

            DrawBarShape(canvas, x, y, barWidth, pixelHeight, barColor);
            DrawBarGlow(canvas, x, y, barWidth, pixelHeight, barColor, animator.CurrentIntensity);
        }

        private void DrawBarShape(SKCanvas canvas, float x, float y, float width, float height, SKColor color)
        {
            using (var paint = new SKPaint
            {
                Color = color,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            })
            {
                float adjustedWidth = CalculateAdjustedBarWidth(width);
                canvas.DrawRect(x, y, adjustedWidth, height, paint);
            }
        }

        private void DrawBarGlow(SKCanvas canvas, float x, float y, float width, float height, SKColor baseColor, ActivityIntensity intensity)
        {
            GlowSettings glowSettings = appearanceProvider.GetGlowSettings(intensity);

            if (ShouldDrawGlow(glowSettings))
            {
                SKColor glowColor = CreateGlowColor(baseColor, glowSettings);

                using (var glowPaint = new SKPaint
                {
                    Color = glowColor,
                    IsAntialias = true,
                    MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, glowSettings.BlurRadius)
                })
                {
                    float adjustedWidth = CalculateAdjustedBarWidth(width);
                    float glowX = CalculateGlowXPosition(x);
                    float glowY = CalculateGlowYPosition(y);
                    float glowWidth = CalculateGlowWidth(adjustedWidth);
                    float glowHeight = CalculateGlowHeight(height);

                    canvas.DrawRect(glowX, glowY, glowWidth, glowHeight, glowPaint);
                }
            }
        }

        private float CalculateBarWidth(int totalWidth, int barCount)
        {
            return totalWidth / (float)barCount;
        }

        private float CalculateCenterPosition(float height)
        {
            return height / 2.0f;
        }

        private float ConvertToPixelHeight(float normalizedHeight, int maxHeight)
        {
            return Math.Abs(normalizedHeight) * maxHeight * 0.4f;
        }

        private float CalculateBarXPosition(int barIndex, float barWidth)
        {
            return barIndex * barWidth;
        }

        private float CalculateBarYPosition(float centerY, float pixelHeight)
        {
            return centerY - (pixelHeight / 2.0f);
        }

        private float CalculateAdjustedBarWidth(float width)
        {
            return width * 0.8f;
        }

        private bool ShouldDrawGlow(GlowSettings glowSettings)
        {
            return glowSettings.BlurRadius > 0;
        }

        private SKColor CreateGlowColor(SKColor baseColor, GlowSettings glowSettings)
        {
            return new SKColor(baseColor.Red, baseColor.Green, baseColor.Blue, (byte)glowSettings.MaxAlpha);
        }

        private float CalculateGlowXPosition(float x)
        {
            return x - 2;
        }

        private float CalculateGlowYPosition(float y)
        {
            return y - 2;
        }

        private float CalculateGlowWidth(float adjustedWidth)
        {
            return adjustedWidth + 4;
        }

        private float CalculateGlowHeight(float height)
        {
            return height + 4;
        }

        private SKColor GetBarColor(int barIndex, int totalBars, ActivityIntensity intensity)
        {
            float position = CalculateColorPosition(barIndex, totalBars);
            float intensityBlend = appearanceProvider.GetIntensityBlend(intensity);

            SKColor lowColor = appearanceProvider.GetColorAtPosition(appearanceProvider.GetLowActivityColors(), position);
            SKColor highColor = appearanceProvider.GetColorAtPosition(appearanceProvider.GetHighActivityColors(), position);

            return appearanceProvider.BlendColors(lowColor, highColor, intensityBlend);
        }

        private float CalculateColorPosition(int barIndex, int totalBars)
        {
            return (float)barIndex / totalBars;
        }
    }
}