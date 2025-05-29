using CodingTracker.View.Forms.Services.WaveVisualizerService;
using CodingTracker.View.Forms.WaveVisualizer.WaveVisualizationControls;

namespace CodingTracker.View
{
    public partial class WaveVisualizationForm : Form
    {
        private readonly IWaveVisualizationControl _waveVisualizationControl;
        private System.Windows.Forms.Timer testTimer;
        private float currentTestIntensity = 0.0f;
        private bool intensityIncreasing = true;

        public WaveVisualizationForm(IWaveVisualizationControl waveControl)
        {
            this._waveVisualizationControl = waveControl;
            InitializeComponent();
            SetupTestForm();
            SetupTestTimer();
        }

        private void SetupTestForm()
        {
            this.Text = "Wave Visualization Test";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            var visualizationControl = (WaveVisualizationControl)_waveVisualizationControl;
            visualizationControl.Dock = DockStyle.Fill;
            this.Controls.Add(visualizationControl);

            this.KeyPreview = true;
            this.KeyDown += TestForm_KeyDown;

            _waveVisualizationControl.StartAnimation();
        }

        private void SetupTestTimer()
        {
            testTimer = new System.Windows.Forms.Timer();
            testTimer.Interval = CalculateTestTimerInterval();
            testTimer.Tick += TestTimer_Tick;
            testTimer.Start();
        }

        private void TestTimer_Tick(object sender, EventArgs e)
        {
            UpdateTestIntensity();
            _waveVisualizationControl.UpdateIntensity(currentTestIntensity);
        }

        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    SimulateActivityLevel(ActivityIntensity.Dormant);
                    break;
                case Keys.D2:
                    SimulateActivityLevel(ActivityIntensity.Light);
                    break;
                case Keys.D3:
                    SimulateActivityLevel(ActivityIntensity.Moderate);
                    break;
                case Keys.D4:
                    SimulateActivityLevel(ActivityIntensity.High);
                    break;
                case Keys.D5:
                    SimulateActivityLevel(ActivityIntensity.Peak);
                    break;
                case Keys.Space:
                    ToggleTestTimer();
                    break;
            }
        }

        private void UpdateTestIntensity()
        {
            if (intensityIncreasing)
            {
                currentTestIntensity = IncreaseIntensity(currentTestIntensity);
                if (IsMaxIntensity(currentTestIntensity))
                {
                    intensityIncreasing = false;
                }
            }
            else
            {
                currentTestIntensity = DecreaseIntensity(currentTestIntensity);
                if (IsMinIntensity(currentTestIntensity))
                {
                    intensityIncreasing = true;
                }
            }
        }

        private void SimulateActivityLevel(ActivityIntensity intensity)
        {
            double sessionSeconds = CalculateSessionSecondsForIntensity(intensity);
            _waveVisualizationControl.UpdateFromSessionData(sessionSeconds);
        }

        private void ToggleTestTimer()
        {
            if (testTimer.Enabled)
            {
                testTimer.Stop();
                _waveVisualizationControl.StopAnimation();
            }
            else
            {
                testTimer.Start();
                _waveVisualizationControl.StartAnimation();
            }
        }

        private int CalculateTestTimerInterval()
        {
            return 100;
        }

        private float IncreaseIntensity(float currentIntensity)
        {
            return Math.Min(1.0f, currentIntensity + 0.02f);
        }

        private float DecreaseIntensity(float currentIntensity)
        {
            return Math.Max(0.0f, currentIntensity - 0.02f);
        }

        private bool IsMaxIntensity(float intensity)
        {
            return intensity >= 1.0f;
        }

        private bool IsMinIntensity(float intensity)
        {
            return intensity <= 0.0f;
        }

        private double CalculateSessionSecondsForIntensity(ActivityIntensity intensity)
        {
            return intensity switch
            {
                ActivityIntensity.Dormant => 0,
                ActivityIntensity.Light => 900,
                ActivityIntensity.Moderate => 2700,
                ActivityIntensity.High => 4500,
                ActivityIntensity.Peak => 7200,
                _ => 0
            };
        }
    }
}