using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.SharedFormServices;

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

        public MainPage(IFormNavigator formNavigator, ILabelAssignment labelAssignment, IButtonHighlighterService buttonHighlighterService, IButtonNotificationManager buttonNotificationManager, INotificationManager notificationManager, ICodingSessionRepository codingSessionRepository)
        {
            InitializeComponent();
            _formNavigator = formNavigator;
            _labelAssignment = labelAssignment;
            _buttonHighlighterService = buttonHighlighterService;
            _buttonNotificationManager = buttonNotificationManager;
            _notificationManager = notificationManager;
            _codingSessionRepository = codingSessionRepository;
            this.Load += MainPage_Load;
            this.Shown += MainPage_Shown;
            closeButton.Click += CloseButton_Click;
            SetAnimationWindow();
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
            _labelAssignment.UpdateAllLabelDisplayMessages(TodayTotalLabel, WeekTotalLabel, AverageSessionLabel, todayText, weekText, averageText);
            _labelAssignment.UpdateDateLabelsWithHTML(Last28DaysPanel);
            _buttonHighlighterService.SetButtonHoverColors(StartSessionButton);
            _buttonHighlighterService.SetButtonHoverColors(ViewSessionsButton);
            _buttonHighlighterService.SetButtonHoverColors(CodingSessionButton);
        }

        private async void MainPage_Shown(object sender, EventArgs e)
        {
            await _labelAssignment.UpdateLast28DayBoxesWithAssignedColorsAsync(Last28DaysPanel);
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

        private  void CloseButton_Click(object sender, EventArgs e)
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
            Dictionary<int,int> sortedStarRatings = await _codingSessionRepository.GetStarRatingsWithZeroValueDefault();

            Don
        }

    }
}