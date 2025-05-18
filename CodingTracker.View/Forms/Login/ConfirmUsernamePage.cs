using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;

namespace CodingTracker.View.LoginPageService
{
    public partial class ConfirmUsernamePage : Form
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IFormNavigator _formNavigator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationLogger _appLogger;
        private readonly INotificationManager _notificationManager;
        private readonly IExitFlowManager _exitFlowManager;
        private readonly IButtonHighlighterService _buttonHighlighterService;

        public ConfirmUsernamePage(ICodingSessionRepository codingSessionRepository, IUserCredentialRepository userCredentialRepository, IFormNavigator formSwitcher, IAuthenticationService authenticationService, IApplicationLogger appLogger, INotificationManager notificationManager, IExitFlowManager exitFlowManager, IButtonHighlighterService buttonHighlighterService)
        {
            _codingSessionRepository = codingSessionRepository;
            _userCredentialRepository = userCredentialRepository;
            _formNavigator = formSwitcher;
            _authenticationService = authenticationService;
            _appLogger = appLogger;
            _notificationManager = notificationManager;
            _exitFlowManager = exitFlowManager;
            _buttonHighlighterService = buttonHighlighterService;

            InitializeComponent();
        }

        private void ConfirmUsernamePage_Load(object sender, EventArgs e)
        {
            _buttonHighlighterService.SetButtonHoverColors(confirmUsernameButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(confirmUsernameButton);
            confirmUsernameButton.Visible = false;
        }

        private async void ConfirmUsernameButton_Click(object sender, EventArgs e)
        {
            string textBoxUsername = UsernameTextBox.Text;
            string message = string.Empty;

            if (textBoxUsername == string.Empty || textBoxUsername == "Username")
            {
                message = "Username cannot be blank.";
                _notificationManager.ShowNotificationDialog(this, message);
                return;
            }

            if (!await _userCredentialRepository.UsernameExistsAsync(textBoxUsername))
            {
                message = "No username located";
                _notificationManager.ShowNotificationDialog(this, message);
                return;
            }

            _formNavigator.SwitchToForm(FormPageEnum.ResetPasswordForm);
            _authenticationService.SetUsernameForPasswordReset(textBoxUsername);

        }





        private void ConfirmUsernameExitButtonControlBox_Click(object sender, EventArgs e)
        {
            _exitFlowManager.ExitCodingTracker();
        }

        private void ConfirmUsernameHomeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.LoginPage);
        }
    }
}
