using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;

namespace CodingTracker.View.LoginPageService
{
    public partial class ResetPasswordPage : Form
    {
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IFormNavigator _formNavigator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUtilityService _utilityService;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly IExitFlowManager _exitFlowManager;
        private readonly INotificationManager _notificationManager;
        public ResetPasswordPage(IUserCredentialRepository userCredentialRepository, IFormNavigator formSwitcher, IAuthenticationService authenticationService, IUtilityService utilityService, IButtonHighlighterService buttonHighlighterService, IExitFlowManager exitFlowManager, INotificationManager notificationManager)
        {

            _userCredentialRepository = userCredentialRepository;
            _formNavigator = formSwitcher;
            _authenticationService = authenticationService;
            _utilityService = utilityService;
            _buttonHighlighterService = buttonHighlighterService;
            _exitFlowManager = exitFlowManager;
            _notificationManager = notificationManager;
            InitializeComponent();
        }

        private void ResetPasswordPage_Load(object sender, EventArgs e)
        {
            _buttonHighlighterService.SetButtonHoverColors(resetPasswordButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(resetPasswordButton);
        }


        private void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            string newPassword = NewPasswordTextBox.Text;
            string message = string.Empty;


            if (newPassword == string.Empty || newPassword == "Reset Password")
            {
                message = "Please choose a new password.";
                _notificationManager.ShowNotificationDialog(this, message);
                return;
            }
            if (!_authenticationService.ValidatePasswordAndReturnErrorMessage(newPassword, out message))
            {
                _notificationManager.ShowNotificationDialog(this, message);
                return;
            }

            string username = _authenticationService.GetUsernameForPasswordReset();
            string hashedPassword = _utilityService.HashPassword(newPassword);
            _userCredentialRepository.UpdatePassWord(username, hashedPassword);

            message = "Password reset successful";
            _notificationManager.ShowNotificationDialog(this, message);
            _formNavigator.SwitchToForm(FormPageEnum.LoginPage);
        }

        private void ResetPasswordPageExitButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        private void ConfirmUsernameHomeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.LoginPage);
        }
    }
}
