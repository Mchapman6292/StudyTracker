using CodingTracker.Common.BusinessInterfaces.CodingSessionService;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.LoggingInterfaces;

namespace CodingTracker.Business.CodingSessionService.AdminModeHandlers
{
    public class AdminModeHandler : IAdminModeHandler
    {
        public bool AdminModeEnabled { get; private set; } = false;

        private readonly IApplicationLogger _appLogger;

        public AdminModeHandler(IApplicationLogger appLogger) 
        {
            _appLogger = appLogger;
        }
        

        public void UpdateAdminModeEnabled(bool adminModeEnabled)
        {
            AdminModeEnabled = adminModeEnabled;
        }

        public bool IsAdminModeEnabled()
        {
            return AdminModeEnabled;
        }

        public bool IsAdminUserCredential(UserCredentialEntity userCredential)
        {
            if (userCredential.Username == "Admin" && userCredential.UserId == 11)
            {
                return true;
            }
            return false;
        }


    }
}
