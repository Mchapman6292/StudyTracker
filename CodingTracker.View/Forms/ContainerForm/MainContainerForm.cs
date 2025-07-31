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


        public void UpdateAllControlLocationsInChildForm(Form childForm)
        {
            foreach (Control control in childForm.Controls)
            {
                if (control.Location.X > 100) 
                {
                    control.Location = new Point(control.Location.X - 178, control.Location.Y);
                }
            }
        }

        private void LoadForm(Form childForm)
        {

            contentPanel.Controls.Clear();

  
            childForm.TopLevel = false;              
            childForm.FormBorderStyle = FormBorderStyle.None;  
            childForm.Dock = DockStyle.Fill;         

         

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
            UpdateAllControlLocationsInChildForm(mainPageForm);
            contentPanel.Controls.Add(mainPageForm);
            mainPageForm.Anchor = AnchorStyles.Left;
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