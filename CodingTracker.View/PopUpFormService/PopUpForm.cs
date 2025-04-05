using Guna.UI2.WinForms;

namespace CodingTracker.View.PopUpFormService
{
    public partial class PopUpForm : Form
    {
        private Guna2Panel mainPanel;
        private Guna2HtmlLabel questionLabel;
        private Guna2TextBox goalTextBox;
        private Guna2Button yesButton;
        private Guna2Button noButton;
        private Guna2BorderlessForm borderlessForm;

        public string Goal { get; private set; }
        public bool GoalSet { get; private set; } = false;
        public PopUpForm()
        {
            InitializeComponent();
        }

        private void PopUpForm_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponents()
        {
            // Configure form settings
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;

            // Initialize Guna UI components
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
            questionLabel.Text = "Would you like to set a goal for this session?";
            questionLabel.ForeColor = Color.White;
            questionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            questionLabel.AutoSize = false;
            questionLabel.Size = new Size(350, 30);
            questionLabel.TextAlignment = ContentAlignment.MiddleCenter;
            questionLabel.Location = new Point(25, 30);

            goalTextBox = new Guna2TextBox();
            goalTextBox.PlaceholderText = "Enter your goal here...";
            goalTextBox.BorderRadius = 8;
            goalTextBox.ForeColor = Color.White;
            goalTextBox.FillColor = Color.FromArgb(45, 46, 50);
            goalTextBox.BorderColor = Color.FromArgb(94, 148, 255);
            goalTextBox.Size = new Size(300, 36);
            goalTextBox.Location = new Point(50, 80);

            yesButton = new Guna2Button();
            yesButton.Text = "Set Goal";
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

            // Add components to form
            mainPanel.Controls.Add(questionLabel);
            mainPanel.Controls.Add(goalTextBox);
            mainPanel.Controls.Add(yesButton);
            mainPanel.Controls.Add(noButton);
            this.Controls.Add(mainPanel);
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            Goal = goalTextBox.Text;
            GoalSet = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            GoalSet = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

