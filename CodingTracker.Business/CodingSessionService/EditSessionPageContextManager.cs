using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers
{
    public class EditSessionPageContextManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly HashSet<int> _sessionIdsForDeletion;
  


        public EditSessionPageContextManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
            _sessionIdsForDeletion = new HashSet<int>();
        }

        public void AddSessionIdForDeletion(int sessionId)
        {
            _sessionIdsForDeletion.Add(sessionId);
        }

        public void RemoveSessionIdForDeletion(int sessionId)
        {
            _sessionIdsForDeletion.Remove(sessionId);
        }

        public void ClearSessionIdsForDeletion()
        {
            _sessionIdsForDeletion.Clear();
        }

        public bool CheckForSessionId(int sessionId)
        {
            _appLogger.Debug($"Checking for sessionId{sessionId} for {nameof(CheckForSessionId)}.");
            return _sessionIdsForDeletion.Contains(sessionId);
        }

        public HashSet<int> GetSessionIdsForDeletion()
        {
            return _sessionIdsForDeletion;
        }

   







    }
}
