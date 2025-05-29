using SkiaSharp;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveColorManager
    {
        SKColor GetInterpolatedColor(int barIndex, float intensity);
        float ApplyQuadraticIntensityScale(float value);
        float ApplyGlowMultiplierScale(float value);
        SKColor Blend(SKColor color1, SKColor color2, float amount);
    }

    public class WaveColorManager : IWaveColorManager
    {
        private readonly SKColor[] spectrumColors = new SKColor[] {
            new SKColor(60, 100, 255), new SKColor(60, 180, 230), new SKColor(60, 200, 120),
            new SKColor(180, 200, 60), new SKColor(230, 120, 60), new SKColor(255, 60, 120),
            new SKColor(200, 60, 200)
        };

        private readonly SKColor[] highIntensityColors = new SKColor[] {
            new SKColor(100, 180, 255), new SKColor(100, 255, 255), new SKColor(100, 255, 150),
            new SKColor(255, 255, 100), new SKColor(255, 180, 100), new SKColor(255, 100, 180),
            new SKColor(255, 100, 255)
        };

        public float ApplyQuadraticIntensityScale(float value)
        {
            return value * value;
        }

        public float ApplyGlowMultiplierScale(float value)
        {
            return 0.1f + ApplyQuadraticIntensityScale(value) * 0.9f;
        }

        public SKColor GetInterpolatedColor(int barIndex, float intensity)
        {
            float scaledIndex = (float)barIndex / 64 * (spectrumColors.Length - 1);
            int index = (int)scaledIndex;
            float blend = scaledIndex - index;

            SKColor low = spectrumColors[Math.Min(index, spectrumColors.Length - 1)];
            SKColor high = highIntensityColors[Math.Min(index, highIntensityColors.Length - 1)];

            SKColor blendedLow = index < spectrumColors.Length - 1 ? Blend(low, spectrumColors[index + 1], blend) : low;
            SKColor blendedHigh = index < highIntensityColors.Length - 1 ? Blend(high, highIntensityColors[index + 1], blend) : high;

            return Blend(blendedLow, blendedHigh, ApplyQuadraticIntensityScale(intensity));
        }

        public SKColor Blend(SKColor color1, SKColor color2, float amount)
        {
            byte r = (byte)(color1.Red + (color2.Red - color1.Red) * amount);
            byte g = (byte)(color1.Green + (color2.Green - color1.Green) * amount);
            byte b = (byte)(color1.Blue + (color2.Blue - color1.Blue) * amount);
            byte a = (byte)(color1.Alpha + (color2.Alpha - color1.Alpha) * amount);
            return new SKColor(r, g, b, a);
        }
    }
}
