using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.SnakeAnimation
{
    public class AnimatedSnake
    {

        public SKPoint HeadStart { get; set; }
        public SKPoint TailStart { get; set; }


        public int SnakeLength { get; set; } = 20;
        public float SnakeThickness { get; set; } = 2f;



        public List<SKPoint> CurrentPath { get; set; } = new List<SKPoint>();
        public float PathProgress { get; set; } = 0f; 
        public float BaseSpeed { get; set; } = 1f; 
        public float CurrentSpeed { get; set; } = 1f; // Current speed (can vary based on animation state)







        public List<AnimatedTimerColumn> ColumnsToTraverse = new List<AnimatedTimerColumn>();
        public AnimatedTimerColumn CurrentColumn { get; set; }



        // Visual Properties
        public SKColor HeadColor { get; set; } = new SKColor(255, 0, 255);
        public SKColor BrightMagenta { get; set; } = new SKColor(224, 64, 228);
        public SKColor SoftNeonPurple { get; set; } = new SKColor(204, 92, 207);
        public SKColor DeepLavenderPurple { get; set; } = new SKColor(180, 106, 191);
        public SKColor TailColor { get; set; } = new SKColor(155, 89, 182);
        public float HeadOpacity { get; set; } = 1f;
        public float TailOpacity { get; set; } = 0.3f;
        public float GlowRadius { get; set; } = 8f; // Neon glow effect radius







        public AnimatedSnake(List<AnimatedTimerColumn> initializedColumns)
        {
            HeadStart = ReturnSnakeStartingPoint(initializedColumns);
            TailStart = ReturnSnakeStartingPoint(initializedColumns);
        }


        private SKPoint ReturnSnakeStartingPoint(List<AnimatedTimerColumn> initializedColumns)
        {
            AnimatedTimerColumn startColumn = initializedColumns.FirstOrDefault(c => c.ColumnType == ColumnUnitType.SecondsSingleDigits);

            return startColumn.Location:
            


        }


    }
}
