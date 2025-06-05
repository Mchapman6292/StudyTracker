using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;


namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories.PanelHelpers
{
    public interface IDurationPanelHelper
    {
        void SetDurationPanelName(DurationPanel panel, DateTime sessionStart, int sessionId);
        void SetParentPanelName(DurationParentPanel panel, DateOnly localDateOnly);
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

    }
}
