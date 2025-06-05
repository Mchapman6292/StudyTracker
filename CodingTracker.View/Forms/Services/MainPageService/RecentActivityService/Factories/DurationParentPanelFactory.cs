using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.Utilities;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories.PanelHelpers;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using Guna.UI2.WinForms;
using System.Xml.Linq;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories
{
    public interface IDurationParentPanelFactory
    {
        DurationParentPanel CreateDurationParentPanel(CodingSessionEntity codingSessionEntity, DateOnly date);
    }

    public class DurationParentPanelFactory : IDurationParentPanelFactory
    {
        // Panel that contains all tools in one row include both labels.
        private Size ParentPanelSize = new Size(513, 35);

        //Contains the dateLAbel, all smaller durationPanels extends until just before testParentPanel.
        private Size DatePanelSize = new Size(443, 19);

        private readonly IDurationPanelHelper _durationPanelHelper;
        private readonly IUtilityService _utilityService;

        public DurationParentPanelFactory(IDurationPanelHelper durationPanelHelper, IUtilityService utilityService)
        {
            _durationPanelHelper = durationPanelHelper;
            _utilityService = utilityService;
        }



        public DurationParentPanel CreateDurationParentPanel(CodingSessionEntity codingSessionEntity, DateOnly panelDateLocal)
        {
            DurationParentPanel durationParentPanel = new DurationParentPanel()
            {

                Size = ParentPanelSize,
                BackColor = Color.Transparent,
                FillColor = Color.Transparent,
                ForeColor = SystemColors.ControlText,
                Padding = new Padding(15, 8, 15, 8),


            };

            _durationPanelHelper.SetParentPanelName(durationParentPanel, codingSessionEntity.StartDateLocal);
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
                Location = new Point(15, 8),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                Size = new Size(36, 19),
                ForeColor = Color.FromArgb(176, 176, 176)
            };
            durationParentPanel.DateLabel = label;
            durationParentPanel.Controls.Add(label);
        }

        public void CreateDurationLabel(DurationParentPanel durationParentPanel, int durationSeconds)
        {
            var label = new Guna2HtmlLabel()
            {
                Text = _utilityService.ConvertDurationSecondsToHHMMStringWithSpace(durationSeconds),
                Location = new Point(15, 8),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                Size = new Size(36, 19),
                ForeColor = Color.FromArgb(176, 176, 176)
            };
            durationParentPanel.DurationLabel = label;
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
                return "Yest";
            }

            else
            {
                return panelDateLocal.ToString("MMM d");
            }
        }



    }
}
