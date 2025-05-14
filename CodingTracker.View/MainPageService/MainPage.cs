using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.MainPageService.LabelAssignments;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ButtonHighlighterServices;
using CodingTracker.View.FormService.NotificationManagers;


namespace CodingTracker.View
{
    public partial class MainPage : Form
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IFormController _formController;
        private readonly IPanelColourAssigner _panelColourAssigner;
        private readonly IFormFactory _formFactory;
        private readonly IFormSwitcher _formSwitcher;
        private readonly ISessionCalculator _sessionCalculator;
        private readonly ILabelAssignment _labelAssignment;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly INotificationManager _notificationManager;
        private readonly IExitFlowManager _exitFlowManager;







        public MainPage(IApplicationLogger appLogger, IFormController formController, IPanelColourAssigner panelAssigner, IFormFactory formFactory, IFormSwitcher formSwitcher, ISessionCalculator sessionCalculator, ILabelAssignment labelAssignment, IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, IExitFlowManager exitFlowManager)
        {
            InitializeComponent();
            _appLogger = appLogger;
            _formController = formController;
            _panelColourAssigner = panelAssigner;
            _formFactory = formFactory;
            _formSwitcher = formSwitcher;
            _sessionCalculator = sessionCalculator;
            _labelAssignment = labelAssignment;
            _buttonHighlighterService = buttonHighlighterService;
            _notificationManager = notificationManager;
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
            // 1
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
            _formSwitcher.SwitchToForm(FormPageEnum.EditSessionPage);
        }















        // Custom click set to True in properties to enable behaviour overriding 
        private async void CloseButton_Click(object sender, EventArgs e)
        {
             _exitFlowManager.HandleExitRequest(sender, e, this);
        }



        private void MainPageExitControlMinimizeButton_Click(object sender, EventArgs e)
        {

        }

        private void MainPageStartSessionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formSwitcher.SwitchToForm(FormPageEnum.SessionGoalPage);
        }

        private void Last28DaysPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Day8Label_Click(object sender, EventArgs e)
        {

        }

        private void WaveTestButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.WaveVisualizationForm);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.SessionNotesForm);
        }
    }
}
