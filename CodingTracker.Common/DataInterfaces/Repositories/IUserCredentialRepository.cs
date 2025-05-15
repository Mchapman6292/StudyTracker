using CodingTracker.Common.Entities.UserCredentialEntities;

namespace CodingTracker.Common.DataInterfaces.Repositories
{
    public interface IUserCredentialRepository
    {
        Task<bool> UserIdExistsAsync(int userId);
        Task<UserCredentialEntity?> GetUserCredentialByIdAsync(int userId);
        Task<bool> UsernameExistsAsync(string username);
        Task<UserCredentialEntity?> GetUserCredentialByUsernameAsync(string username);
        Task<bool> AddUserCredentialAsync(UserCredentialEntity userCredential);
        Task<bool> ValidateUserCredentialsAsync(string username, string hashedPassword);
        Task<bool> UpdateUserCredentialsAsync(string username, string passwordHash, int userId);
        Task<bool> UpdatePassWord(string username, string hashedPassword);
        Task<bool> DeleteUserAsync(int userId);

        Task<bool> DeleteAllUsers();
    }
}
