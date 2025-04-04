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
        Task UpdateTodayLabel(Guna2HtmlLabel TodaySessionLabel);
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


        public async Task UpdateTodayLabel(Guna2HtmlLabel TodaySessionLabel)
        {
            if(! await _codingSessionRepository.CheckTodayCodingSessionsAsync())
            {
                TodaySessionLabel.Text = "Today's Total: 0";
                return;
            }
            double todayTotal = await _sessionCalculator.GetTodayTotalSession();
            TodaySessionLabel.Text = $"Today's Total: {todayTotal}";
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

        public async string GetCodingSessionDataForLabelAsync(MainPageLabels labelEnum)
        {
            switch (labelEnum)
            {
                case MainPageLabels.TodaySessionLabel:
                    double 
            }
        }


        

    }
}
