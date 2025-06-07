using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels
{
    public class DurationParentPanel : Guna2GradientPanel
    {
        public Guna2HtmlLabel DateLabel { get; set; }
        public Guna2HtmlLabel DurationTotalLabel { get; set; }
        public SessionContainerPanel SessionContainerPanel { get; set; }
        public DateOnly PanelDateLocal { get; set; }
        public int TotalDurationSeconds { get; set; }

        public int SumDurationPanelSeconds()
        {
            if (SessionContainerPanel == null) return 0;
            return SessionContainerPanel.GetDurationPanels().Sum(panel => panel.DurationSeconds);
        }

        public void AddSessionContainerPanel(SessionContainerPanel sessionContainerPanel)
        {
             this.Controls.Add(sessionContainerPanel);
             SessionContainerPanel = sessionContainerPanel;
        }

        public void UpdateDurationTotalLabel()
        {
            int totalDurationSeconds = 0;

            List<DurationPanel> durationPanels = SessionContainerPanel.GetDurationPanels();

            foreach (DurationPanel durationPanel in durationPanels)
            {
                totalDurationSeconds += durationPanel.DurationSeconds;
            }

            TimeSpan duration = TimeSpan.FromSeconds(totalDurationSeconds);

            DurationTotalLabel.Text = $"{(int)duration.TotalHours}h {duration.Minutes:D2}m";
        }

    }
}