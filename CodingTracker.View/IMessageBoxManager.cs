namespace CodingTracker.View.IMessageBoxManagers
{
    public interface IMessageBoxManager
    {
        void ShowInformation(string message, string title = "Information");
        void ShowError(string message, string title = "Error");
        DialogResult ShowConfirmation(string message, string title = "Confirmation");

        DialogResult ShowExitButtonConfirmationMessageBox();
    }
}
