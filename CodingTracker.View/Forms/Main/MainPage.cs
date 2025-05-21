using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
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
        private readonly IExitFlowManager _exitFlowManager;

        public MainPage(IFormNavigator formNavigator, ILabelAssignment labelAssignment, IButtonHighlighterService buttonHighlighterService, IExitFlowManager exitFlowManager)
        {
            InitializeComponent();
            _formNavigator = formNavigator;
            _labelAssignment = labelAssignment;
            _buttonHighlighterService = buttonHighlighterService;
            _exitFlowManager = exitFlowManager;
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
            _formNavigator.SwitchToForm(FormPageEnum.WaveVisualizationForm);
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
            _exitFlowManager.HandleExitRequestAndStopSession(sender, e, this);
        }

    

        private void MainPageStartSessionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formNavigator.SwitchToForm(FormPageEnum.SessionGoalForm);
        }


    }
}