using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Security.RightsManagement;

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
        private readonly IAnimatedColumnStateManager _columnStateManager;


        public PathBuilder(IApplicationLogger appLogger, IAnimatedColumnStateManager columnStateManager)
        {
            _appLogger = appLogger;
            _columnStateManager = columnStateManager;
        }

        private  SKPath CreateCirclePath(AnimatedTimerColumn column, TimeSpan elapsed, bool isCircleStatic)
        {
            SKPath circlePath = new SKPath();

            float circleRadius;


            /*
            if (isCircleStatic)
            {
                circleRadius = AnimatedColumnSettings.MaxRadius;
            }
            */

            float easingValue = _columnStateManager.WORKINGCalculateEasingValue(column, TimerAnimationType.CircleAnimation);
            circleRadius = _columnStateManager.CalculateCircleAnimationRadius(column, elapsed);

     


            float halfX = AnimatedColumnSettings.ColumnWidth / 2;
            float halfY = AnimatedColumnSettings.SegmentHeight / 2;
            float centerX = column.InitialLocation.X + halfX;
            float centerY = column.InitialLocation.Y + halfY;

            circlePath.AddCircle(centerX, centerY, circleRadius);
            return circlePath;

        }


        private SKRect CreateSKRectangle(AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.YTranslation;


            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            return SKRect.Create(newLocation, rectangleSize);
        }

        public SKPath CreateRectanglePath(AnimatedTimerColumn column)
        {
            SKRect columnRectangle = CreateSKRectangle(column);

            SKPath rectanglePath = new SKPath();

            rectanglePath.AddRect(columnRectangle);



            return rectanglePath;

        }



        public void CreateTimerPaths(AnimatedTimerColumn column, out SKPath innerSegmentPath, out SKPath outerOverlayPath, TimeSpan elapsed, bool isCircleStatic)
        {
            SKPath rectanglePath = CreateRectanglePath(column);
            SKPath circlePath = CreateCirclePath(column, elapsed, isCircleStatic);

            outerOverlayPath = new SKPath();
            innerSegmentPath = new SKPath();




            // Inner path: The circular window that reveals the highlighted segment, intersection of circle and column rectangle.
            circlePath.Op(rectanglePath, SKPathOp.Difference, outerOverlayPath);

            // Outer path: The expanding circle outside the column bounds, circle minus the column rectangle.
            circlePath.Op(rectanglePath, SKPathOp.Intersect, innerSegmentPath);

        }



       




    }
}
