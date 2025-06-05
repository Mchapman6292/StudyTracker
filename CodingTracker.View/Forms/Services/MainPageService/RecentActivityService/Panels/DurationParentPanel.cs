using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels
{
    public class DurationParentPanel : Guna2GradientPanel
    {


        public Guna2HtmlLabel DateLabel { get; set; }

        public Guna2HtmlLabel DurationLabel { get; set; }

        public DateOnly PanelDateLocal {  get; set; }



        public int TotalDurationSeconds { get; set; }




        public int SumDurationPanelSeconds()
        {
            return this.Controls.OfType<DurationPanel>()
                               .Sum(panel => panel.DurationSeconds);
        }






    }
}
