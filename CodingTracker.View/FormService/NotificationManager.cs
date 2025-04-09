using Guna.UI2.WinForms;

namespace CodingTracker.View.FormService.NotificationManagers
{
    public interface INotificationManager
    {
        void ShowNotificationDialog(object sender, EventArgs e, Guna2MessageDialog messageDialog, string message);
    }

    public class NotificationManager : INotificationManager 
    {

        public void ShowNotificationDialog(object sender, EventArgs e, Guna2MessageDialog messageDialog ,string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                messageDialog.Text = "No message provided.";
            }
            else
            {
                messageDialog.Text = message;
            }

            messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            messageDialog.Caption = "Notification";
            messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;


            messageDialog.Show();

            SendKeys.Send("{ENTER}");
        }
    }
}
