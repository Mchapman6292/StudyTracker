using SkiaSharp;
using System;
using System.Collections.Generic;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveAppearanceProvider
    {
        SKColor BackgroundColor { get; }
        int DefaultBarCount { get; }
        float BaseWaveHeight { get; }
        float WaveSpacing { get; }
        float StaticSpeed { get; }

        SKColor GetColorAtPosition(SKColor[] colorArray, float position);
        SKColor BlendColors(SKColor color1, SKColor color2, float blendAmount);
        float GetIntensityBlend(ActivityIntensity intensity);
        GlowSettings GetGlowSettings(ActivityIntensity intensity);
        SKColor[] GetLowActivityColors();
        SKColor[] GetHighActivityColors();
    }

    public struct GlowSettings
    {
        public int MinAlpha { get; }
        public int MaxAlpha { get; }
        public float BlurRadius { get; }

        public GlowSettings(int minAlpha, int maxAlpha, float blurRadius)
        {
            MinAlpha = minAlpha;
            MaxAlpha = maxAlpha;
            BlurRadius = blurRadius;
        }
    }

    public class WaveAppearanceProvider : IWaveAppearanceProvider
    {
        public SKColor BackgroundColor => new SKColor(32, 33, 36);
        public int DefaultBarCount => 64;
        public float BaseWaveHeight => 0.3f;
        public float WaveSpacing => 0.2f;
        public float StaticSpeed => 0.05f;

        private readonly SKColor[] lowActivityColors = new SKColor[]
        {
            new SKColor(60, 100, 255),
            new SKColor(60, 180, 230),
            new SKColor(60, 200, 120),
            new SKColor(180, 200, 60),
            new SKColor(230, 120, 60),
            new SKColor(255, 60, 120),
            new SKColor(200, 60, 200)
        };

        private readonly SKColor[] highActivityColors = new SKColor[]
        {
            new SKColor(100, 180, 255),
            new SKColor(100, 255, 255),
            new SKColor(100, 255, 150),
            new SKColor(255, 255, 100),
            new SKColor(255, 180, 100),
            new SKColor(255, 100, 180),
            new SKColor(255, 100, 255)
        };

        private readonly Dictionary<ActivityIntensity, GlowSettings> glowConfiguration = new Dictionary<ActivityIntensity, GlowSettings>
        {
            { ActivityIntensity.Dormant, new GlowSettings(0, 40, 1.0f) },
            { ActivityIntensity.Light, new GlowSettings(30, 80, 2.0f) },
            { ActivityIntensity.Moderate, new GlowSettings(60, 120, 3.5f) },
            { ActivityIntensity.High, new GlowSettings(90, 160, 5.0f) },
            { ActivityIntensity.Peak, new GlowSettings(120, 200, 7.0f) }
        };

        public SKColor[] GetLowActivityColors()
        {
            return lowActivityColors;
        }

        public SKColor[] GetHighActivityColors()
        {
            return highActivityColors;
        }

        public SKColor GetColorAtPosition(SKColor[] colorArray, float position)
        {
            if (colorArray.Length == 0) return SKColors.White;
            if (colorArray.Length == 1) return colorArray[0];

            position = ClampPosition(position);

            float scaledPosition = CalculateScaledPosition(position, colorArray.Length);
            int colorIndex = CalculateColorIndex(scaledPosition);
            float colorBlend = CalculateColorBlend(scaledPosition, colorIndex);

            if (IsLastColorIndex(colorIndex, colorArray.Length))
                return colorArray[colorArray.Length - 1];

            return BlendColors(colorArray[colorIndex], colorArray[colorIndex + 1], colorBlend);
        }

        public SKColor BlendColors(SKColor color1, SKColor color2, float blendAmount)
        {
            blendAmount = ClampPosition(blendAmount);

            byte r = CalculateBlendedColorComponent(color1.Red, color2.Red, blendAmount);
            byte g = CalculateBlendedColorComponent(color1.Green, color2.Green, blendAmount);
            byte b = CalculateBlendedColorComponent(color1.Blue, color2.Blue, blendAmount);
            byte a = CalculateBlendedColorComponent(color1.Alpha, color2.Alpha, blendAmount);

            return new SKColor(r, g, b, a);
        }

        public float GetIntensityBlend(ActivityIntensity intensity)
        {
            return intensity switch
            {
                ActivityIntensity.Dormant => 0.0f,
                ActivityIntensity.Light => 0.2f,
                ActivityIntensity.Moderate => 0.5f,
                ActivityIntensity.High => 0.8f,
                ActivityIntensity.Peak => 1.0f,
                _ => 0.0f
            };
        }

        public GlowSettings GetGlowSettings(ActivityIntensity intensity)
        {
            return glowConfiguration[intensity];
        }

        private float ClampPosition(float position)
        {
            return Math.Max(0.0f, Math.Min(1.0f, position));
        }

        private float CalculateScaledPosition(float position, int arrayLength)
        {
            return position * (arrayLength - 1);
        }

        private int CalculateColorIndex(float scaledPosition)
        {
            return (int)scaledPosition;
        }

        private float CalculateColorBlend(float scaledPosition, int colorIndex)
        {
            return scaledPosition - colorIndex;
        }

        private bool IsLastColorIndex(int colorIndex, int arrayLength)
        {
            return colorIndex >= arrayLength - 1;
        }

        private byte CalculateBlendedColorComponent(byte component1, byte component2, float blendAmount)
        {
            return (byte)(component1 + (component2 - component1) * blendAmount);
        }
    }
}