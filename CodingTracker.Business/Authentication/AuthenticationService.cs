
using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.LoggingInterfaces;
using System.Diagnostics;


// resetPassword, updatePassword, rememberUser 
namespace CodingTracker.Business.Authentication.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IApplicationLogger _appLogger;
        private readonly IUtilityService _utilityService;
        private readonly ICodingSessionManager _codingSessionManager;

        private string _usernameForPasswordReset { get; set; }

        public AuthenticationService(IApplicationLogger appLogger, IUserCredentialRepository userCredentialRepository, IUtilityService utilityService, ICodingSessionManager codingSessionManager)
        {
            _appLogger = appLogger;
            _userCredentialRepository = userCredentialRepository;
            _utilityService = utilityService;
            _codingSessionManager = codingSessionManager;
        }

        public void SetUsernameForPasswordReset(string username)
        {
            _usernameForPasswordReset = username;
            _appLogger.Debug($"{nameof(_usernameForPasswordReset)} set to {_usernameForPasswordReset}.");
        }

        public string GetUsernameForPasswordReset()
        {
            return _usernameForPasswordReset;
        }


        public async Task<bool> CreateAccount(string username, string password)
        {
            if (await _userCredentialRepository.UsernameExistsAsync(username))
            {
                _appLogger.Error($"Username already exists.");
                return false;
            }

            UserCredentialEntity user = new UserCredentialEntity
            {
                Username = username,
                PasswordHash = _utilityService.HashPassword(password),
                LastLogin = DateTime.UtcNow,

            };

            return await _userCredentialRepository.AddUserCredentialAsync(user);

        }




        public async Task<bool> AuthenticateLogin(string username, string password)
        {
            bool usernameExist = await _userCredentialRepository.UsernameExistsAsync(username);

            if (!usernameExist)
            {
                _appLogger.Info($"No username exists for {username}");
                return false;
            }

            _appLogger.Debug($"Username found for {username}.");



            UserCredentialEntity loginCredential = await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);


            var inputHash = _utilityService.HashPassword(password);
            var storedHash = loginCredential.PasswordHash;

            bool isValid = inputHash.Equals(storedHash, StringComparison.Ordinal);

            if (!isValid)
            {
                _appLogger.Debug($"Incorrect passwordHash.");
                return false;
            }
            _appLogger.Debug($"User authenticated for {nameof(AuthenticateLogin)}");

            return true;
        }


        public async Task<UserCredentialEntity> ReturnUserCredentialIfLoginAuthenticated(bool loginAuthenticated, string username)
        {
            if (loginAuthenticated)
            {
                _appLogger.Debug($"Login authenticated fro {username}.");
                return await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);
            }
            throw new ArgumentException($"loginAuthenticated bool for {nameof(ReturnUserCredentialIfLoginAuthenticated)} loginAuthenticated: {loginAuthenticated}.");
        }




        public async Task<bool> ResetPassword(string username, string newPassword)
        {
            if (!await _userCredentialRepository.UsernameExistsAsync(username))
            {
                _appLogger.Info($"Password reset failed: No user found for username {username}");
                return false;
            }

            string hashedPassword = _utilityService.HashPassword(newPassword);

            return await _userCredentialRepository.UpdatePassWord(username, hashedPassword);

        }



        public async Task DeleteAllUserCredential()
        {
            bool success = await _userCredentialRepository.DeleteAllUsers();
            string logMessage = success ? "All UserCredentials deleted" : "Failure to delete UserCredentials.";
            _appLogger.Debug(logMessage);
        }



        public bool CheckPasswordValid(string password, out string? message)
        {
            if (!password.Any(c => Char.IsUpper(c)))
            {
                message = "Password must contain at least one capital.";
                return false;
            }

            if (password.Length < 7)
            {
                message = "Password length must be greater than 7";
                return false;
            }

            message = "Password validated";
            return true;
        }
    }
}

