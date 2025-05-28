using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using CodingTracker.Common.Utilities;

namespace CodingTracker.View
{
    partial class LoginPage
    {
        private readonly IInputValidator _inputValidator;
        private readonly IUtilityService _utilityService;
        private Button startSessionButton;
        private Button endSessionButton;
        private Button viewSessionsButton;
        private Button setGoalButton;
        private TextBox goalHoursTextBox;
        private DataGridView sessionsDataGridView;
        private bool _isSessionActive = false;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.AnimatorNS.Animation animation1 = new Guna.UI2.AnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPage));
            rememberMeTextBox = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            LoginPageVLCPLayer = new LibVLCSharp.WinForms.VideoView();
            loginPageUsernameTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            LoginPagePasswordTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            rememberMeToggle = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            LoginPageMediaPanel = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            LoginPageExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            LoginPageCreationSuccessTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            LoginPageAnimateWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            loginButton = new CustomGradientButton();
            forgotPasswordButton = new CustomGradientButton();
            createAccountButton = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2Transition1 = new Guna.UI2.WinForms.Guna2Transition();
            ((System.ComponentModel.ISupportInitialize)LoginPageVLCPLayer).BeginInit();
            LoginPageMediaPanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // rememberMeTextBox
            // 
            guna2Transition1.SetDecoration(rememberMeTextBox, Guna.UI2.AnimatorNS.DecorationType.None);
            rememberMeTextBox.ForeColor = SystemColors.ButtonFace;
            rememberMeTextBox.Location = new Point(99, 294);
            rememberMeTextBox.Name = "rememberMeTextBox";
            rememberMeTextBox.Size = new Size(100, 23);
            rememberMeTextBox.TabIndex = 10;
            rememberMeTextBox.Text = "Remember me";
            // 
            // LoginPageVLCPLayer
            // 
            LoginPageVLCPLayer.AccessibleName = " ";
            LoginPageVLCPLayer.BackColor = Color.Black;
            guna2Transition1.SetDecoration(LoginPageVLCPLayer, Guna.UI2.AnimatorNS.DecorationType.None);
            LoginPageVLCPLayer.Location = new Point(0, 0);
            LoginPageVLCPLayer.Margin = new Padding(0);
            LoginPageVLCPLayer.MediaPlayer = null;
            LoginPageVLCPLayer.Name = "LoginPageVLCPLayer";
            LoginPageVLCPLayer.Size = new Size(820, 558);
            LoginPageVLCPLayer.TabIndex = 13;
            // 
            // loginPageUsernameTextbox
            // 
            loginPageUsernameTextbox.AutoRoundedCorners = true;
            loginPageUsernameTextbox.BorderColor = Color.FromArgb(255, 81, 195);
            loginPageUsernameTextbox.BorderRadius = 17;
            loginPageUsernameTextbox.CustomizableEdges = customizableEdges1;
            guna2Transition1.SetDecoration(loginPageUsernameTextbox, Guna.UI2.AnimatorNS.DecorationType.None);
            loginPageUsernameTextbox.DefaultText = "";
            loginPageUsernameTextbox.DisabledState.BorderColor = Color.FromArgb(255, 81, 195);
            loginPageUsernameTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            loginPageUsernameTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            loginPageUsernameTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            loginPageUsernameTextbox.FillColor = Color.FromArgb(35, 34, 50);
            loginPageUsernameTextbox.FocusedState.BorderColor = Color.FromArgb(168, 228, 255);
            loginPageUsernameTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginPageUsernameTextbox.ForeColor = Color.White;
            loginPageUsernameTextbox.Location = new Point(44, 149);
            loginPageUsernameTextbox.Name = "loginPageUsernameTextbox";
            loginPageUsernameTextbox.PasswordChar = '\0';
            loginPageUsernameTextbox.PlaceholderForeColor = Color.Azure;
            loginPageUsernameTextbox.PlaceholderText = "Username";
            loginPageUsernameTextbox.SelectedText = "";
            loginPageUsernameTextbox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            loginPageUsernameTextbox.Size = new Size(258, 36);
            loginPageUsernameTextbox.TabIndex = 15;
            loginPageUsernameTextbox.Enter += LoginPagePasswordTextbox_Enter;
            loginPageUsernameTextbox.Leave += LoginPageUsernameTextbox_Leave;
            // 
            // LoginPagePasswordTextbox
            // 
            LoginPagePasswordTextbox.AutoRoundedCorners = true;
            LoginPagePasswordTextbox.BorderColor = Color.FromArgb(255, 81, 195);
            LoginPagePasswordTextbox.BorderRadius = 17;
            LoginPagePasswordTextbox.CustomizableEdges = customizableEdges3;
            guna2Transition1.SetDecoration(LoginPagePasswordTextbox, Guna.UI2.AnimatorNS.DecorationType.None);
            LoginPagePasswordTextbox.DefaultText = "";
            LoginPagePasswordTextbox.DisabledState.BorderColor = Color.FromArgb(255, 81, 195);
            LoginPagePasswordTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            LoginPagePasswordTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            LoginPagePasswordTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            LoginPagePasswordTextbox.FillColor = Color.FromArgb(35, 34, 50);
            LoginPagePasswordTextbox.FocusedState.BorderColor = Color.FromArgb(168, 228, 255);
            LoginPagePasswordTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoginPagePasswordTextbox.Location = new Point(44, 225);
            LoginPagePasswordTextbox.Name = "LoginPagePasswordTextbox";
            LoginPagePasswordTextbox.PasswordChar = '●';
            LoginPagePasswordTextbox.PlaceholderForeColor = Color.Azure;
            LoginPagePasswordTextbox.PlaceholderText = "";
            LoginPagePasswordTextbox.SelectedText = "";
            LoginPagePasswordTextbox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            LoginPagePasswordTextbox.Size = new Size(258, 36);
            LoginPagePasswordTextbox.TabIndex = 16;
            LoginPagePasswordTextbox.UseSystemPasswordChar = true;
            LoginPagePasswordTextbox.Enter += LoginPagePasswordTextbox_Enter;
            LoginPagePasswordTextbox.Leave += LoginPagePasswordTextbox_Leave;
            // 
            // rememberMeToggle
            // 
            rememberMeToggle.CheckedState.BorderColor = Color.FromArgb(200, 220, 255);
            rememberMeToggle.CheckedState.BorderThickness = 2;
            rememberMeToggle.CheckedState.FillColor = Color.Transparent;
            rememberMeToggle.CheckedState.InnerColor = Color.FromArgb(168, 228, 255);
            rememberMeToggle.CustomizableEdges = customizableEdges5;
            guna2Transition1.SetDecoration(rememberMeToggle, Guna.UI2.AnimatorNS.DecorationType.None);
            rememberMeToggle.Location = new Point(58, 294);
            rememberMeToggle.Name = "rememberMeToggle";
            rememberMeToggle.ShadowDecoration.CustomizableEdges = customizableEdges6;
            rememberMeToggle.Size = new Size(35, 20);
            rememberMeToggle.TabIndex = 17;
            rememberMeToggle.UncheckedState.BorderColor = Color.FromArgb(70, 70, 80);
            rememberMeToggle.UncheckedState.BorderThickness = 2;
            rememberMeToggle.UncheckedState.FillColor = Color.FromArgb(50, 50, 60);
            rememberMeToggle.UncheckedState.InnerColor = Color.FromArgb(120, 120, 130);
            rememberMeToggle.CheckedChanged += LoginPageRememberMeToggle_CheckedChanged;
            // 
            // LoginPageMediaPanel
            // 
            LoginPageMediaPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LoginPageMediaPanel.Controls.Add(LoginPageVLCPLayer);
            LoginPageMediaPanel.CustomizableEdges = customizableEdges7;
            guna2Transition1.SetDecoration(LoginPageMediaPanel, Guna.UI2.AnimatorNS.DecorationType.None);
            LoginPageMediaPanel.ForeColor = Color.FromArgb(35, 34, 50);
            LoginPageMediaPanel.Location = new Point(461, 48);
            LoginPageMediaPanel.Margin = new Padding(0);
            LoginPageMediaPanel.Name = "LoginPageMediaPanel";
            LoginPageMediaPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            LoginPageMediaPanel.Size = new Size(820, 558);
            LoginPageMediaPanel.TabIndex = 22;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(guna2ControlBox1);
            guna2Panel1.Controls.Add(LoginPageExitControlBox);
            guna2Panel1.CustomizableEdges = customizableEdges13;
            guna2Transition1.SetDecoration(guna2Panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            guna2Panel1.Location = new Point(0, 1);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges14;
            guna2Panel1.Size = new Size(1281, 33);
            guna2Panel1.TabIndex = 23;
            // 
            // guna2ControlBox1
            // 
            guna2ControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            guna2ControlBox1.CustomizableEdges = customizableEdges9;
            guna2Transition1.SetDecoration(guna2ControlBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            guna2ControlBox1.FillColor = Color.FromArgb(25, 24, 40);
            guna2ControlBox1.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            guna2ControlBox1.HoverState.IconColor = Color.White;
            guna2ControlBox1.IconColor = Color.White;
            guna2ControlBox1.Location = new Point(1211, 1);
            guna2ControlBox1.Margin = new Padding(0);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2ControlBox1.Size = new Size(35, 29);
            guna2ControlBox1.TabIndex = 25;
            // 
            // LoginPageExitControlBox
            // 
            LoginPageExitControlBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LoginPageExitControlBox.CustomizableEdges = customizableEdges11;
            guna2Transition1.SetDecoration(LoginPageExitControlBox, Guna.UI2.AnimatorNS.DecorationType.None);
            LoginPageExitControlBox.FillColor = Color.FromArgb(25, 24, 40);
            LoginPageExitControlBox.HoverState.IconColor = Color.White;
            LoginPageExitControlBox.IconColor = Color.White;
            LoginPageExitControlBox.Location = new Point(1246, 1);
            LoginPageExitControlBox.Margin = new Padding(0);
            LoginPageExitControlBox.Name = "LoginPageExitControlBox";
            LoginPageExitControlBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
            LoginPageExitControlBox.Size = new Size(35, 29);
            LoginPageExitControlBox.TabIndex = 24;
            LoginPageExitControlBox.Click += LoginPageExitControlBox_Click;
            // 
            // LoginPageCreationSuccessTextBox
            // 
            LoginPageCreationSuccessTextBox.BorderColor = Color.FromArgb(35, 34, 50);
            LoginPageCreationSuccessTextBox.CustomizableEdges = customizableEdges15;
            guna2Transition1.SetDecoration(LoginPageCreationSuccessTextBox, Guna.UI2.AnimatorNS.DecorationType.None);
            LoginPageCreationSuccessTextBox.DefaultText = "";
            LoginPageCreationSuccessTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            LoginPageCreationSuccessTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            LoginPageCreationSuccessTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            LoginPageCreationSuccessTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            LoginPageCreationSuccessTextBox.FillColor = Color.FromArgb(35, 34, 50);
            LoginPageCreationSuccessTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            LoginPageCreationSuccessTextBox.Font = new Font("Segoe UI", 9F);
            LoginPageCreationSuccessTextBox.ForeColor = Color.YellowGreen;
            LoginPageCreationSuccessTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            LoginPageCreationSuccessTextBox.Location = new Point(44, 434);
            LoginPageCreationSuccessTextBox.Name = "LoginPageCreationSuccessTextBox";
            LoginPageCreationSuccessTextBox.PasswordChar = '\0';
            LoginPageCreationSuccessTextBox.PlaceholderForeColor = Color.FromArgb(35, 34, 50);
            LoginPageCreationSuccessTextBox.PlaceholderText = "";
            LoginPageCreationSuccessTextBox.SelectedText = "";
            LoginPageCreationSuccessTextBox.ShadowDecoration.CustomizableEdges = customizableEdges16;
            LoginPageCreationSuccessTextBox.Size = new Size(200, 36);
            LoginPageCreationSuccessTextBox.TabIndex = 24;
            // 
            // LoginPageAnimateWindow
            // 
            LoginPageAnimateWindow.TargetForm = this;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 20;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // loginButton
            // 
            loginButton.Animated = true;
            loginButton.EnableHoverRipple = true;
            loginButton.UseTransparentBackground = true;
            loginButton.AutoRoundedCorners = true;
            loginButton.BorderRadius = 17;
            loginButton.CustomizableEdges = customizableEdges21;
            guna2Transition1.SetDecoration(loginButton, Guna.UI2.AnimatorNS.DecorationType.None);
            loginButton.FillColor = Color.FromArgb(255, 81, 195);
            loginButton.FillColor2 = Color.FromArgb(168, 228, 255);
            loginButton.Font = new Font("Segoe UI", 9F);
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(44, 352);
            loginButton.Name = "loginButton";

            loginButton.Size = new Size(115, 36);
            loginButton.TabIndex = 27;
            loginButton.Text = "Login";
            loginButton.Click += LoginButton_Click;
            loginButton.MouseEnter += LoginButton_MouseEnter;
            loginButton.MouseLeave += LoginButton_MouseLeave;
            // 
            // forgotPasswordButton
            // 
            forgotPasswordButton.Animated = true;
            forgotPasswordButton.EnableHoverRipple = true;
            forgotPasswordButton.AutoRoundedCorners = true;
            forgotPasswordButton.BorderRadius = 17;
            forgotPasswordButton.CustomizableEdges = customizableEdges17;
            guna2Transition1.SetDecoration(forgotPasswordButton, Guna.UI2.AnimatorNS.DecorationType.None);
            forgotPasswordButton.FillColor = Color.FromArgb(255, 81, 195);
            forgotPasswordButton.FillColor2 = Color.FromArgb(168, 228, 255);
            forgotPasswordButton.Font = new Font("Segoe UI", 9F);
            forgotPasswordButton.ForeColor = Color.White;
            forgotPasswordButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            forgotPasswordButton.Location = new Point(187, 352);
            forgotPasswordButton.Name = "forgotPasswordButton";
            forgotPasswordButton.ShadowDecoration.CustomizableEdges = customizableEdges18;
            forgotPasswordButton.Size = new Size(115, 36);
            forgotPasswordButton.TabIndex = 29;
            forgotPasswordButton.Text = "Forgot Password";
            forgotPasswordButton.Click += NewForgotPasswordButton_Click;
            // 
            // createAccountButton
            // 
            createAccountButton.AutoRoundedCorners = true;
            createAccountButton.BorderColor = Color.Transparent;
            createAccountButton.BorderRadius = 10;
            createAccountButton.BorderThickness = 2;
            createAccountButton.Cursor = Cursors.Hand;
            createAccountButton.CustomizableEdges = customizableEdges19;
            guna2Transition1.SetDecoration(createAccountButton, Guna.UI2.AnimatorNS.DecorationType.None);
            createAccountButton.DisabledState.BorderColor = Color.DarkGray;
            createAccountButton.DisabledState.CustomBorderColor = Color.DarkGray;
            createAccountButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            createAccountButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            createAccountButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            createAccountButton.FillColor = Color.FromArgb(35, 34, 50);
            createAccountButton.FillColor2 = Color.FromArgb(35, 34, 50);
            createAccountButton.Font = new Font("Segoe UI", 9F);
            createAccountButton.ForeColor = Color.White;
            createAccountButton.Location = new Point(196, 291);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            createAccountButton.Size = new Size(123, 23);
            createAccountButton.TabIndex = 28;
            createAccountButton.Text = "Create Account";
            createAccountButton.Click += NewCreateAccountButton_Click;
            createAccountButton.MouseEnter += CreateAccountButton_MouseEnter;
            createAccountButton.MouseLeave += CreateAccountButton_MouseLeave;
            // 
            // guna2Transition1
            // 
            guna2Transition1.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = (PointF)resources.GetObject("animation1.BlindCoeff");
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = (PointF)resources.GetObject("animation1.MosaicCoeff");
            animation1.MosaicShift = (PointF)resources.GetObject("animation1.MosaicShift");
            animation1.MosaicSize = 0;
            animation1.Padding = new Padding(0, 0, 0, 0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = (PointF)resources.GetObject("animation1.ScaleCoeff");
            animation1.SlideCoeff = (PointF)resources.GetObject("animation1.SlideCoeff");
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            guna2Transition1.DefaultAnimation = animation1;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1284, 681);
            Controls.Add(forgotPasswordButton);
            Controls.Add(createAccountButton);
            Controls.Add(loginButton);
            Controls.Add(LoginPageCreationSuccessTextBox);
            Controls.Add(guna2Panel1);
            Controls.Add(rememberMeToggle);
            Controls.Add(LoginPagePasswordTextbox);
            Controls.Add(loginPageUsernameTextbox);
            Controls.Add(rememberMeTextBox);
            Controls.Add(LoginPageMediaPanel);
            guna2Transition1.SetDecoration(this, Guna.UI2.AnimatorNS.DecorationType.None);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginPage";
            SizeGripStyle = SizeGripStyle.Show;
            WindowState = FormWindowState.Minimized;
            Load += LoginPage_Load;
            ((System.ComponentModel.ISupportInitialize)LoginPageVLCPLayer).EndInit();
            LoginPageMediaPanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void ViewSessionsButton_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        #endregion
        private Label loginPageErrorTextbox;
        private Label rememberMeTextBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private LibVLCSharp.WinForms.VideoView LoginPageVLCPLayer;
        private Guna.UI2.WinForms.Guna2TextBox loginPageUsernameTextbox;
        private Guna.UI2.WinForms.Guna2TextBox LoginPagePasswordTextbox;
        private Guna.UI2.WinForms.Guna2ToggleSwitch rememberMeToggle;
        private Guna.UI2.WinForms.Guna2Panel LoginPageMediaPanel;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2TextBox LoginPageCreationSuccessTextBox;
        private Guna.UI2.WinForms.Guna2ControlBox LoginPageExitControlBox;
        private Guna.UI2.WinForms.Guna2AnimateWindow LoginPageAnimateWindow;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private CustomGradientButton loginButton;
        private CustomGradientButton forgotPasswordButton;
        private Guna.UI2.WinForms.Guna2GradientButton createAccountButton;
        private Guna.UI2.WinForms.Guna2Transition guna2Transition1;
    }
}