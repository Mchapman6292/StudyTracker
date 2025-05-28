using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.WaveVisualizerService;
using CodingTracker.View.Forms.WaveVisualizer;
using Guna.Charts.WinForms;
using System.Runtime.InteropServices;

namespace CodingTracker.View
{
    public partial class WaveVisualizationForm : Form
    {
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IWaveVisualizationControl _waveControl;
        private readonly IKeyboardActivityTracker _activityTracker;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public WaveVisualizationForm(
            IStopWatchTimerService stopWatchTimerService,
            IWaveVisualizationControl waveControl,
            IKeyboardActivityTracker activityTracker)
        {
            _stopWatchTimerService = stopWatchTimerService;
            _waveControl = waveControl;
            _activityTracker = activityTracker;

            InitializeComponent();

            var visualizationControl = (WaveVisualizationControl)_waveControl;
            visualizationControl.Dock = DockStyle.Fill;
            visualizationControl.MouseDown += WaveVisualizationForm_MouseDown;
            this.Controls.Add(visualizationControl);

            _activityTracker.ActivityChanged += ActivityTracker_ActivityChanged;

            this.FormBorderStyle = FormBorderStyle.None;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            SetFormPosition();
            this.TopMost = true;
            this.Activate();
            decayTimer.Start();
        }

        private void ActivityTracker_ActivityChanged(object sender, float intensity)
        {
            _waveControl.UpdateIntensity(intensity);
        }

        private void DecayTimer_Tick(object sender, EventArgs e)
        {
            _activityTracker.UpdateDecay();
        }

        private void SetFormPosition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;
            this.Location = new Point(screenWidth - formWidth - 20, 20);
        }

        private void WaveVisualizationForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);

                Point cursor = this.PointToClient(Cursor.Position);
                int edge = 10;

                if (cursor.X <= edge)
                {
                    if (cursor.Y <= edge)
                        m.Result = (IntPtr)HTTOPLEFT;
                    else if (cursor.Y >= this.ClientSize.Height - edge)
                        m.Result = (IntPtr)HTBOTTOMLEFT;
                    else
                        m.Result = (IntPtr)HTLEFT;
                }
                else if (cursor.X >= this.ClientSize.Width - edge)
                {
                    if (cursor.Y <= edge)
                        m.Result = (IntPtr)HTTOPRIGHT;
                    else if (cursor.Y >= this.ClientSize.Height - edge)
                        m.Result = (IntPtr)HTBOTTOMRIGHT;
                    else
                        m.Result = (IntPtr)HTRIGHT;
                }
                else if (cursor.Y <= edge)
                {
                    m.Result = (IntPtr)HTTOP;
                }
                else if (cursor.Y >= this.ClientSize.Height - edge)
                {
                    m.Result = (IntPtr)HTBOTTOM;
                }

                return;
            }

            base.WndProc(ref m);
        }

        public Animation
    }
}
