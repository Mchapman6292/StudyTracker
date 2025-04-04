using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IUtilityServices;
using Guna.UI2.WinForms;

namespace CodingTracker.Business.MainPageService.LabelAssignments
{
    public interface ILabelAssignment
    {
        void UpdateMainPageLabel(Guna2HtmlLabel label, string text);
        Task<string> GetFormattedLabelDisplayMessage(MainPageLabels labelEnum);
    }

    public class LabelAssignment : ILabelAssignment
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ISessionCalculator _sessionCalculator;
        private readonly IUtilityService _utilityService;



        public LabelAssignment(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, ISessionCalculator sessionCalculator, IUtilityService utilityService)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
            _sessionCalculator = sessionCalculator;
            _utilityService = utilityService;
        }




        public void ClearMainPageLabel(Guna2HtmlLabel label)
        {
            label.Text = string.Empty;
        }

        public void UpdateMainPageLabel(Guna2HtmlLabel label, string text)
        {
            ClearMainPageLabel(label);
            label.Text = text;
        }

        public async Task<string> GetFormattedLabelDisplayMessage(MainPageLabels labelEnum)
        {
            string labelMessage = string.Empty;

            switch (labelEnum)
            {
                case MainPageLabels.TodaySessionLabel:
                    double todayTotal = await _codingSessionRepository.GetTodaysTotalDurationAsync();
                    labelMessage = _utilityService.ConvertDurationToHHMM(todayTotal);
                    break;

                case MainPageLabels.AverageSessionLabel:
                    double totalAverage = await _codingSessionRepository.GetAverageDurationOfAllSessionsAsync();
                    labelMessage = _utilityService.ConvertDurationToHHMM(totalAverage);
                    break;

                case MainPageLabels.WeekTotalLabel:
                    double weekTotal = await _codingSessionRepository.GetWeekTotalDurationAsync();
                    labelMessage = _utilityService.ConvertDurationToHHMM(weekTotal);
                    break;
            }
            return labelMessage;
        }

       


    }
}




