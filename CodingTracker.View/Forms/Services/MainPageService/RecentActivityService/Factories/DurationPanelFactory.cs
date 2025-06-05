using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories.PanelHelpers;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;

 namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories
{
    /* Requirements 
      - Scale for different session lengths for each panel.
      - Method to map enums to colors.
      - Class to adjust other panel lengths when new panel added?
        
     
    - Use one pixel = 1 minute

     
     */

    public interface IDurationPanelFactory
    {
        DurationPanel CreateDurationPanel(CodingSessionEntity codingSession, DurationParentPanel parentPanel);


    }

    public class DurationPanelFactory : IDurationPanelFactory
    {
        private int panelHeight = 3;

        private int maximumPanelWidth = 300;
        private int minimumPanelWidth = 8;



        private Color PanelBackColor = Color.FromArgb(45, 46, 60);

        private Color UnderOneHourFillColor1 = Color.FromArgb(225, 40, 120, 200);
        private Color UnderOneHourFillColor2 = Color.FromArgb(225, 30, 100, 160);

        private Color OneToTwoHoursFillColor1 = Color.FromArgb(230, 140, 80, 220);
        private Color OneToTwoHoursFillColor2 = Color.FromArgb(230, 100, 60, 180);

        private Color TwoToFourHoursFillColor1 = Color.FromArgb(235, 240, 90, 190);
        private Color TwoToFourHoursFillColor2 = Color.FromArgb(235, 210, 70, 160);

        private Color FourHoursPlusFillColor1 = Color.FromArgb(235, 255, 180, 90);
        private Color FourHoursPlusFillColor2 = Color.FromArgb(235, 255, 130, 80);

        private readonly IDurationPanelHelper _durationPanelHelper;


        public DurationPanelFactory(IDurationPanelHelper durationPanelHelper)
        {
            _durationPanelHelper = durationPanelHelper;
        }


        public DurationPanel CreateDurationPanel(CodingSessionEntity codingSession, DurationParentPanel parentPanel)
        {
            var durationPanel = new DurationPanel()
            {
                Size = SetDurationPanelSize(codingSession.DurationSeconds),
                DurationHHMM = codingSession.DurationHHMM,
                DurationSeconds = codingSession.DurationSeconds,
                StartTimeLocal = codingSession.StartTimeUTC.ToLocalTime(),
                EndTimeLocal = codingSession.EndTimeUTC.ToLocalTime(),
                StudyNotes = codingSession.StudyNotes,

                AutoSize = false,
                Padding = new Padding(0),
                BackColor = Color.Transparent,
                BorderRadius = 2,
                Dock = DockStyle.Left,
                Margin = new Padding(2, 0, 0, 0),
                Anchor = AnchorStyles.Left | AnchorStyles.Top

            };

            _durationPanelHelper.SetDurationPanelName(durationPanel, codingSession.StartTimeLocal, codingSession.SessionId);
            SetDurationPanelColours(durationPanel, codingSession.DurationSeconds);
            SetDurationPanelBorder(durationPanel);

            return durationPanel;
        }



        private Size SetDurationPanelSize(int durationSeconds)
        {
            int durationMins = durationSeconds / 60;

            int panelWidth = Math.Max(minimumPanelWidth, durationMins);

            return new Size(panelWidth, panelHeight);
        }

  

        private void SetDurationPanelColours(DurationPanel durationPanel, int durationSeconds)
        {

            if (durationSeconds > 0 && durationSeconds < 3600)
            {
                durationPanel.FillColor = UnderOneHourFillColor1;
                durationPanel.FillColor2 = UnderOneHourFillColor2;
            }
            else if (durationSeconds >= 3600 && durationSeconds < 7200)
            {
                durationPanel.FillColor = OneToTwoHoursFillColor1;
                durationPanel.FillColor2 = OneToTwoHoursFillColor2;
            }
            else if (durationSeconds >= 7200 && durationSeconds < 14400)
            {
                durationPanel.FillColor = TwoToFourHoursFillColor1;
                durationPanel.FillColor2 = TwoToFourHoursFillColor2;
            }
            else if (durationSeconds >= 14400)
            {
                durationPanel.FillColor = FourHoursPlusFillColor1;
                durationPanel.FillColor2 = FourHoursPlusFillColor2;
            }
        }

        private void SetDurationPanelBorder(DurationPanel durationPanel)
        {
            durationPanel.BorderThickness = 0;
            durationPanel.BorderColor = Color.Transparent; 
        }


    }
}
