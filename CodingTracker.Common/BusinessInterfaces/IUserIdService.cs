namespace CodingTracker.Common.BusinessInterfaces
{
    public interface IUserIdService
    {
        void SetCurrentUserId(int userId);
        int GetCurrentUserId();
    }
}
