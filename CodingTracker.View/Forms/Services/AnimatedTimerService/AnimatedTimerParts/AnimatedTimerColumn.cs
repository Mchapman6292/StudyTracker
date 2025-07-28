using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using SkiaSharp;
using System.Drawing;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts
{
    public class AnimatedTimerColumn
    {

        public List<AnimatedTimerSegment> TimerSegments;

        public int TargetDigit { get; set; } = 1;
        public int ActiveDigit { get; set; } = 0;
        public AnimatedTimerSegment FocusedSegment { get;  set; }

        public int TotalSegmentCount;

        public float Width = AnimatedColumnSettings.ColumnWidth;
        public float Height => TotalSegmentCount * AnimatedColumnSettings.SegmentHeight;

        public ColumnUnitType ColumnType { get; set; }

        public SKPoint InitialLocation { get; set; }
        public SKPoint Location { get; set; }

    
        public float YTranslation { get; set; }

        public float TargetY {  get; set; }

        public bool IsStandardAnimationOccuring { get; set; }


        public bool PassedFirstTransition { get; set; } = false;

        public TimeSpan AnimationInterval { get; } // How often the column will animate. 





        //The base animation progress of the entire animation over 1s. Used for opactiy and colors. 
        public float BaseAnimationProgress { get; set; }

        // Animnation progress of column scroll which is base animation + scrolloffset (lerp * easing value).
        public float ColumnScrollProgress { get; set; }

        // Animation progress for the circle (0-1 over first 70% of animation)
        public float CircleAnimationProgress { get; set; }

        public float RestartAnimationProgress { get; set; } 

        public int MaxValue;

        public bool IsColumnActive { get; set; }

        public bool IsNumberBlurringActive { get; set; }

        public int StartingYSkControl;

        internal bool IsRestarting { get; set; }

        public bool HasRestarted { get; set; } = false;
        
        // Records columns Y location when restart button pressed. 
        public float YLocationAtRestart { get; set; }


        public bool PassedFirstAnimationTick { get; set; } = false;




        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint initalLocation, ColumnUnitType columnType)
        {
            TimerSegments = timerSegments;
            ColumnType = columnType;
            InitialLocation = initalLocation;
            Location = initalLocation;
            TotalSegmentCount = timerSegments.Count;
            FocusedSegment = timerSegments[0];
            AnimationInterval = AnimatedColumnSettings.UnitTypesToAnimationTimeSpans[columnType];
            MaxValue = FindMaxSegmentValue();
            IsColumnActive = InitializeIsColumnActive();
            IsNumberBlurringActive = InitializeNumberBlurringStartAnimationActive();
        }



        private int FindMaxSegmentValue()
        {
            return TimerSegments.Max(segment => segment.Value);
        }

   

        private bool InitializeIsColumnActive()
        {
            if(ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                return true;
            }
            return false;
        }


        private bool InitializeNumberBlurringStartAnimationActive()
        {
            if (ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                return true;
            }
            return false;
        }


        public int GetNextValueInSequence()
        {
            int focusedSegmentValue = FocusedSegment.Value;
            int maxSegmentValue = FindMaxSegmentValue();

            if (focusedSegmentValue == maxSegmentValue)
            {
                return 0;
            }

            else return focusedSegmentValue + 1;
        }



    




    }
}

