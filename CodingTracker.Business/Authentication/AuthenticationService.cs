﻿
using CodingTracker.Business.CodingSessionService.UserIdServices;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IUtilityServices;
using System.Diagnostics;


// resetPassword, updatePassword, rememberUser 
namespace CodingTracker.Business.Authentication.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IApplicationLogger _appLogger;
        private readonly IUtilityService _utilityService;
        private readonly UserIdService _userIdService;

        public AuthenticationService(IApplicationLogger appLogger, IUserCredentialRepository userCredentialRepository, IUtilityService utilityService, UserIdService userIdService)
        {
            _appLogger = appLogger;
            _userCredentialRepository = userCredentialRepository;
            _utilityService = utilityService;
            _userIdService = userIdService;
        }


        public async Task<bool> CreateAccount(string username, string password)
        {
            if(await _userCredentialRepository.UsernameExistsAsync(username))
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

        public async Task<bool> AuthenticateLogin(string username, string password, Activity activity)
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

            _userIdService.SetCurrentUserId(loginCredential.UserId);
            return true;
        }




        public async Task<bool> AuthenticateLoginWithoutActivity(string username, string password)
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
            _appLogger.Debug($"User authenticated for {nameof(AuthenticateLoginWithoutActivity)}");     

            return true;
        }


        public async Task<UserCredentialEntity> ReturnUserCredentialIfLoginAuthenticated(bool loginAuthenticated, string username)
        {
            if(loginAuthenticated)
            {
                _appLogger.Debug($"Login authenticated fro {username}.");
                return await _userCredentialRepository.GetUserCredentialByUsernameAsync(username);
            }
            throw new ArgumentException($"loginAuthenticated bool for {nameof(ReturnUserCredentialIfLoginAuthenticated)} loginAuthenticated: {loginAuthenticated}.");
        }




        public async Task<bool> ResetPassword(string username, string newPassword)
        {
            if (! await _userCredentialRepository.UsernameExistsAsync(username))
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
    }
}

