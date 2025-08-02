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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.AnimatorNS.Animation animation1 = new Guna.UI2.AnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPage));
            rememberMeTextBox = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            LoginPageVLCPLayer = new LibVLCSharp.WinForms.VideoView();
            loginPageUsernameTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            LoginPagePasswordTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            rememberMeToggle = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            LoginPageMediaPanel = new Guna.UI2.WinForms.Guna2Panel();
            loginPageTopPanel = new Guna.UI2.WinForms.Guna2Panel();
            guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            LoginPageExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            LoginPageAnimateWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            loginButton = new CustomGradientButton();
            createAccountButton = new CustomGradientButton();
            forgotPasswordButton = new CustomGradientButton();
            guna2Transition1 = new Guna.UI2.WinForms.Guna2Transition();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            orLabel = new Label();
            guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            loginPageDragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
            colorDialog1 = new ColorDialog();
            ((System.ComponentModel.ISupportInitialize)LoginPageVLCPLayer).BeginInit();
            LoginPageMediaPanel.SuspendLayout();
            loginPageTopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // rememberMeTextBox
            // 
            guna2Transition1.SetDecoration(rememberMeTextBox, Guna.UI2.AnimatorNS.DecorationType.None);
            rememberMeTextBox.ForeColor = Color.FromArgb(255, 200, 230);
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
            loginPageUsernameTextbox.ForeColor = Color.FromArgb(120, 120, 130);
            loginPageUsernameTextbox.Location = new Point(44, 149);
            loginPageUsernameTextbox.Margin = new Padding(3, 4, 3, 4);
            loginPageUsernameTextbox.Name = "loginPageUsernameTextbox";
            loginPageUsernameTextbox.PlaceholderForeColor = Color.FromArgb(120, 120, 130);
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
            LoginPagePasswordTextbox.Margin = new Padding(3, 4, 3, 4);
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
            rememberMeToggle.CheckedState.BorderColor = Color.FromArgb(204, 84, 144);
            rememberMeToggle.CheckedState.BorderThickness = 2;
            rememberMeToggle.CheckedState.FillColor = Color.Transparent;
            rememberMeToggle.CheckedState.InnerColor = Color.FromArgb(255, 81, 195);
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
            // loginPageTopPanel
            // 
            loginPageTopPanel.Controls.Add(guna2ControlBox1);
            loginPageTopPanel.Controls.Add(LoginPageExitControlBox);
            loginPageTopPanel.CustomizableEdges = customizableEdges13;
            guna2Transition1.SetDecoration(loginPageTopPanel, Guna.UI2.AnimatorNS.DecorationType.None);
            loginPageTopPanel.Location = new Point(0, 1);
            loginPageTopPanel.Name = "loginPageTopPanel";
            loginPageTopPanel.ShadowDecoration.CustomizableEdges = customizableEdges14;
            loginPageTopPanel.Size = new Size(1281, 33);
            loginPageTopPanel.TabIndex = 23;
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
            guna2ControlBox1.IconColor = Color.FromArgb(255, 160, 210);
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
            LoginPageExitControlBox.IconColor = Color.FromArgb(255, 160, 210);
            LoginPageExitControlBox.Location = new Point(1246, 1);
            LoginPageExitControlBox.Margin = new Padding(0);
            LoginPageExitControlBox.Name = "LoginPageExitControlBox";
            LoginPageExitControlBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
            LoginPageExitControlBox.Size = new Size(35, 29);
            LoginPageExitControlBox.TabIndex = 24;
            LoginPageExitControlBox.Click += LoginPageExitControlBox_Click;
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
            loginButton.BackColor = Color.Transparent;
            loginButton.BorderColor = Color.FromArgb(35, 34, 50);
            loginButton.BorderRadius = 23;
            loginButton.CustomizableEdges = customizableEdges19;
            guna2Transition1.SetDecoration(loginButton, Guna.UI2.AnimatorNS.DecorationType.None);
            loginButton.EnableHoverRipple = true;
            loginButton.FillColor = Color.FromArgb(255, 81, 195);
            loginButton.FillColor2 = Color.FromArgb(168, 228, 255);
            loginButton.Font = new Font("Segoe UI", 9F);
            loginButton.ForeColor = Color.FloralWhite;
            loginButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            loginButton.Location = new Point(44, 352);
            loginButton.Name = "loginButton";
            loginButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            loginButton.Size = new Size(258, 36);
            loginButton.TabIndex = 27;
            loginButton.Text = "Login";
            loginButton.UseTransparentBackground = true;
            loginButton.Click += LoginButton_Click;
            loginButton.MouseEnter += LoginButton_MouseEnter;
            loginButton.MouseLeave += LoginButton_MouseLeave;
            // 
            // createAccountButton
            // 
            createAccountButton.Animated = true;
            createAccountButton.BorderColor = Color.HotPink;
            createAccountButton.BorderRadius = 23;
            createAccountButton.BorderThickness = 1;
            createAccountButton.CustomizableEdges = customizableEdges15;
            guna2Transition1.SetDecoration(createAccountButton, Guna.UI2.AnimatorNS.DecorationType.None);
            createAccountButton.EnableHoverRipple = true;
            createAccountButton.FillColor = Color.FromArgb(35, 34, 50);
            createAccountButton.FillColor2 = Color.FromArgb(35, 34, 50);
            createAccountButton.Font = new Font("Segoe UI", 9F);
            createAccountButton.ForeColor = Color.FloralWhite;
            createAccountButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            createAccountButton.Location = new Point(44, 450);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.ShadowDecoration.CustomizableEdges = customizableEdges16;
            createAccountButton.Size = new Size(258, 36);
            createAccountButton.TabIndex = 29;
            createAccountButton.Text = "Create Account";
            createAccountButton.Click += NewForgotPasswordButton_Click;
            // 
            // forgotPasswordButton
            // 
            forgotPasswordButton.Animated = true;
            forgotPasswordButton.EnableHoverRipple = true;
            forgotPasswordButton.EnableHoverRipple = true;
            forgotPasswordButton.AutoRoundedCorners = true;
            forgotPasswordButton.BorderColor = Color.Transparent;
            forgotPasswordButton.BorderRadius = 10;
            forgotPasswordButton.BorderThickness = 2;
            forgotPasswordButton.Cursor = Cursors.Hand;
            forgotPasswordButton.CustomizableEdges = customizableEdges17;
            guna2Transition1.SetDecoration(forgotPasswordButton, Guna.UI2.AnimatorNS.DecorationType.None);
            forgotPasswordButton.DisabledState.BorderColor = Color.DarkGray;
            forgotPasswordButton.DisabledState.CustomBorderColor = Color.DarkGray;
            forgotPasswordButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            forgotPasswordButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            forgotPasswordButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            forgotPasswordButton.FillColor = Color.FromArgb(35, 34, 50);
            forgotPasswordButton.FillColor2 = Color.FromArgb(35, 34, 50);
            forgotPasswordButton.Font = new Font("Segoe UI", 9F);
            forgotPasswordButton.ForeColor = Color.FromArgb(255, 200, 230);
            forgotPasswordButton.Location = new Point(196, 291);
            forgotPasswordButton.Name = "forgotPasswordButton";
            forgotPasswordButton.ShadowDecoration.CustomizableEdges = customizableEdges18;
            forgotPasswordButton.Size = new Size(158, 23);
            forgotPasswordButton.TabIndex = 28;
            forgotPasswordButton.Text = "Forgot password?";
            forgotPasswordButton.Click += NewCreateAccountButton_Click;
            forgotPasswordButton.MouseEnter += CreateAccountButton_MouseEnter;
            forgotPasswordButton.MouseLeave += CreateAccountButton_MouseLeave;
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
            animation1.Padding = new Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = (PointF)resources.GetObject("animation1.ScaleCoeff");
            animation1.SlideCoeff = (PointF)resources.GetObject("animation1.SlideCoeff");
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            guna2Transition1.DefaultAnimation = animation1;
            // 
            // guna2Separator1
            // 
            guna2Transition1.SetDecoration(guna2Separator1, Guna.UI2.AnimatorNS.DecorationType.None);
            guna2Separator1.FillColor = Color.FromArgb(138, 138, 138);
            guna2Separator1.Location = new Point(44, 416);
            guna2Separator1.Margin = new Padding(3, 2, 3, 2);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(114, 14);
            guna2Separator1.TabIndex = 30;
            // 
            // orLabel
            // 
            orLabel.AutoSize = true;
            guna2Transition1.SetDecoration(orLabel, Guna.UI2.AnimatorNS.DecorationType.None);
            orLabel.ForeColor = Color.FromArgb(138, 138, 138);
            orLabel.Location = new Point(162, 416);
            orLabel.Name = "orLabel";
            orLabel.Size = new Size(23, 15);
            orLabel.TabIndex = 31;
            orLabel.Text = "OR";
            // 
            // guna2Separator2
            // 
            guna2Transition1.SetDecoration(guna2Separator2, Guna.UI2.AnimatorNS.DecorationType.None);
            guna2Separator2.FillColor = Color.FromArgb(138, 138, 138);
            guna2Separator2.Location = new Point(188, 416);
            guna2Separator2.Margin = new Padding(3, 2, 3, 2);
            guna2Separator2.Name = "guna2Separator2";
            guna2Separator2.Size = new Size(114, 14);
            guna2Separator2.TabIndex = 32;
            // 
            // guna2HtmlLabel6
            // 
            guna2HtmlLabel6.Anchor = AnchorStyles.None;
            guna2HtmlLabel6.BackColor = Color.Transparent;
            guna2Transition1.SetDecoration(guna2HtmlLabel6, Guna.UI2.AnimatorNS.DecorationType.None);
            guna2HtmlLabel6.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            guna2HtmlLabel6.ForeColor = Color.FromArgb(204, 84, 144);
            guna2HtmlLabel6.Location = new Point(91, 67);
            guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            guna2HtmlLabel6.Size = new Size(196, 34);
            guna2HtmlLabel6.TabIndex = 34;
            guna2HtmlLabel6.Text = "CodingTracker";
            guna2HtmlLabel6.TextAlignment = ContentAlignment.TopCenter;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(35, 34, 50);
            guna2Transition1.SetDecoration(iconPictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            iconPictureBox1.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.CodePullRequest;
            iconPictureBox1.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 34;
            iconPictureBox1.Location = new Point(51, 67);
            iconPictureBox1.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(35, 34);
            iconPictureBox1.TabIndex = 42;
            iconPictureBox1.TabStop = false;
            // 
            // loginPageDragControl
            // 
            loginPageDragControl.DockIndicatorTransparencyValue = 0.6D;
            loginPageDragControl.TargetControl = loginPageTopPanel;
            loginPageDragControl.UseTransparentDrag = true;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1284, 681);
            Controls.Add(iconPictureBox1);
            Controls.Add(guna2HtmlLabel6);
            Controls.Add(guna2Separator2);
            Controls.Add(orLabel);
            Controls.Add(guna2Separator1);
            Controls.Add(createAccountButton);
            Controls.Add(forgotPasswordButton);
            Controls.Add(loginButton);
            Controls.Add(loginPageTopPanel);
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
            loginPageTopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Panel loginPageTopPanel;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox LoginPageExitControlBox;
        private Guna.UI2.WinForms.Guna2AnimateWindow LoginPageAnimateWindow;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private CustomGradientButton loginButton;
        private CustomGradientButton createAccountButton;
        private CustomGradientButton forgotPasswordButton;
        private Guna.UI2.WinForms.Guna2Transition guna2Transition1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Label orLabel;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Guna.UI2.WinForms.Guna2DragControl loginPageDragControl;
        private ColorDialog colorDialog1;
    }
}