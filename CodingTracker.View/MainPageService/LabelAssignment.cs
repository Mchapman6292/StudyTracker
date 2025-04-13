using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IUtilityServices;
using Guna.UI2.WinForms;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker.Business.MainPageService.LabelAssignments
{
    public interface ILabelAssignment
    {
        void UpdateMainPageLabel(Guna2HtmlLabel label, string text);
        Task<string> GetFormattedLabelDisplayMessage(MainPageLabels labelEnum);
        void FormatTodayLabelText(Guna2HtmlLabel label, string formattedTime);
        void FormatWeekTotalLabel(Guna2HtmlLabel label, string formattedTime);
        void FormatAverageSessionLabel(Guna2HtmlLabel label, string formattedTime);
        Task UpdateLast28DayBoxesWithAssignedColorsAsync(Panel parentPanel);
        void UpdateDateLabelsWithHTML(Panel parentPanel);
        void AdjustDateLabelsColumns(Panel parentPanel);
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

        public void FormatDateLabel(Guna2HtmlLabel label, string formattedDate)
        {
            string html = $@"
            <div style='font-family: Segoe UI;'>
                <div style='font-size: 11.5px; font-weight: 400; color: #e6e6e6;'>{formattedDate}</div>
            </div>";

            label.Text = html;
            label.BackColor = Color.Transparent;
        }


        public async Task UpdateLast28DayBoxesWithAssignedColorsAsync(Panel parentPanel)
        {
            var gradientPanels = parentPanel.Controls.OfType<Guna.UI2.WinForms.Guna2GradientPanel>().ToList();
            List<Color> panelColors = await _panelColourAssigner.AssignColorsToSessionsInLast28Days();

            for (int i = 0; i < panelColors.Count && i < gradientPanels.Count; i++)
            {
                gradientPanels[i].BackColor = panelColors[i];
            }
        }




        public void UpdateDateLabelsWithHTML(Panel parentPanel)
        {
            List<DateTime> last28Days = _panelColourAssigner.GetDatesPrevious28days();
            var gunaDateLabels = parentPanel.Controls.OfType<Guna2HtmlLabel>().ToList();

            for (int i = 0; i < last28Days.Count && i < gunaDateLabels.Count; i++)
            {
                string labelDate = last28Days[i].ToShortDateString();
                FormatDateLabel(gunaDateLabels[i], labelDate);
            }
        }


        public void AdjustDateLabelsColumns(Panel parentPanel)
        {
            // Get all date labels
            var dateLabels = parentPanel.Controls.OfType<Guna2HtmlLabel>().ToList();
            List<DateTime> last28Days = _panelColourAssigner.GetDatesPrevious28days();

            // Define the column positions to match the top panels
            int[] columnX = new int[] { 65, 438, 836, 1050 }; // Adjust the last column as needed

            // Determine how many labels per column (assuming current 4-column layout)
            int labelsPerColumn = dateLabels.Count / 4;
            if (dateLabels.Count % 4 != 0) labelsPerColumn++;

            // Update each label's X position while maintaining its current Y position
            for (int i = 0; i < dateLabels.Count; i++)
            {
                // Determine which column this label belongs to
                int columnIndex = i / labelsPerColumn;
                if (columnIndex > 3) columnIndex = 3; // Ensure we stay within our column array

                // Get current position
                Point currentPos = dateLabels[i].Location;

                // Update only the X coordinate to align with our column positions
                dateLabels[i].Location = new Point(columnX[columnIndex], currentPos.Y);

                // Format the label if we have a corresponding date
                if (i < last28Days.Count)
                {
                    string formattedDate = last28Days[i].ToString("dd/MM/yyyy");
                    FormatDateLabel(dateLabels[i], formattedDate);
                }
            }
        }
    }
}




