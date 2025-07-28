// Ignore Spelling: Validators Validator

using AxWMPLib;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.Validators
{
    public interface ITimerValidator
    {
        void HandleFirstAnimation(AnimatedTimerColumn column, TimeSpan elapsed);
    }
    public class TimerValidator : ITimerValidator
    {

        private readonly IApplicationLogger appLogger;
        private readonly IAnimationCalculator _animationCalculator;
        private readonly IAnimatedColumnStateManager _columnStateManager;


        public TimerValidator(IApplicationLogger appLogger, IAnimationCalculator animationCalculator, IAnimatedColumnStateManager columnStateManager)
        {
            this.appLogger = appLogger;
            _animationCalculator = animationCalculator;
            _columnStateManager = columnStateManager;
        }


        public int ValidateCurrentValueForStandardAnimation(AnimatedTimerColumn column, int currentValue, TimeSpan elapsed)
        {
            if (elapsed > TimeSpan.FromSeconds(0) && elapsed < TimeSpan.FromSeconds(1))
            {
                return 1;
            }
            return currentValue;
        }


        public void HandleFirstAnimation(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            if (elapsed > TimeSpan.FromSeconds(0) && elapsed < TimeSpan.FromSeconds(1))
            {
                _columnStateManager.UpdateColumnCurrentValue(column, 0, elapsed);
                _columnStateManager.UpdateTargetSegmentValue(column, 1, elapsed);
                _columnStateManager.SetFocusedSegmentByColumnTargetValue(column, 1);
            }
        }


    }
}
