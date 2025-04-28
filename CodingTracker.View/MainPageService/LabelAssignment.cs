using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IUtilityServices;
using Guna.UI2.WinForms;

namespace CodingTracker.Business.MainPageService.LabelAssignments
{
    public interface ILabelAssignment
    {
        void UpdateAllLabelDisplayMessages(Guna2HtmlLabel todayLabel, Guna2HtmlLabel weekLabel, Guna2HtmlLabel averageLabel, string todayText, string weekText, string averageText);
        Task<(string TodayTotal, string WeekTotal, string AverageSession)> GetAllLabelDisplayMessagesAsync();
        void UpdateMainPageLabel(Guna2HtmlLabel label, string text);    
        void FormatTodayLabelText(Guna2HtmlLabel label, string formattedTime);
        void FormatWeekTotalLabel(Guna2HtmlLabel label, string formattedTime);
        void FormatAverageSessionLabel(Guna2HtmlLabel label, string formattedTime);
        Task UpdateLast28DayBoxesWithAssignedColorsAsync(Panel parentPanel);
        void UpdateDateLabelsWithHTML(Panel parentPanel);
    }

    public class LabelAssignment : ILabelAssignment
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ISessionCalculator _sessionCalculator;
        private readonly IUtilityService _utilityService;
        private readonly IPanelColourAssigner _panelColourAssigner;



        public LabelAssignment(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, ISessionCalculator sessionCalculator, IUtilityService utilityService, IPanelColourAssigner panelColourAssigner)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
            _sessionCalculator = sessionCalculator;
            _utilityService = utilityService;
            _panelColourAssigner = panelColourAssigner;
        }




        public void ClearMainPageLabel(Guna2HtmlLabel label)
        {
            label.Text = string.Empty;
        }

        public void UpdateMainPageLabel(Guna2HtmlLabel label, string text)
        {
            ClearMainPageLabel(label);
            label.Text = text;
        }

        // Returns tuple of totals.
        public async Task<(string TodayTotal, string WeekTotal, string AverageSession)> GetAllLabelDisplayMessagesAsync()
        {
            
            (double todayTotal, double weekTotal, double averageSession) = await _codingSessionRepository.GetLabelDurationsAsync();


            // Format all the values
            string todayText = _utilityService.ConvertDoubleToHHMMString(todayTotal);
            string weekText = _utilityService.ConvertDoubleToHHMMString(weekTotal);
            string averageText = _utilityService.ConvertDoubleToHHMMString(averageSession);

            return (todayText, weekText, averageText);
        }

        public void UpdateAllLabelDisplayMessages(Guna2HtmlLabel todayLabel, Guna2HtmlLabel weekLabel,Guna2HtmlLabel averageLabel, string todayText,string weekText, string averageText)
        {
            FormatTodayLabelText(todayLabel, todayText);
            FormatWeekTotalLabel(weekLabel, weekText);
            FormatAverageSessionLabel(averageLabel, averageText);
        }

        /*
        public async Task<string> GetFormattedLabelDisplayMessage(MainPageLabels labelEnum)
        {
            string labelMessage = string.Empty;
            string prefix = string.Empty;

            switch (labelEnum)
            {
                case MainPageLabels.TodayTotalLabel:
                    prefix = "Today's Total";
                    double todayTotal = await _codingSessionRepository.GetTodaysTotalDurationAsync();
                    labelMessage = _utilityService.ConvertDoubleToHHMMString(todayTotal);
                    break;

                case MainPageLabels.AverageSessionLabel:
                    prefix = "Average Session";
                    double totalAverage = await _codingSessionRepository.GetAverageDurationOfAllSessionsAsync();
                    labelMessage = _utilityService.ConvertDoubleToHHMMString(totalAverage);
                    break;

                case MainPageLabels.WeekTotalLabel:
                    prefix = "Week Total";
                    double weekTotal = await _codingSessionRepository.GetWeekTotalDurationAsync();
                    labelMessage = _utilityService.ConvertDoubleToHHMMString(weekTotal);
                    break;
            }
            return labelMessage;
        }

        */

        public void FormatTodayLabelText(Guna2HtmlLabel label, string formattedTime)
        {
            string html = $@"
            <div style='font-family: Segoe UI;'>
                <div style='font-size: 14px; font-weight: 600; color: white; text-transform: uppercase;'>Today's Total</div>
                <div style='font-size: 24px; font-weight: 700; color: white;'>{formattedTime}</div>
            </div>";

            label.Text = html;
            label.BackColor = Color.Transparent;
        }

        public void FormatWeekTotalLabel(Guna2HtmlLabel label, string formattedTime)
        {
            string html = $@"
            <div style='font-family: Segoe UI;'>
                <div style='font-size: 14px; font-weight: 600; color: white; text-transform: uppercase;'>Week Total</div>
                <div style='font-size: 24px; font-weight: 700; color: white;'>{formattedTime}</div>
            </div>";

            label.Text = html;
            label.BackColor = Color.Transparent;
        }

        public void FormatAverageSessionLabel(Guna2HtmlLabel label, string formattedTime)
        {
            string html = $@"
            <div style='font-family: Segoe UI;'>
                <div style='font-size: 14px; font-weight: 600; color: white; text-transform: uppercase;'>Average Session</div>
                <div style='font-size: 24px; font-weight: 700; color: white;'>{formattedTime}</div>
            </div>";

            label.Text = html;
            label.BackColor = Color.Transparent;
        }










        public void FormatDateLabel(Guna2HtmlLabel label, DateTime labelDate)
        {
            _appLogger.Info($"labelDate passed to {nameof(FormatDateLabel)}: {labelDate}.");

            string html = $@"
    <div style='font-family: Segoe UI; padding: 2px 0;'>
        <div style='font-size: 10.5px; line-height: 1.5; letter-spacing: 0.5px; font-weight: 300; color: #e0e0e0;'>
            {labelDate:dd/MM/yyyy}
        </div>
    </div>";

            label.Text = html;
            label.BackColor = Color.Transparent;
            label.UseGdiPlusTextRendering = true;
            label.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            label.Cursor = Cursors.Hand;
        }

        public async Task UpdateLast28DayBoxesWithAssignedColorsAsync(Panel parentPanel)
        {
            var gradientPanels = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2GradientPanel>().ToList();
            List<(Color StartColor, Color EndColor)> panelGradients = await _panelColourAssigner.AssignGradientColorsToSessionsInLast28Days();

            for (int i = 0; i < panelGradients.Count && i < gradientPanels.Count; i++)
            {
                gradientPanels[i].FillColor = panelGradients[i].StartColor;
                gradientPanels[i].FillColor2 = panelGradients[i].EndColor;
            }
        }



        public void UpdateDateLabelsWithHTML(Panel parentPanel)
        {
            List<DateTime> last28Days = _panelColourAssigner.GetDatesPrevious28days();
            var gunaDateLabels = parentPanel.Controls.OfType<Guna2HtmlLabel>().ToList();

            _appLogger.Info($"Number of labels for {nameof(UpdateDateLabelsWithHTML)}: {gunaDateLabels.Count}.");


            for (int i = 0; i < last28Days.Count && i < gunaDateLabels.Count; i++)
            {
                DateTime labelDate = last28Days[i];
                Guna2HtmlLabel currentLabel = gunaDateLabels[i];   
                FormatDateLabel(currentLabel, labelDate);
            }
        }



        private void ApplyRoundedCornersToPanel(Guna.UI2.WinForms.Guna2GradientPanel panel)
        {
            panel.BorderRadius = 30;
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Depth = 5;
            panel.ShadowDecoration.Color = Color.FromArgb(30, 0, 0, 0);
            panel.BorderThickness = 0;
        }
    }
}




