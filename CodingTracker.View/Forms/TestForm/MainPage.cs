using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.MainPageService.DonutChartManagers;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using CodingTracker.View.Forms.WaveVisualizer;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.TestForm
{
    public partial class MainPage : Form
    {

        private readonly IFormNavigator _formNavigator;
        private readonly ILabelAssignment _labelAssignment;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly IExitFlowManager _exitFlowManager;
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
        private readonly IMainPagePieChartManager _mainPagePieChartManager;


        public MainPage
        (
            IFormNavigator formNavigator,
            ILabelAssignment labelAssignment,
            IButtonHighlighterService buttonHighlighterService,
            IExitFlowManager buttonNotificationManager,
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
            IDurationParentPanelPositionManager durationPanelPositionManager,
            IMainPagePieChartManager mainPagePiechartManager
        )
        {
            InitializeComponent();


            _formNavigator = formNavigator;
            _labelAssignment = labelAssignment;
            _buttonHighlighterService = buttonHighlighterService;
            _exitFlowManager = buttonNotificationManager;
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
            _mainPagePieChartManager = mainPagePiechartManager;

            this.Load += MainPageTestForm_Load;
            this.Shown += MainPageTestForm_Shown;

        }




        private async void MainPageTestForm_Load(object sender, EventArgs e)
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


                await _mainPagePieChartManager.TESTPopulateStarRatingsDoughnut(starRatingsPieChart);

                Last28DaysPanel.BringToFront();


                _mainPagePieChartManager.SetPieChartSettings(starRatingsPieChart);
                SetDurationPanelLegendColors();


                _buttonHighlighterService.SetButtonHoverColors(dashboardButton);
                _buttonHighlighterService.SetButtonHoverColors(sessionsButton);
                _buttonHighlighterService.SetButtonHoverColors(logoutButton);


            }

            finally
            {
                this.Enabled = true;
            }
        }


        private async void MainPageTestForm_Shown(object sender, EventArgs e)
        {
            await InitializeActivityDurationPanel();
        }

        private async Task InitializeActivityDurationPanel()
        {
            List<DurationParentPanel> dppList = await _sessionVisualizationController.CreateDurationParentPanelsWithDataAsync(activityParentPanel);

            if (dppList.Count > 0)
            {
                foreach (var dpp in dppList)
                {
            
                    activityParentPanel.Controls.Add(dpp);


                }
            }
            _durationPanelPositionManager.SetPanelPositionsAfterContainerAdd(dppList);
        }



        private void SetDurationPanelLegendColors()
        {
            zeroMinutesLegendPanel.FillColor = DurationPanelColors.LowestCountColor;
            zeroMinutesLegendPanel.FillColor2 = DurationPanelColors.LowestCountColor;

            lessThanOneHourLegendPanel.FillColor = DurationPanelColors.SecondLowestCountColor;
            lessThanOneHourLegendPanel.FillColor2 = DurationPanelColors.SecondLowestCountColor;

            betweenOneAndTwoHoursLegendPanel.FillColor = DurationPanelColors.ThirdLowestCountColor;
            betweenOneAndTwoHoursLegendPanel.FillColor2 = DurationPanelColors.ThirdLowestCountColor;

            betweenTwoAndFourHoursLegendPanel.FillColor = DurationPanelColors.HighestSecondCountColor;
            betweenTwoAndFourHoursLegendPanel.FillColor2 = DurationPanelColors.HighestSecondCountColor;

        }





        private void startSessionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formNavigator.SwitchToForm(FormPageEnum.SessionGoalForm);
        }

        private void sessionsButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.EditSessionForm);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequestAndStopSession(sender, e, this);
        }
    }
}
