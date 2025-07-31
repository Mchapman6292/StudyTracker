using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories
{
    /* Requirements 
      - Scale for different session lengths for each panel.
      - Method to map enums to colors.
      - Class to adjust other panel lengths when new panel added?
        
     
    - Use one pixel = 1 minute

     
     */

    public static class DurationPanelColors
    {



        public static Color LowestCountColor = Color.FromArgb(247, 182, 210);
        public static Color SecondLowestCountColor = Color.FromArgb(168, 228, 255);
        public static Color ThirdLowestCountColor = Color.FromArgb(212, 161, 236);
        public static Color HighestSecondCountColor = Color.FromArgb(175, 203, 255);
        public static Color HighestCountColor = Color.FromArgb(168, 240, 216);


        public static SKColor under30MinsColor = new SKColor(247, 182, 210);
        // Purple/Blue gradient (medium sessions) 
        public static SKColor HalfHourToOneHourColor = new SKColor(168, 228, 255);
        // Pink/Red gradient (long sessions)
        public static SKColor OneHourTo90MinsColor = new SKColor(212, 161, 236);

        // Blue/Cyan gradient (extra long sessions)
        public static SKColor TwoHoursPlusColor = new SKColor(175, 203, 255);




        public static Color Under30MinsFillColour1 = Color.FromArgb(78, 205, 196);
        public static Color Under30MinsfillColour2 = Color.FromArgb(68, 160, 141);

        // Purple/Blue gradient (medium sessions) 
        public static Color HalfHourToOneHourFillColour1 = Color.FromArgb(102, 126, 234);
        public static Color HalfHourToOneHourFillColour2 = Color.FromArgb(118, 75, 162);

        // Pink/Red gradient (long sessions)
        public static Color OneHourTo90MinsFillColour1 = Color.FromArgb(240, 147, 251);
        public static Color OneHourTo90MinsFillColour2 = Color.FromArgb(245, 87, 108);

        // Blue/Cyan gradient (extra long sessions)
        public static Color TwoHoursPlusFillColor1 = Color.FromArgb(79, 172, 254);
        public static Color TwoHoursPlusFillColor2 = Color.FromArgb(0, 242, 254);








    }


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

        private Color Under30MinsFillColour1 = Color.FromArgb(78, 205, 196);
        private Color Under30MinsfillColour2 = Color.FromArgb(68, 160, 141);

        // Purple/Blue gradient (medium sessions) 
        private Color HalfHourToOneHourFillColour1 = Color.FromArgb(102, 126, 234);
        private Color HalfHourToOneHourFillColour2 = Color.FromArgb(118, 75, 162);

        // Pink/Red gradient (long sessions)
        private Color OneHourTo90MinsFillColour1 = Color.FromArgb(240, 147, 251);
        private Color OneHourTo90MinsFillColour2 = Color.FromArgb(245, 87, 108);

        // Blue/Cyan gradient (extra long sessions)
        private Color TwoHoursPlusFillColor1 = Color.FromArgb(79, 172, 254);
        private Color TwoHoursPlusFillColor2 = Color.FromArgb(0, 242, 254);



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
            int durationMins = (durationSeconds / 60) * 2;

            int panelWidth = Math.Max(minimumPanelWidth, durationMins);

            return new Size(panelWidth, panelHeight);
        }






        private void SetDurationPanelColours(DurationPanel durationPanel, int durationSeconds)
        {
            if (durationSeconds > 0 && durationSeconds < 1800)
            {
                durationPanel.FillColor = DurationPanelColors.LowestCountColor;
                durationPanel.FillColor2 = DurationPanelColors.LowestCountColor;

            }
            else if (durationSeconds >= 1800 && durationSeconds < 3600)
            {
                durationPanel.FillColor = DurationPanelColors.SecondLowestCountColor;
                durationPanel.FillColor2 = DurationPanelColors.SecondLowestCountColor;
            }
            else if (durationSeconds >= 3600 && durationSeconds < 5400)
            {
                durationPanel.FillColor = DurationPanelColors.ThirdLowestCountColor;
                durationPanel.FillColor2 = DurationPanelColors.ThirdLowestCountColor;
            }
            else if (durationSeconds >= 5400)
            {
                durationPanel.FillColor = DurationPanelColors.HighestSecondCountColor;
                durationPanel.FillColor2 = DurationPanelColors.HighestSecondCountColor;
            }
        }

        private void SetDurationPanelBorder(DurationPanel durationPanel)
        {
            durationPanel.BorderThickness = 0;
            durationPanel.BorderColor = Color.Transparent; 
        }


    }
}
