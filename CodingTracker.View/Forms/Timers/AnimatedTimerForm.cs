using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.Utilities;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.SharedFormServices;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;


namespace CodingTracker.View.Forms
{
    public partial class AnimatedTimerForm : Form
    {

        private bool isPaused = false;
        private int progressValue = 0;
        private double? progressTimerGoalSecondsDouble;

        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;

        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedTimerManager _animatedTimerManager;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IFormFactory _formFactory;
        private readonly IFormNavigator _formNavigator;
        private readonly IExitFlowManager _exitFlowManager;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IButtonHighlighterService _buttonHighLighterService;
        private readonly IAnimatedColumnStateManager _animatedColumnStateManager;


        public AnimatedTimerColumn column;

        private System.Windows.Forms.Timer animationTimer;
        private AnimatedTimerColumn hoursTens;
        private AnimatedTimerColumn hoursOnes;
        private AnimatedTimerColumn minutesTens;
        private AnimatedTimerColumn minutesOnes;
        private AnimatedTimerColumn secondsTens;
        private AnimatedTimerColumn secondsOnes;
        private DateTime lastTime;



        private TimerPlaceHolderForm _timerPlaceHolderForm;


        public AnimatedTimerForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, ICodingSessionRepository codingSessionRepository, ILabelAssignment labelAssignment, IApplicationLogger appLogger, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IAnimatedTimerManager animatedTimerManager, IStopWatchTimerService stopWatchTimerService, IFormStateManagement formStateManagement, IFormFactory formFactory, IFormNavigator formNavigator, IExitFlowManager buttonNotificationManager, ICodingSessionManager codingSessionManager, IButtonHighlighterService buttonHighLighterService, IAnimatedColumnStateManager animatedColumnStateManager, IExitFlowManager exitFlowManager)
        {

            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;
            _appLogger = appLogger;
            _animatedTimerManager = animatedTimerManager;
            _stopWatchTimerService = stopWatchTimerService;
            _formStateManagement = formStateManagement;
            _formFactory = formFactory;
            _formNavigator = formNavigator;
            _exitFlowManager = buttonNotificationManager;
            _codingSessionManager = codingSessionManager;
            _buttonHighLighterService = buttonHighLighterService;
            _animatedColumnStateManager = animatedColumnStateManager;


            _timerPlaceHolderForm = (TimerPlaceHolderForm)_formFactory.GetOrCreateForm(FormPageEnum.TimerPlaceHolderForm); ;
            _formStateManagement.SetFormByFormPageEnum(FormPageEnum.TimerPlaceHolderForm, _timerPlaceHolderForm);

            InitializeComponent();

            SetFormSizeAndLocationFromPlaceHolder();
            SetSKControlLocationAndSize();

            _animatedTimerManager.InitializeColumns(this, _timerPlaceHolderForm);


            animatedTimerSKControl.PaintSurface += _animatedTimerManager.DrawColumnsOnTick;
            pauseButton.Click += PauseButton_Click;
            stopButton.Click += StopButton_Click;
            restartButton.Click += RestartSessionButton_Click;

            this.Shown += AnimatedTimerForm_Shown;
            this.Load += AnimatedTimerForm_Load;

            elapsedTestToggleSwitch.CheckedChanged += ElapsedTestToggleSwitch_Checked;


            InitializeAnimationTimer();






        }


        private void ElapsedTestToggleSwitch_Checked(Object sender, EventArgs e)
        {
            _animatedTimerManager.UpdateTimerTestModeEnabled(elapsedTestToggleSwitch.Checked);

            _animatedTimerManager.UpdateTestModeElapsed(new TimeSpan(00,40,00));


        }








        public void SetSKControlLocationAndSize()
        {
            Point placeHolderLocation;
            Size placeHolderSize;

            _timerPlaceHolderForm.GetSkControlSizeAndLocationFromPlaceholder(out placeHolderLocation, out placeHolderSize);


            animatedTimerSKControl.Location = placeHolderLocation;
            animatedTimerSKControl.Size = placeHolderSize;

        }

        public void SetFormSizeAndLocationFromPlaceHolder()
        {
            this.Location = _timerPlaceHolderForm.Location;
            this.Size = _timerPlaceHolderForm.Size;
        }


        private async void AnimatedTimerForm_Load(object sender, EventArgs e)
        {
            _buttonHighLighterService.SetButtonHoverColors(pauseButton);
            _buttonHighLighterService.SetButtonBackColorAndBorderColor(pauseButton);

            _buttonHighligherService.SetButtonHoverColors(stopButton);
            _buttonHighligherService.SetButtonBackColorAndBorderColor(stopButton);

            _buttonHighligherService.SetButtonHoverColors(restartButton);
            _buttonHighligherService.SetButtonBackColorAndBorderColor(restartButton);


        }

        // TODO review, what happens if we close re open form etc. 
        private async void AnimatedTimerForm_Shown(object sender, EventArgs e)
        {
            bool currentCodingSessionActive = _codingSessionManager.IsCurrentCodingSessionNull();

            if(!currentCodingSessionActive)
            {
                _codingSessionManager.InitializeCodingSessionAndSetGoal(0, false);
                _codingSessionManager.UpdateSessionStartTimeAndActiveBoolsToTrue();
            }

            _stopWatchTimerService.StartSessionTimer();
        }

        public string FormatElapsedTimeSPan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }



        public void UpdateTimeDisplayLAbel()
        {
            bool testModeEnabled = _animatedTimerManager.IsTimerTestModeEnabled();

            TimeSpan elapsed;
            string elapsedString = string.Empty;
            if (testModeEnabled)
            {
                elapsed = _animatedTimerManager.ReturnTestModeElapsed();
                elapsedString += LoggerHelper.FormatAllElapsedTimeSpan(elapsed);       
            }
            else
            {
                elapsed = _stopWatchTimerService.ReturnElapsedTimeSpan();
                elapsedString = LoggerHelper.FormatAllElapsedTimeSpan(elapsed);
            }
            timeDisplayLabel.Text = elapsedString;
        }




        private void InitializeAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16; // 16ms = 60fps.
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            _animatedTimerManager.UpdateAndRender(animatedTimerSKControl);
            UpdateTimeDisplayLAbel();



        }




        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                _stopWatchTimerService.StartSessionTimer();
                animationTimer.Start();

                pauseButton.Text = "⏸";
                pauseButton.TextOffset = new Point(2, 0);
                isPaused = false;
            }
            else
            {
                _stopWatchTimerService.StopTimer();
                animationTimer.Stop();
                pauseButton.Text = "▶";
                pauseButton.TextOffset = new Point(3, 0);
                isPaused = true;
            }
        }


        private void homeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToFormWithoutPreviousFormClosing(FormPageEnum.OldMainPage);
            this.WindowState = FormWindowState.Minimized;
        }




        private void StopButton_Click(object sender, EventArgs e)
        {
            TimeSpan duration = _stopWatchTimerService.ReturnElapsedTimeSpan();
            _exitFlowManager.HandleStopButtonRequest(this);

            _codingSessionManager.UpdateCodingSessionTimerEnded(duration);


        }



        private void RestartSessionButton_Click(object sender, EventArgs e)
        {
            TimeSpan elapsed = _stopWatchTimerService.ReturnElapsedTimeSpan();

            _appLogger.Debug($"Timer restarted at: {FormatElapsedTimeSPan(elapsed)}.");

            _stopWatchTimerService.StopTimer();



            TimeSpan elapsedAfterRestart = _stopWatchTimerService.ReturnElapsedTimeSpan();

            _appLogger.Debug($"elapsed after restart: {FormatElapsedTimeSPan(elapsedAfterRestart)}.");


        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            List<AnimatedTimerColumn> columnsList = _animatedTimerManager.ReturnTimerColumns();

            _stopWatchTimerService.StopTimer();
            _exitFlowManager.HandleRestartSessionRequest(this);



        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToFormWithoutPreviousFormClosing(FormPageEnum.OldMainPage);
            this.WindowState = FormWindowState.Minimized;
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void newPauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                _stopWatchTimerService.StartSessionTimer();
                animationTimer.Start();

                pauseButton.Text = "⏸";
                pauseButton.TextOffset = new Point(2, 0);
                isPaused = false;
            }
            else
            {
                _stopWatchTimerService.StopTimer();
                animationTimer.Stop();
                pauseButton.Text = "▶";
                pauseButton.TextOffset = new Point(3, 0);
                isPaused = true;
            }
        }

        private void newRestartButton_Click(object sender, EventArgs e)
        {
            List<AnimatedTimerColumn> columnsList = _animatedTimerManager.ReturnTimerColumns();

            _stopWatchTimerService.StopTimer();
            _exitFlowManager.HandleRestartSessionRequest(this);
        }

        private void newStopButton_Click(object sender, EventArgs e)
        {
            _stopWatchTimerService.StopTimer();
            _exitFlowManager.HandleStopButtonRequest(this);


        }




        /*
        public void TestElapsedBox_TextChange(object sender, EventArgs e)
        {
            if(TimeSpan.TryParseExact(testElapsedBox.Text, "hh\\:mm\\:ss", out TimeSpan elapsed))
            {
                testElapsedBox.Text = elapsed.ToString();
            }
        }
        */


    }
}