using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public class AnimatedCircleOverlay
    {
        public AnimatedTimerColumn Column { get; set; }

        public float CurrentOpacity { get; private set; }
        public float CurrentRadius { get; private set; }
        public float AnimationProgress { get; private set; }
        public float NormalizedAnimationProgress { get; private set; }

        // Animation state
        public bool IsActive { get; private set; }
        public TimeSpan AnimationStartTime { get; private set; }


        // Base values for calculations
        private const float _baseRadius = 20f;
        private const float _minRadiusScale = 0.5f; 
    }

}

