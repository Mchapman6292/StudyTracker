using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.IUserCredentialRepositories;
using CodingTracker.View.FormService;

namespace CodingTracker.View.LoginPageService
{
    public partial class ResetPasswordPage : Form
    {
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IFormSwitcher _formSwitcher;
        public ResetPasswordPage(ICodingSessionRepository codingSessionRepository, IUserCredentialRepository userCredentialRepository, IFormSwitcher formSwitcher)
        {
            _codingSessionRepository = codingSessionRepository;
            _userCredentialRepository = userCredentialRepository;
            _formSwitcher = formSwitcher;
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
    }
}
