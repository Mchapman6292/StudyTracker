using Microsoft.Extensions.DependencyInjection;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.IApplicationLoggers;
using System.Diagnostics;
using CodingTracker.View.FormPageEnums;

namespace CodingTracker.View.FormService
{
    public interface IFormFactory
    {
        Form CreateForm(FormPageEnum formType);
        Form GetOrCreateForm(FormPageEnum formType);


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
                FormPageEnum.CreateAccountPage => typeof(CreateAccountPage),
                FormPageEnum.CodingSessionTimerPage => typeof(CodingSessionTimerForm),
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
    }
}

