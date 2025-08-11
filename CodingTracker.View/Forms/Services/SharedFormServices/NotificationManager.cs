using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
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
        Form CheckForHigherLevelFormAndReturnIfTrue(Form targetForm);
    }



    public class NotificationManager : INotificationManager
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IApplicationLogger _appLogger;

        public NotificationManager(ICodingSessionManager codingSessionManager, IFormStateManagement formStateManagement, IApplicationLogger appLogger)
        {
            _codingSessionManager = codingSessionManager;
            _formStateManagement = formStateManagement;
            _appLogger = appLogger;
        }

        public void SetNotificationParentForm(Form parentForm, Guna2MessageDialog messageDialog)
        {
            messageDialog.Parent = parentForm;
        }


        public void ShowNotificationDialog(Form currentForm, string message)
        {

            Form targetForm = CheckForHigherLevelFormAndReturnIfTrue(currentForm);
            
            var messageDialog = new Guna2MessageDialog
            {
                Text = string.IsNullOrWhiteSpace(message) ? "No message provided." : message,
                Caption = "Notification",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(targetForm, messageDialog);

            messageDialog.Show();
        }

        

        public void ShowDialogWithMultipleMessages(Form currentForm, List<string> messagesList)
        {
            string messages = string.Join(Environment.NewLine, messagesList);
            Form targetForm = CheckForHigherLevelFormAndReturnIfTrue(currentForm);

            var messageDialog = new Guna2MessageDialog
            {
                Text = messages,
                Caption = "Notification",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Dark
            };
            SetNotificationParentForm(targetForm, messageDialog);

            messageDialog.Show();
        }






        /// <summary>
        /// Shows an exit confirmation dialog based on the current session state.
        /// </summary>
        /// <param name="currentForm">The parent form for the dialog.</param>
        /// <returns>A <see cref="CustomDialogResult"/> enum value indicating the user's choice:
        /// <list type="bullet">
        ///     <item><description><see cref="CustomDialogResult.Exit"/> - Exit without saving</description></item>
        ///     <item><description><see cref="CustomDialogResult.SaveCurrentSessionAndExit"/> - Save the current session and exit</description></item>
        ///     <item><description><see cref="CustomDialogResult.ContinueSession"/> - Continue the current session</description></item>
        /// </list>
        /// </returns>

        public ExitDialogResultEnum ShowExitMessageDialog(Form currentForm)
        {
            bool codingSessionActive = _codingSessionManager.ReturnIsCodingSessionActive();
            Form targetForm = CheckForHigherLevelFormAndReturnIfTrue(currentForm);

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

                SetNotificationParentForm(targetForm, exitAppDialog);
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
                SetNotificationParentForm(targetForm, SaveCurrentCodingSessionDialog);

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

        public StopSessionDialogResultEnum ShowStopButtonMessageDialog(Form currentForm)
        {
            Form targetForm = CheckForHigherLevelFormAndReturnIfTrue(currentForm);

            Guna2MessageDialog stopSessionDialog = new Guna2MessageDialog
            {
                Text = "Save coding session?",
                Caption = "End Coding Session",
                Buttons = MessageDialogButtons.YesNoCancel,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(targetForm, stopSessionDialog);

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
            Form targetForm = CheckForHigherLevelFormAndReturnIfTrue(currentForm);
            Guna2MessageDialog restartMessageDialog = new Guna2MessageDialog
            {
                Text = "Restart Coding Session?",
                Caption = "Restart?",
                Buttons = MessageDialogButtons.YesNo,
                Icon = MessageDialogIcon.Question,
                Style = MessageDialogStyle.Dark
            };

            SetNotificationParentForm(targetForm, restartMessageDialog);

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



        // Since I usually pass this as the form to notification manager we need to make sure that the form passed is actually the parent form or else an exception is thrown for a circular dependency. If there is no higher level form return the same one.
        public Form CheckForHigherLevelFormAndReturnIfTrue(Form targetForm)
        {
            if (targetForm == null)
                return null;

            if (targetForm.Owner != null)
            {
                _appLogger.Debug($"Form {targetForm.Name} has owner {targetForm.Owner.Name}, using owner instead");
                return targetForm.Owner;
            }

            if (!targetForm.TopLevel && targetForm.Parent != null)
            {
                Form parentForm = FindParentFormFormEmbeddedForms(targetForm.Parent);
                if (parentForm != null)
                {
                    _appLogger.Debug($"Form {targetForm.Name} is embedded, using parent form {parentForm.Name}");
                    return parentForm;
                }
            }
            return targetForm;
        }

        // When a form is embedded its parent might be a control instead of a form so we need to handle this.
        private Form FindParentFormFormEmbeddedForms(Control control)
        {
            Control current = control;
            while (current != null)
            {
                if (current is Form form)
                    return form;
                current = current.Parent;
            }
            return null;
        }



    }
}


