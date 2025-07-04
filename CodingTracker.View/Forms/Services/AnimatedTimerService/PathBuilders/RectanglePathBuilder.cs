using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders
{
    public interface IRectanglePathBuilder
    {
        SKPath CreateRectanglePath(AnimatedTimerColumn column);
    }
    public class RectanglePathBuilder : IRectanglePathBuilder
    {

        private readonly IApplicationLogger _appLogger;



        public RectanglePathBuilder(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }


        private SKRect CreateSKRectangle(AnimatedTimerColumn column)
        {
            float newY = column.CurrentLocation.Y - column.ScrollOffset;
            SKPoint newLocation = new SKPoint(column.CurrentLocation.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            return SKRect.Create(newLocation, rectangleSize);
        }

        public SKPath CreateRectanglePath(AnimatedTimerColumn column)
        {
            SKRect columnRectangle = CreateSKRectangle(column);

            SKPath rectPath = new SKPath();

            rectPath.AddRect(columnRectangle);

            return rectPath;

        }




    }
}
