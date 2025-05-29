using SkiaSharp;
using System;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveIllustrator
    {
        void RenderWaveVisualization(SKCanvas canvas, int width, int height, IWaveAnimator animator);
    }

    public class WaveIllustrator : IWaveIllustrator
    {
        private readonly IWaveAppearanceProvider appearanceProvider;
        private readonly Random random = new Random();

        public WaveIllustrator(IWaveAppearanceProvider appearanceProvider)
        {
            this.appearanceProvider = appearanceProvider;
        }

        public void RenderWaveVisualization(SKCanvas canvas, int width, int height, IWaveAnimator animator)
        {
            ClearCanvas(canvas);
            DrawSpectrumBars(canvas, width, height, animator);
            DrawLightningConnections(canvas, width, height, animator);
        }

        private void ClearCanvas(SKCanvas canvas)
        {
            canvas.Clear(appearanceProvider.BackgroundColor);
        }

        private void DrawSpectrumBars(SKCanvas canvas, int width, int height, IWaveAnimator animator)
        {
            float barWidth = CalculateBarWidth(width, animator.BarCount);
            float centerY = animator.CalculateCenterPosition(height);

            for (int i = 0; i < animator.BarCount; i++)
            {
                DrawSpectrumBar(canvas, i, barWidth, centerY, height, animator);
            }
        }

        private void DrawSpectrumBar(SKCanvas canvas, int barIndex, float barWidth, float centerY, int maxHeight, IWaveAnimator animator)
        {
            float normalizedHeight = animator.GetBarHeight(barIndex);
            float scaledHeight = animator.ApplyExponentialScale(normalizedHeight);
            float pixelHeight = ConvertToSpectrumHeight(scaledHeight, maxHeight, animator.CurrentIntensity, animator);

            float x = CalculateBarXPosition(barIndex, barWidth);
            float y = centerY - pixelHeight / 2;

            SKColor barColor = GetSpectrumBarColor(barIndex, animator.BarCount, animator.CurrentIntensity, normalizedHeight, animator);

            DrawSolidBar(canvas, x, y, barWidth, pixelHeight, barColor);
            DrawBarGlow(canvas, x, y, barWidth, pixelHeight, barColor, animator.CurrentIntensity);
        }

        private void DrawSolidBar(SKCanvas canvas, float x, float y, float width, float height, SKColor color)
        {
            using (var paint = new SKPaint
            {
                Color = color,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            })
            {
                float adjustedWidth = width * 0.8f;
                float centerOffset = (width - adjustedWidth) / 2;
                canvas.DrawRect(x + centerOffset, y, adjustedWidth, height, paint);
            }
        }

        private void DrawBarGlow(SKCanvas canvas, float x, float y, float width, float height, SKColor baseColor, ActivityIntensity intensity)
        {
            GlowSettings glowSettings = appearanceProvider.GetGlowSettings(intensity);

            if (ShouldDrawGlow(glowSettings) && height > 10)
            {
                SKColor glowColor = CreateGlowColor(baseColor, glowSettings);

                using (var glowPaint = new SKPaint
                {
                    Color = glowColor,
                    IsAntialias = true,
                    MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, glowSettings.BlurRadius)
                })
                {
                    float adjustedWidth = width * 0.8f;
                    float centerOffset = (width - adjustedWidth) / 2;
                    float glowExpansion = glowSettings.BlurRadius;

                    canvas.DrawRect(
                        x + centerOffset - glowExpansion,
                        y - glowExpansion,
                        adjustedWidth + (glowExpansion * 2),
                        height + (glowExpansion * 2),
                        glowPaint);
                }
            }
        }

        private void DrawLightningConnections(SKCanvas canvas, int width, int height, IWaveAnimator animator)
        {
            float barWidth = CalculateBarWidth(width, animator.BarCount);
            float centerY = animator.CalculateCenterPosition(height);
            float intensityBlend = animator.ApplyExponentialScale(appearanceProvider.GetIntensityBlend(animator.CurrentIntensity));

            if (intensityBlend < 0.1f) return;

            SKPoint[] barTops = new SKPoint[animator.BarCount];
            SKPoint[] barBottoms = new SKPoint[animator.BarCount];
            SKPoint[] barMiddles = new SKPoint[animator.BarCount];

            for (int i = 0; i < animator.BarCount; i++)
            {
                float normalizedHeight = animator.GetBarHeight(i);
                float scaledHeight = animator.ApplyExponentialScale(normalizedHeight);
                float pixelHeight = ConvertToSpectrumHeight(scaledHeight, height, animator.CurrentIntensity, animator);
                float x = CalculateBarXPosition(i, barWidth) + barWidth / 2;

                float jitterScale = 1.0f - intensityBlend * 0.5f;
                float jitterY = (float)(random.NextDouble() * 1.5 - 0.75) * jitterScale;

                barTops[i] = new SKPoint(x, centerY - pixelHeight / 2 + jitterY);
                barBottoms[i] = new SKPoint(x, centerY + pixelHeight / 2 + jitterY);

                float middleOffset = (float)Math.Sin(i * 0.2f + normalizedHeight * 5) * (1.5f + intensityBlend * 3);
                barMiddles[i] = new SKPoint(x, centerY + middleOffset + jitterY);
            }

            DrawLightningPath(canvas, barTops, intensityBlend);
            DrawLightningPath(canvas, barBottoms, intensityBlend);
            DrawLightningPath(canvas, barMiddles, intensityBlend * 0.7f);
        }

        private void DrawLightningPath(SKCanvas canvas, SKPoint[] points, float intensity)
        {
            if (points.Length < 2) return;

            using (var paint = new SKPaint
            {
                Color = GetLightningColor(intensity),
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1.0f + intensity * 2.0f
            })
            {
                for (int i = 0; i < points.Length; i++)
                {
                    if (random.NextDouble() < 0.7f + intensity * 0.3f)
                    {
                        float sizeVariation = (float)(random.NextDouble() * 0.8f);
                        float baseDotSize = 0.8f + intensity * 2.0f;
                        float dotSize = baseDotSize + sizeVariation;

                        byte baseAlpha = (byte)(50 + intensity * 205);
                        SKColor dotColor = GetDotColor(i, points.Length).WithAlpha(baseAlpha);

                        DrawDot(canvas, points[i], dotSize, dotColor, intensity);
                    }
                }
            }
        }

        private void DrawDot(SKCanvas canvas, SKPoint position, float dotSize, SKColor dotColor, float glowIntensity)
        {
            using (var paint = new SKPaint
            {
                Color = dotColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            })
            {
                canvas.DrawCircle(position.X, position.Y, dotSize, paint);

                float minGlow = 0.1f;
                float glowScale = glowIntensity > minGlow ? glowIntensity : minGlow;

                byte glowAlpha = (byte)(dotColor.Alpha * (0.2f + glowIntensity * 0.3f));
                using (var glowPaint = new SKPaint
                {
                    Color = dotColor.WithAlpha(glowAlpha),
                    IsAntialias = true,
                    MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 0.5f + 3 * glowScale)
                })
                {
                    canvas.DrawCircle(position.X, position.Y, dotSize * (1.2f + glowScale * 1.0f), glowPaint);
                }
            }
        }

        private SKColor GetDotColor(int position, int totalCount)
        {
            float colorPosition = (float)position / totalCount;
            SKColor lowColor = appearanceProvider.GetColorAtPosition(appearanceProvider.GetLowActivityColors(), colorPosition);
            SKColor highColor = appearanceProvider.GetColorAtPosition(appearanceProvider.GetHighActivityColors(), colorPosition);
            return appearanceProvider.BlendColors(lowColor, highColor, 0.5f);
        }

        private void DrawLightningSegment(SKCanvas canvas, SKPaint paint, SKPoint start, SKPoint end, float intensity)
        {
            using (var path = new SKPath())
            {
                path.MoveTo(start);

                int segments = (int)(1 + intensity * 3);
                float deltaX = (end.X - start.X) / segments;
                float deltaY = (end.Y - start.Y) / segments;

                for (int i = 1; i < segments; i++)
                {
                    float currentX = start.X + (deltaX * i);
                    float currentY = start.Y + (deltaY * i);

                    float jitterX = (float)(random.NextDouble() * 15 - 7.5) * intensity;
                    float jitterY = (float)(random.NextDouble() * 8 - 4) * intensity;

                    path.LineTo(currentX + jitterX, currentY + jitterY);
                }

                path.LineTo(end);
                canvas.DrawPath(path, paint);
            }
        }

        private void DrawRandomLightningBolts(SKCanvas canvas, SKPoint[] tops, float bottomY, float intensity)
        {
            int boltCount = (int)(intensity * 6);

            using (var paint = new SKPaint
            {
                Color = GetLightningColor(intensity).WithAlpha((byte)(intensity * 150)),
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 0.5f + intensity * 1.0f
            })
            {
                for (int i = 0; i < boltCount; i++)
                {
                    int startIndex = random.Next(tops.Length);
                    int endIndex = Math.Min(startIndex + random.Next(2, 8), tops.Length - 1);

                    SKPoint startPoint = tops[startIndex];
                    SKPoint endPoint = new SKPoint(tops[endIndex].X, bottomY);

                    DrawLightningSegment(canvas, paint, startPoint, endPoint, intensity * 0.7f);
                }
            }
        }

        private SKColor GetLightningColor(float intensity)
        {
            byte alpha = (byte)(60 + intensity * 195);

            if (intensity < 0.3f)
                return new SKColor(120, 160, 255, alpha);
            else if (intensity < 0.6f)
                return new SKColor(160, 200, 255, alpha);
            else
                return new SKColor(200, 230, 255, alpha);
        }

        private float CalculateBarWidth(int totalWidth, int barCount)
        {
            return totalWidth / (float)barCount;
        }

        private float ConvertToSpectrumHeight(float normalizedHeight, int maxHeight, ActivityIntensity intensity, IWaveAnimator animator)
        {
            float intensityBlend = animator.ApplyExponentialScale(appearanceProvider.GetIntensityBlend(intensity));
            float baseMaxHeight = maxHeight / 8.0f;
            float dynamicHeight = maxHeight / 3.0f;
            float maxBarHeight = baseMaxHeight + (dynamicHeight - baseMaxHeight) * intensityBlend;
            float finalHeight = normalizedHeight * maxBarHeight;
            return Math.Max(finalHeight, 0.3f);
        }

        private float CalculateBarXPosition(int barIndex, float barWidth)
        {
            return barIndex * barWidth;
        }

        private bool ShouldDrawGlow(GlowSettings glowSettings)
        {
            return glowSettings.BlurRadius > 0;
        }

        private SKColor CreateGlowColor(SKColor baseColor, GlowSettings glowSettings)
        {
            return new SKColor(baseColor.Red, baseColor.Green, baseColor.Blue, (byte)glowSettings.MaxAlpha);
        }

        private SKColor GetSpectrumBarColor(int barIndex, int totalBars, ActivityIntensity intensity, float height, IWaveAnimator animator)
        {
            float position = CalculateColorPosition(barIndex, totalBars);
            float intensityBlend = animator.ApplyExponentialScale(appearanceProvider.GetIntensityBlend(intensity));

            SKColor lowColor = appearanceProvider.GetColorAtPosition(appearanceProvider.GetLowActivityColors(), position);
            SKColor highColor = appearanceProvider.GetColorAtPosition(appearanceProvider.GetHighActivityColors(), position);

            SKColor baseColor = appearanceProvider.BlendColors(lowColor, highColor, intensityBlend);

            byte baseAlpha = (byte)(40 + intensityBlend * 90);
            byte glowAlpha = (byte)(baseAlpha + animator.ApplyExponentialScale(intensityBlend) * 125);
            return baseColor.WithAlpha(glowAlpha);
        }

        private float CalculateColorPosition(int barIndex, int totalBars)
        {
            return (float)barIndex / totalBars;
        }
    }
}