using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.View.FormManagement;

namespace CodingTracker.View.LoginPageService
{
    public partial class ResetPasswordPage : Form
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IFormNavigator _formNavigator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUtilityService _utilityService;
        public ResetPasswordPage(ICodingSessionRepository codingSessionRepository, IUserCredentialRepository userCredentialRepository, IFormNavigator formSwitcher, IAuthenticationService authenticationService, IUtilityService utilityService)
        {
            _codingSessionRepository = codingSessionRepository;
            _userCredentialRepository = userCredentialRepository;
            _formNavigator = formSwitcher;
            _authenticationService = authenticationService;
            _utilityService = utilityService;
            InitializeComponent();
        }

        private void ResetPasswordPage_Load(object sender, EventArgs e)
        {

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

            // Set button text to make it obvious
            DisplayMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            DisplayMessageBox.Caption = "Notification";
            DisplayMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;


            DisplayMessageBox.Show();

        }

        private void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            string newPassword = NewPasswordTextBox.Text;
            string message = string.Empty;


            if (newPassword == string.Empty || newPassword == "Reset Password")
            {
                message = "Please choose a new password.";
                ShowNotificationDialog(this, EventArgs.Empty, message);
                return;
            }
            if (!_authenticationService.CheckPasswordValid(newPassword, out message))
            {
                ShowNotificationDialog(this, EventArgs.Empty, message);
                return;
            }

            string username = _authenticationService.GetUsernameForPasswordReset();
            string hashedPassword = _utilityService.HashPassword(newPassword);
            _userCredentialRepository.UpdatePassWord(username, hashedPassword);

            message = "Password reset successful";
            ShowNotificationDialog(this, EventArgs.Empty, message);
            _formNavigator.SwitchToForm(FormPageEnum.LoginPage);
        }

        private void ResetPasswordPageExitButton_Click(object sender, EventArgs e)
        {

        }

        private void ConfirmUsernameHomeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.LoginPage);
        }
    }
}
