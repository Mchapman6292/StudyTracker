using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.LoginPageService;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.TimerDisplayService;
using LibVLCSharp.Shared;

namespace CodingTracker.View.FormService
{
    public interface IFormStateManagement
    {
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
        private OrbitalTimerPage _orbitalTimerPageInstance;
        private ConfirmUsernamePage _confirmUsernamePageInstance;
        private ResetPasswordPage _resetPasswordPageInstance;

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
        private bool _orbitalTimerPageCreated = false;
        private bool _timerDisplayPageCreated = false;

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
            { FormPageEnum.EditSessionPage, typeof(EditSessionPage)},
            { FormPageEnum.CreateAccountPage, typeof(CreateAccountPage)},
            { FormPageEnum.SessionGoalPage, typeof(SessionGoalPage)},
            { FormPageEnum.ConfirmUsernamePage, typeof(ConfirmUsernamePage)},
            { FormPageEnum.ResetPasswordPage, typeof(ResetPasswordPage)},
            { FormPageEnum.OrbitalTimerPage, typeof(OrbitalTimerPage)},
            { FormPageEnum.WORKINGSessionTimerPage, typeof(CountdownTimerForm)},
            { FormPageEnum.WaveVisualizationForm, typeof (WaveVisualizationTestForm)},
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
            if (formEnum == FormPageEnum.OrbitalTimerPage || formEnum == FormPageEnum.WORKINGSessionTimerPage || formEnum == FormPageEnum.WaveVisualizationForm)
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
                case FormPageEnum.EditSessionPage:
                    return _editSessionPageInstance;
                case FormPageEnum.CreateAccountPage:
                    return _createAccountPageInstance;
                case FormPageEnum.LoginPage:
                    return _loginPageInstance;
                case FormPageEnum.SessionGoalPage:
                    return _popUpPageInstance;
                case FormPageEnum.ConfirmUsernamePage:
                    return _confirmUsernamePageInstance;
                case FormPageEnum.ResetPasswordPage:
                    return _resetPasswordPageInstance;
                case FormPageEnum.OrbitalTimerPage:
                    return _orbitalTimerPageInstance;
                case FormPageEnum.WORKINGSessionTimerPage:
                    return _timerDisplayPageInstance;
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
                case FormPageEnum.EditSessionPage:
                    _editSessionPageInstance = instance as EditSessionPage;
                    break;
                case FormPageEnum.CreateAccountPage:
                    _createAccountPageInstance = instance as CreateAccountPage;
                    break;
                case FormPageEnum.LoginPage:
                    _loginPageInstance = instance as LoginPage;
                    break;
                case FormPageEnum.SessionGoalPage:
                    _popUpPageInstance = instance as SessionGoalPage;
                    break;
                case FormPageEnum.WORKINGSessionTimerPage:
                    _timerDisplayPageInstance = instance as CountdownTimerForm;
                    break;
                case FormPageEnum.ConfirmUsernamePage:
                    _confirmUsernamePageInstance = instance as ConfirmUsernamePage;
                    break;
                case FormPageEnum.ResetPasswordPage:
                    _resetPasswordPageInstance = instance as ResetPasswordPage;
                    break;
                case FormPageEnum.OrbitalTimerPage:
                    _orbitalTimerPageInstance = instance as OrbitalTimerPage;
                    break;
            }
        }

        public bool IsFormCreated(FormPageEnum form)
        {
            switch (form)
            {
                case FormPageEnum.MainPage:
                    return _mainPageCreated;
                case FormPageEnum.EditSessionPage:
                    return _editSessionPageCreated;
                case FormPageEnum.CreateAccountPage:
                    return _createAccountPageCreated;
                case FormPageEnum.LoginPage:
                    return _loginPageCreated;
                case FormPageEnum.SessionGoalPage:
                    return _popUpPageCreated;
                case FormPageEnum.ConfirmUsernamePage:
                    return _confirmUsernamePageCreated;
                case FormPageEnum.ResetPasswordPage:
                    return _resetPasswordPageCreated;
                case FormPageEnum.CountdownTimerPage:
                    return _countdownTimerPageCreated;
                case FormPageEnum.OrbitalTimerPage:
                    return _orbitalTimerPageCreated;
                case FormPageEnum.WORKINGSessionTimerPage:
                    return _timerDisplayPageCreated;
                default:
                    return false;
            }
        }

        public void UpdateAccountCreatedCallBack(Action<string> callback)
        {
            _createAccountPageInstance.AccountCreatedCallback = callback;
        }









    }
}