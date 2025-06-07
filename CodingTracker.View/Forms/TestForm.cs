using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.View.Forms.Services.MainPageService.DoughnutSegments;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

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


    




        public TestForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, IDurationParentPanelFactory durationParentPanelFactory, IDurationPanelFactory durationPanelFactory, ICodingSessionRepository codingSessionRepository, ISessionContainerPanelFactory sessionContainerPanelFactory, ISessionVisualizationController sessionVisualizationController)
        {
            InitializeComponent();
            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;
            _durationParentPanelFactory = durationParentPanelFactory;
            _durationPanelFactory = durationPanelFactory;
            _codingSessionRepository = codingSessionRepository;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
            _sessionVisualizationController = sessionVisualizationController;


            this.Load += TestForm_Load;
        }

        private async void TestForm_Load(object sender, EventArgs e)
        {
            await InitializeTestPanelAsync();
        }




        private async Task InitializeTestPanelAsync()
        {
            try
            {
                await _sessionVisualizationController.CreateAllVisualPanelsAsync(testAcitivtyControllerPanel);

                _sessionVisualizationController.UpdateAllDurationLabels(testAcitivtyControllerPanel);

            }
            catch (Exception ex)
            {
                _notificationManager.ShowNotificationDialog(this, $"Error initializing test panel: {ex.Message}");
            }
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


    }
}