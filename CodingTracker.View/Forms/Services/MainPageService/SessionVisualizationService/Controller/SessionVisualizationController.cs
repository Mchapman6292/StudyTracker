using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers
{
    public interface ISessionVisualizationController
    {
        Task CreateAllVisualPanelsAsync(Guna2GradientPanel displayPanel);
        void UpdateAllDurationLabels(Guna2GradientPanel displayPanel);
        Task<List<DurationParentPanel>> CreateDurationParentPanelsWithDataAsync(Guna2GradientPanel activityParentPanel);
        void LogDurationParentPanel(DurationParentPanel durationParentPanel);
    }

    public class SessionVisualizationController : ISessionVisualizationController
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IDurationParentPanelFactory _durationParentPanelFactory;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;
        private readonly IDurationPanelFactory _durationPanelFactory;
        private readonly IApplicationLogger _appLogger;
        private readonly IDurationParentPanelPositionManager _durationPanelPositionManager;
        private readonly ISessionDurationScaler _sessionDurationScaler;

        public SessionVisualizationController(ICodingSessionRepository codingSessionRepository, IDurationParentPanelFactory durationParentPanelFactory, ISessionContainerPanelFactory sessionContainerPanelFactory, IDurationPanelFactory durationPanelFactory, IApplicationLogger appLogger, IDurationParentPanelPositionManager durationPanelPositionManager, ISessionDurationScaler sessionDurationScaler)
        {
            _codingSessionRepository = codingSessionRepository;
            _durationParentPanelFactory = durationParentPanelFactory;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
            _durationPanelFactory = durationPanelFactory;
            _appLogger = appLogger;
            _durationPanelPositionManager = durationPanelPositionManager;
            _sessionDurationScaler = sessionDurationScaler;
        }

 

        public async Task CreateAllVisualPanelsAsync(Guna2GradientPanel displayPanel)
        {
      

        }

        public async Task<List<DurationParentPanel>> CreateDurationParentPanelsWithDataAsync(Guna2GradientPanel activityParentPanel)
        {
            // Create empty durationParentPanel
            List<DurationParentPanel> newDurationParentPanelsWithoutData = _durationParentPanelFactory.CreateEmptyDurationParentPanels(activityParentPanel);

            _durationPanelPositionManager.SetInitialPosition(newDurationParentPanelsWithoutData);


            // Get sessions last 7 days
            Dictionary<DateOnly, List<CodingSessionEntity>> sessionsByDate = await _codingSessionRepository.GetSessionsGroupedByDateLastSevenDays();

            // Find the durationParentPanels by searching the Dates in sessionsByDate
            foreach (DurationParentPanel durationParentPanel in newDurationParentPanelsWithoutData)
            {
                DateOnly panelDate = durationParentPanel.PanelDateLocal;

                if (sessionsByDate.ContainsKey(panelDate))
                {
                    // Found all of the sessions for that date, now need to create the correct panels and add to the controls.
                    List<CodingSessionEntity> matchingSessions = sessionsByDate[panelDate];
                    SessionContainerPanel sessionContainerPanel = durationParentPanel.SessionContainerPanel;

                    durationParentPanel.SessionContainerPanel.AddToCodingSessionsForOneDay(matchingSessions);
                    List<DurationPanel> durationPanelsForDate = _durationPanelFactory.CreateMultipleDurationPanelsForOneDaySortedByStartTime(matchingSessions);
                    sessionContainerPanel.AddListOfDurationPanels(durationPanelsForDate);

                    _sessionDurationScaler.CalculateAndStoreScalingValues(sessionContainerPanel);
                }
                else
                {
                    durationParentPanel.SessionContainerPanel.AddToCodingSessionsForOneDay(new List<CodingSessionEntity>());
                }

                durationParentPanel.UpdateDurationTotalLabel();
                LogDurationParentPanel(durationParentPanel);
            }

            return newDurationParentPanelsWithoutData;
        }


        private async Task CreateDurationPanelsForSessions(SessionContainerPanel containerPanel, List<CodingSessionEntity> sessions)
        {
            if (!sessions.Any()) return;

            List<DurationPanel> durationPanels = _durationPanelFactory
                .CreateMultipleDurationPanelsForOneDaySortedByStartTime(sessions);

            containerPanel.AddListOfDurationPanels(durationPanels);
        }


        public List<DurationParentPanel> ReturnAllDurationParentPanels(Guna2GradientPanel displayPanel)
        {
            return displayPanel.Controls.OfType<DurationParentPanel>().ToList();
        }


        public void UpdateAllDurationLabels(Guna2GradientPanel displayPanel)
        {
            List<DurationParentPanel> durationParentPanels = ReturnAllDurationParentPanels(displayPanel);

            int logCount = 0;

            foreach (var durationPanel in durationParentPanels)
            {
                durationPanel.UpdateDurationTotalLabel();
                logCount++;
            }

            _appLogger.Info($"Nunber of durationPanel labels updated: {logCount} during {nameof(UpdateAllDurationLabels)}");
        }



        public void LogDurationParentPanel(DurationParentPanel durationParentPanel)
        {
            string panel =
                $"Values for DurationParentPanel \n" +
                $"-------DateLabel : {durationParentPanel.DateLabel?.Text}.\n" +
                $"-------DurationTotalLabel : {durationParentPanel.DurationTotalLabel?.Text}.\n" +
                $"-------PanelDateLocal : {durationParentPanel.PanelDateLocal}.\n" +
                $"-------TotalDurationSeconds : {durationParentPanel.TotalDurationSeconds}.\n" +
                $"-------SessionDateLocal : {durationParentPanel.SessionContainerPanel?.SessionDateLocal}";

            _appLogger.Info(panel);
        }




    }
}