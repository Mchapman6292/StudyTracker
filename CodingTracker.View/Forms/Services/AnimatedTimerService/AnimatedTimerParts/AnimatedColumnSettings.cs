using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public static class AnimatedColumnSettings
    {
        // TODO Change/review why two different values
        public const float ColumnWidth = 5f;

        public const float SegmentWidth = 20f;

        public const float SegmentHeight = 40f;

        public const float TextSize = 30f;

        //TODO review if needed.
        public const float AnimationSpeed = 1.5f;

        public static readonly int[] OneToNineDigit = { 0,1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static readonly int[] OneToSixDigit = { 0 ,1, 2, 3, 4, 5, 6 };


        public static readonly TimeSpan AnimationDuration = TimeSpan.FromMilliseconds(300);

        public static SKColor ColumnColor = new SKColor(44, 45, 65);
        public static SKColor SegmentColor = SKColors.White;




        private const float _circlePaddingMultiplier = 1.1f;
        public const float minRadiusScale = 0.5f;

        public static float BaseRadius
        {
            get
            {
                float halfWidth = SegmentWidth / 2f;
                float halfHeight = SegmentHeight / 2f;
                float diagonal = (float)Math.Sqrt((halfWidth * halfWidth) + (halfHeight * halfHeight));
                return diagonal * _circlePaddingMultiplier;
            }
        }





        public static readonly Dictionary<ColumnUnitType, TimeSpan> UnitTypesToAnimationDurations = new Dictionary<ColumnUnitType, TimeSpan>
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
            { ColumnUnitType.SecondsSingleDigits, TimeSpan.FromSeconds(1) },
            { ColumnUnitType.SecondsLeadingDigit, TimeSpan.FromSeconds(5) },
            {ColumnUnitType.MinutesSingleDigits, TimeSpan.FromSeconds(10) },
            { ColumnUnitType.MinutesLeadingDigits, TimeSpan.FromSeconds(15) },
            {ColumnUnitType.HoursSinglesDigits, TimeSpan.FromSeconds(20) },
            {ColumnUnitType.HoursLeadingDigits, TimeSpan.FromSeconds(25) }

        };
       


    }
}
