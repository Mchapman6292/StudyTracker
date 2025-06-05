using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels
{
    public class DurationPanel : Guna2GradientPanel
    {
        public string DurationHHMM { get; set; }
        public int DurationSeconds { get; set; }
        public DateTime StartTimeLocal { get; set; }
        public DateTime EndTimeLocal { get; set; }
        public string StudyNotes { get; set; }



        public DurationPanel()
        {
            this.BackColor = Color.Transparent;
            this.BorderRadius = 2;
            this.Dock = DockStyle.Left;

            // Set up hover events right here
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.Click += OnClick;
        }



        private void OnMouseEnter(object sender, EventArgs e)
        {
            string tooltip = $"Duration: {DurationHHMM}.";

            var toolTip = new ToolTip();
            toolTip.SetToolTip(this, tooltip);
        }


        private void OnMouseLeave(object sender, EventArgs e)
        {
            
        }

        private void OnClick(object sender, EventArgs e)
        {
            
            MessageBox.Show($"Duration: {DurationHHMM}s\nNotes: {StudyNotes}");
        }
    }
}
