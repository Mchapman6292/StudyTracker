using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.Business.ApplicationControls
{


    public class ApplicationControl : IApplicationControl
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingTrackerDbContext _context;
        private readonly ICodingSessionManager _codingSessionManager;



        public ApplicationControl(IApplicationLogger appLogger, ICodingTrackerDbContext entityContext, ICodingSessionManager codingSessionManager)
        {
            _appLogger = appLogger;
            _context = entityContext;
            _codingSessionManager = codingSessionManager;
        }

        public async Task ExitCodingTrackerAsync()
        {
            bool codingSessionActive = _codingSessionManager.ReturnIsCodingSessionActive();

            if (!codingSessionActive)
            {
                Application.Exit();
                return;
            }
            await _codingSessionManager.EndCodingSessionAsync();
            Application.Exit();

        }
    }
}