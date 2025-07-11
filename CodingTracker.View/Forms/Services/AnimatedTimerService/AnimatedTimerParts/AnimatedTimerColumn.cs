﻿using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
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

        public ColumnUnitType ColumnType { get; set; }

        public SKPoint InitialLocation { get; set; }
        public SKPoint Location { get; set; }
    
        public float YTranslation { get; set; }

        public float TargetY {  get; set; }

        public bool IsAnimating { get; set; }

        public bool PassedFirstTransition { get; set; } = false;

        public TimeSpan AnimationInterval { get; } // How often the column will animate. 

        public int TargetValue { get; set; } = 1;


        public int CurrentValue { get; set; } = 0;



        //The base animation progress of the entire animation over 0.7ms. 
        public float BaseAnimationProgress { get; set; }

        // Animnation progress of column scroll which is base animation + scrolloffset (lerp * easing value).
        public float ColumnScrollProgress { get; set; }

        // Animation progress for the circle (0-1 over first 70% of animation)
        public float CircleAnimationProgress { get; set; }

        public int MaxValue;

        public bool IsColumnActive { get; set; }




        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint initalLocation, ColumnUnitType columnType)
        {
            TimerSegments = timerSegments;
            ColumnType = columnType;
            InitialLocation = initalLocation;
            Location = initalLocation;
            TotalSegmentCount = timerSegments.Count;
            FocusedSegment = timerSegments[0];
            AnimationInterval = AnimatedColumnSettings.UnitTypesToAnimationTimeSpans[columnType];
            MaxValue = FindMaxColumnValue();
            IsColumnActive = SetIsColumnActive();
        }



        private int FindMaxColumnValue()
        {
            return TimerSegments.Max(segment => segment.Value);
        }



        private bool SetIsColumnActive()
        {
            if(ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                return true;
            }
            return false;
        }

   

    

   

    }
}
