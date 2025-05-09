using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.View.FormService.NotificationManagers;
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D;

namespace CodingTracker.View
{
    public partial class SessionNotesForm : Form
    {
        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2HtmlLabel titleLabel;
        private Guna2HtmlLabel projectLabel;
        private Guna2TextBox projectTextBox;
        private Guna2HtmlLabel notesLabel;
        private Guna2TextBox notesTextBox;
        private Guna2GradientButton saveButton;
        private Guna2Button skipButton;

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly INotificationManager _notificationManager;
        private TimeSpan _sessionDuration;

        public SessionNotesForm(ICodingSessionManager codingSessionManager,INotificationManager notificationManager)
        {
            InitializeComponent();
            InitializeComponents();
            _codingSessionManager = codingSessionManager;
            _notificationManager = notificationManager;

        }

        private void InitializeComponents()
        {
            // Form setup
            this.Size = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(35, 34, 50);
            this.ShowInTaskbar = false;

            // Borderless form setup
            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.DragForm = true;
            borderlessForm.BorderRadius = 12;

            // Main panel
            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(32, 33, 36);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderThickness = 1;
            mainPanel.Padding = new Padding(20);

            // Title with session duration
            titleLabel = new Guna2HtmlLabel();
            titleLabel.Text = $"Session Complete - {_sessionDuration.Hours:D2}:{_sessionDuration.Minutes:D2}:{_sessionDuration.Seconds:D2}";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(400, 30);
            titleLabel.TextAlignment = ContentAlignment.MiddleCenter;
            titleLabel.Location = new Point(25, 20);

            // Project label
            projectLabel = new Guna2HtmlLabel();
            projectLabel.Text = "Project Name";
            projectLabel.ForeColor = Color.White;
            projectLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            projectLabel.AutoSize = false;
            projectLabel.Size = new Size(400, 25);
            projectLabel.Location = new Point(25, 70);

            // Project text box
            projectTextBox = new Guna2TextBox();
            projectTextBox.PlaceholderText = "Enter project name...";
            projectTextBox.BorderRadius = 8;
            projectTextBox.ForeColor = Color.White;
            projectTextBox.FillColor = Color.FromArgb(45, 46, 50);
            projectTextBox.BorderColor = Color.FromArgb(255, 81, 195);
            projectTextBox.Size = new Size(400, 36);
            projectTextBox.Location = new Point(25, 100);

            // Notes label
            notesLabel = new Guna2HtmlLabel();
            notesLabel.Text = "Session Notes";
            notesLabel.ForeColor = Color.White;
            notesLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            notesLabel.AutoSize = false;
            notesLabel.Size = new Size(400, 25);
            notesLabel.Location = new Point(25, 150);

            // Notes text box (multiline)
            notesTextBox = new Guna2TextBox();
            notesTextBox.PlaceholderText = "What did you accomplish? What did you learn?";
            notesTextBox.BorderRadius = 8;
            notesTextBox.ForeColor = Color.White;
            notesTextBox.FillColor = Color.FromArgb(45, 46, 50);
            notesTextBox.BorderColor = Color.FromArgb(168, 228, 255);
            notesTextBox.Size = new Size(400, 200);
            notesTextBox.Location = new Point(25, 180);
            notesTextBox.Multiline = true;

            // Save button with gradient
            saveButton = new Guna2GradientButton();
            saveButton.Text = "Save Session";
            saveButton.FillColor = Color.FromArgb(255, 81, 195);
            saveButton.FillColor2 = Color.FromArgb(168, 228, 255);
            saveButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            saveButton.BorderRadius = 8;
            saveButton.Size = new Size(180, 45);
            saveButton.Location = new Point(25, 400);
            saveButton.Click += SaveButton_Click;

            // Skip button
            skipButton = new Guna2Button();
            skipButton.Text = "Skip";
            skipButton.FillColor = Color.FromArgb(72, 73, 77);
            skipButton.BorderRadius = 8;
            skipButton.Size = new Size(180, 45);
            skipButton.Location = new Point(245, 400);
            skipButton.Click += SkipButton_Click;

            // Add controls to panel
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(projectLabel);
            mainPanel.Controls.Add(projectTextBox);
            mainPanel.Controls.Add(notesLabel);
            mainPanel.Controls.Add(notesTextBox);
            mainPanel.Controls.Add(saveButton);
            mainPanel.Controls.Add(skipButton);

            // Add panel to form
            this.Controls.Add(mainPanel);

            // Set initial focus
            this.Load += (s, e) => { projectTextBox.Focus(); };
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
      

            // Close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            // Close without saving
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}