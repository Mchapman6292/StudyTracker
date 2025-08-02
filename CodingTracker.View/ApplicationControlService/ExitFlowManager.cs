using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CodingTracker.View.Forms.Services.SharedFormServices;
using Guna.UI2.WinForms;
using Microsoft.UI.Xaml;



namespace CodingTracker.View.ApplicationControlService.ButtonNotificationManagers
{

    public interface IExitFlowManager
    {
        void HandleExitRequestAndStopSession(object sender, EventArgs e, Form currentForm);
        void HandleStopButtonRequest(Form currentForm);
        void HandleRestartSessionRequest(Form currentForm);
        void ExitCodingTracker();
    }

    public class ExitFlowManager : IExitFlowManager
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IApplicationLogger _appLogger;
        private readonly INotificationManager _notificationManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IAnimatedTimerManager _animatedTimerManager;
        private readonly IAnimatedColumnStateManager _animatedColumnStateManager;


        public ExitFlowManager(ICodingSessionManager codingSessionManager, IApplicationLogger appLogger, INotificationManager notificationManager, IFormNavigator formSwitcher, IStopWatchTimerService stopWatchTimerService, IAnimatedColumnStateManager animatedColumnStateManager, IAnimatedTimerManager animatedTimerManager)
        {
            _codingSessionManager = codingSessionManager;
            _appLogger = appLogger;
            _notificationManager = notificationManager;
            _formNavigator = formSwitcher;
            _stopWatchTimerService = stopWatchTimerService;
            _animatedTimerManager = animatedTimerManager;
            _animatedColumnStateManager = animatedColumnStateManager;
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
                    _formNavigator.SwitchToForm(FormPageEnum.OldMainPage);
                    break;

                case StopSessionDialogResultEnum.Cancel:
                    break;

            }
        }



        public void HandleRestartSessionRequest(Form currentForm)
        {
            RestartSessionDialogResultEnum restartButtonResult = _notificationManager.ShowRestartSessionMessageDialog(currentForm);

            switch (restartButtonResult)
            {
                case RestartSessionDialogResultEnum.Restart:
                    List<AnimatedTimerColumn> columnsList = _animatedTimerManager.ReturnTimerColumns();
                    _codingSessionManager.ResetCurrentCodingSession();
                    _stopWatchTimerService.StartRestartTimer();
                    _animatedColumnStateManager.SetColumnStateAndStartRestartTimerForRestartBeginning(columnsList);
                    break;

                case RestartSessionDialogResultEnum.Continue:
                    _stopWatchTimerService.StartSessionTimer();
                    break;

                case RestartSessionDialogResultEnum.Cancel:
                    _stopWatchTimerService.StartSessionTimer();
                    break;
            }
        }
        




        public void ExitCodingTracker()
        {
            System.Windows.Forms.Application.Exit();
        }

    }
}

