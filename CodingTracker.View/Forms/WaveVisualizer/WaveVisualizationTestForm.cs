using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using CodingTracker.View.Forms.WaveVisualizer;

namespace CodingTracker.View
{
    public partial class WaveVisualizationTestForm : Form
    {
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private WaveVisualizationControl waveControl;
        private KeyboardActivityTracker activityTracker;
        private System.Windows.Forms.Timer decayTimer;

        public WaveVisualizationTestForm(IStopWatchTimerService stopWatchTimerService)
        {
            InitializeComponent();
            _stopWatchTimerService = stopWatchTimerService;

            this.Size = new Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(32, 33, 36);
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;

            waveControl = new WaveVisualizationControl(_stopWatchTimerService);
            waveControl.Dock = DockStyle.Fill;
            this.Controls.Add(waveControl);

            activityTracker = new KeyboardActivityTracker(_stopWatchTimerService);
            activityTracker.ActivityChanged += ActivityTracker_ActivityChanged;

            decayTimer = new System.Windows.Forms.Timer();
            decayTimer.Interval = 50;
            decayTimer.Tick += DecayTimer_Tick;
            decayTimer.Start();

            SetFormPosition();

            this.MouseDown += WaveVisualizationTestForm_MouseDown;
            this.MouseMove += WaveVisualizationTestForm_MouseMove;
            this.MouseUp += WaveVisualizationTestForm_MouseUp;
        }

        private void ActivityTracker_ActivityChanged(object sender, float intensity)
        {
            waveControl.UpdateIntensity(intensity);
        }

        private void DecayTimer_Tick(object sender, EventArgs e)
        {
            activityTracker.UpdateDecay();
        }

        private void SetFormPosition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            this.Location = new Point(screenWidth - formWidth - 20, screenHeight - formHeight - 20);
        }

        private bool isDragging = false;
        private Point dragStartPoint;

        private void WaveVisualizationTestForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void WaveVisualizationTestForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentScreenPos = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(
                    currentScreenPos.X - dragStartPoint.X,
                    currentScreenPos.Y - dragStartPoint.Y);
            }
        }

        private void WaveVisualizationTestForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
    }
}