// SessionGoalPage.Designer.cs
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionGoalPage));

            // borderlessForm
            this.borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DragForm = true;
            this.borderlessForm.BorderRadius = 12;

            // mainPanel
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.FillColor = System.Drawing.Color.FromArgb(32, 33, 36);
            this.mainPanel.BorderRadius = 12;
            this.mainPanel.BorderColor = System.Drawing.Color.FromArgb(70, 71, 117);
            this.mainPanel.BorderThickness = 1;
            this.mainPanel.Padding = new System.Windows.Forms.Padding(20);
            this.mainPanel.Name = "mainPanel";

            // closeButton
            this.closeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.FillColor = System.Drawing.Color.FromArgb(25, 24, 40);
            this.closeButton.HoverState.IconColor = System.Drawing.Color.White;
            this.closeButton.IconColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(341, 0);
            this.closeButton.Size = new System.Drawing.Size(45, 29);
            this.closeButton.BorderRadius = 12;
            this.closeButton.CustomClick = true;
            this.closeButton.Name = "closeButton";
            this.closeButton.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges()
            {
                TopRight = true,
                BottomRight = false,
                TopLeft = false,
                BottomLeft = false
            };
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);

            // minimizeButton
            this.minimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.minimizeButton.FillColor = System.Drawing.Color.FromArgb(25, 24, 40);
            this.minimizeButton.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 9, 43);
            this.minimizeButton.HoverState.IconColor = System.Drawing.Color.White;
            this.minimizeButton.IconColor = System.Drawing.Color.White;
            this.minimizeButton.Location = new System.Drawing.Point(299, 0);
            this.minimizeButton.Size = new System.Drawing.Size(43, 28);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);

            // homeButton
            this.homeButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.homeButton.FillColor = System.Drawing.Color.FromArgb(25, 24, 40);
            this.homeButton.FillColor2 = System.Drawing.Color.FromArgb(25, 24, 40);
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.homeButton.ForeColor = System.Drawing.Color.White;
            this.homeButton.Location = new System.Drawing.Point(257, 0);
            this.homeButton.Size = new System.Drawing.Size(43, 28);
            this.homeButton.Image = global::CodingTracker.View.Properties.Resources.HomeButtonIcon;
            this.homeButton.Name = "homeButton";
            this.homeButton.Click += new System.EventHandler(this.HomeButton_Click);

            // timeDisplayLabel
            this.timeDisplayLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.timeDisplayLabel.Text = "Enter goal time";
            this.timeDisplayLabel.ForeColor = System.Drawing.Color.White;
            this.timeDisplayLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.timeDisplayLabel.AutoSize = false;
            this.timeDisplayLabel.Size = new System.Drawing.Size(350, 40);
            this.timeDisplayLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.timeDisplayLabel.Location = new System.Drawing.Point(25, 40);
            this.timeDisplayLabel.Name = "timeDisplayLabel";
            this.timeDisplayLabel.BackColor = System.Drawing.Color.Transparent;

            // timeGoalTextBox
            this.timeGoalTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.timeGoalTextBox.PlaceholderText = "0100";
            this.timeGoalTextBox.BorderRadius = 8;
            this.timeGoalTextBox.ForeColor = System.Drawing.Color.White;
            this.timeGoalTextBox.FillColor = System.Drawing.Color.FromArgb(45, 46, 50);
            this.timeGoalTextBox.BorderColor = System.Drawing.Color.FromArgb(255, 81, 195);
            this.timeGoalTextBox.Size = new System.Drawing.Size(120, 36);
            this.timeGoalTextBox.Location = new System.Drawing.Point(140, 90);
            this.timeGoalTextBox.MaxLength = 4;
            this.timeGoalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timeGoalTextBox.Name = "timeGoalTextBox";
            this.timeGoalTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimeGoalTextBox_KeyPress);

            // formatLabel
            this.formatLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.formatLabel.Text = "Enter time in HHMM format (e.g., 0130 for 1 hour 30 minutes)";
            this.formatLabel.ForeColor = System.Drawing.Color.Silver;
            this.formatLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.formatLabel.AutoSize = false;
            this.formatLabel.Size = new System.Drawing.Size(350, 20);
            this.formatLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.formatLabel.Location = new System.Drawing.Point(25, 135);
            this.formatLabel.Name = "formatLabel";
            this.formatLabel.BackColor = System.Drawing.Color.Transparent;

            // setTimeGoalButton
            this.setTimeGoalButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.setTimeGoalButton.Text = "Set Time Goal";
            this.setTimeGoalButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.setTimeGoalButton.BorderRadius = 8;
            this.setTimeGoalButton.Size = new System.Drawing.Size(150, 45);
            this.setTimeGoalButton.Location = new System.Drawing.Point(55, 170);
            this.setTimeGoalButton.Name = "setTimeGoalButton";
            this.setTimeGoalButton.Click += new System.EventHandler(this.SetTimeGoalButton_Click);

            // skipButton
            this.skipButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.skipButton.Text = "Skip";
            this.skipButton.BorderRadius = 8;
            this.skipButton.Size = new System.Drawing.Size(150, 45);
            this.skipButton.Location = new System.Drawing.Point(215, 170);
            this.skipButton.Name = "skipButton";
            this.skipButton.Click += new System.EventHandler(this.SkipButton_Click);

            // DisplayMessageBox
            this.DisplayMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.DisplayMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.DisplayMessageBox.Caption = null;
            this.DisplayMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
            this.DisplayMessageBox.Parent = null;
            this.DisplayMessageBox.Style = Guna.UI2.WinForms.MessageDialogStyle.Default;
            this.DisplayMessageBox.Text = null;

            // Add controls to containers
            this.mainPanel.Controls.Add(this.timeDisplayLabel);
            this.mainPanel.Controls.Add(this.timeGoalTextBox);
            this.mainPanel.Controls.Add(this.formatLabel);
            this.mainPanel.Controls.Add(this.setTimeGoalButton);
            this.mainPanel.Controls.Add(this.skipButton);

            // SessionGoalPage
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(35, 34, 50);
            this.ClientSize = new System.Drawing.Size(400, 280);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SessionGoalPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Session Goal";
            this.Load += new System.EventHandler(this.PopUpForm_Load);

            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.homeButton);

            // Bring control buttons to front
            this.closeButton.BringToFront();
            this.minimizeButton.BringToFront();
            this.homeButton.BringToFront();

            this.ResumeLayout(false);
        }
        #endregion
    }
}