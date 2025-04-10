using CodingTracker.Business.ApplicationControls;
using CodingTracker.Business.Authentication.AuthenticationServices;
using CodingTracker.Business.CodingSessionManagers;
using CodingTracker.Business.CodingSessionService;
using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.CodingSessionService.UserIdServices;
using CodingTracker.Business.InputValidators;
using CodingTracker.Business.MainPageService.LabelAssignments;
using CodingTracker.Business.MainPageService.PanelColorControls;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionTimers;
using CodingTracker.Common.BusinessInterfaces.IPanelColourControls;
using CodingTracker.Common.DataInterfaces;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.Common.ErrorHandlers;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IErrorHandlers;
using CodingTracker.Common.IInputValidationResults;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.UtilityServices;
using CodingTracker.Data.Configurations;
using CodingTracker.Data.DbContextService.CodingTrackerDbContexts;
using CodingTracker.Data.Repositories.CodingSessionRepositories;
using CodingTracker.Data.Repositories.UserCredentialRepositories;
using CodingTracker.Logging.ApplicationLoggers;
using CodingTracker.View.FormService;
using CodingTracker.View.IMessageBoxManagers;
using CodingTracker.View.MessageBoxManagers;
using CodingTracker.View.SessionGoalCountDownTimers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodingTracker.View.FormService.LayoutServices;
using CodingTracker.View.EditSessionPageService.DataGridViewManagers;
using CodingTracker.View.EditSessionPageService.DataGridRowManagers;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.LoginPageService;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.TimerDisplayService;

/// To do

/// Change date to Today 18:15 etc
/// Categories for Study type. 




namespace CodingTracker.View.Program
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);



            using var serviceProvider = services.BuildServiceProvider();
            ApplicationConfiguration.Initialize();

            var formFactory = serviceProvider.GetRequiredService<IFormFactory>();
            var loginPage = formFactory.GetOrCreateLoginPage();
            

   

            Application.Run(loginPage);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            IConfiguration configuration = configurationBuilder.Build();

            var connectionString = configuration.GetSection("DatabaseConfig:ConnectionString").Value;

            services.AddSingleton<IConfiguration>(configuration)
                    .AddSingleton<IStartConfiguration, StartConfiguration>()  
                    .AddSingleton<IInputValidator, InputValidator>()
                    .AddSingleton<IApplicationLogger, ApplicationLogger>()
                    .AddSingleton<IUtilityService, UtilityService>()
                    .AddSingleton<IApplicationControl, ApplicationControl>()
                    .AddSingleton<IAuthenticationService, AuthenticationService>()
                    .AddSingleton<ISessionCalculator, SessionCalculator>()
                    .AddSingleton<IFormFactory, FormFactory>()
                    .AddSingleton<IFormController, FormController>()
                    .AddSingleton<IInputValidationResult, InputValidationResult>()
                    .AddSingleton<IMessageBoxManager, MessageBoxManager>()
                    .AddSingleton<IPanelColourControl, PanelColourControl>()
                    .AddSingleton<IErrorHandler, ErrorHandler>()
                    .AddSingleton<IFormSwitcher, FormSwitcher>()
                    .AddSingleton<ICodingSessionTimer, CodingSessionTimer>()
                    .AddSingleton<ICodingSessionCountDownTimer, CodingSessionCountDownTimer>()
                    .AddSingleton<ICodingSessionRepository, CodingSessionRepository>()
                    .AddSingleton<ICodingTrackerDbContext, CodingTrackerDbContext>()
                    .AddSingleton<ICodingSessionManager, CodingSessionManager>()
                    .AddSingleton<IUserCredentialRepository , UserCredentialRepository>()
                    .AddSingleton<IPanelColourAssigner, PanelColourAssigner>()
                    .AddSingleton<ILabelAssignment, LabelAssignment>()
                    .AddSingleton<IFormStateManagement, FormStateManagement>()
                    .AddSingleton<IUserIdService, UserIdService>()
                    .AddSingleton<ILayoutService, LayoutService>()
                    .AddSingleton<IDataGridViewManager , DataGridViewManager>()
                    .AddSingleton<IRowStateManager, RowStateManager>()
                    .AddSingleton<IFormStatePropertyManager, FormStatePropertyManger>()
                    .AddSingleton<LoginPage>()
                    .AddSingleton<OrbitalTimerForm>()
                    .AddSingleton<INotificationManager, NotificationManager>()



                    .AddSingleton<UserIdService , UserIdService>()


                    // Transient services.
                    .AddTransient<ISessionGoalCountDownTimer, SessionGoalCountdownTimer>()
   
                    .AddTransient<MainPage>()
                    .AddTransient<CodingSessionPage>()
                    .AddTransient<EditSessionPage>()
                    .AddTransient<CodingSessionTimerForm>()
                    .AddTransient<PassWordTextBox>()
                    .AddTransient<SessionGoalForm>()
                    .AddTransient<SessionTimerForm>()
                    .AddTransient<ResetPasswordPage>()
                    .AddTransient<ConfirmUsernamePage>()

                    .AddDbContext<CodingTrackerDbContext>(options =>
                    options.UseNpgsql(connectionString), ServiceLifetime.Scoped).AddScoped<ICodingTrackerDbContext, CodingTrackerDbContext>();



            var startConfiguration = services.BuildServiceProvider()
                                             .GetRequiredService<IStartConfiguration>();
            startConfiguration.LoadConfiguration();
        }
    }
}
