using CodingTracker.View.FormManagement;

namespace CodingTracker.View.Forms.Containers
{
    // In MainContainerForm.cs

    public partial class MainContainerForm : Form
    {
        private readonly IFormFactory _formFactory;
        private readonly IFormNavigator _formNavigator;

        public MainContainerForm(IFormFactory formFactory, IFormNavigator formNavigator)
        {

            _formFactory = formFactory;
            _formNavigator = formNavigator;
            InitializeComponent();

            // Load the dashboard by default when the container opens
            LoadMainPageOnStart();

            // Wire up button click events
            dashboardButton.Click += DashboardButton_Click;
            sessionsButton.Click += SessionsButton_Click;
            newSessionButton.Click += NewSessionButton_Click;
            logoutButton.Click += LogoutButton_Click;

        }

        private void LoadForm(Form childForm)
        {

            contentPanel.Controls.Clear();

            // Configure the child form
            childForm.TopLevel = false;              // This is crucial - makes it a child
            childForm.FormBorderStyle = FormBorderStyle.None;  // Remove borders
            childForm.Dock = DockStyle.Fill;         // Fill the entire content panel

            // Add to content panel and show
            contentPanel.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }


        private void LoadMainPageOnStart()
        {
            Form mainPageForm = _formFactory.GetOrCreateForm(FormPageEnum.MainPageTestFormn);

            mainPageForm.TopLevel = false;
            mainPageForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(mainPageForm);
            mainPageForm.BringToFront();
            mainPageForm.Show();
        }


      


        private void UpdateActiveButton(string activeButton)
        {
            // Reset all buttons to transparent with pink border
            dashboardButton.FillColor = Color.FromArgb(35, 34, 50);
            dashboardButton.FillColor2 = Color.FromArgb(35, 34, 50);
            dashboardButton.BorderColor = Color.HotPink;
            dashboardButton.BorderThickness = 1;

            sessionsButton.FillColor = Color.FromArgb(35, 34, 50);
            sessionsButton.FillColor2 = Color.FromArgb(35, 34, 50);
            sessionsButton.BorderColor = Color.HotPink;
            sessionsButton.BorderThickness = 1;

            newSessionButton.FillColor = Color.FromArgb(35, 34, 50);
            newSessionButton.FillColor2 = Color.FromArgb(35, 34, 50);
            newSessionButton.BorderColor = Color.HotPink;
            newSessionButton.BorderThickness = 1;

            // Highlight the active button
            switch (activeButton)
            {
                case "dashboard":
                    dashboardButton.FillColor = Color.FromArgb(255, 81, 195);
                    dashboardButton.FillColor2 = Color.FromArgb(168, 228, 255);
                    dashboardButton.BorderThickness = 0;
                    break;
                case "sessions":
                    sessionsButton.FillColor = Color.FromArgb(255, 81, 195);
                    sessionsButton.FillColor2 = Color.FromArgb(168, 228, 255);
                    sessionsButton.BorderThickness = 0;
                    break;
                case "newSession":
                    newSessionButton.FillColor = Color.FromArgb(255, 81, 195);
                    newSessionButton.FillColor2 = Color.FromArgb(168, 228, 255);
                    newSessionButton.BorderThickness = 0;
                    break;
            }
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            Form mainPage = _formFactory.GetOrCreateForm(FormPageEnum.MainPageTestFormn);
            LoadForm(mainPage);
            UpdateActiveButton("dashboard");
        }

        private void SessionsButton_Click(object sender, EventArgs e)
        {
            Form editSessionPage = _formFactory.GetOrCreateForm(FormPageEnum.EditSessionForm);
            LoadForm(editSessionPage);
            UpdateActiveButton("sessions");
        }

        private void NewSessionButton_Click(object sender, EventArgs e)
        {
           
            UpdateActiveButton("newSession");
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
      
            this.Close();

        }
    }
}