using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.Business.CodingSessionService.UserIdServices
{
    public class UserIdService : IUserIdService
    {
        private readonly IApplicationLogger _appLogger;
        private  int _currentUserId {  get; set; }  


        public UserIdService(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }

        public int GetCurrentUserId()
        {
            return _currentUserId;
        }

        public void SetCurrentUserId(int userId)
        {
            _currentUserId = userId;
        }


    }
}
