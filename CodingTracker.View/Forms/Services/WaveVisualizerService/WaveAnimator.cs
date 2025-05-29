using System;

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

        // Helper methods for visualization
        float ApplyExponentialScale(float value);
        float CalculateCenterPosition(float height);
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
        private readonly float[] staticValues;
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
            staticValues = new float[appearanceProvider.DefaultBarCount];
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
            UpdateStaticFlicker();
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
                staticValues[i] = CalculateInitialStaticValue();
            }
        }

        private void UpdateIntensity(ActivityIntensity newIntensity)
        {
            if (newIntensity != currentIntensity)
            {
                targetIntensity = newIntensity;
                isTransitioning = true;
                transitionProgress = 0.0f;
            }
        }

        private void UpdateTransition()
        {
            if (isTransitioning)
            {
                transitionProgress = Math.Min(1.0f, transitionProgress + TransitionSpeed);

                if (transitionProgress >= 1.0f)
                {
                    currentIntensity = targetIntensity;
                    isTransitioning = false;
                }
            }
        }

        private void UpdateStaticFlicker()
        {
            // Update random static values for each bar
            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                staticValues[i] = (float)random.NextDouble();
            }
        }

        private void UpdateTargetHeights()
        {
            IntensitySettings settings = GetCurrentSettings();
            float intensityBlend = GetIntensityBlend();

            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                if (intensityBlend < 0.1f)
                {
                    // Dormant state - minimal static
                    targetBarHeights[i] = CalculateDormantHeight(settings);
                }
                else
                {
                    // Active states - lightning/synthesizer effect
                    targetBarHeights[i] = CalculateLightningHeight(i, settings, intensityBlend);
                }
            }
        }

        private void UpdateCurrentHeights()
        {
            IntensitySettings settings = GetCurrentSettings();
            float intensityBlend = GetIntensityBlend();
            float responsiveness = CalculateResponsiveness(settings);

            for (int i = 0; i < appearanceProvider.DefaultBarCount; i++)
            {
                if (intensityBlend < 0.1f)
                {
                    // Dormant - direct static flicker
                    barHeights[i] = ApplyStaticFlicker(targetBarHeights[i], settings);
                }
                else
                {
                    // Active - sharp synthesizer response
                    barHeights[i] = ApplySynthesizerResponse(barHeights[i], targetBarHeights[i], responsiveness, settings);
                }

                barHeights[i] = EnsureMinimumHeight(barHeights[i]);
            }
        }

        private float CalculateDormantHeight(IntensitySettings settings)
        {
            // Minimal static flicker for dormant state
            if (random.NextDouble() < 0.02 * settings.FrequencyMultiplier)
            {
                return (float)(random.NextDouble() * 0.15 * settings.AmplitudeMultiplier);
            }
            return (float)(random.NextDouble() * 0.05 * settings.AmplitudeMultiplier);
        }

        private float CalculateLightningHeight(int barIndex, IntensitySettings settings, float intensityBlend)
        {
            float randomValue = (float)random.NextDouble();

            // Frequency-based spike chances
            float majorSpikeChance = intensityBlend * 0.15f * settings.FrequencyMultiplier;
            float mediumSpikeChance = intensityBlend * 0.25f * settings.FrequencyMultiplier;
            float minorSpikeChance = intensityBlend * 0.4f * settings.FrequencyMultiplier;

            // Apply noise multiplier for variation
            float noiseOffset = (float)(random.NextDouble() * settings.NoiseMultiplier);

            if (randomValue < majorSpikeChance)
            {
                // Major spike - full height with amplitude multiplier
                return (0.7f + (float)(random.NextDouble() * 0.3f)) * settings.AmplitudeMultiplier + noiseOffset;
            }
            else if (randomValue < mediumSpikeChance)
            {
                // Medium spike
                return (0.3f + (float)(random.NextDouble() * 0.4f * intensityBlend)) * settings.AmplitudeMultiplier + noiseOffset;
            }
            else if (randomValue < minorSpikeChance)
            {
                // Minor spike
                return (0.1f + (float)(random.NextDouble() * 0.2f * intensityBlend)) * settings.AmplitudeMultiplier + noiseOffset;
            }
            else
            {
                // Base static level
                return (float)(random.NextDouble() * 0.08f * settings.AmplitudeMultiplier);
            }
        }

        private float ApplyStaticFlicker(float targetHeight, IntensitySettings settings)
        {
            // Direct static flicker with noise multiplier
            float flicker = (float)(random.NextDouble() * 0.03 - 0.015) * settings.NoiseMultiplier;
            return targetHeight + flicker;
        }

        private float ApplySynthesizerResponse(float currentHeight, float targetHeight, float responsiveness, IntensitySettings settings)
        {
            // Sharp, immediate response for synthesizer effect
            if (targetHeight > currentHeight * 2.0f)
            {
                // Instant jump for large spikes
                return targetHeight * (0.6f + responsiveness * 0.4f);
            }
            else if (targetHeight < currentHeight * 0.3f)
            {
                // Quick drop with some static
                return targetHeight + (float)(random.NextDouble() * 0.05f * settings.NoiseMultiplier);
            }
            else
            {
                // Normal transition with responsiveness
                float difference = targetHeight - currentHeight;
                float staticNoise = (float)(random.NextDouble() * 0.03f - 0.015f) * settings.NoiseMultiplier;
                return currentHeight + (difference * responsiveness) + staticNoise;
            }
        }

        private float CalculateResponsiveness(IntensitySettings settings)
        {
            // Use smoothing factor inversely - lower smoothing = more responsive
            return 1.0f - settings.SmoothingFactor;
        }

        private IntensitySettings GetCurrentSettings()
        {
            if (!isTransitioning)
            {
                return intensityProvider.GetSettings(currentIntensity);
            }

            // Blend settings during transition
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

        private float GetIntensityBlend()
        {
            return appearanceProvider.GetIntensityBlend(currentIntensity);
        }

        private float LinearInterpolate(float startValue, float endValue, float blendAmount)
        {
            return startValue + (endValue - startValue) * blendAmount;
        }

        private double CalculateSessionSeconds(float keyboardIntensity)
        {
            return keyboardIntensity * 7200;
        }

        private float CalculateInitialHeight()
        {
            return 0.01f + (float)(random.NextDouble() * 0.01);
        }

        private float CalculateInitialStaticValue()
        {
            return (float)random.NextDouble();
        }

        private float EnsureMinimumHeight(float value)
        {
            return Math.Max(value, 0.01f);
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < appearanceProvider.DefaultBarCount;
        }

        public float ApplyExponentialScale(float value)
        {
            return value * value;
        }

        public float CalculateCenterPosition(float height)
        {
            return height / 2.0f;
        }
    }
}