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

        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void TestButton_Click(object sender, EventArgs e)
        {

        }

        private void TestButton_MouseEnter(object sender, EventArgs e)  // Added this method
        {
        }



        private void PauseButton_Click(object sender, EventArgs e)
        {

        }

 
    }
}