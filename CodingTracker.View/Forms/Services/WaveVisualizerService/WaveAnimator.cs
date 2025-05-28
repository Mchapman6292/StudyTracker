using System.Drawing;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveAnimator
    {
        ActivityIntensity CurrentIntensity { get; }
        bool IsTransitioning { get; }
        int BarCount { get; }

        void UpdateFromSessionData(double sessionSeconds);
        void UpdateFromKeyboardActivity(float keyboardIntensity);
        void UpdateAnimation();
        float GetBarHeight(int index);
    }

    public class WaveAnimator : IWaveAnimator
    {
        private ActivityIntensity currentIntensity;
        private ActivityIntensity targetIntensity;
        private bool isTransitioning;
        private float transitionProgress;
        private const float TransitionSpeed = 0.02f;

        private readonly float[] barHeights;
        private readonly float[] targetBarHeights;
        private readonly float[] noiseOffsets;
        private readonly Random random;

        private readonly IActivityIntensityProvider intensityProvider;
        private readonly IWaveAppearanceProvider appearanceProvider;

        public ActivityIntensity CurrentIntensity => currentIntensity;
        public bool IsTransitioning => isTransitioning;
        public int BarCount => appearanceProvider.DefaultBarCount;

        public WaveAnimator(IActivityIntensityProvider intensityProvider, IWaveAppearanceProvider appearanceProvider)
        {
            this.intensityProvider = intensityProvider;
            this.appearanceProvider = appearanceProvider;

            barHeights = new float[appearanceProvider.DefaultBarCount];
            targetBarHeights = new float[appearanceProvider.DefaultBarCount];
            noiseOffsets = new float[appearanceProvider.DefaultBarCount];
            random = new Random();

            currentIntensity = ActivityIntensity.Dormant;
            targetIntensity = ActivityIntensity.Dormant;

            InitializeArrays();
        }

        public void UpdateFromSessionData(double sessionSeconds)
        {
            ActivityIntensity newIntensity = intensityProvider.GetIntensityFromDuration(sessionSeconds);
            UpdateIntensity(newIntensity);
        }

        public void UpdateFromKeyboardActivity(float keyboardIntensity)
        {
            double sessionSeconds = CalculateSessionSeconds(keyboardIntensity);
            UpdateFromSessionData(sessionSeconds);
        }

        public void UpdateAnimation()
        {
            UpdateTransition();
            UpdateNoiseOffsets();
            UpdateTargetHeights();
            UpdateCurrentHeights();
        }

        public float GetBarHeight(int index)
        {
            return IsValidIndex(index) ? barHeights[index] : 0.0f;
        }

        private void InitializeArrays()
        {
            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                barHeights[i] = CalculateInitialHeight();
                targetBarHeights[i] = CalculateInitialHeight();
                noiseOffsets[i] = CalculateInitialNoiseOffset(i);
            }
        }

        private void UpdateIntensity(ActivityIntensity newIntensity)
        {
            if (newIntensity != currentIntensity)
            {
                targetIntensity = newIntensity;
                isTransitioning = true;
                transitionProgress = CalculateInitialTransitionProgress();
            }
        }

        private void UpdateTransition()
        {
            if (isTransitioning)
            {
                transitionProgress = AdvanceTransitionProgress(transitionProgress);

                if (IsTransitionComplete(transitionProgress))
                {
                    transitionProgress = CalculateCompleteTransition();
                    currentIntensity = targetIntensity;
                    isTransitioning = false;
                }
            }
        }

        private void UpdateNoiseOffsets()
        {
            IntensitySettings settings = GetCurrentSettings();
            float adjustedSpeed = CalculateNoiseSpeed(settings.NoiseMultiplier);

            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                noiseOffsets[i] = AdvanceNoiseOffset(noiseOffsets[i], adjustedSpeed);
            }
        }

        private void UpdateTargetHeights()
        {
            float time = CalculateAnimationTime();
            IntensitySettings settings = GetCurrentSettings();

            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                targetBarHeights[i] = CalculateWaveForBar(i, time, settings);
            }
        }

        private void UpdateCurrentHeights()
        {
            IntensitySettings settings = GetCurrentSettings();

            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                barHeights[i] = SmoothTowardsTarget(barHeights[i], targetBarHeights[i], settings.SmoothingFactor);
            }
        }

        private double CalculateSessionSeconds(float keyboardIntensity)
        {
            return keyboardIntensity * 7200;
        }

        private float CalculateInitialHeight()
        {
            return 0.1f;
        }

        private float CalculateInitialNoiseOffset(int barIndex)
        {
            return barIndex * appearanceProvider.WaveSpacing;
        }

        private float CalculateInitialTransitionProgress()
        {
            return 0.0f;
        }

        private float AdvanceTransitionProgress(float currentProgress)
        {
            return currentProgress + TransitionSpeed;
        }

        private bool IsTransitionComplete(float progress)
        {
            return progress >= 1.0f;
        }

        private float CalculateCompleteTransition()
        {
            return 1.0f;
        }

        private float CalculateBaseSineWave(float time, float phase, float frequency)
        {
            return (float)Math.Sin(2 * Math.PI * frequency * time + phase);
        }

        private float CalculatePhaseOffset(int barIndex)
        {
            return barIndex * appearanceProvider.WaveSpacing;
        }

        private float CalculateNoiseValue(float noiseOffset)
        {
            return (float)Math.Sin(noiseOffset);
        }

        private float CalculateJitterValue()
        {
            return (float)(random.NextDouble() * 0.02 - 0.01);
        }

        private float CalculateAnimationTime()
        {
            return Environment.TickCount / 1000.0f;
        }

        private float CalculateNoiseSpeed(float noiseMultiplier)
        {
            return appearanceProvider.NoiseSpeed * noiseMultiplier;
        }

        private float AdvanceNoiseOffset(float currentOffset, float speed)
        {
            return WrapNoiseOffset(currentOffset + speed);
        }

        private float SmoothTowardsTarget(float currentValue, float targetValue, float smoothingFactor)
        {
            return currentValue + (targetValue - currentValue) * smoothingFactor;
        }

        private float CombineWaveComponents(float mainWave, float noise, float jitter, float noiseWeight)
        {
            return mainWave + (noise * noiseWeight) + jitter;
        }

        private float ClampToValidRange(float value, float minValue, float maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, value));
        }

        private float CalculateWaveForBar(int barIndex, float time, IntensitySettings settings)
        {
            float phase = CalculatePhaseOffset(barIndex);
            float mainWave = CalculateBaseSineWave(time, phase, settings.FrequencyMultiplier);
            float scaledAmplitude = CalculateScaledAmplitude(settings.AmplitudeMultiplier);

            float noise = CalculateNoiseValue(noiseOffsets[barIndex]);
            float jitter = CalculateJitterValue();
            float noiseWeight = CalculateNoiseWeight(settings.NoiseMultiplier);

            float waveHeight = CombineWaveComponents(mainWave * scaledAmplitude, noise, jitter, noiseWeight);

            return ClampToValidRange(Math.Abs(waveHeight), 0.01f, 1.0f);
        }

        private float CalculateScaledAmplitude(float amplitudeMultiplier)
        {
            return appearanceProvider.BaseWaveHeight * amplitudeMultiplier;
        }

        private float CalculateNoiseWeight(float noiseMultiplier)
        {
            return noiseMultiplier * 0.1f;
        }

        private IntensitySettings GetCurrentSettings()
        {
            if (!isTransitioning)
            {
                return intensityProvider.GetSettings(currentIntensity);
            }

            IntensitySettings current = intensityProvider.GetSettings(currentIntensity);
            IntensitySettings target = intensityProvider.GetSettings(targetIntensity);

            return BlendIntensitySettings(current, target, transitionProgress);
        }

        private IntensitySettings BlendIntensitySettings(IntensitySettings current, IntensitySettings target, float progress)
        {
            float blendedAmplitude = LinearInterpolate(current.AmplitudeMultiplier, target.AmplitudeMultiplier, progress);
            float blendedFrequency = LinearInterpolate(current.FrequencyMultiplier, target.FrequencyMultiplier, progress);
            float blendedNoise = LinearInterpolate(current.NoiseMultiplier, target.NoiseMultiplier, progress);
            float blendedSmoothing = LinearInterpolate(current.SmoothingFactor, target.SmoothingFactor, progress);

            return new IntensitySettings(blendedAmplitude, blendedFrequency, blendedNoise, blendedSmoothing);
        }

        private float LinearInterpolate(float startValue, float endValue, float blendAmount)
        {
            return startValue + (endValue - startValue) * blendAmount;
        }

        private float WrapNoiseOffset(float offset)
        {
            const float maxOffset = (float)(2 * Math.PI * 10);
            return offset > maxOffset ? offset - maxOffset : offset;
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < appearanceProvider.DefaultBarCount;
        }
    }
}