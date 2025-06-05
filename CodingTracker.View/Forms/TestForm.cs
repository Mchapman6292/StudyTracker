using CodingTracker.View.Forms.Services.SharedFormServices;
using System.Drawing.Drawing2D;
using CodingTracker.View.Forms.Services.MainPageService.DoughnutSegments;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;

namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;
        private readonly IDurationPanelFactory _durationPanelFactory;
        private readonly IDurationParentPanelFactory _durationParentPanelFactory;
        private readonly ICodingSessionRepository _codingSessionRepository;

        public DurationParentPanel testParentPanel;
    



        public TestForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager, IDurationParentPanelFactory durationParentPanelFactory, IDurationPanelFactory durationPanelFactory, ICodingSessionRepository codingSessionRepository)
        {
            InitializeComponent();
            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;
            _durationParentPanelFactory = durationParentPanelFactory;
            _durationPanelFactory = durationPanelFactory;
            _codingSessionRepository = codingSessionRepository;


            this.Load += TestForm_Load;
        }

        private async void TestForm_Load(object sender, EventArgs e)
        {
            await InitializeTestPanelAsync();
        }


        private void TestForm_Shown(object sender, EventArgs e)
        {
            testParentPanel.DurationLabel.BringToFront();
            testParentPanel.DateLabel.BringToFront();
        }


        private async Task InitializeTestPanelAsync()
        {
            try
            {
                DateOnly testDate = new DateOnly(2025, 5, 25);
                List<CodingSessionEntity> allSessionsFromTestDate = await _codingSessionRepository.GetAllCodingSessionsByDateOnlyForStartDateAsync(testDate);
                CodingSessionEntity testEntity = allSessionsFromTestDate.FirstOrDefault(e => e != null);

                if (testEntity == null)
                {
                    _notificationManager.ShowNotificationDialog(this, "No test entity found for the specified date.");
                    return;
                }

                testParentPanel = _durationParentPanelFactory.CreateDurationParentPanel(testEntity, testDate);
                DurationPanel durationPanel = _durationPanelFactory.CreateDurationPanel(testEntity, testParentPanel);

                this.Controls.Add(testParentPanel);
                testParentPanel.Controls.Add(durationPanel);

                BringLabelsToFront();

                if (this.Controls.Contains(testParentPanel))
                {
                    _notificationManager.ShowNotificationDialog(this, "testPanel added.");
                }
                else
                {
                    _notificationManager.ShowNotificationDialog(this, "Not added.");
                }

                testParentPanel.BringToFront();
                testParentPanel.Location = new Point(650, 350);
            }
            catch (Exception ex)
            {
                _notificationManager.ShowNotificationDialog(this, $"Error initializing test panel: {ex.Message}");
            }
        }

        private void BringLabelsToFront()
        {
            if (testParentPanel != null)
            {
                if (testParentPanel.DurationLabel != null)
                {
                    testParentPanel.DurationLabel.BringToFront();
                }

                if (testParentPanel.DateLabel != null)
                {
                    testParentPanel.DateLabel.BringToFront();
                }
            }
        }


    }
}