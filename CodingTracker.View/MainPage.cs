using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.MainPageService.LabelAssignments;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IErrorHandlers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using System.Diagnostics;


namespace CodingTracker.View
{
    public partial class MainPage : Form
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IFormController _formController;
        private readonly IPanelColourAssigner _panelAssigner;
        private readonly IErrorHandler _errorHandler;
        private readonly IFormFactory _formFactory;
        private readonly IFormSwitcher _formSwitcher;
        private readonly ISessionCalculator _sessionCalculator;
        private readonly ILabelAssignment _labelAssignment;


        private bool isActionPanelVisible = false;



        public MainPage(IApplicationLogger appLogger, IFormController formController, IPanelColourAssigner panelAssigner, IErrorHandler errorHandler, IFormFactory formFactory, IFormSwitcher formSwitcher, ISessionCalculator sessionCalculator, ILabelAssignment labelAssignment)
        {
            InitializeComponent();
            _appLogger = appLogger;
            _formController = formController;
            _panelAssigner = panelAssigner;
            _errorHandler = errorHandler;
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

            UpdateLabels(Last28DaysPanel);
            await UpDateLast28Days(Last28DaysPanel);


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



        private void UpdateLabels(Panel parentPanel)
        {
            _appLogger.Debug("UpdateLabels method started.");
            try
            {
                List<DateTime> last28Days = _panelAssigner.GetDatesPrevious28days();
                var GunaLabels = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2HtmlLabel>().ToList();
                for (int i = 0; i < last28Days.Count && i < GunaLabels.Count; i++)
                {
                    GunaLabels[i].Text = last28Days[i].ToShortDateString();
                }
                _appLogger.Debug("UpdateLabels method completed successfully.");
            }
            catch (Exception ex)
            {
                _appLogger.Error($"An error occurred in UpdateLabels: {ex.Message}");
            }
        }

        private async Task UpDateLast28Days(Panel parentPanel)
        {
            using (var activity = new Activity(nameof(UpDateLast28Days)))
            {
                _appLogger.Debug($"UpDateLast28Days method started. Trace ID: {activity.TraceId}.");
                try
                {
                    var gradientPanels = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2GradientPanel>().ToList();
                    List<Color> panelColors = await _panelAssigner.AssignColorsToSessionsInLast28Days();

                    for (int i = 0; i < panelColors.Count && i < gradientPanels.Count; i++)
                    {
                        gradientPanels[i].BackColor = panelColors[i];
                    }
                    _appLogger.Debug("UpdateSessionPanels method completed successfully.");
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"An error occurred in UpdateSessionPanels: {ex.Message}");
                }
            }
        }








        private void Day2Label_Click(object sender, EventArgs e)
        {

        }

        private void MainPageExitControlBox_Click(object sender, EventArgs e)
        {

        }



        private void CodingSessionPageStartSessionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formSwitcher.SwitchToForm(FormPageEnum.SessionGoalPage);
        }

        private void MainPageExitControlMinimizeButton_Click(object sender, EventArgs e)
        {

        }

        private void MainPageStartSessionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formSwitcher.SwitchToForm(FormPageEnum.SessionGoalPage);
        }
    }
}
 