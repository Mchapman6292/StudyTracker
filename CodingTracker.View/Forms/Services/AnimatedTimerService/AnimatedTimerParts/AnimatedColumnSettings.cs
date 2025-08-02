using SkiaSharp;
using Windows.ApplicationModel.Background;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public static class AnimatedColumnSettings
    {
        // TODO Change/review why two different values
        public const float ColumnWidth = 25f;
        public const float SegmentWidth = 25f;
        public const float SegmentHeight = 30f;
        public const float TextSize = 20f;

        public const float StartingXPosition = 250f;

        public const float RoundRectangleRadius = 25;


        public static readonly int[] ZeroToTwoDigit = { 0, 1, 2 };
        public static readonly int[] ZeroToFourDigit = { 0, 1, 2, 3, 4};
        public static readonly int[] ZeroToNineDigit = { 0,1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static readonly int[] ZeroToSixDigit = { 0 ,1, 2, 3, 4, 5, 6 };

        public static readonly float ScrollAnimationDurationRatio = 1.0f;
        public static readonly TimeSpan ScrollAnimationTimespan = TimeSpan.FromSeconds(ScrollAnimationDurationRatio);
    

        // Circle animation is set to 70% of scroll duration
        public static readonly float CircleAnimationDurationRatio = ScrollAnimationDurationRatio * 0.6f;
        public static readonly TimeSpan CircleAnimationTimespan = TimeSpan.FromSeconds(CircleAnimationDurationRatio);


        public static SKColor ColumnColor = new SKColor(35, 34, 50);

        public static SKColor SegmentColor = new SKColor(35, 34, 50);

        public static SKColor MainPageFadedColor = new SKColor(44, 45, 65);



        private const float _circlePaddingMultiplier = 1.2f;
        public const float minRadiusScale = 0.4f;

        public const float NumberHighlightActivationThreshold = 0.6f;


        public static readonly float MinRadius = CalculateMinRadius();

        public static readonly float MaxRadius = CalculateMaxRadius();



        public const byte OuterCircleOpacity = 64;


        public static SKColor FormBackgroundColor = new SKColor(26, 26, 46);
        public static SKColor TESTColumnColor = new SKColor(49, 50, 68);

        public static SKColor MainPageDarkerColor = new SKColor(35, 34, 50);





        public static SKColor TestHotPink = SKColors.HotPink;

        public static SKColor InactiveColumnColor = SKColors.Gray;




        // Base color RGB(35, 34, 50) = #232232
        public static SKColor ElevationBase = new SKColor(35, 34, 50);         // 0dp - Base background
        public static SKColor Elevation1dp = new SKColor(46, 45, 66);          // 1dp - ~4% lighter
        public static SKColor Elevation3dp = new SKColor(52, 51, 74);          // 3dp - ~6% lighter
        public static SKColor Elevation6dp = new SKColor(58, 57, 82);          // 6dp - ~8% lighter
        public static SKColor Elevation8dp = new SKColor(64, 62, 90);          // 8dp - ~10% lighter
        public static SKColor Elevation12dp = new SKColor(70, 68, 98);         // 12dp - ~12% lighter
        public static SKColor Elevation16dp = new SKColor(76, 74, 106);        // 16dp - ~14% lighter
        public static SKColor Elevation24dp = new SKColor(82, 80, 114);        // 24dp - ~16% lighter

        // Shadow properties
        public static readonly SKColor ShadowColor = new SKColor(0, 0, 0, 64); // Black with 25% opacity
        public static readonly float[] ShadowBlurRadii = new float[]
        {
            0f,    // 0dp
            5f,    // 1dp
            8f,    // 3dp
            10f,   // 6dp
            12f,   // 8dp
            14f,   // 12dp
            16f,   // 16dp
            20f    // 24dp
        };








        // Column Shadow Colors
        public static SKColor ColumnTopLeftShadow = new SKColor(50, 49, 65, 80);      // Highlight/rim light effect
        public static SKColor ColumnBottomRightShadow = new SKColor(0, 0, 0, 100);    // Drop shadow for elevation

        // Column Shadow Geometry
        public const float ColumnElevationHeight = 6f;                                // How high column appears above background
        public const float TopLeftShadowBlurRadius = 6f;                             // Blur amount for highlight edge
        public const float BottomRightShadowBlurRadius = 6f;                         // Blur amount for drop shadow

        // Alternative Shadow Intensities
        public static SKColor ColumnTopLeftShadowWeak = new SKColor(45, 44, 60, 60);      // Less pronounced highlight
        public static SKColor ColumnTopLeftShadowIntense = new SKColor(60, 59, 75, 100);  // More pronounced highlight
        public static SKColor ColumnDropShadowWeak = new SKColor(0, 0, 0, 80);            // Subtle elevation effect
        public static SKColor ColumnDropShadowIntense = new SKColor(0, 0, 0, 150);






        public static readonly SKColor NeonGlowMiddle = new SKColor(203, 166, 247);
        public static readonly SKColor NeonGlowOuter = new SKColor(137, 180, 250);
        public static readonly SKColor NeonGlowCore = new SKColor(245, 194, 231);



        public static SKColor MainPageSoftPinkTextColor = new SKColor(255, 200, 230);




        public static float CalculateMinRadius()
        {
            float halfWidth = SegmentWidth / 2f;
            float halfHeight = SegmentHeight / 2f;
            float diagonal = (float)Math.Sqrt((halfWidth * halfWidth) + (halfHeight * halfHeight));
            return diagonal * minRadiusScale;
        }


        public static float CalculateMaxRadius()
        {
            float halfWidth = SegmentWidth / 2f;
            float halfHeight = SegmentHeight / 2f;
            float diagonal = (float)Math.Sqrt((halfWidth * halfWidth) + (halfHeight * halfHeight));
            return diagonal * _circlePaddingMultiplier;
 
        }

        public static readonly Dictionary<ColumnUnitType, TimeSpan> UnitTypesToAnimationTimeSpans = new Dictionary<ColumnUnitType, TimeSpan>
        {
            { ColumnUnitType.SecondsSingleDigits, TimeSpan.FromSeconds(1) },
            { ColumnUnitType.SecondsLeadingDigit, TimeSpan.FromSeconds(10) },
            {ColumnUnitType.MinutesSingleDigits, TimeSpan.FromSeconds(60) },
            { ColumnUnitType.MinutesLeadingDigits, TimeSpan.FromSeconds(600) },
            {ColumnUnitType.HoursSinglesDigits, TimeSpan.FromSeconds(3600) },
            {ColumnUnitType.HoursLeadingDigits, TimeSpan.FromSeconds(36000) }

        };




        public static readonly Dictionary<ColumnUnitType, TimeSpan> TESTUnitTypesToAnimationDurations = new Dictionary<ColumnUnitType, TimeSpan>
        {
            { ColumnUnitType.SecondsSingleDigits, TimeSpan.FromSeconds(0) },
            { ColumnUnitType.SecondsLeadingDigit, TimeSpan.FromSeconds(5) },
            {ColumnUnitType.MinutesSingleDigits, TimeSpan.FromSeconds(10) },
            { ColumnUnitType.MinutesLeadingDigits, TimeSpan.FromSeconds(15) },
            {ColumnUnitType.HoursSinglesDigits, TimeSpan.FromSeconds(20) },
            {ColumnUnitType.HoursLeadingDigits, TimeSpan.FromSeconds(25) }

        };
       


    }
}
