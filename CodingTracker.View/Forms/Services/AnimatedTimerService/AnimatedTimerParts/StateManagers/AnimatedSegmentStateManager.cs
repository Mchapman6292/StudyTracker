using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedSegmentStateManager
    {
        void UpdateSegmentPosition(AnimatedTimerSegment segment, SKPoint newLocation);
        void SetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue);
        void UpdateSegmentPositions(AnimatedTimerColumn column);
    }
    public class AnimatedSegmentStateManager : IAnimatedSegmentStateManager
    {
        private readonly IApplicationLogger _appLogger;


        public AnimatedSegmentStateManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }




        public void UpdateSegmentAnimationState(AnimatedTimerColumn column, AnimatedTimerSegment segment)
        {
            SetFocusedSegmentByValue(column, column.CurrentValue);
        }



        public void SetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue)
        {
            var focusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);
        }


        public void UpdateSegmentPosition(AnimatedTimerSegment segment, SKPoint newLocation)
        {
            segment.Location = newLocation;
        }

        public void UpdateSegmentPositions(AnimatedTimerColumn column)
        {
            float startY = column.Location.Y - column.ScrollOffset;

            for (int i = 0; i < column.TimerSegments.Count; i++)
            {
                var segment = column.TimerSegments[i];
                float newY = startY + (i * AnimatedColumnSettings.SegmentHeight);

                segment.UpdatePosition(column.Location.X, newY);
            }
        }


    }
}
