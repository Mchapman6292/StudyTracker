using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using Guna.UI2.WinForms;

namespace CodingTracker.View.FormService.NotificationManagers
{
    public interface INotificationManager
    {
        void ShowNotificationDialog(object sender, EventArgs e, Guna2MessageDialog messageDialog, string message);
        DialogResult ReturnExitMessageDialog(Form parentForm);
        DialogResult ReturnExitMessageDialogWithActiveCodingSession(Form parentForm);

    }

    public class NotificationManager : INotificationManager
    {
        private readonly ICodingSessionManager _codingSessionManager;

        public NotificationManager(ICodingSessionManager codingSessionManager) 
        {
            _codingSessionManager = codingSessionManager;
        }

        public void SetNotificationParentForm(Form parentForm, Guna2MessageDialog messageDialog)
        {
            messageDialog.Parent = parentForm;
        }


        public void ShowNotificationDialog(object sender, EventArgs e, Guna2MessageDialog messageDialog, string message)
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

        public DialogResult ReturnExitMessageDialog(Form parentForm) 
        {
            Guna2MessageDialog messageDialog = new Guna2MessageDialog
            {
                Text = "Are you sure you want to exit?",
                Caption = "Exit Application",
                Buttons = MessageDialogButtons.YesNo,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(parentForm, messageDialog);

            return messageDialog.Show();
        }

        public DialogResult ReturnExitMessageDialogWithActiveCodingSession(Form parentForm)
        {
            bool codingSessionActive = _codingSessionManager.ReturnIsCodingSessionActive();

            if(!codingSessionActive) 
            {
                throw new InvalidOperationException($"{nameof(ReturnExitMessageDialogWithActiveCodingSession)} should not be called when coding session is not active.");
            }

            Guna2MessageDialog messageDialog = new Guna2MessageDialog
            {
                Caption = "Exit Application",
                Text = "Save current coding Session?",
                Buttons = MessageDialogButtons.YesNo,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(parentForm, messageDialog);

            return messageDialog.Show();
        }
 
    }
}
