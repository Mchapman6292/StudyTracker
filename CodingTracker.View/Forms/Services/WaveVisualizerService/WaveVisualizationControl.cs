using CodingTracker.View.Forms.WaveVisualizer;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.WaveVisualizerService
{
    public class WaveVisualizationControl : UserControl, IWaveVisualizationControl
    {
        private SKControl skControl;
        private System.Windows.Forms.Timer animationTimer;

        private readonly WaveAnimationEngine animationEngine;
        private readonly WaveDrawingService renderingEngine;

        public WaveVisualizationControl()
        {
            animationEngine = new WaveAnimationEngine();
            renderingEngine = new WaveDrawingService();

            InitializeComponent();
            SetupAnimationTimer();
        }

        public void UpdateIntensity(float keyboardIntensity)
        {
            animationEngine.UpdateFromKeyboardActivity(keyboardIntensity);
        }

        public void UpdateFromSessionData(double sessionSeconds)
        {
            animationEngine.UpdateFromSessionData(sessionSeconds);
        }

        public void StartAnimation()
        {
            animationTimer.Start();
        }

        public void StopAnimation()
        {
            animationTimer.Stop();
        }

        private void InitializeComponent()
        {
            skControl = new SKControl();
            skControl.Dock = DockStyle.Fill;
            skControl.PaintSurface += SkControl_PaintSurface;
            this.Controls.Add(skControl);
        }

        private void SetupAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += OnAnimationTick;
            animationTimer.Start();
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            animationEngine.UpdateAnimation();
            skControl.Invalidate();
        }

        private void SkControl_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            renderingEngine.RenderWaveVisualization(
                e.Surface.Canvas,
                e.Info.Width,
                e.Info.Height,
                animationEngine
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Stop();
                animationTimer?.Dispose();
                skControl?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}