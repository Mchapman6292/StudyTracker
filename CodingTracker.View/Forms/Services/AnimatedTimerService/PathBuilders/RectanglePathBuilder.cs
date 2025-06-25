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
    public class RectanglePathBuilder
    {

        private readonly IApplicationLogger _appLogger;



        public RectanglePathBuilder(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }


        private SKRect CreateSKRectangle(AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.ScrollOffset;
            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            return SKRect.Create(newLocation, rectangleSize);
        }

        public SKPath CreateRectPath(AnimatedTimerColumn column)
        {
            SKRect columnRectangle = CreateSKRectangle(column);

            SKPath rectPath = new SKPath();

            rectPath.AddRect(columnRectangle);

            return rectPath;

        }



        private void DrawRectanglePath(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.ScrollOffset;


            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            if (newLocation.X != column.Location.X || newLocation.Y != column.Location.Y)
            {
                _appLogger.Fatal($"newLocation and columnLocation not matching  X: {newLocation.X} Y: {newLocation.Y} \n columnLocation X: {column.Location.X}, Y: {column.Location.Y}.");
            }



            SKRect columnRectangle = SKRect.Create(newLocation, rectangleSize);
            

            SKPath rectPath = new SKPath();

            rectPath.AddRect(columnRectangle);

            _appLogger.Debug($"Drawing column of size {rectangleSize.Width}  {rectangleSize.Height} at X:{newLocation.X}, Y:{newLocation.Y}.");


            using (var rectPaint = new SKPaint())
            {
                rectPaint.Color = AnimatedColumnSettings.MainPageFadedColor;
                rectPaint.Style = SKPaintStyle.Fill;
                rectPaint.IsAntialias = true;

                canvas.DrawPath(rectPath, rectPaint);
            }
        }

    }
}
