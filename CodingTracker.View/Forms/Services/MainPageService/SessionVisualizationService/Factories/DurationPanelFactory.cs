using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.SharedFormServices;

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
        DurationPanel CreateDurationPanel(CodingSessionEntity codingSession);
        List<DurationPanel> CreateMultipleDurationPanelsForOneDaySortedByStartTime(List<CodingSessionEntity> codingSessionsForOneDay);


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
        private readonly IApplicationLogger _appLogger;


        public DurationPanelFactory(IDurationPanelHelper durationPanelHelper, IApplicationLogger appLogger)
        {
            _durationPanelHelper = durationPanelHelper;
            _appLogger = appLogger;

        }


        public List<DurationPanel> CreateMultipleDurationPanelsForOneDaySortedByStartTime(List<CodingSessionEntity> codingSessionsForOneDay)
        {

            List<DurationPanel> sortedDurationPanels = new List<DurationPanel>();

            List<CodingSessionEntity> sortedCodingSessions = codingSessionsForOneDay.OrderBy(s => s.StartTimeUTC).ToList();



            foreach ( var session in sortedCodingSessions )
            {
                sortedDurationPanels.Add(CreateDurationPanel(session));
            }
            return sortedDurationPanels;

        }


        public DurationPanel CreateDurationPanel(CodingSessionEntity codingSession )
        {
            var durationPanel = new DurationPanel()
            {
                Size = SetDurationPanelSize(codingSession.DurationSeconds),
                DurationHHMM = codingSession.DurationHHMM,
                DurationSeconds = codingSession.DurationSeconds,
                StartTimeLocal = codingSession.StartTimeLocal,
                EndTimeLocal = codingSession.EndTimeLocal,
                StudyProject = codingSession.StudyNotes,

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
