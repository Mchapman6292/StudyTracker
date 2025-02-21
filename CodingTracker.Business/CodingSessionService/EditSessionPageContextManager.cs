using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.CommonEnums;

namespace CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers
{
    public class EditSessionPageContextManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly HashSet<int> _sessionIdsForDeletion;
        private readonly ICodingSessionRepository _codingSessionRepository;



        public EditSessionPageContextManager(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
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

        public async Task<int> DeleteSessionsInSessionIdsForDeletion()
        {
           return await _codingSessionRepository.DeleteSessionsByIdAsync(_sessionIdsForDeletion);
        }











    }
}
