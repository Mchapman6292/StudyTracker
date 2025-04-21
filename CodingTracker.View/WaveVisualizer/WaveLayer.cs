using SkiaSharp;

namespace CodingTracker.View.WaveVisualizer.WaveLayers
{
    public class WaveLayer
    {
        public float Frequency { get; set; }
        public float Phase { get; set; }
        public float BaseAmplitude { get; set; }
        public float CurrentAmplitude { get; set; }
        public SKColor Color { get; set; }
        public float Speed { get; set; }
    }
}
