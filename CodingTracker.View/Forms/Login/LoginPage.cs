using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.Entities.UserCredentialEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Npgsql;

namespace CodingTracker.View
{
    public partial class LoginPage : Form
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationLogger _appLogger;
        private readonly IFormManager _formController;
        private readonly IFormNavigator _formNavigator;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormFactory _formFactory;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly IExitFlowManager _exitFlowManager;
        private readonly INotificationManager _notificationManager;
        private LibVLC _libVLC;
        private VideoView _videoView;

        public LoginPage(IAuthenticationService authenticationService, IApplicationLogger applogger, IFormManager formController, IFormNavigator formSwitcher, ICodingSessionManager codingSessionManager, IFormFactory formFactory, IFormStateManagement formStateManagement, IButtonHighlighterService buttonHighlighterService, IExitFlowManager buttonNotificationManager, INotificationManager notificationManager)
        {
            _authenticationService = authenticationService;
            _appLogger = applogger;
            _formController = formController;
            _formNavigator = formSwitcher;
            _codingSessionManager = codingSessionManager;
            _formFactory = formFactory;
            _formStateManagement = formStateManagement;
            _buttonHighlighterService = buttonHighlighterService;
            _exitFlowManager = buttonNotificationManager;
            _notificationManager = notificationManager;
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            InitializeVLCPlayer();

            // Set up text box events
     
            loginPageUsernameTextbox.Leave += LoginPageUsernameTextbox_Leave;
            LoginPagePasswordTextbox.Enter += LoginPagePasswordTextbox_Enter;
            LoginPagePasswordTextbox.Leave += LoginPagePasswordTextbox_Leave;

            // Set up button events
            createAccountButton.MouseEnter += NewForgotPasswordButton_MouseEnter;
            createAccountButton.MouseLeave += NewForgotPasswordButton_MouseLeave;



            // Load saved settings
            rememberMeToggle.Checked = Properties.Settings.Default.RememberMe;
            rememberMeToggle.CheckedChanged += RememberMeToggle_Checked;
            LoadSavedCredentials();

            // Set the _currentForm property in FormStateManagement to ensure that loginPage will be hidden when the forms are swapped. 
            _formStateManagement.SetCurrentForm(this);
            _notificationManager = notificationManager;
        }

        #region Media Player

        private string _videoTempPath;

        private void LoginPage_Load(object sender, EventArgs e)
        {
            _buttonHighlighterService.SetButtonHoverColors(loginButton);
            _buttonHighlighterService.SetButtonHoverColors(newForgotPasswordButton);
            _buttonHighlighterService.SetButtonHoverColors(createAccountButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(loginButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(createAccountButton);
        }


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



            string videoFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "FormMedia", "CodingTrackerLoginPagePlaceHolderMp4.mp4");

            _appLogger.Debug($"VideoFilePath: {videoFilePath}.");

            if (File.Exists(videoFilePath))
            {
                var media = new Media(_libVLC, new Uri(videoFilePath));
                media.AddOption("input-repeat=65535");
                _videoView.MediaPlayer.Play(media);
                _videoView.MediaPlayer.Scale = 0;
                _appLogger.Info($"VLC player loaded video from {videoFilePath}");
            }
            else
            {
                _appLogger.Error($"VLC player video file not found at {videoFilePath}");
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
                    rememberMeToggle.Checked = true;
                }
            }
        }

        private void SaveUsernameForNextLogin(string username)
        {
            try
            {
                if (rememberMeToggle.Checked)
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
            Properties.Settings.Default.RememberMe = rememberMeToggle.Checked;
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

        private void LoginPageUsernameTextbox_Enter(object sender, EventArgs e)
        {
            
        }

        private void LoginPageUsernameTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginPageUsernameTextbox.Text))
            {
                loginPageUsernameTextbox.Text = "Username";
                loginPageUsernameTextbox.PlaceholderForeColor = Color.FromArgb(102, 255, 255, 255);
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
        }

        private void NewForgotPasswordButton_MouseLeave(object sender, EventArgs e)
        {

        }

        private void LoginButton_MouseEnter(object sender, EventArgs e)
        {

        }

        private void LoginButton_MouseLeave(object sender, EventArgs e)
        {

        }

        private void CreateAccountButton_MouseEnter(object sender, EventArgs e)
        {

        }

        private void CreateAccountButton_MouseLeave(object sender, EventArgs e)
        {
        }

        #endregion

        #region Navigation and Authentication

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            /*
            loginButton.Enabled = false;
            */
            await Task.Delay(1000);
            _appLogger.Info($"Starting {nameof(LoginButton_Click)}.");

            try
            {
                string username = loginPageUsernameTextbox.Text;
                string password = LoginPagePasswordTextbox.Text;
                bool isValidLogin = await _authenticationService.AuthenticateLogin(username, password);


                if (isValidLogin)
                {
                    UserCredentialEntity userCredential = await _authenticationService.ReturnUserCredential(username);

                    _codingSessionManager.SetCurrentUserIdPlaceholder(userCredential.UserId);

                    SaveUsernameForNextLogin(username);

                    _formNavigator.SwitchToForm(FormPageEnum.MainPage);
                }
                if(!isValidLogin) 
                {
                    _notificationManager.ShowNotificationDialog(this, "Username or password is incorrect please try again.");
                }
            }
            catch (NpgsqlException ex)
            {
                _appLogger.Error($"Database error during login: {ex.Message}", ex);
                _notificationManager.ShowNotificationDialog(this,$"Unable to connect to the database.");
            }

            finally
            {
                /*
                loginButton.Enabled = true;
                */
            }
        }

        /*Change back once testing complete 
         var createAccountPage = _formNavigator.SwitchToForm(FormPageEnum.CreateAccountForm);
        _formStateManagement.UpdateAccountCreatedCallBack(AccountCreatedSuccessfully);
        */
        private void NewCreateAccountButton_Click(object sender, EventArgs e)
        {
            var createAccountPage = _formNavigator.SwitchToForm(FormPageEnum.MainPageTestFormn);
        }

        private void NewForgotPasswordButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.CreateAccountForm);
            _formStateManagement.UpdateAccountCreatedCallBack(AccountCreatedSuccessfully);
        }

        private void LoginPageExitControlBox_Click(object sender, EventArgs e)
        {
            _exitFlowManager.ExitCodingTracker();
        }

        private void RememberMeToggle_Checked(object sender, EventArgs e)
        {
            if (rememberMeToggle.Checked)
            {
                rememberMeTextBox.ForeColor = SystemColors.ControlLightLight;
            }
            else
            {
                rememberMeTextBox.ForeColor = Color.FromArgb(120, 120, 130);
            }
        }

        /*
                  rememberMeToggle.UncheckedState.FillColor = Color.FromArgb(50, 50, 60);
            rememberMeToggle.UncheckedState.InnerColor = Color.FromArgb(120, 120, 130);
        */

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
        }

        #endregion


    }
}