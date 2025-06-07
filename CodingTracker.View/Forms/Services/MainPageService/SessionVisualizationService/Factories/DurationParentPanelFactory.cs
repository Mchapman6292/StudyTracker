using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.Utilities;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using Guna.UI2.WinForms;
using System.Xml.Linq;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories
{
    public interface IDurationParentPanelFactory
    {
        DurationParentPanel CreateDurationParentPanelWithSessionContainerPanel(DateOnly panelDateLocal);
    }

    public class DurationParentPanelFactory : IDurationParentPanelFactory
    {
        private Size ParentPanelSize = new Size(513, 35);
        private Size DatePanelSize = new Size(443, 19);

        private readonly IDurationPanelHelper _durationPanelHelper;
        private readonly IUtilityService _utilityService;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;

        public DurationParentPanelFactory(IDurationPanelHelper durationPanelHelper, IUtilityService utilityService, ISessionContainerPanelFactory sessionContainerPanelFactory)
        {
            _durationPanelHelper = durationPanelHelper;
            _utilityService = utilityService;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
        }

        public DurationParentPanel CreateDurationParentPanelWithSessionContainerPanel(DateOnly panelDateLocal )
        {
            

            DurationParentPanel durationParentPanel = new DurationParentPanel()
            {
                Size = ParentPanelSize,
                BackColor = Color.Transparent,
                FillColor = Color.Transparent,
                ForeColor = SystemColors.ControlText,
                Padding = new Padding(15, 8, 15, 8),
                SessionContainerPanel = _sessionContainerPanelFactory.CreateSessionContainerPanel(panelDateLocal)

            };

            _durationPanelHelper.SetParentPanelName(durationParentPanel, panelDateLocal);

            CreateDateLabel(durationParentPanel, panelDateLocal);

            int totalDurationPanelSeconds = durationParentPanel.SumDurationPanelSeconds();
            CreateDurationLabel(durationParentPanel, totalDurationPanelSeconds);

            return durationParentPanel;

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