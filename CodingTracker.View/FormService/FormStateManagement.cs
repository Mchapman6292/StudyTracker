using CodingTracker.View.FormPageEnums;
using CodingTracker.View.PopUpFormService;

namespace CodingTracker.View.FormService
{
    public interface IFormStateManagement
    {
        Form GetFormByFormPageEnum(FormPageEnum form);

        void SetFormByFormPageEnum(FormPageEnum form, Form instance);
        bool IsFormCreated(FormPageEnum form);

        void UpdateAccountCreatedCallBack(Action<string> callback);




    }

    public class FormStateManagement : IFormStateManagement
    {
        private MainPage _mainPageInstance;
        private CodingSessionPage _codingSessionPageInstance;
        private EditSessionPage _editSessionPageInstance;
        private CodingSessionTimerForm _codingSessionTimerInstance;
        private CreateAccountPage _createAccountPageInstance;
        private LoginPage _loginPageInstance;
        private SessionGoalForm _popUpPageInstance;
        private TimerDisplayForm _timerDisplayPageInstance;


        private bool _mainPageCreated = false;
        private bool _codingSessionPageCreated = false;
        private bool _editSessionPageCreated = false;
        private bool _codingSessionTimerCreated = false;
        private bool _createAccountPageCreated = false;
        private bool _loginPageCreated = false;


        private readonly Dictionary<FormPageEnum, Type> _formTypes = new Dictionary<FormPageEnum, Type>
        {
            { FormPageEnum.LoginPage, typeof(LoginPage) },
            { FormPageEnum.MainPage, typeof(MainPage) },
            { FormPageEnum.CodingSessionPage, typeof(CodingSessionPage) },
            { FormPageEnum.EditSessionPage, typeof(EditSessionPage) },
            { FormPageEnum.CreateAccountPage, typeof(CreateAccountPage) },
            { FormPageEnum.CodingSessionTimerPage, typeof(CodingSessionTimerForm) }
        };


        public Form GetFormByFormPageEnum(FormPageEnum form)
        {
            switch (form)
            {
                case FormPageEnum.MainPage:
                    return _mainPageInstance;
                case FormPageEnum.CodingSessionPage:
                    return _codingSessionPageInstance;
                case FormPageEnum.EditSessionPage:
                    return _editSessionPageInstance;
                case FormPageEnum.CodingSessionTimerPage:
                    return _codingSessionTimerInstance;
                case FormPageEnum.CreateAccountPage:
                    return _createAccountPageInstance;
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
                case FormPageEnum.CodingSessionPage:
                    _codingSessionPageInstance = instance as CodingSessionPage;
                    break;
                case FormPageEnum.EditSessionPage:
                    _editSessionPageInstance = instance as EditSessionPage;
                    break;
                case FormPageEnum.CodingSessionTimerPage:
                    _codingSessionTimerInstance = instance as CodingSessionTimerForm;
                    break;
                case FormPageEnum.CreateAccountPage:
                    _createAccountPageInstance = instance as CreateAccountPage;
                    break;
                case FormPageEnum.LoginPage:
                    _loginPageInstance = instance as LoginPage;
                    break;
                case FormPageEnum.SessionGoalPage:
                    _popUpPageInstance = instance as SessionGoalForm;
                    break;
                case FormPageEnum.TimerDisplayPage:
                    _timerDisplayPageInstance = instance as TimerDisplayForm;
                    break;
            }
        }


        public bool IsFormCreated(FormPageEnum form)
        {
            switch (form)
            {
                case FormPageEnum.MainPage:
                    return _mainPageCreated;
                case FormPageEnum.CodingSessionPage:
                    return _codingSessionPageCreated;
                case FormPageEnum.EditSessionPage:
                    return _editSessionPageCreated;
                case FormPageEnum.CodingSessionTimerPage:
                    return _codingSessionTimerCreated;
                case FormPageEnum.CreateAccountPage:
                    return _createAccountPageCreated;
                case FormPageEnum.LoginPage:
                    return _loginPageCreated;
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