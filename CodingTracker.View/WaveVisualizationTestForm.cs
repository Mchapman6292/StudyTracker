using CodingTracker.View.TimerDisplayService;
using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using CodingTracker.View.TimerDisplayService.WaveVisualizationControls;

namespace CodingTracker.View
{
    // Dispose method in designer has been modified to dispose of intensityTime & WaveControl.
    public partial class WaveVisualizationTestForm : Form
    {
        private WaveVisualizationControl waveControl;
        private System.Windows.Forms.Timer intensityTimer;
        private float currentIntensity = 0.0f;
        private int direction = 1;
        private readonly IStopWatchTimerService _stopWatchTimerService;

        public WaveVisualizationTestForm(IStopWatchTimerService stopWatchTimerService)
        {
            _stopWatchTimerService = stopWatchTimerService;
            InitializeComponent();

            this.Size = new System.Drawing.Size(800, 400);
            this.Text = "Wave Visualization Test";

            waveControl = new WaveVisualizationControl(_stopWatchTimerService);
            waveControl.Dock = DockStyle.Fill;
            this.Controls.Add(waveControl);

            intensityTimer = new System.Windows.Forms.Timer();
            intensityTimer.Interval = 16;
            intensityTimer.Tick += IntensityTimer_Tick;
            intensityTimer.Start();
        }

        private void IntensityTimer_Tick(object sender, EventArgs e)
        {
            currentIntensity += 0.02f * direction;

            if (currentIntensity >= 1.0f)
            {
                currentIntensity = 1.0f;
                direction = -1;
            }
            else if (currentIntensity <= 0.0f)
            {
                currentIntensity = 0.0f;
                direction = 1;
            }

            waveControl.UpdateIntensity(currentIntensity);
            this.Text = $"Wave Visualization Test - Intensity: {currentIntensity:F2}";
        }


    }
}