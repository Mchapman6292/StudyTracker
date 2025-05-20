// SessionGoalForm.Designer.cs
namespace CodingTracker.View.PopUpFormService
{
    partial class SessionGoalPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Form controls
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel timeDisplayLabel;
        private Guna.UI2.WinForms.Guna2TextBox timeGoalTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel formatLabel;
        private Guna.UI2.WinForms.Guna2GradientButton setTimeGoalButton;
        private Guna.UI2.WinForms.Guna2GradientButton skipButton;
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2ControlBox minimizeButton;
        private Guna.UI2.WinForms.Guna2ControlBox closeButton;
        private Guna.UI2.WinForms.Guna2GradientButton homeButton;
        private Guna.UI2.WinForms.Guna2MessageDialog DisplayMessageBox;

        private int buttonWidth = 40;
        private int buttonHeight = 28;
        private int formWidth = 400;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            homeButton = new Guna.UI2.WinForms.Guna2GradientButton();
            minimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            closeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            timeDisplayLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            timeGoalTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            formatLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            setTimeGoalButton = new Guna.UI2.WinForms.Guna2GradientButton();
            skipButton = new Guna.UI2.WinForms.Guna2GradientButton();
            DisplayMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            gunaAnimationWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // borderlessForm
            // 
            borderlessForm.BorderRadius = 12;
            borderlessForm.ContainerControl = this;
            borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            borderlessForm.TransparentWhileDrag = true;
            // 
            // mainPanel
            // 
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderThickness = 1;
            mainPanel.Controls.Add(homeButton);
            mainPanel.Controls.Add(minimizeButton);
            mainPanel.Controls.Add(closeButton);
            mainPanel.Controls.Add(timeDisplayLabel);
            mainPanel.Controls.Add(timeGoalTextBox);
            mainPanel.Controls.Add(formatLabel);
            mainPanel.Controls.Add(setTimeGoalButton);
            mainPanel.Controls.Add(skipButton);
            mainPanel.CustomizableEdges = customizableEdges13;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.ForeColor = Color.FromArgb(70, 71, 117);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(20);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges14;
            mainPanel.Size = new Size(400, 280);
            mainPanel.TabIndex = 0;
            // 
            // homeButton
            // 
            homeButton.CustomizableEdges = customizableEdges1;
            homeButton.FillColor = Color.FromArgb(25, 24, 40);
            homeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            homeButton.Font = new Font("Segoe UI", 9F);
            homeButton.ForeColor = Color.White;
            homeButton.Image = Properties.Resources.HomeButtonIcon;
            homeButton.Location = new Point(284, -1);
            homeButton.Name = "homeButton";
            homeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            homeButton.Size = new Size(43, 28);
            homeButton.TabIndex = 3;
            homeButton.Click += HomeButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.CustomizableEdges = customizableEdges3;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.White;
            minimizeButton.Location = new Point(322, -1);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            minimizeButton.Size = new Size(43, 28);
            minimizeButton.TabIndex = 2;
            minimizeButton.Click += MinimizeButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            closeButton.CustomizableEdges = customizableEdges5;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(355, -1);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            closeButton.Size = new Size(45, 29);
            closeButton.TabIndex = 1;
            closeButton.Click += CloseButton_Click;
            // 
            // timeDisplayLabel
            // 
            timeDisplayLabel.AutoSize = false;
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            timeDisplayLabel.ForeColor = Color.White;
            timeDisplayLabel.Location = new Point(23, 44);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(350, 40);
            timeDisplayLabel.TabIndex = 0;
            timeDisplayLabel.Text = "Enter goal time";
            timeDisplayLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // timeGoalTextBox
            // 
            timeGoalTextBox.BorderColor = Color.FromArgb(255, 81, 195);
            timeGoalTextBox.BorderRadius = 8;
            timeGoalTextBox.CustomizableEdges = customizableEdges7;
            timeGoalTextBox.DefaultText = "";
            timeGoalTextBox.FillColor = Color.FromArgb(45, 46, 50);
            timeGoalTextBox.Font = new Font("Segoe UI", 9F);
            timeGoalTextBox.ForeColor = Color.White;
            timeGoalTextBox.Location = new Point(140, 90);
            timeGoalTextBox.MaxLength = 4;
            timeGoalTextBox.Name = "timeGoalTextBox";
            timeGoalTextBox.PasswordChar = '\0';
            timeGoalTextBox.PlaceholderText = "0100";
            timeGoalTextBox.SelectedText = "";
            timeGoalTextBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
            timeGoalTextBox.Size = new Size(120, 36);
            timeGoalTextBox.TabIndex = 1;
            timeGoalTextBox.TextAlign = HorizontalAlignment.Center;
            timeGoalTextBox.TextChanged += TimeGoalTextBox_TextChanged;
            timeGoalTextBox.KeyPress += TimeGoalTextBox_KeyPress;
            // 
            // formatLabel
            // 
            formatLabel.AutoSize = false;
            formatLabel.BackColor = Color.Transparent;
            formatLabel.Font = new Font("Segoe UI", 9F);
            formatLabel.ForeColor = Color.Silver;
            formatLabel.Location = new Point(25, 135);
            formatLabel.Name = "formatLabel";
            formatLabel.Size = new Size(350, 20);
            formatLabel.TabIndex = 2;
            formatLabel.Text = "Enter time in HHMM format (e.g., 0130 for 1 hour 30 minutes)";
            formatLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // setTimeGoalButton
            // 
            setTimeGoalButton.BorderRadius = 8;
            setTimeGoalButton.CustomizableEdges = customizableEdges9;
            setTimeGoalButton.FillColor = Color.FromArgb(255, 81, 195);
            setTimeGoalButton.FillColor2 = Color.FromArgb(168, 228, 255);
            setTimeGoalButton.Font = new Font("Segoe UI", 9F);
            setTimeGoalButton.ForeColor = Color.White;
            setTimeGoalButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            setTimeGoalButton.Location = new Point(55, 170);
            setTimeGoalButton.Name = "setTimeGoalButton";
            setTimeGoalButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
            setTimeGoalButton.Size = new Size(150, 45);
            setTimeGoalButton.TabIndex = 3;
            setTimeGoalButton.Text = "Set Time Goal";
            setTimeGoalButton.Click += SetTimeGoalButton_Click;
            // 
            // skipButton
            // 
            skipButton.BorderRadius = 8;
            skipButton.CustomizableEdges = customizableEdges11;
            skipButton.FillColor = Color.FromArgb(255, 81, 195);
            skipButton.FillColor2 = Color.FromArgb(168, 228, 255);
            skipButton.Font = new Font("Segoe UI", 9F);
            skipButton.ForeColor = Color.White;
            skipButton.Location = new Point(215, 170);
            skipButton.Name = "skipButton";
            skipButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
            skipButton.Size = new Size(150, 45);
            skipButton.TabIndex = 4;
            skipButton.Text = "Skip";
            skipButton.Click += SkipButton_Click;
            // 
            // DisplayMessageBox
            // 
            DisplayMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            DisplayMessageBox.Caption = null;
            DisplayMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
            DisplayMessageBox.Parent = null;
            DisplayMessageBox.Style = Guna.UI2.WinForms.MessageDialogStyle.Default;
            DisplayMessageBox.Text = null;
            // 
            // gunaAnimationWindow
            // 
            gunaAnimationWindow.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_ACTIVATE;
            gunaAnimationWindow.TargetForm = this;
            // 
            // SessionGoalPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(400, 280);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SessionGoalPage";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Session Goal";
            Load += SessionGoalPage_Load;
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Guna.UI2.WinForms.Guna2AnimateWindow gunaAnimationWindow;
    }
}