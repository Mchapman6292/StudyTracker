using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using Microsoft.VisualBasic.ApplicationServices;


namespace CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers
{
    public interface IDurationPanelHelper
    {
        void SetDurationPanelName(DurationPanel panel, DateTime sessionStart, int sessionId);
        void SetParentPanelName(DurationParentPanel panel, DateOnly localDateOnly);
        void CheckAllCodingSessionsSameDate(List<CodingSessionEntity> codingSessionsForOneDay);
    }


    public class DurationPanelHelper : IDurationPanelHelper
    {

        public void SetDurationPanelName(DurationPanel panel, DateTime sessionStartTimeLocal, int sessionId)
        {
            panel.Name = $"DurationPanel_{sessionStartTimeLocal:yyyyMMdd}_{sessionId}";
        }

        public void SetParentPanelName(DurationParentPanel panel, DateOnly localDateOnly)
        {
            panel.Name = $"DurationParentPanel_{localDateOnly:yyyyMMdd}";
        }

        public void CheckAllCodingSessionsSameDate(List<CodingSessionEntity> codingSessionsForOneDay)
        {
            bool allSameDay = codingSessionsForOneDay.Select(s => s.StartDateUTC).Distinct().Count() == 1;

            if (!allSameDay)
            {
                var dates = codingSessionsForOneDay.Select(s => s.StartDateUTC).Distinct().ToList();
                throw new ArgumentException($"CodingSessions for multiple days used in { nameof(CheckAllCodingSessionsSameDate)}. Dates: { string.Join(", ", dates)}.");
            }
        }

    }
}
