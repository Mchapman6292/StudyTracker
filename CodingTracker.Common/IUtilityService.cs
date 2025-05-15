using CodingTracker.Common.Entities.CodingSessionEntities;

namespace CodingTracker.Common.IUtilityServices
{
    public interface IUtilityService
    {

        string HashPassword(string password);
        string ConvertDoubleToHHMMString(double duration);
        int ConvertHHMMStringToSeconds(string input);
        string ConvertDurationSecondsToHHMMStringWithSpace(int durationSeconds);
        void ConvertCodingSessionListDatesToLocal(List<CodingSessionEntity> codingSessions);
    }
}
