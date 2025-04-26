using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.IApplicationLoggers;
using Microsoft.EntityFrameworkCore;

namespace CodingTracker.Data.Repositories.UserCredentialRepositories
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingTrackerDbContext _dbContext;

        public UserCredentialRepository(IApplicationLogger appLogger, ICodingTrackerDbContext context)
        {
            _appLogger = appLogger;
            _dbContext = context;
        }

        public async Task<bool> UserIdExistsAsync(int userId)
        {
            return await _dbContext.UserCredentials
                .AnyAsync(u => u.UserId == userId);
        }

        public async Task<UserCredentialEntity?> GetUserCredentialByIdAsync(int userId)
        {
            var userCredential = await _dbContext.UserCredentials
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (userCredential == null)
            {
                throw new KeyNotFoundException($"Usercredential == null, unable to find user or error with data.");
            }
            return userCredential;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _dbContext.UserCredentials
           .AnyAsync(u => u.Username == username);
        }

        public async Task<UserCredentialEntity?> GetUserCredentialByUsernameAsync(string username)
        {
            return await _dbContext.UserCredentials
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> AddUserCredentialAsync(UserCredentialEntity userCredential)
        {
            await _dbContext.UserCredentials.AddAsync(userCredential);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ValidateUserCredentialsAsync(string username, string hashedPassword)
        {
            return await _dbContext.UserCredentials
                .AnyAsync(u => u.Username == username && u.PasswordHash == hashedPassword);
        }

        public async Task<bool> UpdateUserCredentialsAsync(string username, string passwordHash, int userId)
        {
            UserCredentialEntity? user = await GetUserCredentialByIdAsync(userId);

            if (user == null) return false;

            user.Username = username;
            user.PasswordHash = passwordHash;


            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> UpdatePassWord(string username, string hashedPassword)
        {
            UserCredentialEntity? user = await _dbContext.UserCredentials
                    .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return false;

            user.PasswordHash = hashedPassword;

            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _dbContext.UserCredentials
                .Where(u => u.UserId == userId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> DeleteAllUsers()
        {
            return await _dbContext.UserCredentials
                .ExecuteDeleteAsync() > 0;
        }


    }
}