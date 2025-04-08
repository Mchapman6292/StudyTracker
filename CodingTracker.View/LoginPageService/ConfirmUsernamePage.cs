using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormService.NotificationManagers;

namespace CodingTracker.View.LoginPageService
{
    public partial class ConfirmUsernamePage : Form
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationLogger _appLogger;
        private readonly INotificationManager _notificationManager;

        public ConfirmUsernamePage(ICodingSessionRepository codingSessionRepository, IUserCredentialRepository userCredentialRepository, IFormSwitcher formSwitcher, IAuthenticationService authenticationService, IApplicationLogger appLogger, INotificationManager notificationManager)
        {
            _codingSessionRepository = codingSessionRepository;
            _userCredentialRepository = userCredentialRepository;
            _formSwitcher = formSwitcher;
            _authenticationService = authenticationService;
            _appLogger = appLogger;

            InitializeComponent();
        }

        private void ConfirmUsernamePage_Load(object sender, EventArgs e)
        {
            ConfirmUsernameButton.Visible = false;
        }

        private async void ConfirmUsernameButton_Click(object sender, EventArgs e)
        {
            string textBoxUsername = UsernameTextBox.Text;
            string message = string.Empty;

            if (textBoxUsername == string.Empty || textBoxUsername == "Username")
            {
                message = "Username cannot be blank.";
                _notificationManager.ShowNotificationDialog(this, EventArgs.Empty, DisplayMessageBox, message);
                return;
            }

            if (!await _userCredentialRepository.UsernameExistsAsync(textBoxUsername))
            {
                message = "No username located";
                _notificationManager.ShowNotificationDialog(this, EventArgs.Empty, DisplayMessageBox, message);
                return;
            }

            _formSwitcher.SwitchToForm(FormPageEnum.ResetPasswordPage);
            _authenticationService.SetUsernameForPasswordReset(textBoxUsername);

        }



 

        private void ConfirmUsernameExitButtonControlBox_Click(object sender, EventArgs e)
        {
            
        }

        private void ConfirmUsernameHomeButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.LoginPage);
        }
    }
}
