using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
{
    public interface IAnimatedTimerColumnFactory
    {
        AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType timeUnit);
        List<AnimatedTimerColumn> CreateColumnsWithPositions(float xPosition, float yPosition);

        List<AnimatedTimerColumn> TESTCreateColumnsWithPositions(float xPosition, float yPosition, TimerPlaceHolderForm timerPlaceHolderForm);
    }




    public class AnimatedTimerColumnFactory : IAnimatedTimerColumnFactory
    {

        private const float _columnSpacing = 100;
        private const float _spacingBetweenMatchingColumns = 60;
        private readonly IApplicationLogger _appLogger;

        public AnimatedTimerColumnFactory(IApplicationLogger appLogger) 
        {
            _appLogger = appLogger;
        }

        public AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType columnType)
        {
     
            var segments = new List<AnimatedTimerSegment>();
            for(int newSegment = 0; newSegment < segmentNumbers.Length; newSegment++)
            {
                float yPosition = location.Y + (newSegment * AnimatedColumnSettings.SegmentHeight);
                SKPoint segmentLocation = new SKPoint(location.X, yPosition);

                segments.Add(new AnimatedTimerSegment(segmentNumbers[newSegment], segmentLocation));

      
            }
            AnimatedTimerColumn newColumn = new AnimatedTimerColumn(segments, location, columnType);
            newColumn.TimerSegments = segments;
            return newColumn;
        }



        public List<AnimatedTimerColumn> CreateColumnsWithPositions(float xPosition, float yPosition)
        {
            List<AnimatedTimerColumn> columns = new List<AnimatedTimerColumn>();

            AnimatedTimerColumn hoursLeadingDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToTwoDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.HoursLeadingDigits);
            xPosition += _spacingBetweenMatchingColumns;

            AnimatedTimerColumn hoursSinlgeDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.HoursSinglesDigits);
            xPosition += _columnSpacing;

            AnimatedTimerColumn minutesLeadingDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToSixDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.MinutesLeadingDigits);
            xPosition += _spacingBetweenMatchingColumns;

            AnimatedTimerColumn minutesSingleDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.MinutesSingleDigits);
            xPosition += _columnSpacing;
            
      

            AnimatedTimerColumn secondsLeadingDigit = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToSixDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.SecondsLeadingDigit);
            xPosition += _spacingBetweenMatchingColumns;


   
            AnimatedTimerColumn secondsSingleDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.SecondsSingleDigits);
            xPosition += _columnSpacing;

            _appLogger.Debug($"SecondsSingleDigits initialized at Y: {secondsLeadingDigit.InitialLocation.Y}.");
            


            columns.Add(hoursLeadingDigits);
            columns.Add(hoursSinlgeDigits);
            columns.Add(minutesLeadingDigits);
            columns.Add(minutesSingleDigits);


            columns.Add(secondsSingleDigits);
            
      
            columns.Add(secondsLeadingDigit);
  

            return columns;


        }




        public List<AnimatedTimerColumn> TESTCreateColumnsWithPositions(float xPosition, float yPosition, TimerPlaceHolderForm timerPlaceHolderForm)
        {

            Dictionary<ColumnUnitType, SKPoint> placeHolderColumnLocations = timerPlaceHolderForm.GetPlaceHolderColumnLocations();

            List<AnimatedTimerColumn> columns = new List<AnimatedTimerColumn>();

            AnimatedTimerColumn hoursLeadingDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToTwoDigit, placeHolderColumnLocations[ColumnUnitType.HoursLeadingDigits], ColumnUnitType.HoursLeadingDigits);
            xPosition += _spacingBetweenMatchingColumns;

            AnimatedTimerColumn hoursSinlgeDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, placeHolderColumnLocations[ColumnUnitType.HoursSinglesDigits], ColumnUnitType.HoursSinglesDigits);
            xPosition += _columnSpacing;

            AnimatedTimerColumn minutesLeadingDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToSixDigit, placeHolderColumnLocations[ColumnUnitType.MinutesLeadingDigits], ColumnUnitType.MinutesLeadingDigits);
            xPosition += _spacingBetweenMatchingColumns;

            AnimatedTimerColumn minutesSingleDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, placeHolderColumnLocations[ColumnUnitType.MinutesSingleDigits], ColumnUnitType.MinutesSingleDigits);
            xPosition += _columnSpacing;



            AnimatedTimerColumn secondsLeadingDigit = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToSixDigit, placeHolderColumnLocations[ColumnUnitType.SecondsLeadingDigit], ColumnUnitType.SecondsLeadingDigit);
            xPosition += _spacingBetweenMatchingColumns;



            AnimatedTimerColumn secondsSingleDigits = CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, placeHolderColumnLocations[ColumnUnitType.SecondsSingleDigits], ColumnUnitType.SecondsSingleDigits);
            xPosition += _columnSpacing;

            _appLogger.Debug($"SecondsSingleDigits initialized at Y: {secondsLeadingDigit.InitialLocation.Y}.");



            columns.Add(hoursLeadingDigits);
            columns.Add(hoursSinlgeDigits);
            columns.Add(minutesLeadingDigits);
            columns.Add(minutesSingleDigits);


            columns.Add(secondsSingleDigits);


            columns.Add(secondsLeadingDigit);


            return columns;


        }

    }
}


