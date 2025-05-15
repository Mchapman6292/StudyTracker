namespace CodingTracker.Common.BusinessInterfaces
{
    public interface IInputValidationResult
    {
        void AddErrorMessage(string message);
        string GetAllErrorMessages();

    }
}
