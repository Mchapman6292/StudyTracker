using CodingTracker.View.ApplicationControlService;
using System;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveBarStateManager
    {
        float[] BarHeights { get; }
        float[] NoiseValues { get; }
        void UpdateBars(float intensity, Func<double> elapsedTimeProvider);
    }

    public class WaveBarStateManager : IWaveBarStateManager
    {
        private const int BarCount = 64;
        private readonly Random _random = new Random();

        private readonly float[] _barHeights;
        private readonly float[] _targetHeights;
        private readonly float[] _noiseValues;
        private readonly float[] _velocities;
        private readonly float[] _staticBase;

        private readonly IStopWatchTimerService _stopWatchTimerService;

        public float[] BarHeights => _barHeights;
        public float[] NoiseValues => _noiseValues;

        public WaveBarStateManager(IStopWatchTimerService stopWatchTimerService)
        {
            _stopWatchTimerService = stopWatchTimerService;
            _barHeights = new float[BarCount];
            _targetHeights = new float[BarCount];
            _noiseValues = new float[BarCount];
            _velocities = new float[BarCount];
            _staticBase = new float[BarCount];

            InitializeBars();
        }

        private void InitializeBars()
        {
            for (int i = 0; i < BarCount; i++)
            {
                _barHeights[i] = 0.01f;
                _targetHeights[i] = 0.01f;
                _noiseValues[i] = (float)_random.NextDouble();
                _velocities[i] = 0f;
                _staticBase[i] = 0.01f;
            }
        }

        public void UpdateBars(float intensity, Func<double> elapsedTimeProvider)
        {
            double time = elapsedTimeProvider();

            UpdateNoiseValues();

            for (int i = 0; i < BarCount; i++)
            {
                float targetHeight = 0f;

                // Always apply these base effects
                targetHeight += ApplyStaticBase(i, intensity);
                targetHeight += ApplyWavePattern(i, intensity, time);
                targetHeight += ApplyRandomSpikes(i, intensity);

                // Intensity-based effects
                targetHeight += ApplyFrequencyBias(i, targetHeight, intensity);
                targetHeight += ApplyElectricPulse(i, intensity, time);
                targetHeight += ApplyHeatDistortion(i, intensity, time);

                _targetHeights[i] = ClampHeight(targetHeight);
            }

            ApplyRapidSmoothing(intensity);
            ApplyVelocityDecay(intensity);
        }
        private void UpdateNoiseValues()
        {
            for (int i = 0; i < BarCount; i++)
            {
                float noiseChange = CalculateNoiseChange();
                _noiseValues[i] = WrapNoise(_noiseValues[i] + noiseChange);
            }
        }

        private float ApplyStaticBase(int index, float intensity)
        {
            // Ensure minimum activity even at zero intensity
            float minimumActivity = 0.15f;  // All bars always have at least 15% activity
            float baseLevel = minimumActivity + CalculateBaseLevel(intensity);
            float randomStatic = CalculateRandomStatic(intensity);

            // Add constant "breathing" effect
            float breathing = (float)Math.Sin(_stopWatchTimerService.ReturnElapsedSeconds() * 2 + index * 0.1) * 0.05f;

            _staticBase[index] = InterpolateStaticBase(_staticBase[index], baseLevel + randomStatic + breathing);
            return _staticBase[index];
        }


        // New method for "heat" effect that grows with streak
        private float ApplyHeatDistortion(int index, float intensity, double time)
        {
            if (intensity > 0.5f)
            {
                // Heat shimmer effect at high intensities
                float shimmer = (float)Math.Sin(time * 20 + index * 0.5f) * 0.02f;
                float heat = (float)_random.NextDouble() * 0.05f * (intensity - 0.5f);
                return shimmer + heat;
            }
            return 0f;
        }


        private float ApplyRandomSpikes(int index, float intensity)
        {
            // Constant small variations + intensity-based spikes
            float constantJitter = (float)(_random.NextDouble() * 0.05);  // Always present

            float spikeProbability = 0.05f + intensity * 0.2f;  // Much higher base probability

            if (ShouldGenerateSpike(spikeProbability))
            {
                float spikeStrength = 0.2f + intensity * 0.8f;  // Scales from small to large
                float randomHeight = GenerateRandomHeight();
                return constantJitter + (spikeStrength * randomHeight);
            }

            return constantJitter;
        }

        private float ApplyWavePattern(int index, float intensity, double time)
        {
            // Multiple overlapping waves for constant motion
            float waveSpeed = 1.0f + intensity * 4.0f;  // Slower base speed, scales more with intensity

            // Three wave patterns at different frequencies
            float wave1 = (float)Math.Sin(time * waveSpeed + index * 0.1f);
            float wave2 = (float)Math.Sin(time * waveSpeed * 0.7f + index * 0.2f);
            float wave3 = (float)Math.Sin(time * waveSpeed * 1.3f + index * 0.15f);

            // Combine waves with intensity scaling
            float waveAmplitude = 0.05f + intensity * 0.25f;  // Always some movement
            return (wave1 * 0.5f + wave2 * 0.3f + wave3 * 0.2f) * waveAmplitude;
        }
        private float ApplyFrequencyBias(int index, float currentHeight, float intensity)
        {
            float bias = CalculateFrequencyBias(index);
            return currentHeight * bias;
        }

        private float ApplyElectricPulse(int index, float intensity, double time)
        {
            if (ShouldGeneratePulse(intensity))
            {
                float pulseCenter = GeneratePulseCenter();
                float distance = CalculateDistanceFromPulse(index, pulseCenter);

                if (IsWithinPulseRange(distance))
                {
                    float falloff = CalculatePulseFalloff(distance);
                    return CalculatePulseHeight(falloff, intensity);
                }
            }

            return 0f;
        }

        private void ApplyRapidSmoothing(float intensity)
        {
            float smoothingSpeed = CalculateSmoothingSpeed(intensity);

            for (int i = 0; i < BarCount; i++)
            {
                float difference = CalculateHeightDifference(i);
                _velocities[i] = UpdateVelocity(_velocities[i], difference, smoothingSpeed);
                _barHeights[i] = UpdateBarHeight(_barHeights[i], _velocities[i]);
                _barHeights[i] = ClampHeight(_barHeights[i]);
            }
        }

        private void ApplyVelocityDecay(float intensity)
        {
            float decay = CalculateDecayRate(intensity);

            for (int i = 0; i < BarCount; i++)
            {
                if (IsBarFalling(i))
                {
                    _velocities[i] *= decay;
                }
            }
        }

        private float CalculateNoiseChange()
        {
            return (float)(_random.NextDouble() * 0.1 - 0.05);
        }

        private float WrapNoise(float noise)
        {
            if (noise < 0) return noise + 1;
            if (noise > 1) return noise - 1;
            return noise;
        }


        private float CalculateZoneActivity(int index, float intensity)
        {

            float position = index / (float)BarCount;
            float bassActivity = position < 0.2f ? 1.5f : 1.0f;
            float midActivity = (position > 0.3f && position < 0.6f) ? 1.3f : 1.0f;
            float trebleActivity = position > 0.7f ? 1.2f : 1.0f;

            return bassActivity * (0.5f + intensity * 0.5f);
        }

        private float CalculateBaseLevel(float intensity)
        {
            return 0.1f + intensity * 0.3f;
        }

        private float CalculateRandomStatic(float intensity)
        {
            return (float)(_random.NextDouble() * 0.02 - 0.01) * intensity;
        }

        private float InterpolateStaticBase(float current, float target)
        {
            return current * 0.9f + target * 0.05f;
        }

        private float CalculateSpikeProbability(float intensity)
        {
            return 0.01f + intensity * 0.02f;
        }

        private bool ShouldGenerateSpike(float probability)
        {
            return _random.NextDouble() < probability;
        }

        private float CalculateSpikeStrength(float intensity)
        {
            return 0.3f + intensity * 0.7f;
        }

        private float GenerateRandomHeight()
        {
            return (float)_random.NextDouble();
        }

        private float CalculateWaveSpeed(float intensity)
        {
            return 2.0f + intensity * 3.0f;
        }

        private float CalculateWaveAmplitude(float intensity)
        {
            return 0.1f * intensity;
        }

        private float CalculatePhase(int index)
        {
            return index * 0.15f;
        }

        private float CalculateWaveHeight(double time, float speed, float phase, float amplitude)
        {
            return (float)Math.Sin(time * speed + phase) * amplitude;
        }

        private float CalculateFrequencyBias(int index)
        {
            return 1.0f - (index / (float)BarCount) * 0.4f;
        }

        private bool ShouldGeneratePulse(float intensity)
        {
            return intensity > 0.5f && _random.NextDouble() < 0.002f;
        }

        private float GeneratePulseCenter()
        {
            return (float)_random.NextDouble() * BarCount;
        }

        private float CalculateDistanceFromPulse(int index, float pulseCenter)
        {
            return Math.Abs(index - pulseCenter);
        }

        private bool IsWithinPulseRange(float distance)
        {
            return distance < 5f;
        }

        private float CalculatePulseFalloff(float distance)
        {
            return 1.0f - (distance / 5f);
        }

        private float CalculatePulseHeight(float falloff, float intensity)
        {
            return falloff * intensity * 0.5f;
        }

        private float CalculateSmoothingSpeed(float intensity)
        {
            return 0.3f + intensity * 0.25f;
        }

        private float CalculateHeightDifference(int index)
        {
            return _targetHeights[index] - _barHeights[index];
        }

        private float UpdateVelocity(float currentVelocity, float difference, float smoothingSpeed)
        {
            return currentVelocity * 0.85f + difference * smoothingSpeed;
        }

        private float UpdateBarHeight(float currentHeight, float velocity)
        {
            return currentHeight + velocity;
        }

        private float ClampHeight(float height)
        {
            return Math.Max(0.01f, Math.Min(1.0f, height));
        }

        private float CalculateDecayRate(float intensity)
        {
            return 0.95f - intensity * 0.02f;
        }

        private bool IsBarFalling(int index)
        {
            return _targetHeights[index] < _barHeights[index];
        }
    }
}