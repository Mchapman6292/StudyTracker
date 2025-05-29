using CodingTracker.View.ApplicationControlService;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace CodingTracker.View.Forms.WaveVisualizer.WaveVisualizationControls
{
    public interface IWaveVisualizationControl
    {
        void UpdateIntensity(float newIntensity);
    }

    public class WaveVisualizationControl : UserControl, IWaveVisualizationControl
    {
        #region Fields

        // Core components
        private SKControl skControl;
        private System.Windows.Forms.Timer animationTimer;
        private float intensity = 0;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private Random random = new Random();

        // Color configuration
        private readonly SKColor[] spectrumColors = new SKColor[] {
            new SKColor(60, 100, 255),   // Subdued Blue
            new SKColor(60, 180, 230),   // Subdued Cyan
            new SKColor(60, 200, 120),   // Subdued Green
            new SKColor(180, 200, 60),   // Subdued Yellow
            new SKColor(230, 120, 60),   // Subdued Orange
            new SKColor(255, 60, 120),   // Subdued Pink
            new SKColor(200, 60, 200)    // Subdued Purple
        };

        private readonly SKColor[] highIntensityColors = new SKColor[] {
            new SKColor(100, 180, 255),  // Vibrant Blue
            new SKColor(100, 255, 255),  // Vibrant Cyan
            new SKColor(100, 255, 150),  // Vibrant Green
            new SKColor(255, 255, 100),  // Vibrant Yellow
            new SKColor(255, 180, 100),  // Vibrant Orange
            new SKColor(255, 100, 180),  // Vibrant Pink
            new SKColor(255, 100, 255)   // Vibrant Purple
        };

        private readonly SKColor backgroundColor = new SKColor(35, 34, 50);

        // Visualization data
        private const int BAR_COUNT = 64;
        private float[] barHeights = new float[BAR_COUNT];
        private float[] targetBarHeights = new float[BAR_COUNT];
        private float[] noiseValues = new float[BAR_COUNT];

        #endregion

        #region Initialization and Setup

        // Creates a new WaveVisualizationControl with the specified timer service
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
            Controls.Add(skControl);
        }

        // Initializes bars with very small baseline heights for maximum contrast
        private void InitializeBars()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                barHeights[i] = 0.01f + (float)(random.NextDouble() * 0.01);
                targetBarHeights[i] = barHeights[i];
                noiseValues[i] = (float)random.NextDouble();
            }
        }

        // Sets up the timer for continuous animation updates
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

        #endregion

        #region Animation and Bar Updates


        private void UpdateBars()
        {
            UpdateNoiseValues();
            UpdateTargetHeights();
            SmoothBarsTowardTarget();
        }

        // Updates noise values for static effect and continuous variation
        private void UpdateNoiseValues()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {

                noiseValues[i] += (float)(random.NextDouble() * 0.1 - 0.05);

                if (noiseValues[i] < 0) noiseValues[i] += 1;
                if (noiseValues[i] > 1) noiseValues[i] -= 1;
            }
        }

        // Calculates target heights for each bar based on patterns and intensity
        private void UpdateTargetHeights()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                // Constant static effect even at zero intensity - smaller baseline
                float staticEffect = CalculateStaticEffect(i);

                // Dynamic pattern that changes over time - scale with intensity
                double time = _stopWatchTimerService.ReturnElapsedMilliseconds() / 1000.0;
                // Pattern factor now scales directly with intensity (very small when intensity is low)
                float patternFactor = (float)Math.Sin(time * (1.0 + intensity * 2.0) + i * 0.2) * 0.03f * (0.1f + intensity * 0.9f);

                // Intensity-based movement (increases with typing) - now with more dramatic effect
                float intensityMovement = (float)(random.NextDouble() * 0.1 - 0.05) * ApplyExponentialScale(intensity);

                float heightChange = staticEffect + patternFactor + intensityMovement;

                // Set a minimum and maximum for base visualization (much lower when intensity is low)
                float minHeight = 0.005f + intensity * 0.05f;
                targetBarHeights[i] = Math.Max(minHeight, Math.Min(1.0f,
                    targetBarHeights[i] + heightChange));
            }
        }

        private float CalculateStaticEffect(int index)
        {
            // Base static effect - smaller random movements
            float baseStatic = (float)(random.NextDouble() * 0.02 - 0.01);

            // Use noise values for more natural static patterns
            float noiseEffect = (float)(Math.Sin(noiseValues[index] * Math.PI * 6) * 0.01);

            // Add occasional small spikes for "cackle" effect - now scales with intensity
            float spike = 0;
            if (random.NextDouble() < 0.01 + intensity * 0.03) // Chance increases with intensity
            {
                spike = (float)(random.NextDouble() * 0.03 * (0.2f + intensity * 0.8f));
            }

            // Combine static effects
            return baseStatic + noiseEffect + spike;
        }

        // Gradually moves current bar heights toward target heights for smooth animation
        private void SmoothBarsTowardTarget()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                // Smoothing factor increases with intensity - more responsive during active typing
                float smoothingFactor = 0.15f + intensity * 0.5f;
                barHeights[i] += (targetBarHeights[i] - barHeights[i]) * smoothingFactor;

                // Add small constant variation to heights for static feel - scaled down at high intensity
                float staticAmount = 0.01f * (1f - intensity * 0.7f);
                if (intensity < 0.5f)
                {
                    barHeights[i] += (float)(random.NextDouble() * staticAmount - staticAmount / 2);
                }
            }
        }

        // Applies special effects that only occur at high intensity levels
        private void ApplyHighIntensityEffects()
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                // More aggressive high-intensity effects that trigger at lower thresholds
                if (random.NextDouble() < intensity * 0.3)
                {
                    // More pronounced height adjustments at high intensity
                    targetBarHeights[i] = 0.2f + (float)(random.NextDouble() * intensity * 0.8f);
                }
            }
        }

        #endregion

        #region Drawing and Rendering

        // Handles the SKControl paint event for rendering the visualization
        private void SkControl_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(backgroundColor);
            DrawAudioSpectrum(canvas, e.Info.Width, e.Info.Height);
        }

        // Draws the complete audio spectrum visualization
        private void DrawAudioSpectrum(SKCanvas canvas, int width, int height)
        {
            int centerY = height / 2;

            // Make max height scale with intensity for more dramatic effect
            int baseMaxHeight = height / 8; // Even smaller baseline height
            int dynamicHeight = height / 3; // Maximum full height
            float maxBarHeight = baseMaxHeight + (dynamicHeight - baseMaxHeight) * ApplyExponentialScale(intensity);

            float barSpacing = width / (float)BAR_COUNT;
            float barWidth = barSpacing * 0.4f;

            SKPoint[] upperPoints = new SKPoint[BAR_COUNT];
            SKPoint[] lowerPoints = new SKPoint[BAR_COUNT];
            SKPoint[] middlePoints = new SKPoint[BAR_COUNT];

            DrawBarsAndCollectPoints(canvas, width, height, centerY, maxBarHeight, barSpacing, barWidth, upperPoints, lowerPoints, middlePoints);
            DrawDottedLines(canvas, upperPoints, lowerPoints, middlePoints);
        }

        // Draws all bars and collects points for dotted lines
        private void DrawBarsAndCollectPoints(SKCanvas canvas, int width, int height, int centerY, float maxBarHeight, float barSpacing, float barWidth,
            SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints)
        {
            for (int i = 0; i < BAR_COUNT; i++)
            {
                float x = i * barSpacing + (barSpacing - barWidth) / 2;

                // Apply additional static jitter to bar heights for low intensities
                float staticJitter = intensity < 0.3f ? (float)(random.NextDouble() * 0.02 - 0.01) : 0;

                // Apply exponential scaling to bar heights for more dramatic effect
                float scaledHeight = ApplyExponentialScale(barHeights[i]);
                float barHeight = scaledHeight * maxBarHeight;

                // Ensure minimal height for bars for constant static visibility - smaller baseline
                barHeight = Math.Max(barHeight, 0.3f + noiseValues[i] * 0.7f);

                SKColor barColor = GetColorForPosition(i);

                DrawBar(canvas, x, centerY, barWidth, barHeight, barColor);

                StorePointsForDottedLines(i, x, barWidth, centerY, barHeight, upperPoints, lowerPoints, middlePoints);
            }
        }

        // Draws a single bar with glow effect
        private void DrawBar(SKCanvas canvas, float x, int centerY, float barWidth, float barHeight, SKColor barColor)
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

            // Glow effect scales exponentially with intensity
            float glowAmount = 0.1f + ApplyExponentialScale(intensity) * 0.9f;
            byte glowAlpha = (byte)(barColor.Alpha * (0.2f + intensity * 0.3f));
            using (var glowPaint = new SKPaint
            {
                Color = barColor.WithAlpha(glowAlpha),
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 0.5f + 3 * glowAmount)
            })
            {
                canvas.DrawRect(x - 1, centerY - barHeight / 2 - 1,
                               barWidth + 2, barHeight + 2, glowPaint);
            }
        }

        // Stores points for dotted line drawing for a single bar
        private void StorePointsForDottedLines(int index, float x, float barWidth, int centerY, float barHeight,
            SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints)
        {
            // Add small static jitter to dot positions - scaled by intensity
            float jitterScale = 1.0f - intensity * 0.5f; // Less jitter at high intensity
            float jitterY = (float)(random.NextDouble() * 1.5 - 0.75) * jitterScale;

            upperPoints[index] = new SKPoint(x + barWidth / 2, centerY - barHeight / 2 + jitterY);
            lowerPoints[index] = new SKPoint(x + barWidth / 2, centerY + barHeight / 2 + jitterY);

            // More pronounced middle line movement for static effect
            float middleOffset = (float)Math.Sin(index * 0.2f + noiseValues[index] * 5) * (1.5f + intensity * 3);
            middlePoints[index] = new SKPoint(x + barWidth / 2, centerY + middleOffset + jitterY);
        }

        // Draws dotted lines connecting all bars
        private void DrawDottedLines(SKCanvas canvas, SKPoint[] upperPoints, SKPoint[] lowerPoints, SKPoint[] middlePoints)
        {
            // Make dot intensity scale non-linearly
            float upperLineIntensity = Math.Max(0.1f, ApplyExponentialScale(intensity));
            float lowerLineIntensity = Math.Max(0.1f, ApplyExponentialScale(intensity));
            float middleLineIntensity = Math.Max(0.05f, ApplyExponentialScale(intensity) * 0.7f);

            DrawDottedLine(canvas, upperPoints, upperLineIntensity);
            DrawDottedLine(canvas, lowerPoints, lowerLineIntensity);
            DrawDottedLine(canvas, middlePoints, middleLineIntensity);
        }

        // Draws a single dotted line with the specified intensity
        private void DrawDottedLine(SKCanvas canvas, SKPoint[] points, float glowIntensity)
        {
            for (int i = 0; i < points.Length; i++)
            {
                // Dot size now scales non-linearly with intensity
                float sizeVariation = (float)(random.NextDouble() * 0.3);
                // Base dot size is smaller with higher max when intensity is high
                float baseDotSize = 0.5f + glowIntensity * 1.5f;
                float dotSize = baseDotSize + sizeVariation;

                // Base alpha ensures visibility even at low intensity, but much lower
                byte baseAlpha = (byte)(30 + glowIntensity * 225);

                SKColor dotColor = GetColorForPosition(i).WithAlpha(baseAlpha);

                DrawDot(canvas, points[i], dotSize, dotColor, glowIntensity);
            }
        }

        // Draws a single dot with glow effect
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

                // Always have some glow for static effect, but much stronger at high intensity
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
                    // Glow radius scales exponentially with intensity
                    canvas.DrawCircle(position.X, position.Y, dotSize * (1.2f + glowScale * 1.0f), glowPaint);
                }
            }
        }

        #endregion

        #region Color and Intensity Handling

        // Determines the color for a specific bar position based on intensity
        private SKColor GetColorForPosition(int position)
        {
            float colorPosition = (float)position / BAR_COUNT * (spectrumColors.Length - 1);
            int colorIndex = (int)colorPosition;
            float colorBlend = colorPosition - colorIndex;

            // Choose between low and high intensity color sets based on current intensity
            SKColor lowColor = spectrumColors[Math.Min(colorIndex, spectrumColors.Length - 1)];
            SKColor highColor = highIntensityColors[Math.Min(colorIndex, highIntensityColors.Length - 1)];

            // Blend between subdued and vibrant colors based on intensity
            SKColor baseColor;
            if (colorIndex < spectrumColors.Length - 1)
            {
                // Get the next colors in both palettes
                SKColor lowNextColor = spectrumColors[colorIndex + 1];
                SKColor highNextColor = highIntensityColors[colorIndex + 1];

                // Blend within each palette first
                SKColor blendedLowColor = BlendColors(lowColor, lowNextColor, colorBlend);
                SKColor blendedHighColor = BlendColors(highColor, highNextColor, colorBlend);

                // Then blend between the palettes based on intensity
                baseColor = BlendColors(blendedLowColor, blendedHighColor, ApplyExponentialScale(intensity));
            }
            else
            {
                baseColor = BlendColors(lowColor, highColor, ApplyExponentialScale(intensity));
            }

            // Scaled alpha based on intensity - much more transparent at low intensity
            byte baseAlpha = (byte)(40 + intensity * 90); // 40 at 0, 130 at 1.0
            byte glowAlpha = (byte)(baseAlpha + ApplyExponentialScale(intensity) * 125); // More dramatic alpha increase
            return baseColor.WithAlpha(glowAlpha);
        }

        // Blends two colors based on an amount value (0.0 to 1.0)
        private SKColor BlendColors(SKColor color1, SKColor color2, float amount)
        {
            byte r = (byte)(color1.Red + (color2.Red - color1.Red) * amount);
            byte g = (byte)(color1.Green + (color2.Green - color1.Green) * amount);
            byte b = (byte)(color1.Blue + (color2.Blue - color1.Blue) * amount);
            byte a = (byte)(color1.Alpha + (color2.Alpha - color1.Alpha) * amount);
            return new SKColor(r, g, b, a);
        }

        // Applies quadratic transformation to intensity value to create non-linear curve.
        // Low intensity values become smaller (e.g., 0.2 → 0.04) while high values are less affected.
        private float ApplyExponentialScale(float value)
        {
            return value * value;
        }

        // Updates the current intensity value and triggers high-intensity effects
        public void UpdateIntensity(float newIntensity)
        {
            intensity = newIntensity;

            if (intensity > 0.4f)
            {
                ApplyHighIntensityEffects();
            }
        }

        #endregion

        // Cleanup resources when control is disposed
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