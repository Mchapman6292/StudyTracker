using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.SharedFormServices
{
    public enum ExitDialogResultEnum
    {
        Exit,                      // Exit without saving
        SaveCurrentSessionAndExit, // Takes user to session notes form before saving.
        StopSession,                // Called when the user presses the stopButton in CountdownTimerForm.
        ContinueSession,            // Don't exit
    }

    public enum RestartSessionDialogResultEnum
    {
        Restart,
        Continue,
        Cancel
    }


    public enum StopSessionDialogResultEnum
    {
        StopAndSaveSession,
        StopWithoutSaving,
        Cancel
    }

    public interface INotificationManager
    {
        void ShowNotificationDialog(Form parentForm, string message);
        void ShowDialogWithMultipleMessages(Form parentForm, List<string> messagesList);
        ExitDialogResultEnum ShowExitMessageDialog(Form parentForm);
        Guna2MessageDialog ReturnStopSessionDialog();
        StopSessionDialogResultEnum ShowStopButtonMessageDialog(Form parentForm);
        RestartSessionDialogResultEnum ShowRestartSessionMessageDialog(Form parentForm);
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

        

        public void ShowDialogWithMultipleMessages(Form parentForm, List<string> messagesList)
        {
            string messages = string.Join(Environment.NewLine, messagesList);

            var messageDialog = new Guna2MessageDialog
            {
                Text = messages,
                Caption = "Notification",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Dark
            };
            SetNotificationParentForm(parentForm, messageDialog);

            messageDialog.Show();
        }






        /// <summary>
        /// Shows an exit confirmation dialog based on the current session state.
        /// </summary>
        /// <param name="parentForm">The parent form for the dialog.</param>
        /// <returns>A <see cref="CustomDialogResult"/> enum value indicating the user's choice:
        /// <list type="bullet">
        ///     <item><description><see cref="CustomDialogResult.Exit"/> - Exit without saving</description></item>
        ///     <item><description><see cref="CustomDialogResult.SaveCurrentSessionAndExit"/> - Save the current session and exit</description></item>
        ///     <item><description><see cref="CustomDialogResult.ContinueSession"/> - Continue the current session</description></item>
        /// </list>
        /// </returns>

        public ExitDialogResultEnum ShowExitMessageDialog(Form parentForm)
        {
            bool codingSessionActive = _codingSessionManager.ReturnIsCodingSessionActive();

            if (!codingSessionActive)
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

                if (exitResult == DialogResult.Yes)
                {
                    return ExitDialogResultEnum.Exit;
                }
                else
                {
                    return ExitDialogResultEnum.ContinueSession;
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
                    return ExitDialogResultEnum.SaveCurrentSessionAndExit;
                }

                else if (saveResult == DialogResult.No)
                {
                    return ExitDialogResultEnum.Exit;
                }

                else
                {
                    return ExitDialogResultEnum.ContinueSession;
                }
            }
        }

        public Guna2MessageDialog ReturnStopSessionDialog()
        {
            Guna2MessageDialog stopSessionDialog = new Guna2MessageDialog
            {
                Caption = "Session Stopped",
                Text = "Save current session?",
                Buttons = MessageDialogButtons.YesNoCancel,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            return stopSessionDialog;
        }

        public StopSessionDialogResultEnum ShowStopButtonMessageDialog(Form parentForm)
        {
            Guna2MessageDialog stopSessionDialog = new Guna2MessageDialog
            {
                Text = "Save coding session?",
                Caption = "End Coding Session",
                Buttons = MessageDialogButtons.YesNoCancel,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(parentForm, stopSessionDialog);

            DialogResult saveResult = stopSessionDialog.Show();

            if (saveResult == DialogResult.No)
            {
                return StopSessionDialogResultEnum.StopWithoutSaving;
            }

            else if (saveResult == DialogResult.Yes)
            {
                return StopSessionDialogResultEnum.StopAndSaveSession;
            }

            else
            {
                return StopSessionDialogResultEnum.Cancel;
            }
        }



         public RestartSessionDialogResultEnum ShowRestartSessionMessageDialog(Form currentForm)
        {
            Guna2MessageDialog restartMessageDialog = new Guna2MessageDialog
            {
                Text = "Restart Coding Session?",
                Caption = "Restart?",
                Buttons = MessageDialogButtons.YesNo,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(currentForm, restartMessageDialog);

            DialogResult restartResult = restartMessageDialog.Show();

            if(restartResult == DialogResult.Yes)
            {
                return RestartSessionDialogResultEnum.Restart;
            }

            if(restartResult == DialogResult.No)
            {
                return RestartSessionDialogResultEnum.Continue;
            }

            else
            {
                return RestartSessionDialogResultEnum.Cancel;
            }







        }
   


    }
}


