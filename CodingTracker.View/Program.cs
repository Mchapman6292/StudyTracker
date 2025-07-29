using CodingTracker.Business.Authentication.AuthenticationServices;
using CodingTracker.Business.CodingSessionManagers;
using CodingTracker.Business.InputValidators;
using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.DataInterfaces;
using CodingTracker.Common.DataInterfaces.DbContextService;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.Utilities;
using CodingTracker.Data.Configuration;
using CodingTracker.Data.DbContextService.CodingTrackerDbContexts;
using CodingTracker.Data.Repositories.CodingSessionRepositories;
using CodingTracker.Data.Repositories.UserCredentialRepositories;
using CodingTracker.Logging.ApplicationLoggers;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms;
using CodingTracker.View.Forms.Services.AnimatedTimerService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.CountdownTimerService.CountdownTimerColorManagers;
using CodingTracker.View.Forms.Services.EditSessionPageService;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.MainPageService.DonutChartManagers;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using CodingTracker.View.Forms.Session;
using CodingTracker.View.Forms.WaveVisualizer;
using CodingTracker.View.LoginPageService;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.TimerDisplayService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


// Add good session indicator? / mouse up /down?



namespace CodingTracker.View.Program
{

    //https://github.com/am-523?tab=repositories
    //https://github.com/am-523/Animation-Slide-Panel-2/blob/main/tab.Designer.cs
    //https://dribbble.com/tags/dark-theme-dashboard

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            using var serviceProvider = services.BuildServiceProvider();


            ApplicationConfiguration.Initialize();

            var formFactory = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<IFormFactory>(serviceProvider);
            var loginPage = formFactory.GetOrCreateLoginPage();
           

            Application.Run(loginPage);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            IConfiguration configuration = configurationBuilder.Build();

            var remoteConnectionString = configuration.GetSection("DatabaseConfig:RemoteConnectionString").Value;
            var syncFusionKey = configuration.GetSection("SyncFusion:LicenseKey").Value;



            services.AddSingleton<IConfiguration>(configuration)
                    .AddSingleton<IStartConfiguration, StartConfiguration>()
                    .AddSingleton<IInputValidator, InputValidator>()
                    .AddSingleton<IApplicationLogger, ApplicationLogger>()
                    .AddSingleton<IUtilityService, UtilityService>()
                    .AddSingleton<IAuthenticationService, AuthenticationService>()
                    .AddSingleton<IFormFactory, FormFactory>()
                    .AddSingleton<IFormManager, FormManager>()
                    .AddSingleton<IInputValidationResult, InputValidationResult>()
                    .AddSingleton<IFormNavigator, FormNavigator>()
                    .AddSingleton<ICodingSessionRepository, CodingSessionRepository>()
                    .AddSingleton<ICodingSessionManager, CodingSessionManager>()
                    .AddSingleton<IUserCredentialRepository, UserCredentialRepository>()
                    .AddSingleton<IPanelColourAssigner, PanelColourAssigner>()
                    .AddSingleton<ILabelAssignment, LabelAssignment>()
                    .AddSingleton<IFormStateManagement, FormStateManagement>()
                    .AddSingleton<IDataGridViewManager, DataGridViewManager>()
                    .AddSingleton<IRowStateManager, RowStateManager>()
                    .AddSingleton<LoginPage>()
                    .AddSingleton<INotificationManager, NotificationManager>()
                    .AddSingleton<IStopWatchTimerService, StopWatchTimerService>()
                    .AddSingleton<IKeyboardActivityTracker, KeyboardActivityTracker>()
                    .AddSingleton<IExitFlowManager, ExitFlowManager>()
                    .AddSingleton<IButtonHighlighterService,  ButtonHighlighterService>()
                    .AddSingleton<SessionNotesForm>()
                    .AddSingleton<ICountdownTimerColorManager, CountdownTimerColorManager>()

                    .AddSingleton<IWaveRenderer, WaveRenderer>()
                    .AddSingleton<IWaveColorManager, WaveColorManager>()
                    .AddSingleton<IWaveBarStateManager, WaveBarStateManager>()
                    .AddSingleton<WaveVisualizationHost>()
                    .AddSingleton<ILast28DayPanelSettings, Last28Panelsettings>()
                    .AddSingleton<IDurationPanelHelper, DurationPanelHelper>()
                    .AddSingleton<ISessionVisualizationController, SessionVisualizationController>()
                    .AddSingleton<IDurationParentPanelPositionManager, DurationParentPanelPositionManager>()
                    .AddSingleton<ISessionDurationScaler, SessionDurationScaler>()
                    .AddSingleton<IMainPagePieChartManager, MainPagePieChartManager>()
                    .AddSingleton<IAnimatedTimerRenderer, AnimatedTimerRenderer>()
                    .AddSingleton<IAnimatedTimerManager, AnimatedTimerManager>()
                    .AddSingleton<IAnimatedTimerColumnFactory, AnimatedTimerColumnFactory>()
                    .AddSingleton<IPaintManager, PaintManager>()
                    .AddSingleton<IAnimatedColumnStateManager, AnimatedColumnStateManager>()
                    .AddSingleton<IAnimatedSegmentStateManager, AnimatedSegmentStateManager>()
                    .AddSingleton<IShadowBuilder, ShadowBuilder>()
                    .AddSingleton<IGradientManager, GradientManager>()
                    .AddSingleton<IAnimatedLogHelper, AnimatedLogHelper>()
                    .AddSingleton<IAnimationCalculator, AnimationCalculator>()
                    .AddSingleton<IAnimatedLogHelper, AnimatedLogHelper>()

                    .AddSingleton<IPathBuilder, PathBuilder>()
                    .AddSingleton<IRectanglePathBuilder, RectanglePathBuilder>()    

                    .AddSingleton<MainPage>()
                    .AddTransient<EditSessionPage>()
                    .AddTransient<CreateAccountPage>()
                    .AddTransient<SessionGoalPage>()
                    .AddTransient<CountdownTimerForm>()
                    .AddTransient<ResetPasswordPage>()
                    .AddTransient<ConfirmUsernamePage>()
                    .AddTransient<ElapsedTimerPage>()
                    .AddSingleton<SessionRatingForm>()
                    .AddSingleton<TimerPlaceHolderForm>()
                    .AddSingleton<AnimatedTimerForm>()
    


                    .AddSingleton< IDurationParentPanelFactory, DurationParentPanelFactory>()
                    .AddSingleton<IDurationPanelFactory, DurationPanelFactory>()
                    .AddSingleton<ISessionContainerPanelFactory, SessionContainerPanelFactory>()



                    .AddDbContext<CodingTrackerDbContext>(options =>
                    options.UseNpgsql(remoteConnectionString), ServiceLifetime.Scoped).AddTransient<ICodingTrackerDbContext, CodingTrackerDbContext>();



            var startConfiguration = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<IStartConfiguration>(services.BuildServiceProvider());
            startConfiguration.LoadConfiguration();
            startConfiguration.TestDatabaseConnection();
            startConfiguration.LogCodingSessionsTableColumns();
        }
    }
}
