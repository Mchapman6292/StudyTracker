using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers
{
    public interface ISessionVisualizationController
    {
        Task CreateAllVisualPanelsAsync(Guna2GradientPanel displayPanel);
        List<DateOnly> GetDatesForPreviousSevenDaysDescending();
        void UpdateAllDurationLabels(Guna2GradientPanel displayPanel);
    }

    public class SessionVisualizationController : ISessionVisualizationController
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IDurationParentPanelFactory _durationParentPanelFactory;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;
        private readonly IDurationPanelFactory _durationPanelFactory;
        private readonly IApplicationLogger _appLogger;

        public SessionVisualizationController(
            ICodingSessionRepository codingSessionRepository,
            IDurationParentPanelFactory durationParentPanelFactory,
            ISessionContainerPanelFactory sessionContainerPanelFactory,
            IDurationPanelFactory durationPanelFactory,
            IApplicationLogger appLogger)
        {
            _codingSessionRepository = codingSessionRepository;
            _durationParentPanelFactory = durationParentPanelFactory;
            _sessionContainerPanelFactory = sessionContainerPanelFactory;
            _durationPanelFactory = durationPanelFactory;
            _appLogger = appLogger;
        }

        /*
        public async Task CreateAllVisualPanelsAsync(Guna2GradientPanel displayPanel)
        {
            try
            {
                displayPanel.Controls.Clear();

                List<DateOnly> dates = GetDatesForPreviousSevenDaysDescending();

                foreach (DateOnly date in dates)
                {
                    List<CodingSessionEntity> sessionsForDate = await _codingSessionRepository.GetAllCodingSessionsByDateOnlyForStartDateAsync(date);

                    if (!sessionsForDate.Any()) continue;

                    CodingSessionEntity firstSession = sessionsForDate.First();
                    DurationParentPanel parentPanel = _durationParentPanelFactory.CreateDurationParentPanelWithSessionContainerPanel(firstSession, date);

                    SessionContainerPanel containerPanel = _sessionContainerPanelFactory.CreateSessionContainerPanel();
                    parentPanel.AddSessionContainerPanel(containerPanel);

                    List<DurationPanel> durationPanels = _durationPanelFactory.CreateMultipleDurationPanelsForOneDaySortedByStartTime(sessionsForDate);
                    containerPanel.AddListOfDurationPanels(durationPanels);

                    parentPanel.UpdateDurationTotalLabel();

                    displayPanel.Controls.Add(parentPanel);
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in {nameof(CreateAllVisualPanelsAsync)}: {ex.Message}");
                throw;
            }
        }
        */

        public async Task CreateAllVisualPanelsAsync(Guna2GradientPanel displayPanel)
        {
            CreateAllDurationParentPanelsAndAddtoDisplayPanel(displayPanel);

        }


        private async Task<Dictionary<DateOnly, List<CodingSessionEntity>>  GetCodingSessionEntityForPreviousSevenDaysAsync(Guna2GradientPanel displayPanel)
        {
            List<DateOnly> dates = GetDatesForPreviousSevenDaysDescending();

            Dictionary<DateOnly, List<CodingSessionEntity>> sessionsByDate = await _codingSessionRepository.GetSessionsGroupedByDateLastSevenDays();

            List<DurationParentPanel> emptyDurationParentPanels = ReturnAllDurationParentPanels(displayPanel);

            foreach (DurationParentPanel durationParentPanel in emptyDurationParentPanels)
            {
                DateOnly panelDate = durationParentPanel.PanelDateLocal;

                if (sessionsByDate.ContainsKey(panelDate))
                {
                    List<CodingSessionEntity> matchingSessions = sessionsByDate[panelDate];

                }
            }
        }

        private List<DateOnly> GetDatesForPreviousSevenDaysDescending()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);
            return Enumerable.Range(1, 7)
                             .Select(i => today.AddDays(-i))
                             .OrderByDescending(d => d)
                             .ToList();
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


        private void CreateAllDurationParentPanelsAndAddtoDisplayPanel(Guna2GradientPanel displayPanel)
        {
            List<DateOnly> sevenPreviousDays = GetDatesForPreviousSevenDaysDescending();

            foreach(DateOnly date in sevenPreviousDays)
            {
                DurationParentPanel durationParentPanel = _durationParentPanelFactory.CreateDurationParentPanelWithSessionContainerPanel(date);
                displayPanel.Controls.Add(durationParentPanel);
            }
            int totalPanels = displayPanel.Controls.OfType<DurationParentPanel>().Count();
            _appLogger.Info($"Number of DurationParentPanels in displayPanel after {nameof(CreateAllDurationParentPanelsAndAddtoDisplayPanel)}: {totalPanels}");
        }


  
    }
}