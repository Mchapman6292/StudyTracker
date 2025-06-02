using CodingTracker.View.Forms.Services.WaveVisualizerService;
using SkiaSharp;
using System;

namespace CodingTracker.View.Forms.WaveVisualizer
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
            canvas.Clear(GetBackgroundColor());

            int centerY = height / 2;
            int barCount = barHeights.Length;
            float barSpacing = CalculateBarSpacing(width, barCount);
            float maxBarHeight = CalculateMaxBarHeight(height, intensity);

            SKPoint[] upperPoints = new SKPoint[barCount];
            SKPoint[] lowerPoints = new SKPoint[barCount];
            SKPoint[] middlePoints = new SKPoint[barCount];

            for (int i = 0; i < barCount; i++)
            {
                float x = CalculateBarXPosition(i, barSpacing, 0);
                float scaledHeight = ScaleBarHeight(barHeights[i]);
                float barHeight = CalculateFinalBarHeight(scaledHeight, maxBarHeight, noiseValues[i]);

                float barCenterX = x + barSpacing / 2;

                DrawVerticalDotColumn(canvas, barCenterX, centerY, barHeight, i, intensity);

                upperPoints[i] = new SKPoint(barCenterX, centerY - barHeight);
                lowerPoints[i] = new SKPoint(barCenterX, centerY + barHeight);

                float waveOffset = CalculateMiddleWaveOffset(i, noiseValues[i], intensity);
                middlePoints[i] = new SKPoint(barCenterX, centerY + waveOffset);
            }

            DrawConnectingLines(canvas, upperPoints, lowerPoints, middlePoints, intensity);
        }


        private void DrawVerticalDotColumn(SKCanvas canvas, float x, float centerY, float barHeight, int barIndex, float intensity)
        {
            float dotSpacing = 5.0f;  
            float dotRadius = 0.8f;   

            SKColor baseColor = _colorManager.GetInterpolatedColor(barIndex, intensity);

            int totalDots = (int)(barHeight / dotSpacing);

            for (int i = 0; i <= totalDots; i++)
            {
                float y = i * dotSpacing;

                if (y <= barHeight)
                {
                    float normalizedPosition = y / barHeight;
                    byte alpha = CalculateDotAlpha(normalizedPosition, intensity);
                    SKColor dotColor = baseColor.WithAlpha(alpha);

                    DrawSingleDot(canvas, new SKPoint(x, centerY - y), dotRadius, dotColor, intensity * 0.3f);

                    if (y > 0)
                    {
                        DrawSingleDot(canvas, new SKPoint(x, centerY + y), dotRadius, dotColor, intensity * 0.3f);
                    }
                }
            }
        }   

        private void DrawConnectingLines(SKCanvas canvas, SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints, float intensity)
        {
            DrawDottedLine(canvas, upperPoints, intensity, 1.0f);
            DrawDottedLine(canvas, lowerPoints, intensity, 1.0f);
            DrawDottedLine(canvas, middlePoints, intensity, 0.7f);
        }

        private void DrawDottedLine(SKCanvas canvas, SKPoint[] points, float intensity, float alphaMultiplier)
        {
            float dotSpacing = 8.0f;  // Increased from 3.0f for wider gaps
            float dotRadius = 0.4f;   // Reduced from 0.8f for smaller dots

            for (int i = 0; i < points.Length - 1; i++)
            {
                SKPoint start = points[i];
                SKPoint end = points[i + 1];

                float distance = CalculateDistance(start, end);
                int dotCount = (int)(distance / dotSpacing);

                if (dotCount > 0)
                {
                    for (int d = 0; d <= dotCount; d++)
                    {
                        float t = (float)d / dotCount;
                        SKPoint dotPos = InterpolatePoint(start, end, t);

                        byte alpha = (byte)(30 + intensity * 50 * alphaMultiplier);  // Much lower alpha
                        SKColor dotColor = _colorManager.GetInterpolatedColor(i, intensity).WithAlpha(alpha);

                        // Very subtle glow
                        DrawSingleDot(canvas, dotPos, dotRadius, dotColor, intensity * 0.1f);
                    }
                }
            }
        }

        private void DrawSingleDot(SKCanvas canvas, SKPoint position, float radius, SKColor color, float glowStrength)
        {
            using (var paint = new SKPaint
            {
                Color = color,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            })
            {
                canvas.DrawCircle(position.X, position.Y, radius, paint);
            }

            if (glowStrength > 0.2f)
            {
                using (var glowPaint = new SKPaint
                {
                    Color = color.WithAlpha((byte)(color.Alpha * 0.3f)),
                    IsAntialias = true,
                    MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, radius * 2.5f * glowStrength)
                })
                {
                    canvas.DrawCircle(position.X, position.Y, radius * 1.5f, glowPaint);
                }
            }
        }

        private byte CalculateDotAlpha(float normalizedPosition, float intensity)
        {
            float fadeStart = 0.7f;
            float alpha = 1.0f;

            if (normalizedPosition > fadeStart)
            {
                float fadeAmount = (normalizedPosition - fadeStart) / (1.0f - fadeStart);
                alpha = 1.0f - fadeAmount * 0.5f;
            }

            return (byte)(255 * alpha * (0.4f + intensity * 0.6f));
        }

        private float CalculateMiddleWaveOffset(int index, float noiseValue, float intensity)
        {
            return (float)Math.Sin(index * 0.2f + noiseValue * 5) * (1.5f + intensity * 3);
        }

        private float CalculateDistance(SKPoint p1, SKPoint p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        private SKPoint InterpolatePoint(SKPoint start, SKPoint end, float t)
        {
            return new SKPoint(
                start.X + (end.X - start.X) * t,
                start.Y + (end.Y - start.Y) * t
            );
        }

        private SKColor GetBackgroundColor()
        {
            return new SKColor(35, 34, 50);
        }

        private float CalculateBarSpacing(int width, int barCount)
        {
            return width / (float)barCount;
        }

        private float CalculateBarXPosition(int index, float spacing, float barWidth)
        {
            return index * spacing;
        }

        private float CalculateMaxBarHeight(int height, float intensity)
        {
            float baseHeight = CalculateBaseHeight(height);
            float peakHeight = CalculatePeakHeight(height);
            float scaledIntensity = _colorManager.ApplyQuadraticIntensityScale(intensity);
            return InterpolateHeight(baseHeight, peakHeight, scaledIntensity);
        }

        private float CalculateBaseHeight(int height)
        {
            return height / 8f;
        }

        private float CalculatePeakHeight(int height)
        {
            return height / 2f;
        }

        private float InterpolateHeight(float baseHeight, float peakHeight, float factor)
        {
            return baseHeight + (peakHeight - baseHeight) * factor;
        }

        private float ScaleBarHeight(float barHeight)
        {
            return _colorManager.ApplyQuadraticIntensityScale(barHeight);
        }

        private float CalculateFinalBarHeight(float scaledHeight, float maxHeight, float noiseValue)
        {
            float minimumHeight = CalculateMinimumHeight(noiseValue);
            return Math.Max(scaledHeight * maxHeight, minimumHeight);
        }

        private float CalculateMinimumHeight(float noiseValue)
        {
            return 0.3f + noiseValue * 0.7f;
        }
    }
}