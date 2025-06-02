using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using SkiaSharp.Views.Desktop;
using System;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.WaveVisualizer
{

    public class WaveVisualizationHost : UserControl
    {
        private readonly SKControl _skControl;
        private readonly System.Windows.Forms.Timer _animationTimer;
        private readonly IWaveRenderer _waveRenderer;
        private readonly IWaveBarStateManager _waveBarStateManager;
        private readonly IWaveColorManager _colorManager;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        public float _intensity { get; set; }


        // Remove once testing complete. 
        public float Intensity => _intensity;

        public WaveVisualizationHost(IWaveRenderer waveRenderer,IWaveBarStateManager barStateManager, IWaveColorManager colorManager, IStopWatchTimerService stopWatchTimerService)
        {
            _waveRenderer = waveRenderer;
            _waveBarStateManager = barStateManager;
            _colorManager = colorManager;
            _stopWatchTimerService = stopWatchTimerService;

            _skControl = new SKControl();   
            _animationTimer = new System.Windows.Forms.Timer();

            InitializeComponents();
            SetupAnimationTimer();
        }

        private void InitializeComponents()
        {
            _skControl.Dock = DockStyle.Fill;
            _skControl.PaintSurface += (s, e) =>
            {
                _waveRenderer.Render(
                 e.Surface.Canvas,
                 e.Info.Width,
                 e.Info.Height,
                 _waveBarStateManager.BarHeights,
                 _waveBarStateManager.NoiseValues,
                 _intensity);

            };
            Controls.Add(_skControl);
        }

        private void SetupAnimationTimer()
        {
            _animationTimer.Interval = 16;
            _animationTimer.Tick += (s, e) =>
            {
                _waveBarStateManager.UpdateBars(_intensity, _stopWatchTimerService.ReturnElapsedSeconds);
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
