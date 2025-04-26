namespace CodingTracker.Common.IApplicationControls
{
    public interface IApplicationControl
    {
        void StartApplication();
        Task ExitCodingTrackerAsync();
    }
}
