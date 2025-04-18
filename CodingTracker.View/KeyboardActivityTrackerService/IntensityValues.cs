using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.KeyboardActivityTrackerService
{
    // These represent the threshholds 
    public static class IntensityValues
    {
        // Build up rates.
        public const float SlowBuildUp = 0.05f;
        public const float MediumBuildUp = 0.08f;
        public const float FastBuildUp = 0.12f;

        // Decay rates.
        public const float SlowDecay = 0.02f;
        public const float MediumDecay = 0.04f;
        public const float FastDecay = 0.06f;


    }
}
