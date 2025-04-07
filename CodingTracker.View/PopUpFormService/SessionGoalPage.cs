using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.View.FormService;
using Guna.UI2.WinForms;
using CodingTracker.View.FormPageEnums;

namespace CodingTracker.View.PopUpFormService
{
    public partial class SessionGoalForm : Form
    {
        private Guna2Panel mainPanel;
        private Guna2HtmlLabel questionLabel;
        private Guna2TextBox timeGoalTextBox;
        private Guna2HtmlLabel formatLabel;
        private Guna2Button yesButton;
        private Guna2Button noButton;
        private Guna2BorderlessForm borderlessForm;

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;

        public string TimeGoal { get; private set; }
        public bool GoalSet { get; private set; } = false;
        public SessionGoalForm(ICodingSessionManager codingSessionManager, IFormSwitcher formSwitcher)
        {
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;

            InitializeComponent();
            InitializeToolsAndComponents();
        }

        private void PopUpForm_Load(object sender, EventArgs e)
        {
            
        }

        private void InitializeToolsAndComponents()
        {
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(35, 34, 50);
            this.ShowInTaskbar = false;

            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.DragForm = true;
            borderlessForm.BorderRadius = 12;

            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(32, 33, 36);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderThickness = 1;

            questionLabel = new Guna2HtmlLabel();
            questionLabel.Text = "Would you like to set a time goal for this session?";
            questionLabel.ForeColor = Color.White;
            questionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            questionLabel.AutoSize = false;
            questionLabel.Size = new Size(350, 30);
            questionLabel.TextAlignment = ContentAlignment.MiddleCenter;
            questionLabel.Location = new Point(25, 30);

            timeGoalTextBox = new Guna2TextBox();
            timeGoalTextBox.PlaceholderText = "0100";
            timeGoalTextBox.BorderRadius = 8;
            timeGoalTextBox.ForeColor = Color.White;
            timeGoalTextBox.FillColor = Color.FromArgb(45, 46, 50);
            timeGoalTextBox.BorderColor = Color.FromArgb(94, 148, 255);
            timeGoalTextBox.Size = new Size(120, 36);
            timeGoalTextBox.Location = new Point(140, 70);
            timeGoalTextBox.MaxLength = 4;
            timeGoalTextBox.TextAlign = HorizontalAlignment.Center;
            timeGoalTextBox.KeyPress += TimeGoalTextBox_KeyPress;

            formatLabel = new Guna2HtmlLabel();
            formatLabel.Text = "Enter time in HHMM format (e.g., 0130 for 1 hour 30 minutes)";
            formatLabel.ForeColor = Color.Silver;
            formatLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            formatLabel.AutoSize = false;
            formatLabel.Size = new Size(350, 20);
            formatLabel.TextAlignment = ContentAlignment.MiddleCenter;
            formatLabel.Location = new Point(25, 110);

            yesButton = new Guna2Button();
            yesButton.Text = "Set TimeGoal";
            yesButton.FillColor = Color.FromArgb(94, 148, 255);
            yesButton.BorderRadius = 8;
            yesButton.Size = new Size(120, 45);
            yesButton.Location = new Point(70, 150);
            yesButton.Click += YesButton_Click;

            noButton = new Guna2Button();
            noButton.Text = "Skip";
            noButton.FillColor = Color.FromArgb(72, 73, 77);
            noButton.BorderRadius = 8;
            noButton.Size = new Size(120, 45);
            noButton.Location = new Point(210, 150);
            noButton.Click += NoButton_Click;

            mainPanel.Controls.Add(questionLabel);
            mainPanel.Controls.Add(timeGoalTextBox);
            mainPanel.Controls.Add(formatLabel);
            mainPanel.Controls.Add(yesButton);
            mainPanel.Controls.Add(noButton);
            this.Controls.Add(mainPanel);
        }

        private void TimeGoalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits and control characters (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private bool ValidateTimeFormat(string time)
        {
            if (time.Length != 4 || !int.TryParse(time, out int _))
                return false;

            int hours = int.Parse(time.Substring(0, 2));
            int minutes = int.Parse(time.Substring(2, 2));

            if (hours > 23 || minutes > 59)
                return false;

            return true;
        }


        private void YesButton_Click(object sender, EventArgs e)
        {
            string timeInput = timeGoalTextBox.Text;

            if (!ValidateTimeFormat(timeInput))
            {
                MessageBox.Show("Please enter a valid time in HHMM format.\nHours: 00-23, Minutes: 00-59", "Invalid Format",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeGoal = timeInput;
            GoalSet = true;
            _codingSessionManager.UpdateIsSessionTimerActive(true);
            _codingSessionManager.StartCodingSessionTimer();
            this.DialogResult = DialogResult.OK;
            _formSwitcher.SwitchToForm(FormPageEnum.FloatingBubblePage);
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            GoalSet = false;
            _codingSessionManager.UpdateIsSessionTimerActive(true);
            _codingSessionManager.StartCodingSessionTimer();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

