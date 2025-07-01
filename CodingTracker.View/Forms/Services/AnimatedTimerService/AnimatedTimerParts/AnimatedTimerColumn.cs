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





  




        // True when the columns
        public bool IsTimeToAnimate { get; set; }



        public ColumnUnitType ColumnType { get; set; }


        public bool EnableHighlight { get; set; } = true;


         


        public float OverlayOpacity { get; private set; } = 1f;
        public float OverlayRadius { get; private set; }



        public float NormalizedProgress { get; private set; }



        public float AnimationProgressBetweenNumbers { get; private set; }


        public bool IsAnimating { get; set; }

        public TimeSpan LastAnimationStartTime { get; set; }  // When current animation began
        public TimeSpan NextTransitionTime { get; set; } // When next animation should start

        public TimeSpan AnimationInterval { get; } // How long animation takes (300ms)


        public int CurrentValue { get; set; }
        public int PreviousValue { get; set; } = -1;








        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint location, ColumnUnitType columnType)
        {
            TimerSegments = timerSegments;
            ColumnType = columnType;
            Location = location;
            TotalSegmentCount = timerSegments.Count;


            // TODO review, this should be set in AnimatedColumnSettings
            OverlayRadius = (AnimatedColumnSettings.ColumnWidth / 2) + 5;

            FocusedSegment = timerSegments[0];

            AnimationInterval = AnimatedColumnSettings.UnitTypesToAnimationDurations[columnType];

            NextTransitionTime = TimeSpan.Zero + AnimationInterval;
        }



        /*
        public void UpdateAnimationState(float animationProgress, float normalizedProgress)
        {
            AnimationProgress = animationProgress;
            NormalizedProgress = normalizedProgress;
        }
        */

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



        public float CalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }




        public float GetColumnAnimationProgress(TimeSpan elapsed)
        {
           return (float)(elapsed.TotalSeconds % 1.0); 
        }

        public float CalculateOverlayAnimationProgress(TimeSpan elapsed)
        {
            float columnProgress = GetColumnAnimationProgress(elapsed);

            if(columnProgress > AnimatedColumnSettings.CircleAnimationDurationRatio)
            {
                return 1f;
            }
            return columnProgress / AnimatedColumnSettings.CircleAnimationDurationRatio;
        }





    


        private void UpdateNexTransitionTime()
        {
            NextTransitionTime += AnimationInterval;
        }

   

    }
}
