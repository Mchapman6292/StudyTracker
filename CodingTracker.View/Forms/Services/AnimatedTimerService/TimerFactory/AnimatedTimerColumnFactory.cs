using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
{
    public interface IAnimatedTimerColumnFactory
    {
        AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType timeUnit);
        List<AnimatedTimerColumn> CreateColumnsWithPositions(float xPosition, float yPosition);
    }




    public class AnimatedTimerColumnFactory : IAnimatedTimerColumnFactory
    {

        private const float _columnSpacing = 100;

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

 
            AnimatedTimerColumn hoursLeadingDigits = CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.HoursLeadingDigits);
            xPosition += _columnSpacing;

            AnimatedTimerColumn hoursSinlgeDigits = CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.HoursSinglesDigits);
            xPosition += _columnSpacing;

            AnimatedTimerColumn minutesLeadingDigits = CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.MinutesLeadingDigits);
            xPosition += _columnSpacing;

            AnimatedTimerColumn minutesSingleDigits = CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.MinutesSingleDigits);
            xPosition += _columnSpacing;
            
      

            AnimatedTimerColumn secondsLeadingDigit = CreateColumnWithSegments(AnimatedColumnSettings.OneToSixDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.SecondsLeadingDigit);
            xPosition += _columnSpacing;


   
            AnimatedTimerColumn secondsSingleDigits = CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, yPosition)), ColumnUnitType.SecondsSingleDigits);
            xPosition += _columnSpacing;
            


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


