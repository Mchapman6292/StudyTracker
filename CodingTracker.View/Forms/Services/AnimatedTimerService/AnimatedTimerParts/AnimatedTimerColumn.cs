using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using SkiaSharp;
using System.Drawing;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts
{
    public class AnimatedTimerColumn
    {

        public List<AnimatedTimerSegment> TimerSegments;

        public AnimatedTimerSegment FocusedSegment { get;  set; }

        public int TotalSegmentCount;

        public float Width = AnimatedColumnSettings.ColumnWidth;
        public float Height => TotalSegmentCount * AnimatedColumnSettings.SegmentHeight;
        public SKPoint Location { get; set; }
        public float ScrollOffset { get; set; }
        public ColumnUnitType ColumnType { get; set; }

        public bool IsAnimating { get; set; }

        public float ColumnAnimationProgress;
        public float CircleAnimationProgress;

        public TimeSpan AnimationStartTime { get; set; }  // When current animation began
        public TimeSpan NextTransitionTime { get; set; } // When next animation should start
        public TimeSpan CurrentAnimationEndTime { get; set; }
        public TimeSpan AnimationInterval { get; } // How long animation takes (300ms)


        public int CurrentValue { get; set; }
        public int PreviousValue { get; set; } = 1;



        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint location, ColumnUnitType columnType)
        {
            TimerSegments = timerSegments;
            ColumnType = columnType;
            Location = location;
            TotalSegmentCount = timerSegments.Count;
            FocusedSegment = timerSegments[0];
            AnimationInterval = AnimatedColumnSettings.UnitTypesToAnimationDurations[columnType];
            NextTransitionTime = TimeSpan.Zero + AnimationInterval;
        }



        



    

   

    }
}
