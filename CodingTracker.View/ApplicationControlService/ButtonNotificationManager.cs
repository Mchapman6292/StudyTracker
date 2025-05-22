using Guna.UI2.WinForms;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;


namespace CodingTracker.View.ApplicationControlService.ButtonNotificationManagers
{

    public interface IButtonNotificationManager
    {
        void HandleExitRequestAndStopSession(object sender, EventArgs e, Form currentForm);
        void HandleStopButtonRequest(Form currentForm);
        void ExitCodingTracker();
    }

    public class ButtonNotificationManager : IButtonNotificationManager
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IApplicationLogger _appLogger;
        private readonly INotificationManager _notificationManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IStopWatchTimerService _stopWatchTimerService;


        public ButtonNotificationManager(ICodingSessionManager codingSessionManager, IApplicationLogger appLogger, INotificationManager notificationManager, IFormNavigator formSwitcher, IStopWatchTimerService stopWatchTimerService)
        {
            _codingSessionManager = codingSessionManager;
            _appLogger = appLogger;
            _notificationManager = notificationManager;
            _formNavigator = formSwitcher;
            _stopWatchTimerService = stopWatchTimerService;
        }


        /// <summary>
        /// This method is called by all exit buttons and handles all exit logic by using the ExitDialogResultEnum enum to determine the codingSession state and take appropriate action.


        public void HandleExitRequestAndStopSession(object sender, EventArgs e, Form currentForm)
        {
            ExitDialogResultEnum exitResult = _notificationManager.ShowExitMessageDialog(currentForm);

            switch(exitResult)
            {
                case ExitDialogResultEnum.Exit:
                    // Exit CodingTracker
                    ExitCodingTracker();
                    break;

                case ExitDialogResultEnum.SaveCurrentSessionAndExit:
                case ExitDialogResultEnum.StopSession:

                    bool sessionTimerActive = _codingSessionManager.ReturnIsSessionTimerActive();

                    if(sessionTimerActive)
                    {
                        _stopWatchTimerService.StopTimer();
                        TimeSpan duration = _stopWatchTimerService.ReturnElapsedTimeSpan();
                        _codingSessionManager.UpdateCodingSessionTimerEnded(duration);
                    }

                    _formNavigator.SwitchToForm(FormPageEnum.SessionNotesForm);
                    break;

                case ExitDialogResultEnum.ContinueSession:
                    // Continue with current session.
                    break;
            }
        }

        public void HandleStopButtonRequest(Form currentForm) 
        {
            StopSessionDialogResultEnum stopButtonResult = _notificationManager.ShowStopButtonMessageDialog(currentForm);

            switch(stopButtonResult) 
            {
                case StopSessionDialogResultEnum.StopAndSaveSession:

                    _stopWatchTimerService.StopTimer();
                    TimeSpan duration = _stopWatchTimerService.ReturnElapsedTimeSpan();
                    _codingSessionManager.UpdateCodingSessionTimerEnded(duration);
                    _codingSessionManager.UpdateSessionTimerActiveBooleansToFalse();
                    _formNavigator.SwitchToForm(FormPageEnum.SessionNotesForm);
                    break;

                case StopSessionDialogResultEnum.StopWithoutSaving:
                    _stopWatchTimerService.StopTimer();
                    _codingSessionManager.UpdateSessionTimerActiveBooleansToFalse();
                    _codingSessionManager.ResetCurrentCodingSession();
                    _formNavigator.SwitchToForm(FormPageEnum.MainPage);
                    break;

                case StopSessionDialogResultEnum.Cancel:
                    break;

            }
        }

       

        public void ExitCodingTracker()
        {
            Application.Exit();
        }

    }
}

