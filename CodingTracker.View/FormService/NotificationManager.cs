using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using Guna.UI2.WinForms;

namespace CodingTracker.View.FormService.NotificationManagers
{
    public enum CustomDialogResult
    {
        Exit,                      // Exit without saving
        SaveCurrentSessionAndExit, // Takes user to session notes form before saving.
        ContinueSession,           // Don't exit
    }

    public interface INotificationManager
    {
        void ShowNotificationDialog(Form parentForm, string message);
        CustomDialogResult ShowExitMessageDialog(Form parentForm);
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


        public void ShowNotificationDialog(Form parentForm, string message)
        {
            var messageDialog = new Guna2MessageDialog
            {
                Text = string.IsNullOrWhiteSpace(message) ? "No message provided." : message,
                Caption = "Notification",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(parentForm, messageDialog);

            messageDialog.Show();
        }









        /// <summary>
        /// Returns a CustomDialogResult enum which is used in ExitFlowManager to control application based on users choices. 
        /// </summary>
        /// <param name="parentForm"></param>
        /// <returns></returns>

        public CustomDialogResult ShowExitMessageDialog(Form parentForm)
        {
            bool codingSessionActive = _codingSessionManager.ReturnIsCodingSessionActive();

            if(!codingSessionActive) 
            {
                Guna2MessageDialog exitAppDialog = new Guna2MessageDialog
                {
                    Text = "Exit application?",
                    Caption = "Exit Application",
                    Buttons = MessageDialogButtons.YesNo,
                    Icon = MessageDialogIcon.Question,
                    Style = MessageDialogStyle.Dark
                };

                SetNotificationParentForm(parentForm, exitAppDialog);
                DialogResult exitResult = exitAppDialog.Show();

                if(exitResult == DialogResult.Yes) 
                {
                    return CustomDialogResult.Exit;
                }
                else
                {
                    return CustomDialogResult.ContinueSession;
                }

            }

            else
            // If coding session is active show "Save current coding session" and action user choice. 
            {
                Guna2MessageDialog SaveCurrentCodingSessionDialog = new Guna2MessageDialog
                {
                    Caption = "Exit Application",
                    Text = "Save current coding Session?",
                    Buttons = MessageDialogButtons.YesNoCancel,
                    Icon = MessageDialogIcon.Question,
                    Style = MessageDialogStyle.Dark
                };
                SetNotificationParentForm(parentForm, SaveCurrentCodingSessionDialog);

                DialogResult saveResult = SaveCurrentCodingSessionDialog.Show();

                if (saveResult == DialogResult.Yes)
                {
                    return CustomDialogResult.SaveCurrentSessionAndExit;
                }

                else if (saveResult == DialogResult.No)
                {
                    return CustomDialogResult.Exit;
                }

                else
                {
                    return CustomDialogResult.ContinueSession;
                }
            }

  
          

        }




    }
}
