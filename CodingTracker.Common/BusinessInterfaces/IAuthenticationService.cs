
using CodingTracker.Common.Entities.UserCredentialEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Common.BusinessInterfaces.IAuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<bool> CreateAccount(string username, string password);
        Task<bool> AuthenticateLogin(string username, string password, Activity activity);

        Task<bool> AuthenticateLoginWithoutActivity(string username, string password);

        Task<UserCredentialEntity> ReturnUserCredentialIfLoginAuthenticated(bool loginAuthenticated, string username);

        Task<bool> ResetPassword(string username, string newPassword);

        Task DeleteAllUserCredential();


    }
}