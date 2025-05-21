using CodingTracker.Common.Entities.UserCredentialEntities;

namespace CodingTracker.Common.BusinessInterfaces.Authentication
{
    public interface IAuthenticationService
    {
        void SetUsernameForPasswordReset(string username);
        string GetUsernameForPasswordReset();
        Task<bool> CreateAccount(string username, string password);

        Task<bool> AuthenticateLogin(string username, string password);


        Task<UserCredentialEntity> ReturnUserCredential(string username);

        Task<bool> ResetPassword(string username, string newPassword);

        Task DeleteAllUserCredential();

        bool ValidatePasswordAndReturnErrorMessage(string password, out string? message);


    }
}