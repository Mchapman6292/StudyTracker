using CodingTracker.View.IMessageBoxManagers;

namespace CodingTracker.View.MessageBoxManagers
{
    public class MessageBoxManager : IMessageBoxManager
    {
        public void ShowInformation(string message, string title = "Information")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult ShowConfirmation(string message, string title = "Confirmation")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public DialogResult ShowExitButtonConfirmationMessageBox()
        {
            return MessageBox.Show(
            "Press Enter to confirm exit, Escape to cancel",
            "Confirm Exit",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1); 
        }
    }
}

