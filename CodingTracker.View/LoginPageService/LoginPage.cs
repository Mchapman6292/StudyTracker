using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ButtonHighlighterServices;
using Guna.UI2.WinForms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace CodingTracker.View
{
    public partial class LoginPage : Form
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationControl _appControl;
        private readonly IApplicationLogger _appLogger;
        private readonly IFormController _formController;
        private readonly IFormSwitcher _formSwitcher;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormFactory _formFactory;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private LibVLC _libVLC;
        private VideoView _videoView;

        public LoginPage(IAuthenticationService authenticationService, IApplicationControl appControl, IApplicationLogger applogger, IFormController formController, IFormSwitcher formSwitcher, ICodingSessionManager codingSessionManager,IFormFactory formFactory, IFormStateManagement formStateManagement, IButtonHighlighterService buttonHighlighterService)
        {
            _authenticationService = authenticationService;
            _appControl = appControl;
            _appLogger = applogger;
            _formController = formController;
            _formSwitcher = formSwitcher;
            _codingSessionManager = codingSessionManager;
            _formFactory = formFactory;
            _formStateManagement = formStateManagement;
            _buttonHighlighterService = buttonHighlighterService;
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            InitializeVLCPlayer();

            // Set up text box events
            loginPageUsernameTextbox.Enter += LoginPagePasswordTextbox_Enter;
            loginPageUsernameTextbox.Leave += LoginPageUsernameTextbox_Leave;
            LoginPagePasswordTextbox.Enter += LoginPagePasswordTextbox_Enter;
            LoginPagePasswordTextbox.Leave += LoginPagePasswordTextbox_Leave;

            // Set up button events
            NewForgotPasswordButton.MouseEnter += NewForgotPasswordButton_MouseEnter;
            NewForgotPasswordButton.MouseLeave += NewForgotPasswordButton_MouseLeave;

            // Load saved settings
            LoginPageRememberMeToggle.Checked = Properties.Settings.Default.RememberMe;
            LoadSavedCredentials();

            // Set the _currentForm property in FormStateManagement to ensure that loginPage will be hidden when the forms are swapped. 
            _formStateManagement.SetCurrentForm(this);
        }

        #region Media Player

        private void InitializeVLCPlayer()
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _videoView = new VideoView
            {
                MediaPlayer = new MediaPlayer(_libVLC)
            };
            _videoView.Location = new Point(0, 0);
            _videoView.Size = new Size(888, 581);
            LoginPageMediaPanel.Controls.Add(_videoView);
            _videoView.BringToFront();

            // Path is relative to the executable location
            string videoFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormMedia", "CodingTrackerLoginPagePlaceHolderMp4.mp4");

            if (File.Exists(videoFilePath))
            {
                var media = new Media(_libVLC, new Uri(videoFilePath));
                media.AddOption("input-repeat=65535"); // Loop the video indefinitely
                _videoView.MediaPlayer.Play(media);
                _videoView.MediaPlayer.Scale = 0;
                _appLogger.Info($"VLC player loaded video from {videoFilePath}");
            }
            else
            {
                _appLogger.Warning($"VLC player video file not found at {videoFilePath}");
                MessageBox.Show("Video file not found at the specified path: " + videoFilePath);
            }
        }

        #endregion

        #region Credential Management

        private void LoadSavedCredentials()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                var lastUsername = Properties.Settings.Default.LastUsername;
                if (!string.IsNullOrEmpty(lastUsername))
                {
                    loginPageUsernameTextbox.Text = lastUsername;
                    LoginPageRememberMeToggle.Checked = true;
                }
            }
        }

        private void SaveUsernameForNextLogin(string username)
        {
            try
            {
                if (LoginPageRememberMeToggle.Checked)
                {
                    Properties.Settings.Default.LastUsername = username;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error saving username: {ex.Message}");
            }
        }

        private void LoginPageRememberMeToggle_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RememberMe = LoginPageRememberMeToggle.Checked;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Text Box Handling

        private void LoginPagePasswordTextbox_Enter(object sender, EventArgs e)
        {
            if (LoginPagePasswordTextbox.Text == "Password")
            {
                LoginPagePasswordTextbox.Text = "";
                LoginPagePasswordTextbox.ForeColor = Color.Black;
                LoginPagePasswordTextbox.PasswordChar = '●';
            }
        }

        private void LoginPageUsernameTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginPageUsernameTextbox.Text))
            {
                loginPageUsernameTextbox.Text = "Username";
                loginPageUsernameTextbox.ForeColor = Color.White;
            }
        }

        private void LoginPagePasswordTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginPagePasswordTextbox.Text))
            {
                LoginPagePasswordTextbox.Text = "Password";
                LoginPagePasswordTextbox.ForeColor = Color.Gray;
                LoginPagePasswordTextbox.PasswordChar = '\0';
            }
        }

        #endregion

        #region Button Handling

        private void NewForgotPasswordButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2GradientButton button)
            {
                _buttonHighlighterService.HighlightButtonWithHoverColour(button);
            }
        }

        private void NewForgotPasswordButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Guna2GradientButton button)
            {
                _buttonHighlighterService.SetFillColorToTransparent(button);
            }
        }

        #endregion

        #region Navigation and Authentication

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            _appLogger.Info($"Starting {nameof(LoginButton_Click)}.");

            string username = loginPageUsernameTextbox.Text;
            string password = LoginPagePasswordTextbox.Text;
            bool isValidLogin = await _authenticationService.AuthenticateLogin(username, password);

            await _authenticationService.ReturnUserCredentialIfLoginAuthenticated(isValidLogin, username);

            if (isValidLogin)
            {
                UserCredentialEntity userCredential = await _authenticationService.ReturnUserCredentialIfLoginAuthenticated(isValidLogin, username);

                // Create the codingSession object, CodingSession timers are started separately when the timer is started by the user.
                await _codingSessionManager.OldStartCodingSession(username);
                await _codingSessionManager.SetUserIdForCurrentSessionAsync(username, password);
                _codingSessionManager.SetCurrentUserId(userCredential.UserId);

                SaveUsernameForNextLogin(username);

                Form mainPage = _formSwitcher.SwitchToForm(FormPageEnum.MainPage);

                this.Hide();
                mainPage.Show();
            }
        }

        private void NewCreateAccountButton_Click(object sender, EventArgs e)
        {
            var createAccountPage = _formSwitcher.SwitchToForm(FormPageEnum.CreateAccountPage);
            _formStateManagement.UpdateAccountCreatedCallBack(AccountCreatedSuccessfully);
        }

        private void NewForgotPasswordButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.ConfirmUsernamePage);
        }

        private void LoginPageExitControlBox_Click(object sender, EventArgs e)
        {
            _appControl.ExitCodingTrackerAsync();
        }

        #endregion

        #region Feedback Messages

        private void AccountCreatedSuccessfully(string message)
        {
            _appLogger.Debug("AccountCreatedSuccessfully method called.");

            this.Invoke((MethodInvoker)(() =>
            {
                _appLogger.Debug("Inside Invoke method of AccountCreatedSuccessfully.");
                LoginPageDisplaySuccessMessage(message);
            }));
        }

        private void LoginPageDisplaySuccessMessage(string message)
        {
            LoginPageCreationSuccessTextBox.Text = message;
        }

        #endregion
    }
}