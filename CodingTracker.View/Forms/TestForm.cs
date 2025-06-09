using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.MainPageService.DoughnutSegments;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;

namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;
        private readonly IDurationPanelFactory _durationPanelFactory;
        private readonly IDurationParentPanelFactory _durationParentPanelFactory;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ISessionVisualizationController _sessionVisualizationController;
        private readonly IPanelColourAssigner _panelColorAssigner;
        private readonly ILast28DayPanelSettings _last28DayPanelSettings;
        private readonly ILabelAssignment _labelAssignment;
        private readonly IDurationPanelPositionManager _durationPanelPositionManager;







        public TestForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, IDurationParentPanelFactory durationParentPanelFactory, IDurationPanelFactory durationPanelFactory, ICodingSessionRepository codingSessionRepository, ISessionContainerPanelFactory sessionContainerPanelFactory, ISessionVisualizationController sessionVisualizationController, ILabelAssignment labelAssignment, IDurationPanelPositionManager durationPanelPositionManager)
        {
            InitializeComponent();
            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;
            _durationParentPanelFactory = durationParentPanelFactory;
            _durationPanelFactory = durationPanelFactory;
            _codingSessionRepository = codingSessionRepository;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
            _sessionVisualizationController = sessionVisualizationController;
            _labelAssignment = labelAssignment;
            _durationPanelPositionManager = durationPanelPositionManager;
            


            this.Load += TestForm_Load;
            this.Shown += TestForm_Shwon;
            _labelAssignment = labelAssignment;
        }

        private async void TestForm_Load(object sender, EventArgs e)
        {


        }

        private async void TestForm_Shwon(object sender, EventArgs e)
        {
            await InitializeTestPanelAsync();


        }


        private async Task InitializeTestPanelAsync()
        {
            List<DurationParentPanel> dppList = await _sessionVisualizationController.CreateDurationParentPanelsWithDataAsync();

            if (dppList.Count > 0)
            {
                foreach (var dpp in dppList)
                {
                    _sessionVisualizationController.LogDurationParentPanel(dpp);
                    testAcitivtyControllerPanel.Controls.Add(dpp);

  
                }
            }
            _durationPanelPositionManager.SetPanelPositionsAfterContainerAdd(dppList);


        }



        private void LogControlZOrder(Control container, string containerName)
        {
            var message = $"{containerName} contains {container.Controls.Count} controls:\n";

            for (int i = 0; i < container.Controls.Count; i++)
            {
                var control = container.Controls[i];
                message += $"  [{i}] {control.GetType().Name} - {control.Name}\n";
            }

            _notificationManager.ShowNotificationDialog(this, message);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            CheckSessionInParent();

        }

        private void CheckSessionInParent()
        {
            int sessionTrue = 0;
            var dppList = testAcitivtyControllerPanel.Controls.OfType<DurationParentPanel>().ToList();

            foreach (var dpp in dppList)
            {
                bool inPanel = dpp.SessionContainerPanel.ReturnIsInDurationParentPanelControls();

                dpp.BackColor = Color.Green;
                dpp.ForeColor = Color.Green;

                if (inPanel)
                {
                    sessionTrue++;
                }
            }
            _notificationManager.ShowNotificationDialog(this, $"SessionContainers in ParentPanels: {sessionTrue}");
        }



    }
}