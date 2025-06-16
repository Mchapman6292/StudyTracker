using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts
{
    public class AnimatedTimerColumn
    {
        private SKRect _rectDrawing;

        private Color GradientColorOne = Color.FromArgb(255, 81, 195);

        private Color GradientColorTwo = Color.Turquoise;


        public int width = 10;

        public int segmentHeight;

        public List<int> displayDigits;

        private int SegmentCount;

        Point position;
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;




        public AnimatedTimerColumn(int width, List<int> displayDigits, Point position)
        {
            this.width = width;
            segmentHeight = width;
            SegmentCount = displayDigits.Count;
            this.position = position;
        }





    }
}
