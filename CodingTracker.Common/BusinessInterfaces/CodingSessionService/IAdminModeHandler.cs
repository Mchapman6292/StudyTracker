

using CodingTracker.Common.Entities.UserCredentialEntities;

namespace CodingTracker.Common.BusinessInterfaces.CodingSessionService
{
    public interface IAdminModeHandler
    {
        void UpdateAdminModeEnabled(bool adminModeEnabled);
        bool IsAdminModeEnabled();
        bool IsAdminUserCredential(UserCredentialEntity userCredential);
    }
}
