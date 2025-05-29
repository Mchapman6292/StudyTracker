using System;
using System.Collections.Generic;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IActivityIntensityProvider
    {
        ActivityIntensity GetIntensityFromDuration(double sessionSeconds);
        IntensitySettings GetSettings(ActivityIntensity intensity);
    }

    public class ActivityIntensityProvider : IActivityIntensityProvider
    {
        public const int LightActivityThreshold = 1800;
        public const int ModerateActivityThreshold = 3600;
        public const int HighActivityThreshold = 5400;

        private static readonly Dictionary<ActivityIntensity, IntensitySettings> Settings =
            new Dictionary<ActivityIntensity, IntensitySettings>
            {
            { ActivityIntensity.Dormant, new IntensitySettings(0.1f, 0.3f, 0.1f, 0.8f) },
            { ActivityIntensity.Light, new IntensitySettings(0.3f, 0.6f, 0.3f, 0.7f) },
            { ActivityIntensity.Moderate, new IntensitySettings(0.6f, 1.0f, 0.6f, 0.6f) },
            { ActivityIntensity.High, new IntensitySettings(0.8f, 1.4f, 0.9f, 0.5f) },
            { ActivityIntensity.Peak, new IntensitySettings(1.0f, 1.8f, 1.2f, 0.4f) }
            };

        public ActivityIntensity GetIntensityFromDuration(double sessionSeconds)
        {
            if (sessionSeconds <= 0)
                return ActivityIntensity.Dormant;
            else if (sessionSeconds <= LightActivityThreshold)
                return ActivityIntensity.Light;
            else if (sessionSeconds <= ModerateActivityThreshold)
                return ActivityIntensity.Moderate;
            else if (sessionSeconds <= HighActivityThreshold)
                return ActivityIntensity.High;
            else
                return ActivityIntensity.Peak;
        }

        public IntensitySettings GetSettings(ActivityIntensity intensity)
        {
            return Settings[intensity];
        }
    }

    public enum ActivityIntensity
    {
        Dormant,
        Light,
        Moderate,
        High,
        Peak
    }

    public struct IntensitySettings
    {
        public float AmplitudeMultiplier { get; }
        public float FrequencyMultiplier { get; }
        public float NoiseMultiplier { get; }
        public float SmoothingFactor { get; }

        public IntensitySettings(float amplitude, float frequency, float noise, float smoothing)
        {
            AmplitudeMultiplier = amplitude;
            FrequencyMultiplier = frequency;
            NoiseMultiplier = noise;
            SmoothingFactor = smoothing;
        }
    }
}