using Microsoft.Extensions.DependencyInjection;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.IApplicationLoggers;
using System.Diagnostics;

namespace CodingTracker.View.FormService
{
    public interface IFormFactory
    {
        T CreateForm<T>(string methodName) where T : class;
        LoginPage CreateLoginPage();
        MainPage CreateMainPage();
        CodingSessionPage CreateCodingSessionPage();
        EditSessionPage CreateEditSessionPage();
        CreateAccountPage CreateAccountPage();
        CodingSessionTimerForm CreateCodingSessionTimer();

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

        public T CreateForm<T>(string methodName) where T : class
        {
            using (var activity = new Activity(methodName).Start())
            {
                _appLogger.Info($"Starting {methodName}. TraceID: {activity.TraceId}");
                try
                {
                    var page = _serviceProvider.GetRequiredService<T>();
                    _appLogger.Info($"Created {typeof(T).Name} successfully. TraceID: {activity.TraceId}");
                    return page;
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"An error occurred during {methodName}. Error: {ex.Message}. TraceID: {activity.TraceId}", ex);
                    throw;
                }
            }
        }

        public LoginPage CreateLoginPage()
        {
            return CreateForm<LoginPage>(nameof(CreateLoginPage));
        }

        public MainPage CreateMainPage()
        {
            if (_formStateManagement.GetMainPageInstance() == null)
            {
                _formStateManagement.SetMainPageInstance(CreateForm<MainPage>(nameof(CreateMainPage)));
            }
            return _formStateManagement.GetMainPageInstance();
        }


        public CodingSessionPage CreateCodingSessionPage()
        {
            if (_formStateManagement.GetCodingSessionPageInstance() == null)
            {
                _formStateManagement.SetCodingSessionPageInstance(CreateForm<CodingSessionPage>(nameof(CreateCodingSessionPage)));
            }
            return _formStateManagement.GetCodingSessionPageInstance();
        }

        public EditSessionPage CreateEditSessionPage()
        {
            if (_formStateManagement.GetEditSessionPageInstance() == null)
            {
                _formStateManagement.SetEditSessionPageInstance(CreateForm<EditSessionPage>(nameof(CreateEditSessionPage)));
            }
            return _formStateManagement.GetEditSessionPageInstance();
        }

        public CreateAccountPage CreateAccountPage()
        {
            if (_formStateManagement.GetCreateAccountPageInstance() == null)
            {
                _formStateManagement.SetCreateAccountPageInstance(CreateForm<CreateAccountPage>(nameof(CreateAccountPage)));
            }
            return _formStateManagement.GetCreateAccountPageInstance();
        }

        public CodingSessionTimerForm CreateCodingSessionTimer()
        {
            if (_formStateManagement.GetCodingSessionTimerInstance() == null)
            {
                _formStateManagement.SetCodingSessionTimerInstance(CreateForm<CodingSessionTimerForm>(nameof(CreateCodingSessionTimer)));
            }
            return _formStateManagement.GetCodingSessionTimerInstance();
        }
    }
}
