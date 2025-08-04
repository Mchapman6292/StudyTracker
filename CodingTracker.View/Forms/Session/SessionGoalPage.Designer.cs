// SessionGoalForm.Designer.cs
namespace CodingTracker.View.PopUpFormService
{
    partial class SessionGoalPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            DisplayMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            gunaAnimationWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            formatLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            timeDisplayLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            skipButton = new Guna.UI2.WinForms.Guna2GradientButton();
            setTimeGoalButton = new Guna.UI2.WinForms.Guna2GradientButton();
            timeGoalTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            closeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            minimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            sessionGoalPageMainPanel = new Guna.UI2.WinForms.Guna2Panel();
            newHomeButton = new FontAwesome.Sharp.IconPictureBox();
            sessionGoalDragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
            sessionGoalPageMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)newHomeButton).BeginInit();
            SuspendLayout();
            // 
            // borderlessForm
            // 
            borderlessForm.BorderRadius = 12;
            borderlessForm.ContainerControl = this;
            borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            borderlessForm.TransparentWhileDrag = true;
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
            // formatLabel
            // 
            formatLabel.AutoSize = false;
            formatLabel.BackColor = Color.Transparent;
            formatLabel.Font = new Font("Segoe UI", 9F);
            formatLabel.ForeColor = Color.FromArgb(255, 200, 230);
            formatLabel.Location = new Point(27, 154);
            formatLabel.Name = "formatLabel";
            formatLabel.Size = new Size(350, 20);
            formatLabel.TabIndex = 2;
            formatLabel.Text = "HH:MM";
            formatLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // timeDisplayLabel
            // 
            timeDisplayLabel.AutoSize = false;
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            timeDisplayLabel.ForeColor = Color.FromArgb(255, 200, 230);
            timeDisplayLabel.Location = new Point(38, 44);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(350, 40);
            timeDisplayLabel.TabIndex = 0;
            timeDisplayLabel.Text = "Enter goal time";
            timeDisplayLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // skipButton
            // 
            skipButton.BorderColor = Color.FromArgb(35, 34, 50);
            skipButton.BorderRadius = 23;
            skipButton.CustomizableEdges = customizableEdges13;
            skipButton.FillColor = Color.FromArgb(255, 81, 195);
            skipButton.FillColor2 = Color.FromArgb(168, 228, 255);
            skipButton.Font = new Font("Segoe UI", 9F);
            skipButton.ForeColor = Color.White;
            skipButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            skipButton.Location = new Point(215, 191);
            skipButton.Name = "skipButton";
            skipButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
            skipButton.Size = new Size(150, 36);
            skipButton.TabIndex = 4;
            skipButton.Text = "Skip";
            skipButton.Click += SkipButton_Click;
            // 
            // setTimeGoalButton
            // 
            setTimeGoalButton.BorderColor = Color.FromArgb(35, 34, 50);
            setTimeGoalButton.BorderRadius = 23;
            setTimeGoalButton.CustomizableEdges = customizableEdges15;
            setTimeGoalButton.FillColor = Color.FromArgb(255, 81, 195);
            setTimeGoalButton.FillColor2 = Color.FromArgb(168, 228, 255);
            setTimeGoalButton.Font = new Font("Segoe UI", 9F);
            setTimeGoalButton.ForeColor = Color.White;
            setTimeGoalButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            setTimeGoalButton.Location = new Point(55, 191);
            setTimeGoalButton.Name = "setTimeGoalButton";
            setTimeGoalButton.ShadowDecoration.CustomizableEdges = customizableEdges16;
            setTimeGoalButton.Size = new Size(150, 36);
            setTimeGoalButton.TabIndex = 3;
            setTimeGoalButton.Text = "Set Time Goal";
            setTimeGoalButton.Click += SetTimeGoalButton_Click;
            // 
            // timeGoalTextBox
            // 
            timeGoalTextBox.BorderColor = Color.FromArgb(255, 81, 195);
            timeGoalTextBox.BorderRadius = 8;
            timeGoalTextBox.CustomizableEdges = customizableEdges17;
            timeGoalTextBox.DefaultText = "";
            timeGoalTextBox.FillColor = Color.FromArgb(45, 46, 50);
            timeGoalTextBox.Font = new Font("Segoe UI", 9F);
            timeGoalTextBox.ForeColor = Color.FromArgb(255, 200, 230);
            timeGoalTextBox.Location = new Point(151, 100);
            timeGoalTextBox.MaxLength = 4;
            timeGoalTextBox.Name = "timeGoalTextBox";
            timeGoalTextBox.PlaceholderText = "0100";
            timeGoalTextBox.SelectedText = "";
            timeGoalTextBox.ShadowDecoration.CustomizableEdges = customizableEdges18;
            timeGoalTextBox.Size = new Size(120, 36);
            timeGoalTextBox.TabIndex = 1;
            timeGoalTextBox.TextAlign = HorizontalAlignment.Center;
            timeGoalTextBox.TextChanged += TimeGoalTextBox_TextChanged;
            timeGoalTextBox.KeyPress += TimeGoalTextBox_KeyPress;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            closeButton.CustomizableEdges = customizableEdges19;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.FromArgb(255, 160, 210);
            closeButton.Location = new Point(355, -1);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            closeButton.Size = new Size(45, 29);
            closeButton.TabIndex = 1;
            closeButton.Click += CloseButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.CustomizableEdges = customizableEdges21;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.FromArgb(255, 160, 210);
            minimizeButton.Location = new Point(322, -1);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges22;
            minimizeButton.Size = new Size(43, 28);
            minimizeButton.TabIndex = 2;
            minimizeButton.Click += MinimizeButton_Click;
            // 
            // sessionGoalPageMainPanel
            // 
            sessionGoalPageMainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            sessionGoalPageMainPanel.BorderRadius = 12;
            sessionGoalPageMainPanel.BorderThickness = 1;
            sessionGoalPageMainPanel.Controls.Add(newHomeButton);
            sessionGoalPageMainPanel.Controls.Add(minimizeButton);
            sessionGoalPageMainPanel.Controls.Add(closeButton);
            sessionGoalPageMainPanel.Controls.Add(timeGoalTextBox);
            sessionGoalPageMainPanel.Controls.Add(setTimeGoalButton);
            sessionGoalPageMainPanel.Controls.Add(skipButton);
            sessionGoalPageMainPanel.Controls.Add(formatLabel);
            sessionGoalPageMainPanel.Controls.Add(timeDisplayLabel);
            sessionGoalPageMainPanel.CustomizableEdges = customizableEdges23;
            sessionGoalPageMainPanel.Dock = DockStyle.Fill;
            sessionGoalPageMainPanel.FillColor = Color.FromArgb(35, 34, 50);
            sessionGoalPageMainPanel.ForeColor = Color.FromArgb(70, 71, 117);
            sessionGoalPageMainPanel.Location = new Point(0, 0);
            sessionGoalPageMainPanel.Name = "sessionGoalPageMainPanel";
            sessionGoalPageMainPanel.Padding = new Padding(20);
            sessionGoalPageMainPanel.ShadowDecoration.CustomizableEdges = customizableEdges24;
            sessionGoalPageMainPanel.Size = new Size(400, 280);
            sessionGoalPageMainPanel.TabIndex = 0;
            // 
            // newHomeButton
            // 
            newHomeButton.BackColor = Color.FromArgb(25, 24, 40);
            newHomeButton.ForeColor = Color.FromArgb(255, 160, 210);
            newHomeButton.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            newHomeButton.IconColor = Color.FromArgb(255, 160, 210);
            newHomeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            newHomeButton.IconSize = 28;
            newHomeButton.Location = new Point(286, 0);
            newHomeButton.Margin = new Padding(3, 2, 3, 2);
            newHomeButton.Name = "newHomeButton";
            newHomeButton.Size = new Size(43, 28);
            newHomeButton.SizeMode = PictureBoxSizeMode.CenterImage;
            newHomeButton.TabIndex = 46;
            newHomeButton.TabStop = false;
            newHomeButton.Click += newHomeButton_Click;
            // 
            // sessionGoalDragControl
            // 
            sessionGoalDragControl.DockIndicatorTransparencyValue = 0.6D;
            sessionGoalDragControl.TargetControl = sessionGoalPageMainPanel;
            sessionGoalDragControl.UseTransparentDrag = true;
            // 
            // SessionGoalPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(400, 280);
            Controls.Add(sessionGoalPageMainPanel);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SessionGoalPage";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Session Goal";
            Load += SessionGoalPage_Load;
            sessionGoalPageMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)newHomeButton).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Guna.UI2.WinForms.Guna2AnimateWindow gunaAnimationWindow;
        private Guna.UI2.WinForms.Guna2HtmlLabel formatLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel timeDisplayLabel;
        private Guna.UI2.WinForms.Guna2Panel sessionGoalPageMainPanel;
        private Guna.UI2.WinForms.Guna2ControlBox minimizeButton;
        private Guna.UI2.WinForms.Guna2ControlBox closeButton;
        private Guna.UI2.WinForms.Guna2TextBox timeGoalTextBox;
        private Guna.UI2.WinForms.Guna2GradientButton setTimeGoalButton;
        private Guna.UI2.WinForms.Guna2GradientButton skipButton;
        private FontAwesome.Sharp.IconPictureBox newHomeButton;
        private Guna.UI2.WinForms.Guna2DragControl sessionGoalDragControl;
    }
}