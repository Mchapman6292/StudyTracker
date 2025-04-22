using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.MainPageService.LabelAssignments;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;


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


        private bool isActionPanelVisible = false;



        public MainPage(IApplicationLogger appLogger, IFormController formController, IPanelColourAssigner panelAssigner, IFormFactory formFactory, IFormSwitcher formSwitcher, ISessionCalculator sessionCalculator, ILabelAssignment labelAssignment)
        {
            InitializeComponent();
            _appLogger = appLogger;
            _formController = formController;
            _panelColourAssigner = panelAssigner;
            _formFactory = formFactory;
            _formSwitcher = formSwitcher;
            _sessionCalculator = sessionCalculator;
            _labelAssignment = labelAssignment;
        }

        private async void MainPage_Load(object sender, EventArgs e)
        {
            string todayText = await _labelAssignment.GetFormattedLabelDisplayMessage(MainPageLabels.TodayTotalLabel);
            _labelAssignment.FormatTodayLabelText(TodayTotalLabel, todayText);

            string weekText = await _labelAssignment.GetFormattedLabelDisplayMessage(MainPageLabels.WeekTotalLabel);
            _labelAssignment.FormatWeekTotalLabel(WeekTotalLabel, weekText);

            string averageText = await _labelAssignment.GetFormattedLabelDisplayMessage(MainPageLabels.AverageSessionLabel);
            _labelAssignment.FormatAverageSessionLabel(AverageSessionLabel, averageText);

            _labelAssignment.UpdateDateLabelsWithHTML(Last28DaysPanel);
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
















        private void MainPageExitControlBox_Click(object sender, EventArgs e)
        {

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
    }
}
