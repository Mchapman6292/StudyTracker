using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using CodingTracker.View.WaveVisualizer.WaveLayers;

namespace CodingTracker.View.TimerDisplayService.WaveVisualizationControls
{
    public interface IWaveVisualizationControl
    {
        void UpdateIntensity(float newIntensity);
    }

    public class WaveVisualizationControl : UserControl, IWaveVisualizationControl
    {
        private SKControl skControl;
        private System.Windows.Forms.Timer animationTimer;
        private float intensity = 0;
        private readonly IStopWatchTimerService _stopWatchTimerService;

        private readonly SKColor[] spectrumColors = new SKColor[] {
            new SKColor(100, 150, 255),  // Blue
            new SKColor(100, 220, 220),  // Cyan
            new SKColor(100, 255, 150),  // Green
            new SKColor(220, 220, 100),  // Yellow
            new SKColor(255, 150, 100),  // Orange
            new SKColor(255, 100, 150),  // Pink
            new SKColor(220, 100, 220)   // Purple
        };

        private readonly SKColor backgroundColor = new SKColor(32, 33, 36);

        private const int BAR_COUNT = 64;
        private float[] barHeights = new float[BAR_COUNT];
        private float[] targetBarHeights = new float[BAR_COUNT];
        private Random random = new Random();

        public WaveVisualizationControl(IStopWatchTimerService stopWatchTimerService)
        {
            _stopWatchTimerService = stopWatchTimerService;
            InitializeComponent();
            InitializeBars();
            SetupAnimationTimer();
        }

        private void InitializeComponent()
        {
            skControl = new SKControl();
            skControl.Dock = DockStyle.Fill;
            skControl.PaintSurface += SkControl_PaintSurface;
            this.Controls.Add(skControl);
        }

        private void InitializeBars()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                barHeights[i] = 0.05f + (float)(random.NextDouble() * 0.1);
                targetBarHeights[i] = barHeights[i];
            }
        }

        private void SetupAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += (s, e) =>
            {
                UpdateBars();
                skControl.Invalidate();
            };
            animationTimer.Start();
        }

        private void UpdateBars()
        {
            UpdateTargetHeights();
            SmoothBarsTowardTarget();
        }

        private void UpdateTargetHeights()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                float ambientMovement = (float)(random.NextDouble() * 0.05) - 0.025f;

                double time = _stopWatchTimerService.ReturnElapsedMilliseconds() / 1000.0;
                float patternFactor = (float)Math.Sin(time * 1.5 + i * 0.2) * 0.08f;

                float intensityMovement = (float)(random.NextDouble() * 0.2 - 0.1) * intensity;

                float heightChange = ambientMovement + patternFactor + intensityMovement;

                targetBarHeights[i] = Math.Max(0.05f, Math.Min(1.0f,
                    targetBarHeights[i] + heightChange));
            }
        }

        private void SmoothBarsTowardTarget()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                barHeights[i] += (targetBarHeights[i] - barHeights[i]) * 0.3f;
            }
        }

        private void SkControl_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(backgroundColor);
            DrawAudioSpectrum(canvas, e.Info.Width, e.Info.Height);
        }

        private void DrawAudioSpectrum(SKCanvas canvas, int width, int height)
        {
            int centerY = height / 2;
            int maxBarHeight = height / 3;
            float barSpacing = width / (float)BAR_COUNT;
            float barWidth = barSpacing * 0.4f;

            SKPoint[] upperPoints = new SKPoint[BAR_COUNT];
            SKPoint[] lowerPoints = new SKPoint[BAR_COUNT];
            SKPoint[] middlePoints = new SKPoint[BAR_COUNT];

            DrawBarsAndCollectPoints(canvas, width, height, centerY, maxBarHeight, barSpacing, barWidth, upperPoints, lowerPoints, middlePoints);
            DrawDottedLines(canvas, upperPoints, lowerPoints, middlePoints);
        }

        private void DrawBarsAndCollectPoints(SKCanvas canvas, int width, int height, int centerY, int maxBarHeight, float barSpacing, float barWidth,
            SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints)
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                float x = i * barSpacing + (barSpacing - barWidth) / 2;
                float barHeight = barHeights[i] * maxBarHeight;

                SKColor barColor = GetColorForPosition(i);

                DrawBar(canvas, x, centerY, barWidth, barHeight, barColor);

                StorePointsForDottedLines(i, x, barWidth, centerY, barHeight, upperPoints, lowerPoints, middlePoints);
            }
        }

        private SKColor GetColorForPosition(int position)
        {
            float colorPosition = (float)position / BAR_COUNT * (spectrumColors.Length - 1);
            int colorIndex = (int)colorPosition;
            float colorBlend = colorPosition - colorIndex;

            SKColor color;
            if (colorIndex < spectrumColors.Length - 1)
            {
                color = BlendColors(spectrumColors[colorIndex],
                                    spectrumColors[colorIndex + 1],
                                    colorBlend);
            }
            else
            {
                color = spectrumColors[spectrumColors.Length - 1];
            }

            byte glowAlpha = (byte)(100 + (intensity * 155));
            return color.WithAlpha(glowAlpha);
        }

        private void DrawBar(SKCanvas canvas, float x, int centerY, float barWidth, float barHeight, SKColor barColor)
        {
            if (barHeight > 2)
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

                if (intensity > 0.3f)
                {
                    byte glowAlpha = (byte)(barColor.Alpha * 0.4f);
                    using (var glowPaint = new SKPaint
                    {
                        Color = barColor.WithAlpha(glowAlpha),
                        IsAntialias = true,
                        MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 3 * intensity)
                    })
                    {
                        canvas.DrawRect(x - 2, centerY - barHeight / 2 - 2,
                                       barWidth + 4, barHeight + 4, glowPaint);
                    }
                }
            }
        }

        private void StorePointsForDottedLines(int index, float x, float barWidth, int centerY, float barHeight,
            SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints)
        {
            upperPoints[index] = new SKPoint(x + barWidth / 2, centerY - barHeight / 2);
            lowerPoints[index] = new SKPoint(x + barWidth / 2, centerY + barHeight / 2);

            float middleOffset = (float)Math.Sin(index * 0.2f) * 3;
            middlePoints[index] = new SKPoint(x + barWidth / 2, centerY + middleOffset);
        }

        private void DrawDottedLines(SKCanvas canvas, SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints)
        {
            DrawDottedLine(canvas, upperPoints, intensity);
            DrawDottedLine(canvas, lowerPoints, intensity);
            DrawDottedLine(canvas, middlePoints, intensity * 0.7f);
        }

        private void DrawDottedLine(SKCanvas canvas, SKPoint[] points, float glowIntensity)
        {
            for (int i = 0; i < points.Length; i++)
            {
                float dotSize = 2 + (glowIntensity * 1);
                byte alpha = (byte)(150 + (glowIntensity * 105));
                SKColor dotColor = GetColorForPosition(i).WithAlpha(alpha);

                DrawDot(canvas, points[i], dotSize, dotColor, glowIntensity);
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

                if (glowIntensity > 0.3f)
                {
                    byte glowAlpha = (byte)(dotColor.Alpha * 0.4f);
                    using (var glowPaint = new SKPaint
                    {
                        Color = dotColor.WithAlpha(glowAlpha),
                        IsAntialias = true,
                        MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 3 * glowIntensity)
                    })
                    {
                        canvas.DrawCircle(position.X, position.Y, dotSize * 2, glowPaint);
                    }
                }
            }
        }

        private SKColor BlendColors(SKColor color1, SKColor color2, float amount)
        {
            byte r = (byte)(color1.Red + (color2.Red - color1.Red) * amount);
            byte g = (byte)(color1.Green + (color2.Green - color1.Green) * amount);
            byte b = (byte)(color1.Blue + (color2.Blue - color1.Blue) * amount);
            byte a = (byte)(color1.Alpha + (color2.Alpha - color1.Alpha) * amount);
            return new SKColor(r, g, b, a);
        }

        public void UpdateIntensity(float newIntensity)
        {
            intensity = newIntensity;

            if (intensity > 0.5f)
            {
                ApplyHighIntensityEffects();
            }
        }

        private void ApplyHighIntensityEffects()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                if (random.NextDouble() < intensity * 0.2)
                {
                    targetBarHeights[i] = 0.3f + (float)(random.NextDouble() * intensity * 0.7f);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Stop();
                animationTimer?.Dispose();
                skControl?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}