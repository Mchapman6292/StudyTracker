using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using CodingTracker.View.Forms.WaveVisualizer;  
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;


namespace CodingTracker.View
{
    public partial class MainPage : Form
    {
        private readonly IFormNavigator _formNavigator;
        private readonly ILabelAssignment _labelAssignment;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly IButtonNotificationManager _buttonNotificationManager;
        private readonly INotificationManager _notificationManager;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IFormFactory _formFactory;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IApplicationLogger _appLogger;

        private IWaveRenderer _waveRenderer;
        private IWaveBarStateManager _barStateManager;
        private IWaveColorManager _colorManager;
        private readonly IPanelColourAssigner _panelColorAssigner;
        private readonly ILast28DayPanelSettings _last28DayPanelSettings;
        private readonly ISessionVisualizationController _sessionVisualizationController;
        private readonly IDurationParentPanelPositionManager _durationPanelPositionManager;


        // Side panel visibility = false, change

        private WaveVisualizationHost waveVisualizationHost;
        private ImageAnimator imageAnimator;

        private Guna2Transition chartAnimator;
        private Guna2Panel hoverInfoPanel;


        public Dictionary<Guna2HtmlLabel, Guna2GradientPanel> LabelToPanelMap { get; private set; }

        public List<(Guna2GradientPanel, int)> sessionDurationsToPanelMap;

        private Guna.Charts.WinForms.Animation starChartAnimation;

        private Stopwatch waveStopWatch = new Stopwatch();

        public MainPage(
            IFormNavigator formNavigator,
            ILabelAssignment labelAssignment,
            IButtonHighlighterService buttonHighlighterService,
            IButtonNotificationManager buttonNotificationManager,
            INotificationManager notificationManager,
            ICodingSessionRepository codingSessionRepository,
            IFormStateManagement formStateManagement,
            IFormFactory formFactory,
            IStopWatchTimerService stopWatchTimerService,
            IWaveRenderer waveRenderer,
            IWaveBarStateManager barStateManager,
            IWaveColorManager colorManager,
            IPanelColourAssigner panelColourAssigner,
            ILast28DayPanelSettings last28DayPanelSettings,
            IApplicationLogger appLogger,
            ISessionVisualizationController sessionVisualizationController,
            IDurationParentPanelPositionManager durationPanelPositionManager


        )
        {
            InitializeComponent();
            _formNavigator = formNavigator;
            _labelAssignment = labelAssignment;
            _buttonHighlighterService = buttonHighlighterService;
            _buttonNotificationManager = buttonNotificationManager;
            _notificationManager = notificationManager;
            _codingSessionRepository = codingSessionRepository;
            _formFactory = formFactory;
            _formStateManagement = formStateManagement;
            _stopWatchTimerService = stopWatchTimerService;
            _waveRenderer = waveRenderer;
            _barStateManager = barStateManager;
            _colorManager = colorManager;
            _panelColorAssigner = panelColourAssigner;
            _last28DayPanelSettings = last28DayPanelSettings;
            _appLogger = appLogger;
            _sessionVisualizationController = sessionVisualizationController;
            _durationPanelPositionManager = durationPanelPositionManager;

            waveVisualizationHost = new WaveVisualizationHost(_waveRenderer, _barStateManager, _colorManager, _stopWatchTimerService);


            this.Load += MainPage_Load;
            this.Shown += MainPage_Shown;
            closeButton.Click += CloseButton_Click;

            startSessionButton.MouseEnter += StartSessionButtonNew_MouseEnter;
            startSessionButton.MouseLeave += StartSessionButtonNew_MouseLeave;


            waveStopWatch.Start();





            SetAnimationWindow();
            InitializeAnimator();
            _sessionVisualizationController = sessionVisualizationController;
        }


        private async Task InitializeActivityDurationPanel()
        {
            List<DurationParentPanel> dppList = await _sessionVisualizationController.CreateDurationParentPanelsWithDataAsync(activityParentPanel);

            if (dppList.Count > 0)
            {
                foreach (var dpp in dppList)
                {
                    _sessionVisualizationController.LogDurationParentPanel(dpp);
                    activityParentPanel.Controls.Add(dpp);


                }
            }
            _durationPanelPositionManager.SetPanelPositionsAfterContainerAdd(dppList);


        }






        private void EnableMossGifAfterMouseEnter()
        {

        }

        private void SetWaveHostSize()
        {
            /*
            WaveVisualizationPanel.Controls.Add(waveVisualizationHost);

            waveVisualizationHost.Size = new Size(362, 70);
            waveVisualizationHost.Dock = DockStyle.Bottom;

            waveVisualizationHost.BackColor = Color.FromArgb(35, 34, 50);

            waveVisualizationHost.Start();

            waveVisualizationHost.UpdateIntensity(1.0f);
            */
        }


        private void waveStopWatch_Tick(object sender, EventArgs e)
        {
            float intensity = waveVisualizationHost.Intensity;

            if (intensity > 1.0f)
            {
                intensity = 0.1f;
                waveStopWatch.Stop();
            }

            intensity += 0.1f;

            waveVisualizationHost.UpdateIntensity(intensity);

        }




        private void InitializeAnimator()
        {
            Guna.Charts.WinForms.Animation starChartAnimation = new Guna.Charts.WinForms.Animation(Easing.EaseInCirc, 500);
            starChart.Animation = starChartAnimation;

        }


        private void SetAnimationWindow()
        {
            gunaAnimationWindow.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_ACTIVATE | Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_BLEND;
        }

        private async void MainPage_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {

                var results = await _labelAssignment.GetAllLabelDisplayMessagesAsync();
                string todayText = results.TodayTotal;
                string weekText = results.WeekTotal;
                string averageText = results.AverageSession;
                _labelAssignment.UpdateAllLabelDisplayMessages(todayTotalLabel, WeekTotalLabel, AverageSessionLabel, todayText, weekText, averageText);
                _labelAssignment.UpdateDateLabelsWithHTML(Last28DaysPanel);


                _buttonHighlighterService.SetButtonHoverColors(startSessionButton);
                _buttonHighlighterService.SetButtonHoverColors(viewSessionButton);

                SetWaveHostSize();


                await PopulateDoughnutDataSet();


                Last28DaysPanel.BringToFront();
                EditMossGifRegion();
            }

            finally
            {
                this.Enabled = true;
            }
        }


        private async void MainPage_Shown(object sender, EventArgs e)
        {

            await InitializeActivityDurationPanel();

        }

        private void MainPageCodingSessionButton_Click(object sender, EventArgs e)
        {
            Size buttonSize = startSessionButton.CalculateButtonEdges();

            int height = buttonSize.Height;
            int width = buttonSize.Width;

            string message = $"height = {height}, width = {width}.";

            _notificationManager.ShowNotificationDialog(this, message);
        }

        private void MainPageCodingSessionButton_MouseEnter(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2GradientButton btn = sender as Guna.UI2.WinForms.Guna2GradientButton;
            btn.FillColor = Color.FromArgb(255, 81, 195);
            btn.FillColor2 = Color.FromArgb(168, 228, 255);
        }

        private void MainPageCodingSessionButton_MouseLeave(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2GradientButton btn = sender as Guna.UI2.WinForms.Guna2GradientButton;
            btn.FillColor = Color.FromArgb(35, 34, 50);
            btn.FillColor2 = Color.FromArgb(35, 34, 50);
        }

        private void MainPageEditSessionsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formNavigator.SwitchToForm(FormPageEnum.EditSessionForm);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _buttonNotificationManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        private void MainPageStartSessionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formNavigator.SwitchToForm(FormPageEnum.SessionGoalForm);
        }

        public async Task LoadRatingsIntoDonutChart()
        {
            Dictionary<int, int> sortedStarRatings = await _codingSessionRepository.GetStarRatingsWithZeroValueDefault();
        }

        private async Task PopulateDoughnutDataSet()
        {
            Dictionary<int, int> sessionStarRatings = await _codingSessionRepository.GetStarRatingsWithZeroValueDefault();
            doughnutDataset.DataPoints.Clear();

            // Configure hover effects
            doughnutDataset.BorderWidth = 2;
            doughnutDataset.FillColors.Clear();

            doughnutDataset.FillColors.Add(Color.FromArgb(40, 100, 120));    // 0 stars - Dark cyan (like zero duration)
            doughnutDataset.FillColors.Add(Color.FromArgb(80, 200, 220));    // 1 star - Light cyan (like under 1 hour)
            doughnutDataset.FillColors.Add(Color.FromArgb(140, 120, 220));   // 2 stars - Purple-blue (like 1-2 hours)
            doughnutDataset.FillColors.Add(Color.FromArgb(180, 100, 200));   // 3 stars - Purple (like 2-4 hours start)
            doughnutDataset.FillColors.Add(Color.FromArgb(255, 120, 180));   // 4 stars - Pink-purple (like 2-4 hours end)
            doughnutDataset.FillColors.Add(Color.FromArgb(255, 80, 140));    // 5 stars - Intense orange-purple

            doughnutDataset.BorderColors.Clear();
            doughnutDataset.BorderColors.Add(Color.FromArgb(60, 120, 140));     // 0 stars - Darker cyan border
            doughnutDataset.BorderColors.Add(Color.FromArgb(100, 220, 240));    // 1 star - Bright cyan border
            doughnutDataset.BorderColors.Add(Color.FromArgb(160, 140, 240));    // 2 stars - Purple-blue border
            doughnutDataset.BorderColors.Add(Color.FromArgb(200, 120, 220));    // 3 stars - Purple border
            doughnutDataset.BorderColors.Add(Color.FromArgb(255, 140, 200));    // 4 stars - Pink border
            doughnutDataset.BorderColors.Add(Color.FromArgb(255, 100, 160));    // 5 stars - Intense pink-orange border



            foreach (var rating in sessionStarRatings)
            {
                doughnutDataset.DataPoints.Add(new LPoint()
                {
                    Label = $"⭐ {rating.Key} Star{(rating.Key > 1 ? "s" : "")}",
                    Y = rating.Value
                });
            }

            starChart.Update();
        }




        private void SetWaveFormSettings()
        {
            Form waveForm = _formStateManagement.GetFormByFormPageEnum(FormPageEnum.WaveVisualizationForm);


        }


        /*
  
        private void HideDatesWithNoDuration()
        {
            int labels = 0;

            foreach (var labelAndPanelPair in LabelToPanelMap)
            {
                if (labelAndPanelPair.Value.FillColor == Color.Empty || labelAndPanelPair.Value.FillColor2 == Color.Empty)
                {
                    Guna2HtmlLabel targetLabel = labelAndPanelPair.Key;
                    string currentHtml = targetLabel.Text;
                    string newHtml = currentHtml.Replace("color: #e0e0e0", "color: #808080"); // Gray color
                    targetLabel.Text = newHtml;

                }

                else
                {
                    labels++;
                }
            }
            string panels = $"{labels} not text not changed.";
            _notificationManager.ShowNotificationDialog(this, panels);
        }
        */


        /*
        private void InitializeSessionDurationToPanelMap()
        {
            sessionDurationsToPanelMap = new List<(Guna2GradientPanel, int)>
            {
                (dayOneColourPanel, 0), (dayTwoColorPanel, 0), (dayThreeColorPanel, 0), (dayFourColorPanel, 0), (dayFiveColorPanel, 0), (daySixColorPanel, 0), (daySevenColorPanel, 0),
                (dayEightColorPanel, 0), (dayNineColorPanel, 0), (dayTenColorPanel, 0), (dayElevenColorPanel, 0), (dayTwelveColorPanel, 0), (dayThirteenColorPanel, 0), (dayFourteenColorPanel, 0),
                (dayFifteenColorPanel, 0), (daySixteenColorPanel, 0), (daySeventeenColorPanel, 0), (dayEighteenColorPanel, 0), (dayNineteenColorPanel, 0), (dayTwentyColorPanel, 0), (dayTwentyOneColorPanel, 0),
                (dayTwentyTwoColorPanel, 0), (dayTwentyThreeColorPanel, 0), (dayTwentyFourColorPanel, 0), (dayTwentyFiveColorPanel, 0), (dayTwentySixColorPanel, 0), (dayTwentySevenColorPanel, 0), (dayTwentyEightColorPanel, 0)
            };
        }

        */

        /*
        private async Task UpdateSessionDurationToPanelMapAsync()
        {
            List<int> orderedSessionDurations = await _codingSessionRepository.GetLast28DayDurationSecondsWithDefaultZeroValues();

            int durationsIndex = 0;

            if (orderedSessionDurations.Count != sessionDurationsToPanelMap.Count)
            {
                _appLogger.Error($"Number of session durations is not equal to the number of durations in {nameof(sessionDurationsToPanelMap)}.");
            }

            for (int i = 0; i < orderedSessionDurations.Count; i++)
            {
                sessionDurationsToPanelMap[i] = (sessionDurationsToPanelMap[i].Item1, orderedSessionDurations[i]);
                _appLogger.Info($"Panel: {sessionDurationsToPanelMap[i].Item1.Name} duration: {orderedSessionDurations[i]}.");

            }
        }
        */

        /*

        private List<Guna2GradientPanel> GetOrderedPanelsList()
        {
            return new List<Guna2GradientPanel>
            {
                dayOneColourPanel, dayTwoColorPanel, dayThreeColorPanel, dayFourColorPanel, dayFiveColorPanel, daySixColorPanel, daySevenColorPanel,
                dayEightColorPanel, dayNineColorPanel, dayTenColorPanel, dayElevenColorPanel, dayTwelveColorPanel, dayThirteenColorPanel, dayFourteenColorPanel,
                dayFifteenColorPanel, daySixteenColorPanel, daySeventeenColorPanel, dayEighteenColorPanel, dayNineteenColorPanel, dayTwentyColorPanel, dayTwentyOneColorPanel,
                dayTwentyTwoColorPanel, dayTwentyThreeColorPanel, dayTwentyFourColorPanel, dayTwentyFiveColorPanel, dayTwentySixColorPanel, dayTwentySevenColorPanel, dayTwentyEightColorPanel
            };
        }
        */

        /*
        private async Task AssignLast28PanelVisualsAsync()
        {
            Dictionary<SessionDurationEnum, Guna2GradientPanel> last28DayDurations = _last28DayPanelSettings.ReturnDurationPanelSettingsDict();

            List<Guna2GradientPanel> dayPanelList = GetOrderedPanelsList();
            await UpdateSessionDurationToPanelMapAsync();

            foreach (var (panel, durationSeconds) in sessionDurationsToPanelMap)
            {
                SessionDurationEnum durationEnum = _last28DayPanelSettings.ConvertDurationSecondsToSessionDurationEnum(durationSeconds);
                Guna2GradientPanel templatePanel = last28DayDurations[durationEnum];

                panel.FillColor = templatePanel.FillColor;
                panel.FillColor2 = templatePanel.FillColor2;
                panel.BorderColor = templatePanel.BorderColor;
                panel.BorderThickness = templatePanel.BorderThickness;
                panel.ShadowDecoration.Enabled = templatePanel.ShadowDecoration.Enabled;
                panel.ShadowDecoration.Color = templatePanel.ShadowDecoration.Color;
                panel.ShadowDecoration.Shadow = templatePanel.ShadowDecoration.Shadow;
            }
        }
        */


        private void AdjustStarPanelToolLocations()
        {

        }
        private void StandardizeVerticalSpacing()
        {
            int startX = 38;
            int columnWidth = 160;

            int[] labelXPositions = {
               startX,
                startX + columnWidth,
                startX + (2 * columnWidth),
                startX + (3 * columnWidth)
            };

            int panelOffset = 77;
            int startY = 17;
            int rowSpacing = 39;

            var panels = Last28DaysPanel.Controls.OfType<Guna2GradientPanel>().OrderBy(p => p.Name).ToArray();
            var labels = Last28DaysPanel.Controls.OfType<Guna2HtmlLabel>().OrderBy(l => l.Name).ToArray();

            for (int i = 0; i < 28; i++)
            {
                int column = i / 7;
                int row = i % 7;
                int yPosition = startY + (row * rowSpacing);
                int labelX = labelXPositions[column];
                int panelX = labelX + panelOffset;

                labels[i].Location = new Point(labelX, yPosition);
                panels[i].Location = new Point(panelX, yPosition);
            }
        }


        private void StartSessionButtonNew_MouseEnter(object sender, EventArgs e)
        {
            mossPictureBoxGif.Visible = true;
            mossPictureBoxGif.Refresh();
            DisableMossGifAfterOneLoop();

        }

        private void StartSessionButtonNew_MouseLeave(object sender, EventArgs e)
        {

        }

        private void DisableMossGifAfterOneLoop()
        {
            var mossGifTimer = new System.Windows.Forms.Timer();
            mossGifTimer.Interval = 850;

            mossGifTimer.Tick += (sender, e) =>
            {

                mossGifTimer.Stop();
                mossGifTimer.Dispose();

                mossPictureBoxGif.Visible = false;
            };

            mossGifTimer.Start();
        }


        private void EditMossGifRegion()
        {
            int trimTop = 15;
            int trimWBottom = 15;

            Bitmap mossGifBitmap = Properties.Resources.The_IT_Crowd_Intro_BitMap;

            var clipRegion = new Region(new Rectangle(0, trimTop, mossPictureBoxGif.Width, mossPictureBoxGif.Height - trimTop - trimWBottom));


            mossPictureBoxGif.Region = clipRegion;

        }

        private void viewSessionButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.EditSessionForm);
        }
    }
}

