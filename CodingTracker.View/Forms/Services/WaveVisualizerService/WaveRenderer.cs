using SkiaSharp;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveRenderer
    {
        void Render(SKCanvas canvas, int width, int height, float[] barHeights, float[] noiseValues, float intensity);
    }

    public class WaveRenderer : IWaveRenderer
    {
        private readonly IWaveColorManager _colorManager;

        public WaveRenderer(IWaveColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        public void Render(SKCanvas canvas, int width, int height, float[] barHeights, float[] noiseValues, float intensity)
        {
            canvas.Clear(new SKColor(35, 34, 50));

            int centerY = height / 2;
            int barCount = barHeights.Length;
            float barSpacing = width / (float)barCount;
            float barWidth = barSpacing * 0.4f;
            float maxBarHeight = CalculateMaxBarHeight(height, intensity);

            for (int i = 0; i < barCount; i++)
            {
                float x = i * barSpacing + (barSpacing - barWidth) / 2;
                float scaledHeight = _colorManager.ApplyQuadraticIntensityScale(barHeights[i]);
                float barHeight = Math.Max(scaledHeight * maxBarHeight, 0.3f + noiseValues[i] * 0.7f);
                SKColor barColor = _colorManager.GetInterpolatedColor(i, intensity);

                DrawBar(canvas, x, centerY, barWidth, barHeight, barColor, intensity);
            }
        }

        private float CalculateMaxBarHeight(int height, float intensity)
        {
            float baseHeight = height / 8f;
            float peakHeight = height / 3f;
            return baseHeight + (peakHeight - baseHeight) * _colorManager.ApplyQuadraticIntensityScale(intensity);
        }

        private void DrawBar(SKCanvas canvas, float x, int centerY, float barWidth, float barHeight, SKColor barColor, float intensity)
        {
            using (var paint = new SKPaint
            {
                Color = barColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            })
            {
                canvas.DrawRect(x, centerY - barHeight / 2, barWidth, barHeight, paint);
            }

            float glowAmount = 0.1f + _colorManager.ApplyQuadraticIntensityScale(intensity) * 0.9f;
            byte glowAlpha = (byte)(barColor.Alpha * (0.2f + intensity * 0.3f));

            using (var glowPaint = new SKPaint
            {
                Color = barColor.WithAlpha(glowAlpha),
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 0.5f + 3 * glowAmount)
            })
            {
                canvas.DrawRect(x - 1, centerY - barHeight / 2 - 1, barWidth + 2, barHeight + 2, glowPaint);
            }
        }
    }
}
