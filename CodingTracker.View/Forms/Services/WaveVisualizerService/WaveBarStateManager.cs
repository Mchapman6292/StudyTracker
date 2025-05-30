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
        private readonly int _barCount;
        private readonly Random _random = new Random();

        private readonly float[] _barHeights;
        private readonly float[] _targetBarHeights;
        private readonly float[] _noiseValues;

        public float[] BarHeights => _barHeights;
        public float[] NoiseValues => _noiseValues;

        public WaveBarStateManager(int barCount)
        {
            _barCount = barCount;
            _barHeights = new float[_barCount];
            _targetBarHeights = new float[_barCount];
            _noiseValues = new float[_barCount];

            InitializeBars();
        }

        private void InitializeBars()
        {
            for (int i = 0; i < _barCount; i++)
            {
                _barHeights[i] = 0.01f + (float)(_random.NextDouble() * 0.01);
                _targetBarHeights[i] = _barHeights[i];
                _noiseValues[i] = (float)_random.NextDouble();
            }
        }

        public void UpdateBars(float intensity, Func<double> elapsedTimeProvider)
        {
            UpdateNoiseValues();
            UpdateTargetHeights(intensity, elapsedTimeProvider);
            SmoothBarsTowardTarget(intensity);
        }

        private void UpdateNoiseValues()
        {
            for (int i = 0; i < _barCount; i++)
            {
                _noiseValues[i] += (float)(_random.NextDouble() * 0.1 - 0.05);
                if (_noiseValues[i] < 0) _noiseValues[i] += 1;
                if (_noiseValues[i] > 1) _noiseValues[i] -= 1;
            }
        }

        private void UpdateTargetHeights(float intensity, Func<double> elapsedTimeProvider)
        {
            double time = elapsedTimeProvider();

            for (int i = 0; i < _barCount; i++)
            {
                float staticEffect = (float)(_random.NextDouble() * 0.02 - 0.01);
                float noiseEffect = (float)(Math.Sin(_noiseValues[i] * Math.PI * 6) * 0.01);
                float spike = _random.NextDouble() < 0.01 + intensity * 0.03
                    ? (float)(_random.NextDouble() * 0.03 * (0.2f + intensity * 0.8f))
                    : 0;

                float patternFactor = (float)Math.Sin(time * (1.0 + intensity * 2.0) + i * 0.2) * 0.03f * (0.1f + intensity * 0.9f);
                float intensityMovement = (float)(_random.NextDouble() * 0.1 - 0.05) * (intensity * intensity);

                float heightChange = staticEffect + noiseEffect + spike + patternFactor + intensityMovement;
                float minHeight = 0.005f + intensity * 0.05f;

                _targetBarHeights[i] = Math.Max(minHeight, Math.Min(1.0f, _targetBarHeights[i] + heightChange));
            }
        }

        private void SmoothBarsTowardTarget(float intensity)
        {
            for (int i = 0; i < _barCount; i++)
            {
                float smoothingFactor = 0.15f + intensity * 0.5f;
                _barHeights[i] += (_targetBarHeights[i] - _barHeights[i]) * smoothingFactor;

                float staticAmount = 0.01f * (1f - intensity * 0.7f);
                if (intensity < 0.5f)
                {
                    _barHeights[i] += (float)(_random.NextDouble() * staticAmount - staticAmount / 2);
                }
            }
        }
    }
}
