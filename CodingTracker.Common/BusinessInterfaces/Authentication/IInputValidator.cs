// Ignore Spelling: Validator


// Ignore Spelling: Validator

using CodingTracker.Common.BusinessInterfaces;

namespace CodingTracker.Common.BusinessInterfaces.Authentication
{
    public interface IInputValidator
    {

        bool CheckDateInput(string input, out DateTime result);
        bool CheckTimeInput(string input, out DateTime result);
        bool IsValidTimeFormatHHMM(string input);
        bool TryParseTime(string input, out TimeSpan timeSpan);

        InputValidationResult ValidateUsername(string username);

        InputValidationResult ValidatePassword(string password);


        DateTime GetValidDateFromUser();
        DateTime GetValidTimeFromUser();

        bool ValidateTimeFormat(string time);



    }
}