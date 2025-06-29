using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders
{
    public interface IPathBuilder
    {
        void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath, TimeSpan elapsed);
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

        private SKPath CreateCirclePath(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            SKPath circlePath = new SKPath();

            float centerX = column.FocusedSegment.Location.X + (column.FocusedSegment.SegmentWidth / 2f);
            float centerY = column.FocusedSegment.Location.Y + (column.FocusedSegment.SegmentHeight / 2f);

            float normalizedRadius = _segmentOverlayCalculator.CalculateCircleAnimationRadius(column, elapsed);


            circlePath.AddCircle(centerX, centerY, normalizedRadius);

            _appLogger.Debug($"Circle path added for X:{centerX} Y:({centerY} radius: {normalizedRadius})");

            _appLogger.Debug($" CreateCirclePath Inner path innerBounds: {circlePath.Bounds}");

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


            _appLogger.Debug($"Rectangle: W:{columnRectangle.Width} H: ({columnRectangle.Height}, Location: {columnRectangle.Location}).");

            _appLogger.Debug($"CreateRectanglePathInner path innerBounds: {rectanglePath.Bounds}");

            return rectanglePath;

        }



        public void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath, TimeSpan elapsed)
        {
            SKPath rectanglePath = CreateRectanglePath(column);
            SKPath circlePath = CreateCirclePath(column, elapsed);

            outerOverlayPath = new SKPath();
            innerSegmentPath = new SKPath();

            circlePath.Op(rectanglePath, SKPathOp.Difference, outerOverlayPath);
            circlePath.Op(rectanglePath, SKPathOp.Intersect, innerSegmentPath);

            _appLogger.Debug($"CreateTimerPaths Inner path bounds: {innerSegmentPath.Bounds}");
            _appLogger.Debug($"CreateTimerPaths Outer path bounds: {outerOverlayPath.Bounds}");
        }





    }
}
