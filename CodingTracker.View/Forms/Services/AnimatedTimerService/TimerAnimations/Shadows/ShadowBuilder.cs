using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows
{
    public interface IShadowBuilder
    {
        void DrawColumnLeftShadow(SKCanvas canvas, SKRect rect, ShadowIntensity intensity = ShadowIntensity.Normal);
        void DrawColumnRightShadow(SKCanvas canvas, SKRect rect, ShadowIntensity intensity = ShadowIntensity.Normal);
        SKRect CreateRectangleForShadow(AnimatedTimerColumn column);
    }

    public enum ShadowIntensity
    {
        Subtle,
        Normal,
        Strong
    }



    public class ShadowBuilder : IShadowBuilder
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IPathBuilder _pathBuilder;
        private readonly IPaintManager _paintManager;


        public ShadowBuilder(IApplicationLogger appLogger, IPathBuilder pathBuilder, IPaintManager paintManager)
        {
            _appLogger = appLogger;
            _pathBuilder = pathBuilder;
            _paintManager = paintManager;
        }




        public void AdjustRectangleLocationForShadow(SKRect baseRectangle)
        {
            baseRectangle.Inflate(2,2);
        }



        public void DrawColumnLeftShadow(SKCanvas canvas, SKRect baseRectangle, ShadowIntensity intensity = ShadowIntensity.Normal)
        {
            canvas.Save();

            using(var leftShadowPaint = _paintManager.CreateTopLeftLightShadowPaint(intensity, AnimatedColumnSettings.ColumnTopLeftShadow))
            {
                float shadowOffset = AnimatedColumnSettings.ColumnElevationHeight;
                baseRectangle.Offset(-shadowOffset, -shadowOffset);
                baseRectangle.Inflate(-2, -2);
                canvas.DrawRect(baseRectangle, leftShadowPaint);
            }
            canvas.Restore();
        }


        public void DrawColumnRightShadow(SKCanvas canvas, SKRect baseRectangle, ShadowIntensity intensity = ShadowIntensity.Normal)
        {
            canvas.Save();


            using (var rightShadowPaint = _paintManager.CreateRightSideDarkShadowPaint(intensity, AnimatedColumnSettings.ColumnBottomRightShadow))
            {
                float shadowOffset = AnimatedColumnSettings.ColumnElevationHeight;

                baseRectangle.Offset(+shadowOffset, +shadowOffset);
                baseRectangle.Inflate(2, 2);
                canvas.DrawRect(baseRectangle, rightShadowPaint);
            }
            canvas.Restore();
        }


        // This should only create a rect the size of the segment width and height
        public SKRect CreateRectangleForShadow(AnimatedTimerColumn column)
        {

            // Needs to be changed to be taken from column
            float newY = column.Location.Y - column.YTranslation;


            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            return SKRect.Create(newLocation, rectangleSize);
        }


    }
}

