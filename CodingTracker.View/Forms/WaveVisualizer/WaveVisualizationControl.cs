using CodingTracker.View.ApplicationControlService;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using CodingTracker.View.Forms.Services.WaveVisualizerService.AnimatedBars;
using System.Runtime.CompilerServices;

namespace CodingTracker.View.Forms.WaveVisualizer
{
    public interface IWaveVisualizationControl
    {
        void UpdateIntensity(float newIntensity);
    }
    /// <summary>
    /// SessionActivityVisualizer - Live coding session intensity visualization control
    /// 
    /// Important design points:
    /// - Uses array of floating bars (barHeights[]) that animate via sine waves and noise to create organic wave motion
    /// - UpdateSessionProgress() maps daily coding minutes to intensity (0.0-1.0) which drives amplitude, color, and movement speed
    /// - Animation loop runs at 60 FPS updating: noise values → target heights → smooth interpolation toward targets
    /// - Wave effect achieved by phase-offsetting each bar: sin(time + barIndex * phaseStep) creates flowing ripple across bars
    /// - Color progression from cool (blue) to warm (orange/pink) based on session intensity using linear interpolation
    /// - Exponential smoothing (currentValue += (target - current) * factor) prevents jarring movements
    /// - Multiple wave layers combined: base sine wave + noise + activity-driven randomness for complex organic motion
    /// - SkiaSharp rendering with anti-aliasing and glow effects that intensify with higher session activity
    /// - Compact corner display (200x150) designed for ambient peripheral awareness rather than active focus
    /// - Real-time visual feedback loop: more coding → higher intensity → more energetic animation → motivation boost
    /// </summary>



    public class WaveVisualizationControl : UserControl, IWaveVisualizationControl
    {
        #region Fields
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


        private readonly SKColor backgroundColor = new SKColor(32, 33, 36);

        // Visualization data
        private const int BAR_COUNT = 64;
        private float[] barHeights = new float[BAR_COUNT];
        private float[] targetBarHeights = new float[BAR_COUNT];
        private float[] noiseValues = new float[BAR_COUNT];



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

        #region Single formula

        private float CalculateJitterValue()
        {
            return (float)(random.NextDouble() * 0.02 - 0.01);
        }

        private float CalculateBaseSineWave(float time, float phase, float frequency)
        {
            return (float)Math.Sin(2 * Math.PI * frequency * time + phase);
        }

        private float CalculatePhaseOffset(int barIndex, float phaseSpacing)
        {
            return barIndex * phaseSpacing;
        }

        private float CalculateNoiseValue(float noiseOffset)
        {
            return (float)Math.Sin(noiseOffset);
        }

        private float AdvanceNoiseOffset(float currentOffset, float speed)
        {
            return currentOffset + speed;
        }

        private float WrapNoiseOffset(float offset, float maxValue)
        {
            return offset > maxValue ? offset - maxValue : offset;
        }

        private float CombineWaveComponents(float mainWave, float noise, float jitter, float noiseWeight, float jitterWeight)
        {
            return mainWave + (noise * noiseWeight) + (jitter * jitterWeight);
        }

        private float SmoothTowardsTarget(float currentValue, float targetValue, float smoothingFactor)
        {
            return currentValue + (targetValue - currentValue) * smoothingFactor;
        }

        private float ClampToValidRange(float value, float minValue, float maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        private float ApplyExponentialScale(float value)
        {
            return value * value;
        }

        private float MapIntensityToMultiplier(float intensity, float minMultiplier, float maxMultiplier)
        {
            return minMultiplier + (maxMultiplier - minMultiplier) * intensity;
        }

        private float CalculateAnimationTime()
        {
            return Environment.TickCount / 1000.0f;
        }

        private float ConvertToPixelHeight(float normalizedHeight, float maxPixelHeight)
        {
            return Math.Abs(normalizedHeight) * maxPixelHeight;
        }

        private float CalculateCenterPosition(float controlHeight)
        {
            return controlHeight / 2.0f;
        }

        private float CalculateBarTopPosition(float centerY, float waveHeight)
        {
            return centerY - (waveHeight / 2.0f);
        }

        private float ClampBlendAmount(float blendAmount)
        {
            return ClampToValidRange(blendAmount, 0.0f, 1.0f);
        }






        #region Color Mathematics

        private Color BlendTwoColors(Color color1, Color color2, float blendAmount)
        {
            blendAmount = ClampBlendAmount(blendAmount);

            int r = (int)(color1.R + (color2.R - color1.R) * blendAmount);
            int g = (int)(color1.G + (color2.G - color1.G) * blendAmount);
            int b = (int)(color1.B + (color2.B - color1.B) * blendAmount);
            int a = (int)(color1.A + (color2.A - color1.A) * blendAmount);

            return Color.FromArgb(a, r, g, b);
        }

        private Color CalculateIntensityColor(float intensity, Color[] colorSteps)
        {
            if (colorSteps.Length == 0) return Color.White;
            if (colorSteps.Length == 1) return colorSteps[0];

            intensity = ClampBlendAmount(intensity);

            float scaledIntensity = intensity * (colorSteps.Length - 1);
            int colorIndex = (int)scaledIntensity;
            float colorBlend = scaledIntensity - colorIndex;

            if (colorIndex >= colorSteps.Length - 1)
                return colorSteps[colorSteps.Length - 1];

            return BlendTwoColors(colorSteps[colorIndex], colorSteps[colorIndex + 1], colorBlend);
        }



        public void UpdateIntensity(float newIntensity)
        {

        }
    }
    #endregion
}