using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using System.Diagnostics;

namespace CodingTracker.View
{

    public partial class CreateAccountPage : Form
    {
        private readonly IInputValidator _inputValidator;
        private readonly IApplicationLogger _appLogger;
        private readonly IFormManager _formController;
        private readonly IFormNavigator _formNavigator;
        private readonly IAuthenticationService _authenticationService;
        public Action<string> AccountCreatedCallback { get; set; }

        public CreateAccountPage(IInputValidator inputValidator, IApplicationLogger appLogger, IFormManager formController, IFormNavigator formSwitcher, IAuthenticationService authentication)
        {
            InitializeComponent();
            _inputValidator = inputValidator;
            _appLogger = appLogger;
            _formController = formController;
            _formNavigator = formSwitcher;
            _authenticationService = authentication;

        }

        private void DisplayErrorMessage(string message)
        {
            CreateAccountPageErrorTextBox.Text = message;
        }



        private async void CreateAccountPageCreateAccountButton_Click(object sender, EventArgs e)
        {
            Stopwatch overallStopwatch = Stopwatch.StartNew();
            using (var activity = new Activity(nameof(CreateAccountPageCreateAccountButton)).Start())
            {
                _appLogger.Debug($"Starting {nameof(CreateAccountPageCreateAccountButton)}. TraceID: {activity.TraceId}");

                string username = CreateAccountPageUsernameTextbox.Text;
                string password = CreateAccountPasswordTextbox.Text;

                var usernameResult = _inputValidator.ValidateUsername(username);
                var passwordResult = _inputValidator.ValidatePassword(password);

                if (usernameResult.IsValid && passwordResult.IsValid)
                {
                    try
                    {
                        bool isAccountCreated = await _authenticationService.CreateAccount(username, password);

                        if (isAccountCreated)
                        {
                            _appLogger.Info($"Account creation successful for user: {username}. Total Duration: {overallStopwatch.ElapsedMilliseconds}ms. TraceID: {activity.TraceId}");

                            AccountCreatedCallback?.Invoke("Account created successfully.");
                            _formNavigator.SwitchToForm(FormPageEnum.LoginPage);
                        }
                        else
                        {
                            _appLogger.Warning($"Account creation verification failed for user: {username}. Total Duration: {overallStopwatch.ElapsedMilliseconds}ms. TraceID: {activity.TraceId}");
                            DisplayErrorMessage("Account creation failed. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Account creation failed for user: {username}. Error: {ex.Message}. Total Duration: {overallStopwatch.ElapsedMilliseconds}ms. TraceID: {activity.TraceId}", ex);
                        DisplayErrorMessage(ex.Message);
                    }
                }
                else
                {
                    var errorMessages = $"{usernameResult.GetAllErrorMessages()}\n{passwordResult.GetAllErrorMessages()}";
                    _appLogger.Warning($"Validation failed for username or password. Total Duration: {overallStopwatch.ElapsedMilliseconds}ms. TraceID: {activity.TraceId}");
                    DisplayErrorMessage(errorMessages);
                }
            }
            overallStopwatch.Stop();
        }



    }
}