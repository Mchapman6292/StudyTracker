using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.View.LoginPageService
{
    public partial class ConfirmUsernamePage : Form
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationLogger _appLogger;

        public ConfirmUsernamePage(ICodingSessionRepository codingSessionRepository, IUserCredentialRepository userCredentialRepository, IFormSwitcher formSwitcher, IAuthenticationService authenticationService, IApplicationLogger appLogger)
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
                ShowNotificationDialog(this, EventArgs.Empty, message);
                return;
            }

            if (!await _userCredentialRepository.UsernameExistsAsync(textBoxUsername))
            {
                message = "No username located";
                ShowNotificationDialog(this, EventArgs.Empty, message);
                return;
            }

            _formSwitcher.SwitchToForm(FormPageEnum.ResetPasswordPage);
            _authenticationService.SetUsernameForPasswordReset(textBoxUsername);

        }



        private void ShowNotificationDialog(object sender, EventArgs e, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                DisplayMessageBox.Text = "No message provided.";
            }
            else
            {
                DisplayMessageBox.Text = message;
            }

            DisplayMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            DisplayMessageBox.Caption = "Notification";
            DisplayMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;


            DisplayMessageBox.Show();

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
