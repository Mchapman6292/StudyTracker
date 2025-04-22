using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using Guna.UI2.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace CodingTracker.View.TimerDisplayService
{
    public partial class OrbitalTimerPage : Form
    {
        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2HtmlLabel timeLabel;
        private Guna2ControlBox closeControlBox;
        private Guna2ControlBox minimizeControlBox;
        private Guna2Button pauseButton;

        private System.Windows.Forms.Timer sessionTimer;
        private DateTime sessionStartTime;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private bool isPaused = false;

        private System.Windows.Forms.Timer animationTimer;
        private float secondsAngle = 0;
        private float minutesAngle = 0;
        private float hoursAngle = 0;
        private bool animationSpeedup = false;
        private int speedupCounter = 0;

        private bool isDragging = false;
        private Point dragStartPoint;

        private Stopwatch stopwatchTimer;

        private Color primaryColor = Color.FromArgb(255, 81, 195);
        private Color secondaryColor = Color.FromArgb(168, 228, 255);

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly INotificationManager _notificationManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IStopWatchTimerService _stopWatchTimerService;

        public OrbitalTimerPage(ICodingSessionManager codingSessionManager, INotificationManager notificationManager, IFormSwitcher formSwitcher, IStopWatchTimerService stopWatchTimerService)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _notificationManager = notificationManager;
            _stopWatchTimerService = stopWatchTimerService;
            stopwatchTimer = _stopWatchTimerService.ReturnStopWatch();
            InitializeComponents();
            _codingSessionManager.SetCurrentSessionGoalSet(false);
            SetupTimers();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(200, 200);
            this.Text = "";
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(35, 34, 50);
            this.ShowInTaskbar = true;
            this.DoubleBuffered = true;

            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.DragForm = true;
            borderlessForm.BorderRadius = 100;

            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(32, 33, 36);
            mainPanel.BorderRadius = 100;
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderThickness = 1;
            mainPanel.Paint += MainPanel_Paint;

            timeLabel = new Guna2HtmlLabel();
            timeLabel.Text = "00:00:00";
            timeLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            timeLabel.ForeColor = Color.White;
            timeLabel.AutoSize = false;
            timeLabel.Size = new Size(120, 30);
            timeLabel.TextAlignment = ContentAlignment.MiddleCenter;
            timeLabel.Location = new Point((200 - 120) / 2, 85);
            timeLabel.BackColor = Color.Transparent;

            Guna2Panel controlPanel = new Guna2Panel();
            controlPanel.Size = new Size(80, 30);
            controlPanel.Location = new Point((mainPanel.Width - 80) / 2, 125);
            controlPanel.FillColor = Color.FromArgb(40, 70, 71, 117);
            controlPanel.BorderRadius = 15;
            controlPanel.BackColor = Color.Transparent;

            closeControlBox = new Guna2ControlBox();
            closeControlBox.Size = new Size(20, 20);
            closeControlBox.Location = new Point(controlPanel.Width - 25, 5);
            closeControlBox.FillColor = Color.Transparent;
            closeControlBox.IconColor = Color.White;
            closeControlBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.CloseBox;
            closeControlBox.Click += CloseControlBox_Click;
            closeControlBox.BackColor = Color.Transparent;

            minimizeControlBox = new Guna2ControlBox();
            minimizeControlBox.Size = new Size(20, 20);
            minimizeControlBox.Location = new Point(5, 5);
            minimizeControlBox.FillColor = Color.Transparent;
            minimizeControlBox.IconColor = Color.White;
            minimizeControlBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeControlBox.BackColor = Color.Transparent;


            Guna2GradientButton homeButton = new Guna2GradientButton();
            homeButton.Size = new Size(45, 29);
            homeButton.Location = new Point(minimizeControlBox.Location.X - 50, 0);
            homeButton.FillColor = Color.FromArgb(25, 24, 40);
            homeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            homeButton.Font = new Font("Segoe UI", 9F);
            homeButton.ForeColor = Color.White;
            homeButton.BackColor = Color.Transparent;

            pauseButton = new Guna2Button();
            pauseButton.Size = new Size(24, 24);
            pauseButton.FillColor = Color.FromArgb(255, 81, 195);
            pauseButton.BorderRadius = 12;
            pauseButton.Text = "";
            pauseButton.Image = Properties.Resources.pause;
            pauseButton.Location = new Point((controlPanel.Width - 24) / 2, 3);
            pauseButton.Cursor = Cursors.Hand;
            pauseButton.ForeColor = Color.White;
            pauseButton.Click += PauseButton_Click;
            pauseButton.BackColor = Color.Transparent;

            controlPanel.Controls.Add(minimizeControlBox);
            controlPanel.Controls.Add(pauseButton);
            controlPanel.Controls.Add(closeControlBox);

            mainPanel.Controls.Add(timeLabel);
            mainPanel.Controls.Add(controlPanel);

            this.Controls.Add(mainPanel);

            SetFormPosition();

            this.Load += OrbitalTimerForm_Load;

            homeButton.Click += HomeButton_Click;
            controlPanel.Controls.Add(homeButton);

            mainPanel.MouseDown += MainPanel_MouseDown;
            mainPanel.MouseMove += MainPanel_MouseMove;
            mainPanel.MouseUp += MainPanel_MouseUp;
        }


        private void SetupTimers()
        {
            sessionTimer = new System.Windows.Forms.Timer();
            sessionTimer.Interval = 1000;
            sessionTimer.Tick += SessionTimer_Tick;

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void OrbitalTimerForm_Load(object sender, EventArgs e)
        {
            sessionStartTime = DateTime.Now;
            _codingSessionManager.SetCodingSessionStartTimeAndDate(sessionStartTime);
            _codingSessionManager.UpdateISCodingSessionActive(true);
            sessionTimer.Start();
            animationTimer.Start();
            stopwatchTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                elapsedTime = stopwatchTimer.Elapsed;
                UpdateTimeDisplay();

                if (elapsedTime.TotalMinutes % 30 == 0 && elapsedTime.Seconds == 0)
                {
                    TriggerMilestoneEffect();
                }
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            float secondsSpeed = 6.0f;
            float minutesSpeed = 0.1f;
            float hoursSpeed = 0.0083f;

            if (animationSpeedup)
            {
                secondsSpeed *= 5;
                minutesSpeed *= 5;
                hoursSpeed *= 5;

                speedupCounter++;
                if (speedupCounter > 60)
                {
                    animationSpeedup = false;
                    speedupCounter = 0;
                }
            }

            secondsAngle = (secondsAngle + secondsSpeed) % 360;
            minutesAngle = (minutesAngle + minutesSpeed) % 360;
            hoursAngle = (hoursAngle + hoursSpeed) % 360;

            mainPanel.Invalidate();
        }

        private void UpdateTimeDisplay()
        {
            timeLabel.Text = string.Format("{0:00}:{1:00}:{2:00}",
                elapsedTime.Hours,
                elapsedTime.Minutes,
                elapsedTime.Seconds);
        }

        private void TriggerMilestoneEffect()
        {

        }

        private Color LerpColor(Color color1, Color color2, float amount)
        {
            int r = (int)(color1.R + (color2.R - color1.R) * amount);
            int g = (int)(color1.G + (color2.G - color1.G) * amount);
            int b = (int)(color1.B + (color2.B - color1.B) * amount);
            return Color.FromArgb(r, g, b);
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int centerX = mainPanel.Width / 2;
            int centerY = mainPanel.Height / 2;

            using (GraphicsPath centerGlow = new GraphicsPath())
            {
                centerGlow.AddEllipse(centerX - 30, centerY - 30, 60, 60);
                using (PathGradientBrush brush = new PathGradientBrush(centerGlow))
                {
                    brush.CenterColor = Color.FromArgb(20, 255, 255, 255);
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };
                    g.FillPath(brush, centerGlow);
                }
            }

            DrawOrbitTrack(g, centerX, centerY, 70, 2, Color.FromArgb(40, primaryColor));
            DrawOrbitTrack(g, centerX, centerY, 85, 2, Color.FromArgb(40, LerpColor(primaryColor, secondaryColor, 0.5f)));
            DrawOrbitTrack(g, centerX, centerY, 95, 2, Color.FromArgb(40, secondaryColor));

            DrawOrbitParticle(g, centerX, centerY, 70, secondsAngle, 4, primaryColor);
            DrawOrbitParticle(g, centerX, centerY, 85, minutesAngle, 4, LerpColor(primaryColor, secondaryColor, 0.5f));
            DrawOrbitParticle(g, centerX, centerY, 95, hoursAngle, 4, secondaryColor);
        }

        private void DrawOrbitTrack(Graphics g, int centerX, int centerY, int radius, int thickness, Color color)
        {
            using (Pen pen = new Pen(color, thickness))
            {
                g.DrawEllipse(
                    pen,
                    centerX - radius,
                    centerY - radius,
                    radius * 2,
                    radius * 2
                );
            }
        }

        private void DrawOrbitParticle(Graphics g, int centerX, int centerY, int radius, float angle, int size, Color color)
        {
            double radians = angle * Math.PI / 180.0;

            int x = (int)(centerX + radius * Math.Cos(radians));
            int y = (int)(centerY + radius * Math.Sin(radians));

            using (GraphicsPath outerGlowPath = new GraphicsPath())
            {
                outerGlowPath.AddEllipse(x - size * 3, y - size * 3, size * 6, size * 6);

                using (PathGradientBrush brush = new PathGradientBrush(outerGlowPath))
                {
                    Color glowColor = Color.FromArgb(60, color);
                    brush.CenterColor = glowColor;
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, color) };
                    brush.FocusScales = new PointF(0.3f, 0.3f);

                    g.FillPath(brush, outerGlowPath);
                }
            }

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(x - size * 2, y - size * 2, size * 4, size * 4);

                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = color;
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, color) };
                    brush.FocusScales = new PointF(0.2f, 0.2f);

                    g.FillPath(brush, path);
                }
            }

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, color)))
            {
                g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
            }

            using (SolidBrush highlightBrush = new SolidBrush(Color.FromArgb(180, 255, 255, 255)))
            {
                g.FillEllipse(highlightBrush, x - size / 4, y - size / 4, size / 2, size / 2);
            }
        }

        private void SetFormPosition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            this.Location = new Point(screenWidth - formWidth - 20, screenHeight - formHeight - 20);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                sessionTimer.Stop();
                stopwatchTimer.Stop();
                pauseButton.Image = Properties.Resources.playButton;
            }
            else
            {
                sessionTimer.Start();
                stopwatchTimer.Start();
                pauseButton.Image = Properties.Resources.pause;
            }

            animationSpeedup = true;
            speedupCounter = 0;
        }

        private async void CloseControlBox_Click(object sender, EventArgs e)
        {
            sessionTimer.Stop();
            animationTimer.Stop();
            stopwatchTimer.Stop();

            string message = $"End session and record time?\nElapsed: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";



            _codingSessionManager.SetDurationSeconds(stopwatchTimer.Elapsed.TotalSeconds);
            _codingSessionManager.SetCodingSessionEndTimeAndDate(DateTime.Now);
            await _codingSessionManager.EndCodingSessionAsync();

            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
        }


        private void DisposeCustomResources()
        {
            sessionTimer?.Dispose();
            animationTimer?.Dispose();
            borderlessForm?.Dispose();
        }


        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentScreenPos = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(
                    currentScreenPos.X - dragStartPoint.X,
                    currentScreenPos.Y - dragStartPoint.Y);
            }
        }

        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private async void HomeButton_Click(object sender, EventArgs e)
        {
            sessionTimer.Stop();
            animationTimer.Stop();
            stopwatchTimer.Stop();



            _codingSessionManager.SetDurationSeconds(stopwatchTimer.Elapsed.TotalSeconds);
            _codingSessionManager.SetCodingSessionEndTimeAndDate(DateTime.Now);
            await _codingSessionManager.EndCodingSessionAsync();

            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
        }



    }
}
