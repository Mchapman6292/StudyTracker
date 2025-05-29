using SkiaSharp;
using SkiaSharp.Views.Desktop;
using CodingTracker.View.Forms.Services.WaveVisualizerService;

namespace CodingTracker.View.Forms.WaveVisualizer.WaveVisualizationControls
{
    public interface IWaveVisualizationControl
    {
        void UpdateIntensity(float intensity);
        void UpdateFromSessionData(double sessionSeconds);
        void StartAnimation();
        void StopAnimation();
    }

    public class WaveVisualizationControl : SKControl, IWaveVisualizationControl
    {
        private readonly IWaveAnimator waveAnimator;
        private readonly IWaveIllustrator waveIllustrator;
        private System.Windows.Forms.Timer animationTimer;
        private bool isAnimating = false;

        public WaveVisualizationControl(IWaveAnimator animator, IWaveIllustrator illustrator)
        {
            this.waveAnimator = animator;
            this.waveIllustrator = illustrator;

            SetupAnimationTimer();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.DoubleBuffer |
                         ControlStyles.ResizeRedraw, true);
        }

        private void SetupAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (isAnimating)
            {
                waveAnimator.UpdateAnimation();
                this.Invalidate();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            var width = e.Info.Width;
            var height = e.Info.Height;

            waveIllustrator.RenderWaveVisualization(canvas, width, height, waveAnimator);
        }

        public void UpdateIntensity(float intensity)
        {
            waveAnimator.UpdateFromKeyboardActivity(intensity);
            this.Invalidate();
        }

        public void UpdateFromSessionData(double sessionSeconds)
        {
            waveAnimator.UpdateFromSessionData(sessionSeconds);
            this.Invalidate();
        }

        public void StartAnimation()
        {
            isAnimating = true;
            animationTimer.Start();
            waveAnimator.UpdateAnimation();
            this.Invalidate();
        }

        public void StopAnimation()
        {
            isAnimating = false;
            animationTimer.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}