
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            loginPageErrorTextbox = new Label();
            label1 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            LoginPageVLCPLayer = new LibVLCSharp.WinForms.VideoView();
            loginPageUsernameTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            LoginPagePasswordTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            LoginPageRememberMeToggle = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            LoginPageMediaPanel = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            LoginPageExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            LoginPageCreationSuccessTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            LoginPageAnimateWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            NewLoginButton = new Guna.UI2.WinForms.Guna2GradientButton();
            NewCreateAccountButton = new Guna.UI2.WinForms.Guna2GradientButton();
            NewForgotPasswordButton = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)LoginPageVLCPLayer).BeginInit();
            LoginPageMediaPanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // loginPageErrorTextbox
            // 
            loginPageErrorTextbox.Location = new Point(58, 447);
            loginPageErrorTextbox.Name = "loginPageErrorTextbox";
            loginPageErrorTextbox.Size = new Size(226, 23);
            loginPageErrorTextbox.TabIndex = 5;
            loginPageErrorTextbox.Visible = false;
            // 
            // label1
            // 
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(85, 294);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 10;
            label1.Text = "Remember me";
            // 
            // LoginPageVLCPLayer
            // 
            LoginPageVLCPLayer.AccessibleName = " ";
            LoginPageVLCPLayer.BackColor = Color.Black;
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
            loginPageUsernameTextbox.BorderColor = Color.FromArgb(234, 153, 149);
            loginPageUsernameTextbox.BorderRadius = 17;
            loginPageUsernameTextbox.CustomizableEdges = customizableEdges1;
            loginPageUsernameTextbox.DefaultText = "";
            loginPageUsernameTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            loginPageUsernameTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            loginPageUsernameTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            loginPageUsernameTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            loginPageUsernameTextbox.FillColor = Color.FromArgb(35, 34, 50);
            loginPageUsernameTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            loginPageUsernameTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginPageUsernameTextbox.ForeColor = Color.White;
            loginPageUsernameTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            loginPageUsernameTextbox.Location = new Point(31, 162);
            loginPageUsernameTextbox.Name = "loginPageUsernameTextbox";
            loginPageUsernameTextbox.PasswordChar = '\0';
            loginPageUsernameTextbox.PlaceholderForeColor = Color.Azure;
            loginPageUsernameTextbox.PlaceholderText = "Username";
            loginPageUsernameTextbox.SelectedText = "";
            loginPageUsernameTextbox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            loginPageUsernameTextbox.Size = new Size(200, 36);
            loginPageUsernameTextbox.TabIndex = 15;
            loginPageUsernameTextbox.Enter += LoginPagePasswordTextbox_Enter;
            loginPageUsernameTextbox.Leave += LoginPageUsernameTextbox_Leave;
            // 
            // LoginPagePasswordTextbox
            // 
            LoginPagePasswordTextbox.AutoRoundedCorners = true;
            LoginPagePasswordTextbox.BorderColor = Color.FromArgb(234, 153, 149);
            LoginPagePasswordTextbox.BorderRadius = 17;
            LoginPagePasswordTextbox.CustomizableEdges = customizableEdges3;
            LoginPagePasswordTextbox.DefaultText = "";
            LoginPagePasswordTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            LoginPagePasswordTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            LoginPagePasswordTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            LoginPagePasswordTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            LoginPagePasswordTextbox.FillColor = Color.FromArgb(35, 34, 50);
            LoginPagePasswordTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            LoginPagePasswordTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoginPagePasswordTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            LoginPagePasswordTextbox.Location = new Point(31, 236);
            LoginPagePasswordTextbox.Name = "LoginPagePasswordTextbox";
            LoginPagePasswordTextbox.PasswordChar = '●';
            LoginPagePasswordTextbox.PlaceholderForeColor = Color.Azure;
            LoginPagePasswordTextbox.PlaceholderText = "";
            LoginPagePasswordTextbox.SelectedText = "";
            LoginPagePasswordTextbox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            LoginPagePasswordTextbox.Size = new Size(200, 36);
            LoginPagePasswordTextbox.TabIndex = 16;
            LoginPagePasswordTextbox.UseSystemPasswordChar = true;
            LoginPagePasswordTextbox.Enter += LoginPagePasswordTextbox_Enter;
            LoginPagePasswordTextbox.Leave += LoginPagePasswordTextbox_Leave;
            // 
            // LoginPageRememberMeToggle
            // 
            LoginPageRememberMeToggle.CheckedState.BorderColor = Color.Indigo;
            LoginPageRememberMeToggle.CheckedState.FillColor = Color.Maroon;
            LoginPageRememberMeToggle.CheckedState.InnerBorderColor = Color.White;
            LoginPageRememberMeToggle.CheckedState.InnerColor = Color.White;
            LoginPageRememberMeToggle.CustomizableEdges = customizableEdges5;
            LoginPageRememberMeToggle.Location = new Point(44, 294);
            LoginPageRememberMeToggle.Name = "LoginPageRememberMeToggle";
            LoginPageRememberMeToggle.ShadowDecoration.CustomizableEdges = customizableEdges6;
            LoginPageRememberMeToggle.Size = new Size(35, 20);
            LoginPageRememberMeToggle.TabIndex = 17;
            LoginPageRememberMeToggle.UncheckedState.BorderColor = Color.FromArgb(234, 153, 149);
            LoginPageRememberMeToggle.UncheckedState.BorderThickness = 2;
            LoginPageRememberMeToggle.UncheckedState.FillColor = Color.FromArgb(35, 34, 50);
            LoginPageRememberMeToggle.UncheckedState.InnerBorderColor = Color.White;
            LoginPageRememberMeToggle.UncheckedState.InnerColor = Color.FromArgb(234, 153, 149);
            LoginPageRememberMeToggle.CheckedChanged += LoginPageRememberMeToggle_CheckedChanged;
            // 
            // LoginPageMediaPanel
            // 
            LoginPageMediaPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LoginPageMediaPanel.Controls.Add(LoginPageVLCPLayer);
            LoginPageMediaPanel.CustomizableEdges = customizableEdges7;
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
            LoginPageAnimateWindow.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_BLEND;
            LoginPageAnimateWindow.TargetForm = this;
            // 
            // guna2AnimateWindow1
            // 
            guna2AnimateWindow1.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_ACTIVATE;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 20;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // NewLoginButton
            // 
            NewLoginButton.AutoRoundedCorners = true;
            NewLoginButton.BorderRadius = 18;
            NewLoginButton.BorderThickness = 2;
            NewLoginButton.Cursor = Cursors.Hand;
            NewLoginButton.CustomizableEdges = customizableEdges21;
            NewLoginButton.DisabledState.BorderColor = Color.DarkGray;
            NewLoginButton.DisabledState.CustomBorderColor = Color.DarkGray;
            NewLoginButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            NewLoginButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            NewLoginButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            NewLoginButton.FillColor = Color.FromArgb(175, 30, 130);
            NewLoginButton.FillColor2 = Color.FromArgb(175, 30, 130);
            NewLoginButton.Font = new Font("Segoe UI", 9F);
            NewLoginButton.ForeColor = Color.White;
            NewLoginButton.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            NewLoginButton.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            NewLoginButton.Location = new Point(31, 347);
            NewLoginButton.Name = "NewLoginButton";
            NewLoginButton.ShadowDecoration.CustomizableEdges = customizableEdges22;
            NewLoginButton.Size = new Size(120, 38);
            NewLoginButton.TabIndex = 27;
            NewLoginButton.Text = "Login";
            NewLoginButton.Click += LoginButton_Click;
            // 
            // NewCreateAccountButton
            // 
            NewCreateAccountButton.AutoRoundedCorners = true;
            NewCreateAccountButton.BorderRadius = 18;
            NewCreateAccountButton.BorderThickness = 2;
            NewCreateAccountButton.Cursor = Cursors.Hand;
            NewCreateAccountButton.CustomizableEdges = customizableEdges19;
            NewCreateAccountButton.DisabledState.BorderColor = Color.DarkGray;
            NewCreateAccountButton.DisabledState.CustomBorderColor = Color.DarkGray;
            NewCreateAccountButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            NewCreateAccountButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            NewCreateAccountButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            NewCreateAccountButton.FillColor = Color.FromArgb(175, 30, 130);
            NewCreateAccountButton.FillColor2 = Color.FromArgb(175, 30, 130);
            NewCreateAccountButton.Font = new Font("Segoe UI", 9F);
            NewCreateAccountButton.ForeColor = Color.White;
            NewCreateAccountButton.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            NewCreateAccountButton.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            NewCreateAccountButton.Location = new Point(182, 347);
            NewCreateAccountButton.Name = "NewCreateAccountButton";
            NewCreateAccountButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            NewCreateAccountButton.Size = new Size(120, 38);
            NewCreateAccountButton.TabIndex = 28;
            NewCreateAccountButton.Text = "Create Account";
            NewCreateAccountButton.Click += NewCreateAccountButton_Click;
            // 
            // NewForgotPasswordButton
            // 
            NewForgotPasswordButton.AutoRoundedCorners = true;
            NewForgotPasswordButton.BorderColor = Color.Transparent;
            NewForgotPasswordButton.BorderRadius = 14;
            NewForgotPasswordButton.BorderThickness = 2;
            NewForgotPasswordButton.Cursor = Cursors.Hand;
            NewForgotPasswordButton.CustomizableEdges = customizableEdges17;
            NewForgotPasswordButton.DisabledState.BorderColor = Color.DarkGray;
            NewForgotPasswordButton.DisabledState.CustomBorderColor = Color.DarkGray;
            NewForgotPasswordButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            NewForgotPasswordButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            NewForgotPasswordButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            NewForgotPasswordButton.FillColor = Color.Transparent;
            NewForgotPasswordButton.FillColor2 = Color.Transparent;
            NewForgotPasswordButton.Font = new Font("Segoe UI", 9F);
            NewForgotPasswordButton.ForeColor = Color.White;
            NewForgotPasswordButton.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            NewForgotPasswordButton.HoverState.FillColor2 = Color.FromArgb(255, 77, 165);
            NewForgotPasswordButton.Location = new Point(234, 291);
            NewForgotPasswordButton.Name = "NewForgotPasswordButton";
            NewForgotPasswordButton.ShadowDecoration.CustomizableEdges = customizableEdges18;
            NewForgotPasswordButton.Size = new Size(140, 30);
            NewForgotPasswordButton.TabIndex = 29;
            NewForgotPasswordButton.Text = "Forgot Password";
            NewForgotPasswordButton.Click += NewForgotPasswordButton_Click;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1284, 681);
            Controls.Add(NewForgotPasswordButton);
            Controls.Add(NewCreateAccountButton);
            Controls.Add(NewLoginButton);
            Controls.Add(LoginPageCreationSuccessTextBox);
            Controls.Add(guna2Panel1);
            Controls.Add(LoginPageRememberMeToggle);
            Controls.Add(LoginPagePasswordTextbox);
            Controls.Add(loginPageUsernameTextbox);
            Controls.Add(label1);
            Controls.Add(loginPageErrorTextbox);
            Controls.Add(LoginPageMediaPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginPage";
            SizeGripStyle = SizeGripStyle.Show;
            WindowState = FormWindowState.Minimized;
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
        private Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private LibVLCSharp.WinForms.VideoView LoginPageVLCPLayer;
        private Guna.UI2.WinForms.Guna2TextBox loginPageUsernameTextbox;
        private Guna.UI2.WinForms.Guna2TextBox LoginPagePasswordTextbox;
        private Guna.UI2.WinForms.Guna2ToggleSwitch LoginPageRememberMeToggle;
        private Guna.UI2.WinForms.Guna2Panel LoginPageMediaPanel;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2TextBox LoginPageCreationSuccessTextBox;
        private Guna.UI2.WinForms.Guna2ControlBox LoginPageExitControlBox;
        private Guna.UI2.WinForms.Guna2AnimateWindow LoginPageAnimateWindow;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2GradientButton NewLoginButton;
        private Guna.UI2.WinForms.Guna2GradientButton NewCreateAccountButton;
        private Guna.UI2.WinForms.Guna2GradientButton NewForgotPasswordButton;
    }
}
