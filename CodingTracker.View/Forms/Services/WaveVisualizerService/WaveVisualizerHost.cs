using CodingTracker.View.ApplicationControlService;
using SkiaSharp.Views.Desktop;
using System;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public interface IWaveVisualizationHost
    {
        Control GetControl();
        void Start();
        void Stop();
        void UpdateIntensity(float intensity);
        event EventHandler PaintRequested;
    }

    public class WaveVisualizationHost : UserControl, IWaveVisualizationHost
    {
        private readonly SKControl _skControl;
        private readonly System.Windows.Forms.Timer _animationTimer;
        private readonly IWaveRenderer _waveRenderer;
        private readonly IWaveBarStateManager _barStateManager;
        private readonly IWaveColorManager _colorManager;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private float _intensity;

        public event EventHandler PaintRequested;

        public WaveVisualizationHost(
            IWaveRenderer waveRenderer,
            IWaveBarStateManager barStateManager,
            IWaveColorManager colorManager,
            IStopWatchTimerService stopWatchTimerService)
        {
            _waveRenderer = waveRenderer;
            _barStateManager = barStateManager;
            _colorManager = colorManager;
            _stopWatchTimerService = stopWatchTimerService;

            _skControl = new SKControl();
            _animationTimer = new System.Windows.Forms.Timer();

            InitializeComponent();
            SetupAnimationTimer();
        }

        private void InitializeComponent()
        {
            _skControl.Dock = DockStyle.Fill;
            _skControl.PaintSurface += (s, e) =>
            {
                _waveRenderer.Render(
                 e.Surface.Canvas,
                 e.Info.Width,
                 e.Info.Height,
                 _barStateManager.BarHeights,
                 _barStateManager.NoiseValues,
                 _intensity);

                PaintRequested?.Invoke(s, e);
            };
            Controls.Add(_skControl);
        }

        private void SetupAnimationTimer()
        {
            _animationTimer.Interval = 16;
            _animationTimer.Tick += (s, e) =>
            {
                _barStateManager.UpdateBars(_intensity, _stopWatchTimerService.ReturnElapsedSeconds);
                _skControl.Invalidate();
            };
        }

        public void Start()
        {
            _animationTimer.Start();
        }

        public void Stop()
        {
            _animationTimer.Stop();
        }

        public Control GetControl()
        {
            return this;
        }

        public void UpdateIntensity(float intensity)
        {
            _intensity = intensity;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer?.Stop();
                _animationTimer?.Dispose();
                _skControl?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
