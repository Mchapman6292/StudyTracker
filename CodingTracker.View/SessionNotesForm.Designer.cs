using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;

namespace CodingTracker.View
{
    partial class SessionNotesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2HtmlLabel titleLabel;
        private Guna2HtmlLabel projectLabel;
        private Guna2TextBox ProjectNameTextBox;
        private Guna2HtmlLabel notesLabel;
        private Guna2TextBox SessionNotesTextBox;
        private Guna2GradientButton saveButton;
        private Guna2GradientButton skipButton;
        private Guna2GradientButton homeButton;
        private Guna2ControlBox minimizeButton;
        private Guna2ControlBox closeButton;

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
            components = new Container();
            CustomizableEdges customizableEdges25 = new CustomizableEdges();
            CustomizableEdges customizableEdges26 = new CustomizableEdges();
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges18 = new CustomizableEdges();
            CustomizableEdges customizableEdges19 = new CustomizableEdges();
            CustomizableEdges customizableEdges20 = new CustomizableEdges();
            CustomizableEdges customizableEdges21 = new CustomizableEdges();
            CustomizableEdges customizableEdges22 = new CustomizableEdges();
            CustomizableEdges customizableEdges23 = new CustomizableEdges();
            CustomizableEdges customizableEdges24 = new CustomizableEdges();
            CustomizableEdges customizableEdges27 = new CustomizableEdges();
            CustomizableEdges customizableEdges28 = new CustomizableEdges();
            CustomizableEdges customizableEdges29 = new CustomizableEdges();
            CustomizableEdges customizableEdges30 = new CustomizableEdges();
            CustomizableEdges customizableEdges31 = new CustomizableEdges();
            CustomizableEdges customizableEdges32 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            skipButton = new Guna2GradientButton();
            saveButton = new Guna2GradientButton();
            SessionNotesTextBox = new Guna2TextBox();
            notesLabel = new Guna2HtmlLabel();
            ProjectNameTextBox = new Guna2TextBox();
            projectLabel = new Guna2HtmlLabel();
            titleLabel = new Guna2HtmlLabel();
            homeButton = new Guna2GradientButton();
            minimizeButton = new Guna2ControlBox();
            closeButton = new Guna2ControlBox();
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
            mainPanel.Controls.Add(skipButton);
            mainPanel.Controls.Add(saveButton);
            mainPanel.Controls.Add(SessionNotesTextBox);
            mainPanel.Controls.Add(notesLabel);
            mainPanel.Controls.Add(ProjectNameTextBox);
            mainPanel.Controls.Add(projectLabel);
            mainPanel.Controls.Add(titleLabel);
            customizableEdges25.BottomRight = false;
            customizableEdges25.TopLeft = false;
            mainPanel.CustomizableEdges = customizableEdges25;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(20);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges26;
            mainPanel.Size = new Size(450, 500);
            mainPanel.TabIndex = 0;
            // 
            // skipButton
            // 
            skipButton.BorderRadius = 8;
            customizableEdges17.BottomRight = false;
            customizableEdges17.TopLeft = false;
            skipButton.CustomizableEdges = customizableEdges17;
            skipButton.FillColor = Color.FromArgb(255, 81, 195);
            skipButton.FillColor2 = Color.FromArgb(168, 228, 255);
            skipButton.Font = new Font("Segoe UI", 9F);
            skipButton.ForeColor = Color.White;
            skipButton.Location = new Point(245, 400);
            skipButton.Name = "skipButton";
            skipButton.ShadowDecoration.CustomizableEdges = customizableEdges18;
            skipButton.Size = new Size(180, 45);
            skipButton.TabIndex = 6;
            skipButton.Text = "Skip";
            skipButton.Click += SkipButton_Click;
            // 
            // saveButton
            // 
            saveButton.BorderRadius = 8;
            customizableEdges19.BottomRight = false;
            customizableEdges19.TopLeft = false;
            saveButton.CustomizableEdges = customizableEdges19;
            saveButton.FillColor = Color.FromArgb(255, 81, 195);
            saveButton.FillColor2 = Color.FromArgb(168, 228, 255);
            saveButton.Font = new Font("Segoe UI", 9F);
            saveButton.ForeColor = Color.White;
            saveButton.GradientMode = LinearGradientMode.ForwardDiagonal;
            saveButton.Location = new Point(23, 400);
            saveButton.Name = "saveButton";
            saveButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            saveButton.Size = new Size(180, 45);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save Session";
            saveButton.Click += SaveButton_Click;
            // 
            // SessionNotesTextBox
            // 
            SessionNotesTextBox.BorderColor = Color.FromArgb(168, 228, 255);
            SessionNotesTextBox.BorderRadius = 8;
            customizableEdges21.BottomRight = false;
            customizableEdges21.TopLeft = false;
            SessionNotesTextBox.CustomizableEdges = customizableEdges21;
            SessionNotesTextBox.DefaultText = "";
            SessionNotesTextBox.FillColor = Color.FromArgb(45, 46, 50);
            SessionNotesTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            SessionNotesTextBox.Font = new Font("Segoe UI", 9F);
            SessionNotesTextBox.ForeColor = Color.White;
            SessionNotesTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            SessionNotesTextBox.Location = new Point(25, 180);
            SessionNotesTextBox.Multiline = true;
            SessionNotesTextBox.Name = "SessionNotesTextBox";
            SessionNotesTextBox.PasswordChar = '\0';
            SessionNotesTextBox.PlaceholderText = "Session notes";
            SessionNotesTextBox.SelectedText = "";
            SessionNotesTextBox.ShadowDecoration.CustomizableEdges = customizableEdges22;
            SessionNotesTextBox.Size = new Size(400, 200);
            SessionNotesTextBox.TabIndex = 4;
            // 
            // notesLabel
            // 
            notesLabel.BackColor = Color.Transparent;
            notesLabel.Font = new Font("Segoe UI", 11F);
            notesLabel.ForeColor = Color.White;
            notesLabel.Location = new Point(25, 150);
            notesLabel.Name = "notesLabel";
            notesLabel.Size = new Size(95, 22);
            notesLabel.TabIndex = 3;
            notesLabel.Text = "Session Notes";
            // 
            // ProjectNameTextBox
            // 
            ProjectNameTextBox.BorderColor = Color.FromArgb(255, 81, 195);
            ProjectNameTextBox.BorderRadius = 8;
            customizableEdges23.BottomRight = false;
            customizableEdges23.TopLeft = false;
            ProjectNameTextBox.CustomizableEdges = customizableEdges23;
            ProjectNameTextBox.DefaultText = "";
            ProjectNameTextBox.FillColor = Color.FromArgb(45, 46, 50);
            ProjectNameTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            ProjectNameTextBox.Font = new Font("Segoe UI", 9F);
            ProjectNameTextBox.ForeColor = Color.White;
            ProjectNameTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            ProjectNameTextBox.Location = new Point(25, 100);
            ProjectNameTextBox.Name = "ProjectNameTextBox";
            ProjectNameTextBox.PasswordChar = '\0';
            ProjectNameTextBox.PlaceholderText = "Enter study category...";
            ProjectNameTextBox.SelectedText = "";
            ProjectNameTextBox.ShadowDecoration.CustomizableEdges = customizableEdges24;
            ProjectNameTextBox.Size = new Size(400, 36);
            ProjectNameTextBox.TabIndex = 2;
            // 
            // projectLabel
            // 
            projectLabel.BackColor = Color.Transparent;
            projectLabel.Font = new Font("Segoe UI", 11F);
            projectLabel.ForeColor = Color.White;
            projectLabel.Location = new Point(25, 70);
            projectLabel.Name = "projectLabel";
            projectLabel.Size = new Size(93, 22);
            projectLabel.TabIndex = 1;
            projectLabel.Text = "Project Name";
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(25, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(159, 27);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Session Complete";
            titleLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // homeButton
            // 
            customizableEdges27.BottomRight = false;
            customizableEdges27.TopRight = false;
            homeButton.CustomizableEdges = customizableEdges27;
            homeButton.FillColor = Color.FromArgb(25, 24, 40);
            homeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            homeButton.Font = new Font("Segoe UI", 9F);
            homeButton.ForeColor = Color.White;
            homeButton.Image = Properties.Resources.HomeButtonIcon;
            homeButton.Location = new Point(321, -1);
            homeButton.Name = "homeButton";
            customizableEdges28.BottomLeft = false;
            customizableEdges28.TopLeft = false;
            homeButton.ShadowDecoration.CustomizableEdges = customizableEdges28;
            homeButton.Size = new Size(43, 28);
            homeButton.TabIndex = 7;
            homeButton.Click += HomeButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            customizableEdges29.BottomRight = false;
            customizableEdges29.TopLeft = false;
            customizableEdges29.TopRight = false;
            minimizeButton.CustomizableEdges = customizableEdges29;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.White;
            minimizeButton.Location = new Point(364, -1);
            minimizeButton.Name = "minimizeButton";
            customizableEdges30.BottomLeft = false;
            customizableEdges30.TopLeft = false;
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges30;
            minimizeButton.Size = new Size(43, 28);
            minimizeButton.TabIndex = 8;
            minimizeButton.Click += MinimizeButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            customizableEdges31.BottomLeft = false;
            customizableEdges31.TopLeft = false;
            closeButton.CustomizableEdges = customizableEdges31;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(407, -1);
            closeButton.Name = "closeButton";
            customizableEdges32.BottomRight = false;
            customizableEdges32.TopRight = false;
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges32;
            closeButton.Size = new Size(45, 29);
            closeButton.TabIndex = 9;
            closeButton.Click += CloseButton_Click;
            // 
            // SessionNotesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(450, 500);
            Controls.Add(closeButton);
            Controls.Add(minimizeButton);
            Controls.Add(homeButton);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SessionNotesForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Session Notes";
            Load += SessionNotesForm_Load;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }
        #endregion
    }
}