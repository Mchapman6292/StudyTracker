using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

    }

    public class AnimatedColumnStateManager : IAnimatedColumnStateManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalulcator;


        public AnimatedColumnStateManager(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverlayCalulcator)
        {
            _appLogger = appLogger;
            _segmentOverlayCalulcator = segmentOverlayCalulcator;
        }



        private bool IsWithinAnimationInterval(AnimatedTimerColumn column ,TimeSpan elapsed)
        {
            double secondsUntilChange = column.AnimationInterval.TotalSeconds - (elapsed.TotalSeconds / column.AnimationInterval.TotalSeconds);
            return secondsUntilChange <= 1.0;

        }


        public void UpdateNextTransitionTime(AnimatedTimerColumn column)
        {
            column.NextTransitionTime += column.AnimationInterval;
        }
    }
}
