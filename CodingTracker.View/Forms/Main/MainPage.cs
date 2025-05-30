using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using Guna.Charts.WinForms;
using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;
using System.Windows.Forms;

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

        private IWaveRenderer _waveRenderer;
        private IWaveBarStateManager _barStateManager;
        private IWaveColorManager _colorManager;



        private WaveVisualizationHost waveVisualizationHost;

        private Guna2Transition chartAnimator;
        private Guna2Panel hoverInfoPanel;



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
            IWaveColorManager colorManager
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

            waveVisualizationHost = new WaveVisualizationHost(_waveRenderer, _barStateManager, _colorManager, _stopWatchTimerService);


            this.Load += MainPage_Load;
            this.Shown += MainPage_Shown;
            closeButton.Click += CloseButton_Click;
 

            waveStopWatch.Start();


  
            SetAnimationWindow();
            InitializeAnimator();
        }





        private void SetWaveHostSize()
        {
            starRatingPanel.Controls.Add(waveVisualizationHost);

            waveVisualizationHost.Size = new Size(362, 70);
            waveVisualizationHost.Dock = DockStyle.Bottom;
            waveVisualizationHost.BackColor = Color.FromArgb(35, 34, 50);

            waveVisualizationHost.Start();

            waveVisualizationHost.UpdateIntensity(1.0f);    
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
            var results = await _labelAssignment.GetAllLabelDisplayMessagesAsync();
            string todayText = results.TodayTotal;
            string weekText = results.WeekTotal;
            string averageText = results.AverageSession;
            _labelAssignment.UpdateAllLabelDisplayMessages(TodayTotalLabel, WeekTotalLabel, AverageSessionLabel, streakLabel, todayText, weekText, averageText);
            _labelAssignment.UpdateDateLabelsWithHTML(Last28DaysPanel);
            _buttonHighlighterService.SetButtonHoverColors(StartSessionButton);
            _buttonHighlighterService.SetButtonHoverColors(ViewSessionsButton);
            _buttonHighlighterService.SetButtonHoverColors(CodingSessionButton);

            SetWaveHostSize();


            await PopulateDoughnutDataSet(); 
        }

        private async void MainPage_Shown(object sender, EventArgs e)
        {
            await _labelAssignment.UpdateLast28DayBoxesWithAssignedColorsAsync(Last28DaysPanel);

            if (!starRatingPanel.Controls.Contains(waveVisualizationHost))
            {
                throw new InvalidOperationException($"Host not in starPanels");
            }


        }

        private void MainPageCodingSessionButton_Click(object sender, EventArgs e)
        {
            Size buttonSize = CodingSessionButton.CalculateButtonEdges();

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

            doughnutDataset.FillColors.Add(Color.FromArgb(255, 81, 195));   // Primary Pink
            doughnutDataset.FillColors.Add(Color.FromArgb(255, 120, 200));  // Light Pink
            doughnutDataset.FillColors.Add(Color.FromArgb(200, 150, 220));  // Pink-Purple blend
            doughnutDataset.FillColors.Add(Color.FromArgb(150, 180, 240));  // Purple-Blue blend
            doughnutDataset.FillColors.Add(Color.FromArgb(100, 200, 250));  // Blue-Cyan blend
            doughnutDataset.FillColors.Add(Color.FromArgb(100, 220, 220));  // Cyan
            doughnutDataset.FillColors.Add(Color.FromArgb(168, 228, 255));

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


        public void SetLabelVisibility()
        {
            foreach(Control control in Last28DaysPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2HtmlLabel htmlLabel)
                {

                }
            }    
        }


        private void SetWaveFormSettings()
        {
            Form waveForm = _formStateManagement.GetFormByFormPageEnum(FormPageEnum.WaveVisualizationForm);
            

        }

    }
}
