using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders
{
    public interface ICirclePathBuilder
    {
        SKPath CreateCirclePath(AnimatedTimerColumn column);
    }

    public class CirclePathBuilder : ICirclePathBuilder
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalculator;


        public CirclePathBuilder(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverLayCalculator)
        {
            _appLogger = appLogger;
            _segmentOverlayCalculator = segmentOverLayCalculator;
        }

        public SKPath CreateCirclePath(AnimatedTimerColumn column)
        {
            SKPath circlePath = new SKPath();

            float circleLocationX = column.FocusedSegment.Location.X;
            float circleLocationY = column.FocusedSegment.Location.Y;

            float normalizedRadius = _segmentOverlayCalculator.CalculateNormalizedRadius(column);


            circlePath.AddCircle(circleLocationX, circleLocationY, normalizedRadius);

            return circlePath;  
            
        }





    }
}
