using Guna.UI2.WinForms;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;


namespace CodingTracker.View.ApplicationControlService.ExitFlowManagers
{

    public interface IExitFlowManager
    {
        void HandleExitRequestAndStopSession(object sender, EventArgs e, Form currentForm);
        void ExitCodingTracker();
    }

    public class ExitFlowManager : IExitFlowManager
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IApplicationLogger _appLogger;
        private readonly INotificationManager _notificationManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IStopWatchTimerService _stopWatchTimerService;


        public ExitFlowManager(ICodingSessionManager codingSessionManager, IApplicationLogger appLogger, INotificationManager notificationManager, IFormNavigator formSwitcher, IStopWatchTimerService stopWatchTimerService)
        {
            _codingSessionManager = codingSessionManager;
            _appLogger = appLogger;
            _notificationManager = notificationManager;
            _formNavigator = formSwitcher;
            _stopWatchTimerService = stopWatchTimerService;
        }


        /// <summary>
        /// This method is called by all exit buttons and handles all exit logic by using the CustomDialogResultEnum enum to determine the codingSession state and take appropriate action.


        public void HandleExitRequestAndStopSession(object sender, EventArgs e, Form currentForm)
        {
            CustomDialogResultEnum exitResult = _notificationManager.ShowExitMessageDialog(currentForm);

            switch(exitResult)
            {
                case CustomDialogResultEnum.Exit:
                    // Exit CodingTracker
                    ExitCodingTracker();
                    break;

                case CustomDialogResultEnum.SaveCurrentSessionAndExit:
                case CustomDialogResultEnum.StopSession:

                    bool sessionTimerActive = _codingSessionManager.ReturnIsSessionTimerActive();

                    if(sessionTimerActive)
                    {
                        _stopWatchTimerService.StopTimer();
                        TimeSpan duration = _stopWatchTimerService.ReturnElapsedTimeSpan();
                        _codingSessionManager.UpdateCodingSessionTimerEnded(duration);
                    }

                    _formNavigator.SwitchToForm(FormPageEnum.SessionNotesForm);
                    break;

                case CustomDialogResultEnum.ContinueSession:
                    // Continue with current session.
                    break;


            }
        }

        public void ExitCodingTracker()
        {
            Application.Exit();
        }

    }
}
