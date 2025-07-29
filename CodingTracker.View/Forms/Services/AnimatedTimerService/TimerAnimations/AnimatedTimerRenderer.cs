using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawRoundedColumn(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection);
        void DrawColumnNumbers(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection);
        bool TimeToBeginStandardAnimation(TimeSpan elapsed, AnimatedTimerColumn column);

        void DrawRestartAnimationForAllColumns(SKCanvas canvas, TimeSpan restartTimerElapsed, List<AnimatedTimerColumn> columns, float restartAnimationProgress);



    }


    public enum ColumnAnimationType
    {
        Restart,
        Animating,
        Static
    }


    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IPaintManager _paintManager;
        private readonly IAnimatedColumnStateManager _columnStateManager;
        private readonly IAnimationCalculator _animationCalculator;
        private readonly IAnimatedLogHelper _animatedLogHelper;


        public AnimatedTimerRenderer(IApplicationLogger appLogger,  IPaintManager circleHighlight, IAnimatedColumnStateManager columnStateManager, IAnimationCalculator renderingCalculator, IAnimatedLogHelper animatedLogHelper)
        {
            _appLogger = appLogger;
            _paintManager = circleHighlight;
            _columnStateManager = columnStateManager;
            _animationCalculator = renderingCalculator;
            _animatedLogHelper = animatedLogHelper;
        }




        public void DrawRoundedColumn(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection)
        {
            SKSize rectangleSize = new SKSize(column.Width, column.Height);
            SKRect columnRectangle = SKRect.Create(column.Location, rectangleSize);


            SKRoundRect roundRect = new SKRoundRect(columnRectangle, AnimatedColumnSettings.RoundRectangleRadius);

            canvas.Save();

            if (animationDirection == ColumnAnimationSetting.IsMovingUp)
            {
                canvas.Translate(0, -column.YTranslation);
            }

            if (animationDirection == ColumnAnimationSetting.IsMovingDown)
            {
                canvas.Translate(0, +column.YTranslation);
            }

            using (var rectPaint = _paintManager.CreateColumnPaint(column))
            {
                canvas.DrawRoundRect(roundRect, rectPaint);
            }

            canvas.Restore();
        }




        public void DrawColumnNumbers(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection)
        {
            using (var nonFocusedNumberPaint = _paintManager.CreateActiveNumberPaintAndGradient(column))
            using (var font = _paintManager.CreateNumberFont())
            using (var focusedNumberPaint = _paintManager.TESTCreateFocusedNumberPaintAndGradient(column))

            {
                canvas.Save();

                for (int currentSegmentIndex = 0; currentSegmentIndex < column.TotalSegmentCount; currentSegmentIndex++)
                {
                    var segment = column.TimerSegments[currentSegmentIndex];
                    float newY = segment.LocationCenterPoint.Y - column.YTranslation;

                    if (segment.Value == column.TargetDigit && column.IsColumnActive && column.BaseAnimationProgress > AnimatedColumnSettings.NumberHighlightActivationThreshold)
                    {
                        canvas.DrawText(segment.Value.ToString(), segment.LocationCenterPoint.X, newY, font, focusedNumberPaint);
                    }

                    else
                    {
                        canvas.DrawText(segment.Value.ToString(), segment.LocationCenterPoint.X, newY, font, nonFocusedNumberPaint);
                    }
                }
                canvas.Restore();
            }
        }




        public bool TimeToBeginStandardAnimation(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                return true;
            }

            double secondsBeforeNextAnimationInterval = _animationCalculator.CalculateSecondsUntilNextAnimationInterval(column, elapsed);
            bool isTimeToAnimate = secondsBeforeNextAnimationInterval <= 1.0;

            return isTimeToAnimate;
        }



        public void DrawRestartAnimationForAllColumns(SKCanvas canvas, TimeSpan restartTimerElapsed, List<AnimatedTimerColumn> columns, float restartAnimationProgress)
        {
            foreach (var column in columns)
            {
                float currentYPosition = column.Location.Y - column.YTranslation;

                _columnStateManager.UpdateRestartAnimationProgress(column, restartAnimationProgress);


                column.YTranslation = _animationCalculator.TESTCalculateYTranslation(column, restartTimerElapsed, restartAnimationProgress);

                DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);


                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    _animatedLogHelper.LogColumnDuringRestart(column, restartTimerElapsed, column.RestartAnimationProgress, currentYPosition);
                }
            }
        }




  








        // Used for old circle overlay animation and defining a unique path inside each segment, can be used to give focused segment distinct color etc. 


        /*
        public void WORKINGDrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan restartTimerElapsed, bool isCircleStatic, ColumnAnimationSetting animationDirection)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;

            _pathBuilder.CreateTimerPaths(column, out innerSegmentPath, out outerOverlayPath, restartTimerElapsed, isCircleStatic);

            using (var innerPaint = _paintManager.TESTCreateInnerSegmentPaint(column))
            using (var outerPaint = _paintManager.TESTCreateOuterSegmentPaint(column))
            {



                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.DrawPath(outerOverlayPath, outerPaint);


                canvas.Restore();
            }
        }
        */



        // Draws a path of Segment dimensions over the column, can be used to add more colors/effects to segments. 

        /*
        public void DrawInnerPathOnly(SKCanvas canvas, AnimatedTimerColumn column)
        {
            SKPath innerSegmentPath = _pathBuilder.CreateSegmentRectanglePath(column);

            using (var innerPaint = _paintManager.TESTCreateInnerSegmentPaint(column))
            {
                canvas.Save();
                canvas.Translate(0, +column.YTranslation);
                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.Restore();
            }
        }
        */






    }
}



