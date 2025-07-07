using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public static class AnimatedColumnSettings
    {
        // TODO Change/review why two different values
        public const float ColumnWidth = 20f;
        public const float SegmentWidth = 20f;
        public const float SegmentHeight = 40f;
        public const float TextSize = 15f;


        public static readonly int[] OneToNineDigit = { 0,1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static readonly int[] OneToSixDigit = { 0 ,1, 2, 3, 4, 5, 6 };

        public static readonly float ScrollAnimationDuration = 0.7f;
        public static readonly TimeSpan ScrollAnimationTimespan = TimeSpan.FromMilliseconds(700);
        public static readonly float ScrollAnimationDurationSeconds = (float)ScrollAnimationTimespan.TotalSeconds; // 0.7f

        // Circle animation is set to 70% of scroll duration
        public static readonly float CircleAnimationRatio = 0.7f;
        public static readonly TimeSpan CircleAnimationTimespan = ScrollAnimationTimespan * CircleAnimationRatio; // 490ms
        public static readonly float CircleAnimationDurationSeconds = (float)CircleAnimationTimespan.TotalSeconds; 


        public static SKColor ColumnColor = new SKColor(35, 34, 50);

        public static SKColor SegmentColor = new SKColor(35, 34, 50);

        public static SKColor MainPageFadedColor = new SKColor(44, 45, 65);



        private const float _circlePaddingMultiplier = 1.0f;
        public const float minRadiusScale = 0.5f;


        public static readonly float MinRadius = CalculateMinRadius();

        public static readonly float MaxRadius = CalculateMaxRadius();



        public const byte OuterCircleOpacity = 64;


        public static SKColor FormBackgroundColor = new SKColor(35, 34, 50);




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
            { ColumnUnitType.SecondsSingleDigits, TimeSpan.FromSeconds(1) - ScrollAnimationTimespan },
            { ColumnUnitType.SecondsLeadingDigit, TimeSpan.FromSeconds(10) - ScrollAnimationTimespan },
            { ColumnUnitType.MinutesSingleDigits, TimeSpan.FromSeconds(60) - ScrollAnimationTimespan },
            { ColumnUnitType.MinutesLeadingDigits, TimeSpan.FromSeconds(600) - ScrollAnimationTimespan },
            { ColumnUnitType.HoursSinglesDigits, TimeSpan.FromSeconds(3600) - ScrollAnimationTimespan },
            { ColumnUnitType.HoursLeadingDigits, TimeSpan.FromSeconds(36000) - ScrollAnimationTimespan }
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
