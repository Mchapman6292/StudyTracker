using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.LoginPageService;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.TimerDisplayService;
using Microsoft.Extensions.DependencyInjection;

namespace CodingTracker.View.FormService
{
    public interface IFormFactory
    {
        Form CreateForm(FormPageEnum formType);
        Form GetOrCreateForm(FormPageEnum formType);
        Form GetOrCreateLoginPage();
        Form GetOrCreateTimerDisplayForm(bool goalSet, string goalTimeHHMM = null);


    }

    public class FormFactory : IFormFactory
    {

        private IServiceProvider _serviceProvider;
        private IApplicationLogger _appLogger;
        private readonly IFormStateManagement _formStateManagement;

        public FormFactory(IServiceProvider serviceProvider, IApplicationLogger appLogger, IFormStateManagement formStateManagement)
        {
            _serviceProvider = serviceProvider;
            _appLogger = appLogger;
            _formStateManagement = formStateManagement;
        }

        public Form CreateForm(FormPageEnum formType)
        {
            Type formClassType = formType switch
            {
                FormPageEnum.LoginPage => typeof(LoginPage),
                FormPageEnum.MainPage => typeof(MainPage),
                FormPageEnum.EditSessionPage => typeof(EditSessionPage),
                FormPageEnum.CreateAccountPage => typeof(CreateAccountPage),
                FormPageEnum.SessionGoalPage => typeof(SessionGoalPage),
                FormPageEnum.WORKINGSessionTimerPage => typeof(WORKINGSessionTimerForm),
                FormPageEnum.ResetPasswordPage => typeof(ResetPasswordPage),
                FormPageEnum.ConfirmUsernamePage => typeof(ConfirmUsernamePage), 
                FormPageEnum.OrbitalTimerPage => typeof(OrbitalTimerPage),



                _ => throw new ArgumentException($"Unknown form type: {formType}")
            };

            return _serviceProvider.GetRequiredService(formClassType) as Form;
        }


        public Form GetOrCreateForm(FormPageEnum formType)
        {
            var existingForm = _formStateManagement.GetFormByFormPageEnum(formType);
            if (existingForm == null || existingForm.IsDisposed)
            {
                var newForm = CreateForm(formType);
                _formStateManagement.SetFormByFormPageEnum(formType, newForm);
                return newForm;
            }
            return existingForm;
        }

        public Form GetOrCreateLoginPage()
        {
            var loginForm = _formStateManagement.GetFormByFormPageEnum(FormPageEnum.LoginPage);

            if(loginForm == null || loginForm.IsDisposed)
            {
                loginForm = CreateForm(FormPageEnum.LoginPage);
            }
            loginForm.WindowState = FormWindowState.Normal;
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.BringToFront();
            loginForm.TopLevel = true;
            return loginForm;
        }

        public Form GetOrCreateTimerDisplayForm(bool goalSet, string goalTimeHHMM = null)
        {
            var existingForm = _formStateManagement.GetFormByFormPageEnum(FormPageEnum.WORKINGSessionTimerPage);

            if (existingForm == null || existingForm.IsDisposed)
            {
                var newForm = CreateForm(FormPageEnum.WORKINGSessionTimerPage);
                _formStateManagement.SetFormByFormPageEnum(FormPageEnum.WORKINGSessionTimerPage, newForm);
                return newForm;
            }
            return existingForm;
        }

      
    }
}

