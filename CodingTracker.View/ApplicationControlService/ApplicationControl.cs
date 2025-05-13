using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.ApplicationControlService.DurationManagers;

namespace CodingTracker.View.ApplicationControlService
{


    public class ApplicationControl : IApplicationControl
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingTrackerDbContext _context;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IDurationManager _durationManager;



        public ApplicationControl(IApplicationLogger appLogger, ICodingTrackerDbContext entityContext, ICodingSessionManager codingSessionManager, IDurationManager durationManager)
        {
            _appLogger = appLogger;
            _context = entityContext;
            _codingSessionManager = codingSessionManager;
            _durationManager = durationManager;
        }

        public async Task ExitCodingTrackerAsync()
        {
            // By the time we get here, there should be no active sessions
            // or if there is one, the user must have explicitly chosen to discard it
            
            Application.Exit();
        }



    }
}