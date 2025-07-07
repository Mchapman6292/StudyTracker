using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;


namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;
        private readonly IDurationPanelFactory _durationPanelFactory;
        private readonly IDurationParentPanelFactory _durationParentPanelFactory;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ISessionVisualizationController _sessionVisualizationController;
        private readonly IPanelColourAssigner _panelColorAssigner;
        private readonly ILast28DayPanelSettings _last28DayPanelSettings;
        private readonly ILabelAssignment _labelAssignment;
        private readonly IDurationParentPanelPositionManager _durationPanelPositionManager;
        private readonly IAnimatedTimerRenderer _animatedTimerRenderer;
        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedTimerColumnFactory _animatedTimerColumnFactory;
        private readonly IAnimatedTimerManager _animatedTimerManager;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IAnimatedColumnStateManager _columnStateManager;

        public AnimatedTimerColumn column;

        private System.Windows.Forms.Timer animationTimer;
        private AnimatedTimerColumn hoursTens;
        private AnimatedTimerColumn hoursOnes;
        private AnimatedTimerColumn minutesTens;
        private AnimatedTimerColumn minutesOnes;
        private AnimatedTimerColumn secondsTens;
        private AnimatedTimerColumn secondsOnes;
        private DateTime lastTime;


        public TestForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, IDurationPanelFactory durationPanelFactory, IDurationParentPanelFactory durationParentPanelFactory, ISessionContainerPanelFactory sessionContainerPanelFactory, ICodingSessionRepository codingSessionRepository, ISessionVisualizationController sessionVisualizationController, IPanelColourAssigner panelColorAssigner, ILast28DayPanelSettings last28DayPanelSettings, ILabelAssignment labelAssignment, IDurationParentPanelPositionManager durationPanelPositionManager, IAnimatedTimerRenderer animatedTimerRenderer, IApplicationLogger appLogger, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IAnimatedTimerManager animatedTimerManager, IStopWatchTimerService stopWatchTimerService, IAnimatedColumnStateManager columnStateManager)
        {

            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;
            _durationPanelFactory = durationPanelFactory;
            _durationParentPanelFactory = durationParentPanelFactory;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
            _codingSessionRepository = codingSessionRepository;
            _sessionVisualizationController = sessionVisualizationController;
            _panelColorAssigner = panelColorAssigner;
            _last28DayPanelSettings = last28DayPanelSettings;
            _labelAssignment = labelAssignment;
            _durationPanelPositionManager = durationPanelPositionManager;
            _animatedTimerRenderer = animatedTimerRenderer;
            _appLogger = appLogger;
            _animatedTimerColumnFactory = animatedTimerColumnFactory;
            _animatedTimerManager = animatedTimerManager;
            _stopWatchTimerService = stopWatchTimerService;
            _columnStateManager = columnStateManager;

            InitializeComponent();

            _animatedTimerManager.InitializeColumns(this);

            _animatedTimerManager.LogColumn();


            skControlTest.PaintSurface += _animatedTimerManager.DrawColumnsOnTick;




            InitializeAnimationTimer();



        }


  
    

        private async void TestForm_Load(object sender, EventArgs e)
        {

            skControlTest.Invalidate();


        }

        private async void TestForm_Shwon(object sender, EventArgs e)
        {

        }






  

        private void InitializeAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {

            TimeSpan elapsed = _stopWatchTimerService.ReturnElapsedTimeSpan();
            _animatedTimerManager.UpdateAndRender(skControlTest);

            

   

        }



        private void newTestButton_Click(object sender, EventArgs e)
        {

        }
    }

}