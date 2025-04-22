namespace CodingTracker.Common.IInputValidationResults
{
    public interface IInputValidationResult
    {
        void AddErrorMessage(string message);
        string GetAllErrorMessages();

    }
}
