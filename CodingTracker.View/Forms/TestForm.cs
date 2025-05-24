using CodingTracker.View.Forms.Services.SharedFormServices;
namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private System.Windows.Forms.Timer progressTimer;
        private DateTime startTime;
        private bool isPaused = false;
        private TimeSpan pausedElapsedTime;
        private double timerDurationSeconds = 60;
        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;

        public TestForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager)
        {
            InitializeComponent();
            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;

            this.Load += TestForm_Load;
            pauseButton.Click += PauseButton_Click;
            testButton.Click += TestButton_Click;
            testButton.MouseEnter += TestButton_MouseEnter;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            _buttonHighligherService.SetButtonHoverColors(testButton);
            _buttonHighligherService.SetButtonBackColorAndBorderColor(testButton);
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            testButton.Checked = !testButton.Checked;

            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                _notificationManager.ShowNotificationDialog(this, "Real click detected.");
            }
        }

        private void TestButton_MouseEnter(object sender, EventArgs e)  // Added this method
        {
            testButton.PerformClick();
        }



        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                pausedElapsedTime = DateTime.Now - startTime;
                progressTimer.Stop();
                isPaused = true;
                pauseButton.Text = "▶";
            }
            else
            {
                startTime = DateTime.Now - pausedElapsedTime;
                progressTimer.Start();
                isPaused = false;
                pauseButton.Text = "⏸";
            }
        }

 
    }
}