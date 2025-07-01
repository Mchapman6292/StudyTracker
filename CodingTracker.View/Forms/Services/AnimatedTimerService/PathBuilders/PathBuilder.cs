using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using System.Security.RightsManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders
{
    public interface IPathBuilder
    {
        void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath, TimeSpan elapsed, bool isCircleStatic);
        SKPath CreateRectanglePath(AnimatedTimerColumn column);
    }

    public class PathBuilder : IPathBuilder
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalculator;
        private readonly IAnimatedColumnStateManager _columnStateManager;


        public PathBuilder(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverLayCalculator, IAnimatedColumnStateManager columnStateManager)
        {
            _appLogger = appLogger;
            _segmentOverlayCalculator = segmentOverLayCalculator;
            _columnStateManager = columnStateManager;
        }

        private  SKPath CreateCirclePath(AnimatedTimerColumn column, TimeSpan elapsed, bool isCircleStatic)
        {
            SKPath circlePath = new SKPath();

            float circleRadius;

            if (isCircleStatic)
            {
                circleRadius = AnimatedColumnSettings.MaxRadius;
            }
            else
            {
                circleRadius = _columnStateManager.CalculateCircleAnimationRadius(column, elapsed);
            }

             float centerX = column.FocusedSegment.Location.X + (column.FocusedSegment.SegmentWidth / 2f);
            float centerY = column.FocusedSegment.Location.Y + (column.FocusedSegment.SegmentHeight / 2f);



            circlePath.AddCircle(centerX, centerY, circleRadius);



            return circlePath;
            
        }


        private SKRect CreateSKRectangle(AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.ScrollOffset;


            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            return SKRect.Create(newLocation, rectangleSize);
        }

        public SKPath CreateRectanglePath(AnimatedTimerColumn column)
        {
            SKRect columnRectangle = CreateSKRectangle(column);

            SKPath rectanglePath = new SKPath();

            rectanglePath.AddRect(columnRectangle);


            _appLogger.Debug($"Rectangle: W:{columnRectangle.Width} H: ({columnRectangle.Height}, Location: {columnRectangle.Location}).");

            _appLogger.Debug($"CreateRectanglePathInner path innerBounds: {rectanglePath.Bounds}");

            return rectanglePath;

        }



        public void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath, TimeSpan elapsed, bool isCircleStatic)
        {
            SKPath rectanglePath = CreateRectanglePath(column);
            SKPath circlePath = CreateCirclePath(column, elapsed, isCircleStatic);

            outerOverlayPath = new SKPath();
            innerSegmentPath = new SKPath();


            circlePath.Op(rectanglePath, SKPathOp.Difference, innerSegmentPath);
            circlePath.Op(rectanglePath, SKPathOp.Intersect, outerOverlayPath);

            _appLogger.Debug($"CreateTimerPaths Inner path bounds: {innerSegmentPath.Bounds}");
            _appLogger.Debug($"CreateTimerPaths Outer path bounds: {outerOverlayPath.Bounds}");
        }



       




    }
}
