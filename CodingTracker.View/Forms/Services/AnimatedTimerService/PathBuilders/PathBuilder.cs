using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders
{
    public interface IPathBuilder
    {
        void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath);
    }

    public class PathBuilder : IPathBuilder
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalculator;


        public PathBuilder(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverLayCalculator)
        {
            _appLogger = appLogger;
            _segmentOverlayCalculator = segmentOverLayCalculator;
        }

        private SKPath CreateCirclePath(AnimatedTimerColumn column)
        {
            SKPath circlePath = new SKPath();

            float circleLocationX = column.FocusedSegment.Location.X;
            float circleLocationY = column.FocusedSegment.Location.Y;

            float normalizedRadius = _segmentOverlayCalculator.CalculateNormalizedRadius(column);


            circlePath.AddCircle(circleLocationX, circleLocationY, normalizedRadius);

            return circlePath;  
            
        }


        private SKRect CreateSKRectangle(AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.ScrollOffset;
            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            return SKRect.Create(newLocation, rectangleSize);
        }

        private SKPath CreateRectanglePath(AnimatedTimerColumn column)
        {
            SKRect columnRectangle = CreateSKRectangle(column);

            SKPath rectanglePath = new SKPath();

            rectanglePath.AddRect(columnRectangle);

            return rectanglePath;

        }



        public void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath)
        {

            SKPath rectanglePath = CreateRectanglePath(column);
            SKPath circlePath = CreateCirclePath(column);
            

            // Creates a new path containing only the parts of the circle that dont touch the rectangle.
            outerOverlayPath = new SKPath();
            outerOverlayPath.Op(circlePath, SKPathOp.Difference, rectanglePath);


            // Define the area where the circle and rectangle overlap as a new path.
            innerSegmentPath = new SKPath();
            innerSegmentPath.Op(circlePath, SKPathOp.Intersect, rectanglePath);


  

        }   





    }
}
