using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;

public interface ISessionDurationScaler
{
    void CalculateAndStoreScalingValues(SessionContainerPanel sessionContainerPanel);
}

public class SessionDurationScaler : ISessionDurationScaler
{
    private const float MinDurationSeconds = 300f;
    private const float MaxDurationSeconds = 14400f;
    private const int GapBetweenPanels = 2;
    private const int MinimumPanelWidth = 8;

    public void CalculateAndStoreScalingValues(SessionContainerPanel sessionContainerPanel)
    {
        List<DurationPanel> panels = sessionContainerPanel.ReturnDurationPanelsForOneDay();
        if (panels.Count == 0) return;

        CalculateAndStoreLogRatios(panels);
        sessionContainerPanel.TotalLogRatioUnits = panels.Sum(p => p.LogRatio);
        sessionContainerPanel.AvailableWidthForPanels = CalculateAvailableWidth(sessionContainerPanel);
        sessionContainerPanel.ScaleFactor = sessionContainerPanel.AvailableWidthForPanels / sessionContainerPanel.TotalLogRatioUnits;
        CalculateAndStoreScaledWidths(panels, sessionContainerPanel.ScaleFactor);
        ApplyWidthsToPanels(panels);

        sessionContainerPanel.HasCalculatedScaling = true;
    }

    private void CalculateAndStoreLogRatios(List<DurationPanel> panels)
    {
        float logMin = (float)Math.Log(MinDurationSeconds);
        float logMax = (float)Math.Log(MaxDurationSeconds);

        foreach (var panel in panels)
        {
            float clampedDuration = Math.Max(MinDurationSeconds, Math.Min(MaxDurationSeconds, panel.DurationSeconds));
            float logCurrent = (float)Math.Log(clampedDuration);
            float ratio = (logCurrent - logMin) / (logMax - logMin);
            panel.LogRatio = Math.Max(0.05f, ratio);
        }
    }

    private int CalculateAvailableWidth(SessionContainerPanel container)
    {
        int panelCount = container.ReturnDurationPanelsForOneDay().Count;
        int totalGapWidth = (panelCount - 1) * GapBetweenPanels;
        return container.Width - totalGapWidth - container.Padding.Left - container.Padding.Right;
    }

    private void CalculateAndStoreScaledWidths(List<DurationPanel> panels, float scaleFactor)
    {
        foreach (var panel in panels)
        {
            panel.CalculatedScaledWidth = (int)(panel.LogRatio * scaleFactor);
            panel.FinalAppliedWidth = Math.Max(MinimumPanelWidth, panel.CalculatedScaledWidth);
        }
    }

    private void ApplyWidthsToPanels(List<DurationPanel> panels)
    {
        foreach (var panel in panels)
        {
            panel.Width = panel.FinalAppliedWidth;
        }
    }
}