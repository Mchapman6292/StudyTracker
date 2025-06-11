using CodingTracker.Common.Utilities;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories
{
    public interface IDurationParentPanelFactory
    {
        List<DurationParentPanel> CreateEmptyDurationParentPanels(Guna2GradientPanel activityParentPanel);
        DurationParentPanel CreateDurationParentPanelWithSessionContainerPanel(DateOnly panelDateLocal, Guna2GradientPanel activityParentPanel);
    }

    public class DurationParentPanelFactory : IDurationParentPanelFactory
    {
        private Size ParentPanelSize = new Size(513, 35);
        private Size DatePanelSize = new Size(443, 19);

        private readonly IDurationPanelHelper _durationPanelHelper;
        private readonly IUtilityService _utilityService;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;
        private readonly IDurationParentPanelPositionManager _durationPanelPositionManager;


        public DurationParentPanelFactory(IDurationPanelHelper durationPanelHelper, IUtilityService utilityService, ISessionContainerPanelFactory sessionContainerPanelFactory, IDurationParentPanelPositionManager durationPanelPositionManager)
        {
            _durationPanelHelper = durationPanelHelper;
            _utilityService = utilityService;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
            _durationPanelPositionManager = durationPanelPositionManager;
        }

        public DurationParentPanel CreateDurationParentPanelWithSessionContainerPanel(DateOnly panelDateLocal, Guna2GradientPanel activityParentPanel)
        {


            DurationParentPanel durationParentPanel = new DurationParentPanel()
            {
                Size = new Size(activityParentPanel.Width, 35),
                BackColor = Color.Transparent,
                FillColor = Color.Transparent,
                ForeColor = SystemColors.ControlText,
                Padding = new Padding(15, 8, 15, 8),
                PanelDateLocal = panelDateLocal

            };

            _durationPanelHelper.SetParentPanelName(durationParentPanel, panelDateLocal);

            CreateDateLabel(durationParentPanel, panelDateLocal);

            int totalDurationPanelSeconds = durationParentPanel.SumDurationPanelSeconds();
            CreateDurationLabel(durationParentPanel, totalDurationPanelSeconds);

            SessionContainerPanel sessionContainerPanel = _sessionContainerPanelFactory.CreateSessionContainerPanel(panelDateLocal);

            durationParentPanel.AddSessionContainerPanel(sessionContainerPanel); 

            return durationParentPanel;

        }



        public List<DurationParentPanel> CreateEmptyDurationParentPanels(Guna2GradientPanel activityParentPanel)
        {
            List<DurationParentPanel> newDurationParentPanelsWithoutData = new List<DurationParentPanel>();

      
            List<DateOnly> sevenPreviousDays = GetDatesForPreviousSevenDaysDescending();

            foreach (DateOnly localDate in sevenPreviousDays)
            {
                DurationParentPanel durationParentPanel = CreateDurationParentPanelWithSessionContainerPanel(localDate, activityParentPanel);
                newDurationParentPanelsWithoutData.Add(durationParentPanel);
            }

            return newDurationParentPanelsWithoutData;
        }


        private List<DateOnly> GetDatesForPreviousSevenDaysDescending()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);
            return Enumerable.Range(1, 7)
                             .Select(i => today.AddDays(-i))
                             .OrderByDescending(d => d)
                             .ToList();
        }


        public void CreateDateLabel(DurationParentPanel durationParentPanel, DateOnly panelDateLocal)
        {
            var label = new Guna2HtmlLabel()
            {
                Text = ReturnFormattedDateLabelText(panelDateLocal),
                Dock = DockStyle.Left,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                Size = new Size(60, 19),
                ForeColor = Color.FromArgb(176, 176, 176),
                TextAlignment = ContentAlignment.MiddleLeft,
            };
            durationParentPanel.DateLabel = label;
            durationParentPanel.Controls.Add(label);
        }

        public void CreateDurationLabel(DurationParentPanel durationParentPanel, int durationSeconds = 0)
        {
            var label = new Guna2HtmlLabel()
            {
                Text = _utilityService.ConvertDurationSecondsToHHMMStringWithSpace(durationSeconds),
                Dock = DockStyle.Right,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F),
                Size = new Size(50, 19),
                ForeColor = Color.FromArgb(136, 136, 136),
          
                TextAlignment = ContentAlignment.MiddleRight
            };
            durationParentPanel.DurationTotalLabel = label;
            durationParentPanel.Controls.Add(label);
        }

   
        private string ReturnFormattedDateLabelText(DateOnly panelDateLocal)
        {
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Today);
            DateOnly yesterdayDate = todayDate.AddDays(-1);

            if (panelDateLocal == todayDate)
            {
                return "Today";
            }
            else if (panelDateLocal == yesterdayDate)
            {
                return "Yest.";
            }
            else
            {
                return panelDateLocal.ToString("MMM d");
            }
        }
        
    }
}