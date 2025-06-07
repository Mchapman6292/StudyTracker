using Guna.UI2.WinForms;
using CodingTracker.Common.Entities.CodingSessionEntities;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels
{
    // This class contains all of the DurationPanels and is placed within a DurationParentPanel.
    // This is needed to control the height & center the DurationPanels properly, also enforces the size and padding.

    public class SessionContainerPanel : Guna2GradientPanel
    {
        public int TotalSessionCount { get; set; }
        public int TotalDurationSeconds { get; set; }

        List<CodingSessionEntity> dayCodingSessions { get; set; }

        DateOnly SessionDateLocal { get; set; }
        bool NoSessions { get; set; }   

        public SessionContainerPanel()
        {
            this.BackColor = Color.Transparent;
            this.FillColor = Color.Transparent;
            this.Padding = new Padding(0, 4, 0, 4);
        }

        public void AddToCodingSessionsForOneDay(List<CodingSessionEntity> codingSessionsForOneDay)
        {
            dayCodingSessions = codingSessionsForOneDay;
        }

        public void AddListOfDurationPanels(List<DurationPanel> sortedDurationPanels)
        {
            foreach (DurationPanel durationPanel in sortedDurationPanels)
            {
                AddDurationPanel(durationPanel);
            }

        }

        public void AddDurationPanel(DurationPanel durationPanel)
        {
            this.Controls.Add(durationPanel);
            TotalSessionCount++;
            TotalDurationSeconds += durationPanel.DurationSeconds;
        }




        public void ClearDurationPanels()
        {
            this.Controls.Clear();
            TotalSessionCount = 0;
            TotalDurationSeconds = 0;
        }

        public List<DurationPanel> GetDurationPanels()
        {
            return this.Controls.OfType<DurationPanel>().ToList();
        }




    }
}
