// Ignore Spelling: Lerp


namespace CodingTracker.Common.Utilities
{
    public static class AnimationMathHelper
    {
        public static float CalculateLerp(float startValue, float endValue, float progressFraction)
        {
            return startValue + (endValue - startValue) * progressFraction;
        }

        public static float CalculateEasingValue(float t)
        {
            return t < 0.5f
                ? 4f * t * t * t
                : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

        }
    }
}
