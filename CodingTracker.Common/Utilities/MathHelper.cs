// Ignore Spelling: Lerp


namespace CodingTracker.Common.Utilities
{
    public static class AnimationMathHelper
    {
        public static float CalculateLerp(float startValue, float endValue, float progressFraction)
        {
            return startValue + (endValue - startValue) * progressFraction;
        }
    }
}
