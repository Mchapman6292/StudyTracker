namespace CodingTracker.Common.IUtilityServices
{
    public interface IUtilityService
    {
        bool IsValidString(string input);
        int TryParseInt(string input);
        bool TryParseDate(string input, out DateTime result);

        string HashPassword(string password);

        string ConvertDurationToHHMM(double duration);


    }
}
