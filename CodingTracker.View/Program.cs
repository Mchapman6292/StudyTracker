using CodingTracker.Business.Authentication.AuthenticationServices;
using CodingTracker.Business.CodingSessionManagers;
using CodingTracker.Business.CodingSessionService;
using CodingTracker.Business.CodingSessionService.SessionCalculators;
using CodingTracker.Business.InputValidators;
using CodingTracker.Business.MainPageService.LabelAssignments;
using CodingTracker.Business.MainPageService.PanelColorControls;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.BusinessInterfaces.IPanelColourControls;
using CodingTracker.Common.DataInterfaces;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IInputValidationResults;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.UtilityServices;
using CodingTracker.Data.Configurations;
using CodingTracker.Data.DbContextService.CodingTrackerDbContexts;
using CodingTracker.Data.Repositories.CodingSessionRepositories;
using CodingTracker.Data.Repositories.UserCredentialRepositories;
using CodingTracker.Logging.ApplicationLoggers;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.EditSessionPageService.DataGridRowManagers;
using CodingTracker.View.EditSessionPageService.DataGridViewManagers;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ButtonHighlighterServices;
using CodingTracker.View.FormService.LayoutServices;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.KeyboardActivityTrackerService.KeyboardActivityTrackers;
using CodingTracker.View.LoginPageService;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.TimerDisplayService;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;
using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using CodingTracker.View.TimerDisplayService.WaveVisualizationControls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.ApplicationControlService.DurationManagers;





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
                    .AddSingleton<IPanelColourControl, PanelColourControl>()
                    .AddSingleton<IFormSwitcher, FormSwitcher>()
                    .AddSingleton<ICodingSessionCountDownTimer, CodingSessionCountDownTimer>()
                    .AddSingleton<ICodingSessionRepository, CodingSessionRepository>()
                    .AddSingleton<ICodingSessionManager, CodingSessionManager>()
                    .AddSingleton<IUserCredentialRepository, UserCredentialRepository>()
                    .AddSingleton<IPanelColourAssigner, PanelColourAssigner>()
                    .AddSingleton<ILabelAssignment, LabelAssignment>()
                    .AddSingleton<IFormStateManagement, FormStateManagement>()
                    .AddSingleton<ILayoutService, LayoutService>()
                    .AddSingleton<IDataGridViewManager, DataGridViewManager>()
                    .AddSingleton<IRowStateManager, RowStateManager>()
                    .AddSingleton<IFormStatePropertyManager, FormStatePropertyManger>()
                    .AddSingleton<LoginPage>()
                    .AddSingleton<OrbitalTimerPage>()
                    .AddSingleton<INotificationManager, NotificationManager>()
                    .AddSingleton<IStopWatchTimerService, StopWatchTimerService>()
                    .AddSingleton<IKeyboardActivityTracker, KeyboardActivityTracker>()
                    .AddSingleton<IWaveVisualizationControl, WaveVisualizationControl>()
                    .AddSingleton<IExitFlowManager, ExitFlowManager>()
                    .AddSingleton<WaveVisualizationTestForm>()
                    .AddSingleton<IButtonHighlighterService,  ButtonHighlighterService>()
                    .AddSingleton<IDurationManager , DurationManager>()
                    .AddSingleton<TestForm>()
                    .AddSingleton<SessionNotesForm>()




                    .AddSingleton<MainPage>()
                    .AddTransient<EditSessionPage>()
                    .AddTransient<CreateAccountPage>()
                    .AddTransient<SessionGoalPage>()
                    .AddTransient<CountdownTimerForm>()
                    .AddTransient<ResetPasswordPage>()
                    .AddTransient<ConfirmUsernamePage>()

                    .AddDbContext<CodingTrackerDbContext>(options =>
                    options.UseNpgsql(connectionString), ServiceLifetime.Scoped).AddTransient<ICodingTrackerDbContext, CodingTrackerDbContext>();



            var startConfiguration = services.BuildServiceProvider()
                                             .GetRequiredService<IStartConfiguration>();
            startConfiguration.LoadConfiguration();
        }
    }
}
