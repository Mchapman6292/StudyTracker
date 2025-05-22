using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;


namespace CodingTracker.View.Forms.Session
{
    public partial class ElapsedTimerPage : Form
    {

        private bool isPaused = false;

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IApplicationLogger _appLogger;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IButtonNotificationManager _buttonNotificationManager;
 
        private readonly IButtonHighlighterService _buttonHighlighterService;


        public ElapsedTimerPage(ICodingSessionManager codingSessionManager, IFormNavigator formNavigator, IApplicationLogger appLogger, IStopWatchTimerService stopWatchTimerService, IButtonNotificationManager buttonNotificationManager, IButtonHighlighterService buttonHighlighterService)
        {
            _codingSessionManager = codingSessionManager;
            _formNavigator = formNavigator;
            _appLogger = appLogger;
            _stopWatchTimerService = stopWatchTimerService;
            _buttonNotificationManager = buttonNotificationManager;
            _buttonHighlighterService = buttonHighlighterService;

            InitializeComponent();

            winFormsTimer.Tick += WinFormsTimer_Tick;

            _buttonHighlighterService.SetButtonBackColorAndBorderColor(elapsedTimerPauseButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(restartSessionButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(elapsedPageStopButton);
            
            _buttonHighlighterService.SetButtonHoverColors(elapsedTimerPauseButton);
            _buttonHighlighterService.SetButtonHoverColors(restartSessionButton);
            _buttonHighlighterService.SetButtonHoverColors(elapsedPageStopButton);
            

  
        }



        private void ElapsedTimerForm_Load(object sender, EventArgs e)
        {

            _codingSessionManager.InitializeCodingSessionAndSetGoal(0, false);
            _codingSessionManager.UpdateSessionStartTimeAndActiveBoolsToTrue();
            _stopWatchTimerService.RestartTimer();
            _stopWatchTimerService.StartTimer();
            SetFormPosition();
            winFormsTimer.Start();


        }

        private void WinFormsTimer_Tick(object? sender, EventArgs e) 
        {
            TimeSpan elapsedTime = _stopWatchTimerService.ReturnElapsedTimeSpan();

            string formattedTimeSpan = elapsedTime.ToString(@"hh\:mm\:ss");

            durationLabel.Text = formattedTimeSpan;

        }

        private void RestartSessionButton_Click(object sender, EventArgs e) 
        {
            _formNavigator.SwitchToForm(FormPageEnum.ElapsedTimerForm);
        }

        private void StopButton_Click(Object sender, EventArgs e) 
        {
            _buttonNotificationManager.HandleStopButtonRequest(this);
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            _buttonNotificationManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                _stopWatchTimerService.StartTimer();
                winFormsTimer.Start();
                elapsedTimerPauseButton.Text = "⏸";
                elapsedTimerPauseButton.TextOffset = new Point(2, 0);
                isPaused = false;
            }
            else
            {
                _stopWatchTimerService.StopTimer();
                winFormsTimer.Stop();
                elapsedTimerPauseButton.Text = "▶";
                elapsedTimerPauseButton.TextOffset = new Point(3, 0);
                isPaused = true;
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToFormWithoutPreviousFormClosing(FormPageEnum.MainPage);
            this.WindowState = FormWindowState.Minimized;
        }

        private void SetFormPosition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            this.Location = new Point(screenWidth - formWidth - 20, screenHeight - formHeight - 20);
        }
    }
}
