
using CodingTracker.Common.Entities.UserCredentialEntities;
using System.Diagnostics;

namespace CodingTracker.Common.BusinessInterfaces.IAuthenticationServices
{
    public interface IAuthenticationService
    {
        void SetUsernameForPasswordReset(string username);
        string GetUsernameForPasswordReset();
        Task<bool> CreateAccount(string username, string password);
        Task<bool> AuthenticateLogin(string username, string password, Activity activity);

        Task<bool> AuthenticateLoginWithoutActivity(string username, string password);

        Task<UserCredentialEntity> ReturnUserCredentialIfLoginAuthenticated(bool loginAuthenticated, string username);

        Task<bool> ResetPassword(string username, string newPassword);

        Task DeleteAllUserCredential();

        bool CheckPasswordValid(string password, out string? message);


    }
}