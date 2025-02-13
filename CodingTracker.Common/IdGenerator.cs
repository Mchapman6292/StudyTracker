using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.Common.IdGenerators
{
    public interface IIdGenerators
    {
        int GenerateUserId();
        int GenerateSessionId();
    }

    public class IdGenerators : IIdGenerators
    {
        private int lastAssignedUserId = 0;
        private int LastAssignedSessionId = 0;
        private readonly IApplicationLogger _appLogger;

        public IdGenerators(IApplicationLogger appLogger)
        {
            _appLogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
        }

        public int GenerateUserId()
        {
            _appLogger.Info($"Starting {nameof(GenerateUserId)}");
            int newId = ++lastAssignedUserId;
            _appLogger.Info($"LastAssignedUserId updated to {lastAssignedUserId}, new userId = {newId}");
            return newId;
        }

        public int GenerateSessionId()
        {
            _appLogger.Info($"Starting {nameof(GenerateSessionId)}");
            int newId = ++LastAssignedSessionId;
            _appLogger.Info($"LastAssignedSessionId updated to {LastAssignedSessionId}, new userId = {newId}");
            return newId;
        }
    }
}