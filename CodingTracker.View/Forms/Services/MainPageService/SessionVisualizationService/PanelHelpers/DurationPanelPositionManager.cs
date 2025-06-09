using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers
{
    public interface IDurationPanelPositionManager
    {
        void SetInitialPosition(List<DurationParentPanel> panels);
        Point GetNextPosition(Size panelSize);
        void ResetPosition(int startingY = 0);
        int GetCurrentYPosition();

        void SetPanelPositionsAfterContainerAdd(List<DurationParentPanel> panels);

        void SetDateLabelPosition(Guna2GradientPanel activityParentPanel, List<DurationParentPanel> durationParentPanels);


    }

    public class DurationPanelPositionManager : IDurationPanelPositionManager
    {
        private int currentYPosition = 0;
        private int panelSpacing = 2;
        private Guna2GradientPanel parentPanel;

        public void SetInitialPosition(List<DurationParentPanel> panels)
        {
            currentYPosition = 0;

            foreach (var panel in panels)
            {
                panel.Location = new Point(0, currentYPosition);
                currentYPosition += panel.Height + panelSpacing;
            }
        }

        public Point GetNextPosition(Size panelSize)
        {
            Point position = new Point(0, currentYPosition);
            currentYPosition += panelSize.Height + panelSpacing;
            return position;
        }

        public void ResetPosition(int startingY = 0)
        {
            currentYPosition = startingY;
        }

        public int GetCurrentYPosition()
        {
            return currentYPosition;
        }

        public void SetPositionsWithoutSpacing(List<DurationParentPanel> panels)
        {
            ResetPosition();
            foreach (var panel in panels)
            {
                panel.Location = new Point(0, currentYPosition);
                currentYPosition += panel.Height;
            }
        }

        public void SetPanelPositionsAfterContainerAdd(List<DurationParentPanel> panels)
        {
            ResetPosition();
            foreach (var panel in panels)
            {
                panel.Location = new Point(0, currentYPosition);
                currentYPosition += panel.Height;
            }
        }

        public void SetDateLabelPosition(Guna2GradientPanel activityParentPanel, List<DurationParentPanel> durationParentPanels)
        {
            foreach(var durationParentPanel in durationParentPanels)
            {
                int labelXposition = activityParentPanel.Width - durationParentPanel.DurationTotalLabel.Width;
            }
;
        }




    }
}
