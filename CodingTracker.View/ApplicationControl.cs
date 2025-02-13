using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using System.Diagnostics;

namespace CodingTracker.Business.ApplicationControls
{
    public class ApplicationControl : IApplicationControl
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingTrackerDbContext _context;
        private readonly ICodingSessionManager _codingSessionManager;
        public bool ApplicationIsRunning { get; private set; }



        public ApplicationControl(IApplicationLogger appLogger, ICodingTrackerDbContext entityContext, ICodingSessionManager codingSessionManager)
        {
            ApplicationIsRunning = false; // Set to false instead of true to ensure that processes don't run or exit prematurely or unintentionally.
            _appLogger = appLogger;
            _context = entityContext;
            _codingSessionManager = codingSessionManager;
        }

        public void StartApplication()
        {
            ApplicationIsRunning = true;
        }

        public async Task ExitApplicationAsync()
        {
            using var activity = new Activity(nameof(ExitApplicationAsync)).Start();
            _appLogger.Info($"Starting {nameof(ExitApplicationAsync)}. TraceID: {activity.TraceId}");

            var stopwatch = Stopwatch.StartNew();

            try
            {
                if (_codingSessionManager.CheckIfCodingSessionActive())
                {
                    await _codingSessionManager.EndCodingSessionAsync();
                    _appLogger.Info($"Active coding session saved. TraceID: {activity.TraceId}");
                }

                await _context.SaveChangesAsync();

                stopwatch.Stop();
                _appLogger.Info($"{nameof(ExitApplicationAsync)} completed. Execution Time: {stopwatch.ElapsedMilliseconds}ms. TraceID: {activity.TraceId}");

                Application.Exit();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _appLogger.Error($"An error occurred during {nameof(ExitApplicationAsync)}. Error: {ex.Message}. Execution Time: {stopwatch.ElapsedMilliseconds}ms. TraceID: {activity.TraceId}", ex);
            }
        }
    }
}