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
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            homeButton = new FontAwesome.Sharp.IconPictureBox();
            skipButton = new Guna2GradientButton();
            saveButton = new Guna2GradientButton();
            SessionNotesTextBox = new Guna2TextBox();
            notesLabel = new Guna2HtmlLabel();
            ProjectNameTextBox = new Guna2TextBox();
            projectLabel = new Guna2HtmlLabel();
            titleLabel = new Guna2HtmlLabel();
            minimizeButton = new Guna2ControlBox();
            closeButton = new Guna2ControlBox();
            mainPanel.SuspendLayout();
            ((ISupportInitialize)homeButton).BeginInit();
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
            mainPanel.Controls.Add(skipButton);
            mainPanel.Controls.Add(saveButton);
            mainPanel.Controls.Add(SessionNotesTextBox);
            mainPanel.Controls.Add(notesLabel);
            mainPanel.Controls.Add(ProjectNameTextBox);
            mainPanel.Controls.Add(projectLabel);
            mainPanel.Controls.Add(titleLabel);
            customizableEdges13.BottomRight = false;
            customizableEdges13.TopLeft = false;
            mainPanel.CustomizableEdges = customizableEdges13;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(20);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges14;
            mainPanel.Size = new Size(450, 500);
            mainPanel.TabIndex = 0;
            // 
            // homeButton
            // 
            homeButton.BackColor = Color.FromArgb(25, 24, 40);
            homeButton.ForeColor = Color.FromArgb(255, 160, 210);
            homeButton.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            homeButton.IconColor = Color.FromArgb(255, 160, 210);
            homeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            homeButton.IconSize = 28;
            homeButton.Location = new Point(328, 0);
            homeButton.Margin = new Padding(3, 2, 3, 2);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(43, 28);
            homeButton.SizeMode = PictureBoxSizeMode.CenterImage;
            homeButton.TabIndex = 46;
            homeButton.TabStop = false;
            // 
            // skipButton
            // 
            skipButton.BorderRadius = 8;
            customizableEdges5.BottomRight = false;
            customizableEdges5.TopLeft = false;
            skipButton.CustomizableEdges = customizableEdges5;
            skipButton.FillColor = Color.FromArgb(255, 81, 195);
            skipButton.FillColor2 = Color.FromArgb(168, 228, 255);
            skipButton.Font = new Font("Segoe UI", 9F);
            skipButton.ForeColor = Color.White;
            skipButton.Location = new Point(245, 400);
            skipButton.Name = "skipButton";
            skipButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            skipButton.Size = new Size(180, 45);
            skipButton.TabIndex = 6;
            skipButton.Text = "Skip";
            skipButton.Click += SkipButton_Click;
            // 
            // saveButton
            // 
            saveButton.BorderRadius = 8;
            customizableEdges7.BottomRight = false;
            customizableEdges7.TopLeft = false;
            saveButton.CustomizableEdges = customizableEdges7;
            saveButton.FillColor = Color.FromArgb(255, 81, 195);
            saveButton.FillColor2 = Color.FromArgb(168, 228, 255);
            saveButton.Font = new Font("Segoe UI", 9F);
            saveButton.ForeColor = Color.White;
            saveButton.GradientMode = LinearGradientMode.ForwardDiagonal;
            saveButton.Location = new Point(23, 400);
            saveButton.Name = "saveButton";
            saveButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
            saveButton.Size = new Size(180, 45);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save Session";
            saveButton.Click += SaveButton_Click;
            // 
            // SessionNotesTextBox
            // 
            SessionNotesTextBox.BorderColor = Color.FromArgb(168, 228, 255);
            SessionNotesTextBox.BorderRadius = 8;
            customizableEdges9.BottomRight = false;
            customizableEdges9.TopLeft = false;
            SessionNotesTextBox.CustomizableEdges = customizableEdges9;
            SessionNotesTextBox.DefaultText = "";
            SessionNotesTextBox.FillColor = Color.FromArgb(45, 46, 50);
            SessionNotesTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            SessionNotesTextBox.Font = new Font("Segoe UI", 9F);
            SessionNotesTextBox.ForeColor = Color.White;
            SessionNotesTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            SessionNotesTextBox.Location = new Point(25, 180);
            SessionNotesTextBox.Multiline = true;
            SessionNotesTextBox.Name = "SessionNotesTextBox";
            SessionNotesTextBox.PlaceholderForeColor = Color.FromArgb(255, 200, 230);
            SessionNotesTextBox.PlaceholderText = "...";
            SessionNotesTextBox.SelectedText = "";
            SessionNotesTextBox.ShadowDecoration.CustomizableEdges = customizableEdges10;
            SessionNotesTextBox.Size = new Size(400, 200);
            SessionNotesTextBox.TabIndex = 4;
            // 
            // notesLabel
            // 
            notesLabel.BackColor = Color.Transparent;
            notesLabel.Font = new Font("Segoe UI", 11F);
            notesLabel.ForeColor = Color.FromArgb(255, 200, 230);
            notesLabel.Location = new Point(25, 150);
            notesLabel.Name = "notesLabel";
            notesLabel.Size = new Size(42, 22);
            notesLabel.TabIndex = 3;
            notesLabel.Text = "Notes";
            // 
            // ProjectNameTextBox
            // 
            ProjectNameTextBox.BorderColor = Color.FromArgb(255, 81, 195);
            ProjectNameTextBox.BorderRadius = 8;
            customizableEdges11.BottomRight = false;
            customizableEdges11.TopLeft = false;
            ProjectNameTextBox.CustomizableEdges = customizableEdges11;
            ProjectNameTextBox.DefaultText = "";
            ProjectNameTextBox.FillColor = Color.FromArgb(45, 46, 50);
            ProjectNameTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            ProjectNameTextBox.Font = new Font("Segoe UI", 9F);
            ProjectNameTextBox.ForeColor = Color.White;
            ProjectNameTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            ProjectNameTextBox.Location = new Point(25, 100);
            ProjectNameTextBox.Name = "ProjectNameTextBox";
            ProjectNameTextBox.PlaceholderText = "Enter study category...";
            ProjectNameTextBox.SelectedText = "";
            ProjectNameTextBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
            ProjectNameTextBox.Size = new Size(400, 36);
            ProjectNameTextBox.TabIndex = 2;
            // 
            // projectLabel
            // 
            projectLabel.BackColor = Color.Transparent;
            projectLabel.Font = new Font("Segoe UI", 11F);
            projectLabel.ForeColor = Color.FromArgb(255, 200, 230);
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
            titleLabel.ForeColor = Color.HotPink;
            titleLabel.Location = new Point(25, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(159, 27);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Session Complete";
            titleLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            customizableEdges3.BottomRight = false;
            customizableEdges3.TopLeft = false;
            customizableEdges3.TopRight = false;
            minimizeButton.CustomizableEdges = customizableEdges3;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.FromArgb(255, 160, 210);
            minimizeButton.Location = new Point(364, -1);
            minimizeButton.Name = "minimizeButton";
            customizableEdges4.BottomLeft = false;
            customizableEdges4.TopLeft = false;
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            minimizeButton.Size = new Size(43, 28);
            minimizeButton.TabIndex = 8;
            minimizeButton.Click += MinimizeButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            customizableEdges1.BottomLeft = false;
            customizableEdges1.TopLeft = false;
            closeButton.CustomizableEdges = customizableEdges1;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.FromArgb(255, 160, 210);
            closeButton.Location = new Point(407, -1);
            closeButton.Name = "closeButton";
            customizableEdges2.BottomRight = false;
            customizableEdges2.TopRight = false;
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
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
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SessionNotesForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Session Notes";
            Load += SessionNotesForm_Load;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((ISupportInitialize)homeButton).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private FontAwesome.Sharp.IconPictureBox homeButton;
    }
}