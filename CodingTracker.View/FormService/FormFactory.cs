﻿using CodingTracker.Common.IApplicationLoggers;
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
                FormPageEnum.CodingSessionPage => typeof(CodingSessionPage),
                FormPageEnum.EditSessionPage => typeof(EditSessionPage),
                FormPageEnum.CreateAccountPage => typeof(PassWordTextBox),
                FormPageEnum.CodingSessionTimerPage => typeof(CodingSessionTimerForm),
                FormPageEnum.SessionGoalPage => typeof(SessionGoalForm),
                FormPageEnum.SessionTimerPage => typeof(SessionTimerForm),
                FormPageEnum.ResetPasswordPage => typeof(ResetPasswordPage),
                FormPageEnum.ConfirmUsernamePage => typeof(ConfirmUsernamePage), 
                FormPageEnum.CountdownTimerPage => typeof(OrbitalTimerForm),
                FormPageEnum.OrbitalTimerPage => typeof(OrbitalTimerForm),



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

        // This is needed as the generic form creation method (GetOrCreateForm) does not work with the goalTime parameter required for SessionTimerForm.
        public SessionTimerForm CreateTimerDisplayForm(bool goalSet, string goalTimeHHMM = null)
        {
            
            return new SessionTimerForm();
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
            var existingForm = _formStateManagement.GetFormByFormPageEnum(FormPageEnum.SessionTimerPage);

            if (existingForm == null || existingForm.IsDisposed)
            {
                var newForm = CreateForm(FormPageEnum.SessionTimerPage);
                _formStateManagement.SetFormByFormPageEnum(FormPageEnum.SessionTimerPage, newForm);
                return newForm;
            }
            return existingForm;
        }

      
    }
}

