namespace CodingTracker.View.PopUpFormService
{
    public partial class FloatingBubbleTimer : Form
    {
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private bool isMinimized = false;
        private Size expandedSize = new Size(140, 50);
        private Size minimizedSize = new Size(30, 30);
        private DateTime sessionStartTime;
        private System.Windows.Forms.Timer sessionTimer = new System.Windows.Forms.Timer();
        private string sessionName = "New Session";
        private int pulseDirection = 1;
        private int pulseCount = 0;

        // Define your app theme colors as constants
        private static readonly Color AppBackground = Color.FromArgb(50, 55, 75);
        private static readonly Color AppAccent = Color.FromArgb(255, 100, 180);
        private static readonly Color AppTextPrimary = Color.FromArgb(0, 255, 255);
        private static readonly Color AppTextSecondary = Color.FromArgb(220, 220, 220);

        public FloatingBubbleTimer(string sessionName = null)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(sessionName))
                this.sessionName = sessionName;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 200, 100);
            this.Size = expandedSize;
            this.BackColor = AppBackground;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Opacity = 0.90; // Slightly more opaque than original

            // Apply theme colors to controls
            ApplyThemeColors();

            // Set rounded corners for the form
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            // Update the session label
            sessionLabel.Text = this.sessionName;

            // Configure event handlers for drag functionality
            mainPanel.MouseDown += MainPanel_MouseDown;
            mainPanel.MouseMove += MainPanel_MouseMove;
            mainPanel.MouseUp += MainPanel_MouseUp;

            // Configure button click events
            minimizeButton.Click += MinimizeButton_Click;
            stopButton.Click += StopButton_Click;

            sessionStartTime = DateTime.Now;
            SetupTimer();
        }

        private void ApplyThemeColors()
        {
            // Apply theme colors to mainPanel
            mainPanel.BorderColor = AppAccent;
            mainPanel.FillColor = AppBackground;

            // Apply theme colors to buttons
            minimizeButton.FillColor = AppAccent;
            minimizeButton.ForeColor = Color.White;
            stopButton.FillColor = AppAccent;
            stopButton.ForeColor = Color.White;

            // Apply theme colors to labels
            timerLabel.ForeColor = AppTextPrimary;
            sessionLabel.ForeColor = AppTextSecondary;
        }

        private void SetupTimer()
        {
            sessionTimer.Interval = 1000;
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - sessionStartTime;
            timerLabel.Text = elapsed.ToString(@"hh\:mm\:ss");

            if (isMinimized)
            {
                PulseEffect();
            }
        }

        private void PulseEffect()
        {
            if (pulseCount >= 5)
            {
                pulseDirection = -1;
            }
            else if (pulseCount <= 0)
            {
                pulseDirection = 1;
            }

            pulseCount += pulseDirection;

            // Use the app's accent color for the pulse effect
            mainPanel.BorderColor = Color.FromArgb(
                Math.Min(255, AppAccent.R + pulseCount * 10),
                Math.Max(0, AppAccent.G - pulseCount * 10),
                Math.Min(255, AppAccent.B + pulseCount * 5)
            );
            mainPanel.BorderThickness = pulseCount / 2 + 1;
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            if (isMinimized)
            {
                this.Size = expandedSize;
                this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
                timerLabel.Location = new Point(5, 7);
                timerLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                timerLabel.Size = new Size(expandedSize.Width - 10, 30);

                minimizeButton.Visible = true;
                stopButton.Visible = true;
                sessionLabel.Visible = true;
                minimizeButton.Text = "−";

                mainPanel.BorderColor = AppAccent;
                mainPanel.BorderThickness = 1;
                pulseCount = 0;
            }
            else
            {
                this.Size = minimizedSize;
                this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
                timerLabel.Location = new Point(0, 0);
                timerLabel.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                timerLabel.Size = new Size(minimizedSize.Width, minimizedSize.Height);

                minimizeButton.Visible = false;
                stopButton.Visible = false;
                sessionLabel.Visible = false;
            }

            isMinimized = !isMinimized;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            sessionTimer.Stop();

            DialogResult result = MessageBox.Show(
                $"End coding session?\nTotal time: {timerLabel.Text}",
                "End Session",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                sessionTimer.Start();
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startPoint = new Point(e.X, e.Y);
            }
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;

            if (isMinimized && Math.Abs(e.X - startPoint.X) < 5 && Math.Abs(e.Y - startPoint.Y) < 5)
            {
                MinimizeButton_Click(this, EventArgs.Empty);
            }
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
    }
}