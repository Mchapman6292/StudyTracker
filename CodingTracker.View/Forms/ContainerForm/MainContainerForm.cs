using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;
using Guna.UI2.WinForms;
using OpenTK.Platform.Windows;

namespace CodingTracker.View.Forms.Containers
{
    // In MainContainerForm.cs

    public partial class MainContainerForm : Form
    {
        private readonly IFormFactory _formFactory;
        private readonly IFormNavigator _formNavigator;
        private readonly IApplicationLogger _appLogger;
        private readonly IButtonHighlighterService _buttonHighlighterService;


        private int mainContentPanelXLocation => mainContentPanel.Location.X;

        public MainContainerForm(IFormFactory formFactory, IFormNavigator formNavigator, IApplicationLogger appLogger, IButtonHighlighterService buttonHighlighterService)
        {

            _formFactory = formFactory;
            _formNavigator = formNavigator;
            _appLogger = appLogger;
            InitializeComponent();

            // Load the dashboard by default when the container opens
            LoadMainPageOnStart();


            mainPageButton.Click += MainPageButton_Click;
            sessionsButton.Click += SessionsButton_Click;
            newSessionButton.Click += NewSessionButton_Click;
            logoutButton.Click += LogoutButton_Click;

            _buttonHighlighterService = buttonHighlighterService;   
            

        }


        private void MainContainerForm_Load(object sender, EventArgs e)
        {
            _buttonHighlighterService.SetButtonHoverColors(mainPageButton);
            _buttonHighlighterService.SetButtonHoverColors(newSessionButton);
            _buttonHighlighterService.SetButtonHoverColors(sessionsButton);
        }


        public void UpdateAllControlLocationsInChildForm(Form childForm)
        {
            int sideBarXLocation = sidebarPanel.Location.X;

            int controlsChanged = 0;

            foreach (Control control in childForm.Controls)
            {
                if (control.Location.X > sideBarXLocation) 
                {
                    control.Location = new Point(control.Location.X - 178, control.Location.Y);
                    controlsChanged++;
                    _appLogger.Debug($"Updated {control.Name} from X:{control.Location.X + 178} to X:{control.Location.X}");

                    if(control.Location.X < sideBarXLocation)
                    {
                        _appLogger.Error($"Control {control.Name} location set incorrect at {control.Location.X}"); 
                    }
                }
            }
            _appLogger.Debug($"Number of controls changed for {childForm.Name}: {controlsChanged}.");
        }


        public void CalculateControlNewLocation(Control childFormControl)
        {

        }






        public void TestUpdateControls(Form childForm)
        {
            string logMessage = $"Updating controls for form: {childForm.Name}.";
            int newX = 0;

            foreach(Control control in childForm.Controls)
            {
                Point oldLocation = control.Location;
                Point newLocation = new Point(newX, control.Location.Y);
                control.Location = newLocation;
                logMessage += $"\n {control.Name} location changed from oldX: {oldLocation.X} oldY: {oldLocation.Y}. to newX {newLocation.X} newY: {newLocation.Y}.";
            }
            _appLogger.Debug(logMessage);

        }




        public void TESTUpdateAllControlLocationsInChildForm(Form childForm)
        {
            int controlsChanged = 0;
            UpdateControlsRecursively(childForm, ref controlsChanged);
            _appLogger.Debug($"Number of controls changed for {childForm.Name}: {controlsChanged}.");
        }

        private void UpdateControlsRecursively(Control parent, ref int controlsChanged)
        {
            foreach (Control control in parent.Controls)
            {

                if (control.Dock == DockStyle.None && control.Location.X > 100)
                {
                    control.Location = new Point(control.Location.X - 178, control.Location.Y);
                    controlsChanged++;
                    _appLogger.Debug($"Updated {control.Name} from X:{control.Location.X + 178} to X:{control.Location.X}");
                }

                if (control.HasChildren)
                {
                    UpdateControlsRecursively(control, ref controlsChanged);
                }
            }
        }



        public void SetDataGridViewLocation(Guna2DataGridView dgv)
        {
            int sideBarXLocation = sidebarPanel.Location.X;
            dgv.Location = new Point(sideBarXLocation, dgv.Location.Y);

        }

        private void LoadForm(Form childForm)
        {

            mainContentPanel.Controls.Clear();
            childForm.TopLevel = false;              
            childForm.Dock = DockStyle.Fill;



            // Add to content panel and show
            mainContentPanel.Controls.Add(childForm);
            childForm.Anchor = AnchorStyles.Left;
            childForm.BringToFront();
            childForm.Show();
        }


        private void LoadMainPageOnStart()
        {
            Form mainPageForm = _formFactory.GetOrCreateForm(FormPageEnum.MainPageForm);

            mainPageForm.TopLevel = false;
            mainPageForm.Dock = DockStyle.Fill;

            UpdateAllControlLocationsInChildForm(mainPageForm);
            mainContentPanel.Controls.Add(mainPageForm);
            mainPageForm.Anchor = AnchorStyles.Left;
            mainPageForm.BringToFront();
            mainPageForm.Show();
        }


        public void SwitchToForm(FormPageEnum targetChildForm)
        {
            mainContentPanel.Controls.Clear();

            Form targetForm = _formFactory.GetOrCreateForm(targetChildForm);

            targetForm.TopLevel = false;
            targetForm.Dock = DockStyle.Fill;
      

            mainContentPanel.Controls.Add(targetForm);
            targetForm.Anchor = AnchorStyles.Left;
            targetForm.BringToFront();
            targetForm.Show();
        }


      


        private void UpdateActiveButton(string activeButton)
        {
            // Reset all buttons to transparent with pink border
            mainPageButton.FillColor = Color.FromArgb(35, 34, 50);
            mainPageButton.FillColor2 = Color.FromArgb(35, 34, 50);
            mainPageButton.BorderColor = Color.HotPink;
            mainPageButton.BorderThickness = 1;

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
                    mainPageButton.FillColor = Color.FromArgb(255, 81, 195);
                    mainPageButton.FillColor2 = Color.FromArgb(168, 228, 255);
                    mainPageButton.BorderThickness = 0;
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

        private void MainPageButton_Click(object sender, EventArgs e)
        {
            /*
            Form mainPage = _formFactory.GetOrCreateForm(FormPageEnum.MainPageForm);
            LoadForm(mainPage);
            UpdateActiveButton("dashboard");
            */

            SwitchToForm(FormPageEnum.MainPageForm);
 
        }

        private void SessionsButton_Click(object sender, EventArgs e)
        {
            Form editSessionPage = _formFactory.GetOrCreateForm(FormPageEnum.EditSessionForm);
            LoadForm(editSessionPage);


        }

        private void NewSessionButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.SessionGoalForm);
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
      
            this.Close();

        }
    }
}