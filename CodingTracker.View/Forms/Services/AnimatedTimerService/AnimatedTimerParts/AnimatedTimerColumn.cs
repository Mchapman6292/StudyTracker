using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using SkiaSharp;
using System.Drawing;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts
{
    public class AnimatedTimerColumn
    {
        private SKRect _rectDrawing;

        private SKColor GradientColorOne = AnimatedColumnSettings.ColumnColor;



        public List<AnimatedTimerSegment> TimerSegments;

        public int FocusedSegmentIndex = 0;
        

        public AnimatedTimerSegment FocusedSegment { get; private set; }



        public int TotalSegmentCount;

        public float Width = AnimatedColumnSettings.ColumnWidth;
        public float Height => TotalSegmentCount * AnimatedColumnSettings.SegmentHeight;

        public SKPoint Location { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;

        public float TextSize = AnimatedColumnSettings.TextSize;

        
        //TODO review if needed here or in calculation class. 
        public float ScrollOffset { get; set; }

        public int CurrentValue { get; set; }
        public int PreviousValue { get; set; } = -1;
        public TimeSpan AnimationInterval { get; }
        public TimeSpan LastAnimationStartTime {  get; set; }
        public bool IsAnimating { get; set; }



        public ColumnUnitType ColumnType { get; set; }
        public bool EnableHighlight { get; set; } = true;



         


        public float OverlayOpacity { get; private set; } = 1f;
        public float OverlayRadius { get; private set; }

        public float AnimationProgress { get; private set; }

        public float NormalizedProgress { get; private set; }



        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint location, ColumnUnitType columnType)
        {
            TimerSegments = timerSegments;
            ColumnType = columnType;
            Location = location;
            TotalSegmentCount = timerSegments.Count;
            AnimationInterval = AnimatedColumnSettings.UnitTypesToAnimationDurations[columnType];

            OverlayRadius = (AnimatedColumnSettings.ColumnWidth / 2) + 5;

            FocusedSegment = timerSegments[0];
           
            
        }




        public void UpdateAnimationState(float animationProgress, float normalizedProgress)
        {
            AnimationProgress = animationProgress;
            NormalizedProgress = normalizedProgress;
        }

        public void UpdateFocusedSegment(AnimatedTimerSegment segment)
        {
            IncrementFocusedSegmentIndex();

            FocusedSegment = TimerSegments[FocusedSegmentIndex];
        }

        private void IncrementFocusedSegmentIndex()
        {
            FocusedSegmentIndex++;
            if (FocusedSegmentIndex >= TimerSegments.Count)
            {
                FocusedSegmentIndex = 0;
            
            }
        }


        public void SetFocusedSegmentByValue(int newValue)
        {
            var focusedSegment = TimerSegments.FirstOrDefault(s => s.Value == newValue);
        }

    }
}
