using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.MainPageService.LabelAssignments;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
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
        private readonly IApplicationControl _applicationControl;




        private bool isActionPanelVisible = false;




        public MainPage(IApplicationLogger appLogger, IFormController formController, IPanelColourAssigner panelAssigner, IFormFactory formFactory, IFormSwitcher formSwitcher, ISessionCalculator sessionCalculator, ILabelAssignment labelAssignment, IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, IApplicationControl applicationControl)
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
            _applicationControl = applicationControl;
            this.Load += MainPage_Load;
            this.Shown += MainPage_Shown;


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
            btn.FillColor = Color.FromArgb(94, 148, 255);
            btn.FillColor2 = Color.FromArgb(255, 77, 165);
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
















        private async void MainPageExitControlBox_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = _notificationManager.ReturnExitMessageDialog(this);

            if(dialogResult == DialogResult.Yes) 
            {
                await _applicationControl.ExitCodingTrackerAsync();
            }
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
            _formSwitcher.SwitchToForm(FormPageEnum.TestForm);
        }
    }
}
