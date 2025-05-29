using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Session;
using CodingTracker.View.LoginPageService;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.TimerDisplayService;
using LibVLCSharp.Shared;

namespace CodingTracker.View.FormManagement
{
    public interface IFormStateManagement
    {
        public bool ShowCountdownTimerGoalVisuals { get; set; }
        void UpdateShowCountdownTimerGoalVisuals(bool showVisuals);
        Form GetCurrentForm();
        void SetCurrentForm(Form form);
        void AddActiveTimerForm(FormPageEnum formEnum, Form form);
        void RemoveActiveTimerForm(FormPageEnum formEnum, Form form);
        bool CheckIfFormEnumIsTimerForm(FormPageEnum formEnum);
        Form GetFormByFormPageEnum(FormPageEnum form);
        void SetFormByFormPageEnum(FormPageEnum form, Form instance);
        bool IsFormCreated(FormPageEnum form);
        void UpdateAccountCreatedCallBack(Action<string> callback);
    }

    public class FormStateManagement : IFormStateManagement
    {
        private MainPage _mainPageInstance;
        private EditSessionPage _editSessionPageInstance;
        private CreateAccountPage _createAccountPageInstance;
        private LoginPage _loginPageInstance;
        private SessionGoalPage _popUpPageInstance;
        private CountdownTimerForm _timerDisplayPageInstance;
        private ConfirmUsernamePage _confirmUsernamePageInstance;
        private ResetPasswordPage _resetPasswordPageInstance;
        private ElapsedTimerPage _elapsedTimerPageInstance;
        private SessionRatingForm _sessionRatingFormInstance;
        private WaveVisualizationForm _waveVisualizationFormInstance;

        private readonly IApplicationLogger _appLogger;

        private bool _mainPageCreated = false;
        private bool _codingSessionPageCreated = false;
        private bool _editSessionPageCreated = false;
        private bool _codingSessionTimerCreated = false;
        private bool _createAccountPageCreated = false;
        private bool _loginPageCreated = false;
        private bool _popUpPageCreated = false;
        private bool _confirmUsernamePageCreated = false;
        private bool _resetPasswordPageCreated = false;
        private bool _countdownTimerPageCreated = false;
        private bool _timerDisplayPageCreated = false;
        private bool _elapsedTimerPageCrated = false;
        private bool _sessionRatingFormCreated = false;
        private bool _waveVisualizationFormCreated = false;

        public bool ShowCountdownTimerGoalVisuals { get; set; }

        Form _currentForm;

        private Dictionary<FormPageEnum, Form> _activeTimerForms = new Dictionary<FormPageEnum, Form>();

        public FormStateManagement(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }

        private readonly Dictionary<FormPageEnum, Type> _formTypes = new Dictionary<FormPageEnum, Type>
        {
            { FormPageEnum.LoginPage, typeof(LoginPage)},
            { FormPageEnum.MainPage, typeof(MainPage)},
            { FormPageEnum.EditSessionForm, typeof(EditSessionPage)},
            { FormPageEnum.CreateAccountForm, typeof(CreateAccountPage)},
            { FormPageEnum.SessionGoalForm, typeof(SessionGoalPage)},
            { FormPageEnum.ConfirmUsernameForm, typeof(ConfirmUsernamePage)},
            { FormPageEnum.ResetPasswordForm, typeof(ResetPasswordPage)},
            { FormPageEnum.CountdownTimerForm, typeof(CountdownTimerForm)},
            { FormPageEnum.WaveVisualizationForm, typeof(WaveVisualizationForm)},
            { FormPageEnum.SessionRatingForm, typeof(SessionRatingForm)},
        };

        public Form GetCurrentForm()
        {
            return _currentForm;
        }

        public void SetCurrentForm(Form form)
        {
            _currentForm = form;
            _appLogger.Debug($"Current form set to: {form.GetType().Name}");
        }

        public void AddActiveTimerForm(FormPageEnum formEnum, Form form)
        {
            _activeTimerForms[formEnum] = form;
            _appLogger.Debug($"Added active timer form: {form.GetType().Name}");
        }

        public void RemoveActiveTimerForm(FormPageEnum formEnum, Form form)
        {
            _activeTimerForms.Remove(formEnum);
            _appLogger.Debug($"Removed active timer form: {form.GetType().Name}");
        }

        public bool CheckIfFormEnumIsTimerForm(FormPageEnum formEnum)
        {
            if (formEnum == FormPageEnum.OrbitalTimerForm ||
                formEnum == FormPageEnum.CountdownTimerForm ||
                formEnum == FormPageEnum.WaveVisualizationForm ||
                formEnum == FormPageEnum.ElapsedTimerForm)
            {
                return true;
            }
            return false;
        }

        public Form GetFormByFormPageEnum(FormPageEnum form)
        {
            switch (form)
            {
                case FormPageEnum.MainPage:
                    return _mainPageInstance;
                case FormPageEnum.EditSessionForm:
                    return _editSessionPageInstance;
                case FormPageEnum.CreateAccountForm:
                    return _createAccountPageInstance;
                case FormPageEnum.LoginPage:
                    return _loginPageInstance;
                case FormPageEnum.SessionGoalForm:
                    return _popUpPageInstance;
                case FormPageEnum.ConfirmUsernameForm:
                    return _confirmUsernamePageInstance;
                case FormPageEnum.ResetPasswordForm:
                    return _resetPasswordPageInstance;
                case FormPageEnum.CountdownTimerForm:
                    return _timerDisplayPageInstance;
                case FormPageEnum.ElapsedTimerForm:
                    return _elapsedTimerPageInstance;
                case FormPageEnum.SessionRatingForm:
                    return _sessionRatingFormInstance;
                case FormPageEnum.WaveVisualizationForm:
                    return _waveVisualizationFormInstance;
                default:
                    return null;
            }
        }

        public void SetFormByFormPageEnum(FormPageEnum form, Form instance)
        {
            switch (form)
            {
                case FormPageEnum.MainPage:
                    _mainPageInstance = instance as MainPage;
                    break;
                case FormPageEnum.EditSessionForm:
                    _editSessionPageInstance = instance as EditSessionPage;
                    break;
                case FormPageEnum.CreateAccountForm:
                    _createAccountPageInstance = instance as CreateAccountPage;
                    break;
                case FormPageEnum.LoginPage:
                    _loginPageInstance = instance as LoginPage;
                    break;
                case FormPageEnum.SessionGoalForm:
                    _popUpPageInstance = instance as SessionGoalPage;
                    break;
                case FormPageEnum.CountdownTimerForm:
                    _timerDisplayPageInstance = instance as CountdownTimerForm;
                    break;
                case FormPageEnum.ConfirmUsernameForm:
                    _confirmUsernamePageInstance = instance as ConfirmUsernamePage;
                    break;
                case FormPageEnum.ResetPasswordForm:
                    _resetPasswordPageInstance = instance as ResetPasswordPage;
                    break;
                case FormPageEnum.ElapsedTimerForm:
                    _elapsedTimerPageInstance = instance as ElapsedTimerPage;
                    break;
                case FormPageEnum.SessionRatingForm:
                    _sessionRatingFormInstance = instance as SessionRatingForm;
                    break;
                case FormPageEnum.WaveVisualizationForm:
                    _waveVisualizationFormInstance = instance as WaveVisualizationForm;
                    break;
            }
        }

        public bool IsFormCreated(FormPageEnum form)
        {
            switch (form)
            {
                case FormPageEnum.MainPage:
                    return _mainPageCreated;
                case FormPageEnum.EditSessionForm:
                    return _editSessionPageCreated;
                case FormPageEnum.CreateAccountForm:
                    return _createAccountPageCreated;
                case FormPageEnum.LoginPage:
                    return _loginPageCreated;
                case FormPageEnum.SessionGoalForm:
                    return _popUpPageCreated;
                case FormPageEnum.ConfirmUsernameForm:
                    return _confirmUsernamePageCreated;
                case FormPageEnum.ResetPasswordForm:
                    return _resetPasswordPageCreated;
                case FormPageEnum.CountdownTimerForm:
                    return _countdownTimerPageCreated;
                case FormPageEnum.OrbitalTimerForm:
                    return _timerDisplayPageCreated;
                case FormPageEnum.ElapsedTimerForm:
                    return _elapsedTimerPageCrated;
                case FormPageEnum.SessionRatingForm:
                    return _sessionRatingFormCreated;
                case FormPageEnum.WaveVisualizationForm:
                    return _waveVisualizationFormCreated;
                default:
                    return false;
            }
        }

        public void UpdateAccountCreatedCallBack(Action<string> callback)
        {
            _createAccountPageInstance.AccountCreatedCallback = callback;
        }

        public void UpdateShowCountdownTimerGoalVisuals(bool showVisuals)
        {
            ShowCountdownTimerGoalVisuals = showVisuals;
        }
    }
}