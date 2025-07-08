using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {

        /*
        void NEWDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
        */


        void WORKINGDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);

    }

    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IAnimationPhaseCalculator _phaseCalculator;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IPaintManager _paintManager;
        private readonly IPathBuilder _pathBuilder;
        private readonly IAnimatedColumnStateManager _columnStateManager;
        private readonly IAnimatedSegmentStateManager _segmentStateManager;

        public AnimatedTimerRenderer(IApplicationLogger appLogger, IAnimationPhaseCalculator phaseCalculator, IStopWatchTimerService stopwWatchTimerService, IPaintManager circleHighlight, IPathBuilder pathBuilder, IAnimatedColumnStateManager columnStateManager, IAnimatedSegmentStateManager segmentStateManager)
        {
            _appLogger = appLogger;
            _phaseCalculator = phaseCalculator;
            _stopWatchTimerService = stopwWatchTimerService;
            _paintManager = circleHighlight;
            _pathBuilder = pathBuilder;
            _columnStateManager = columnStateManager;
            _segmentStateManager = segmentStateManager;
        }








        private void NEWDrawColumn(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection)
        {
            SKSize rectangleSize = new SKSize(column.Width, column.Height);
            SKRect columnRectangle = SKRect.Create(column.Location, rectangleSize);

            canvas.Save();

            if (animationDirection == ColumnAnimationSetting.IsMovingUp)
            {
                canvas.Translate(0, - column.ScrollOffset);
            }

            if (animationDirection == ColumnAnimationSetting.IsMovingDown)
            {
                canvas.Translate(0, + column.ScrollOffset);
            }




            using (var rectPaint = _paintManager.CreateColumnPaint())
            {
                canvas.DrawRect(columnRectangle, rectPaint);
            }

            canvas.Restore();
        }

















        public void DrawInnerPathOnly(SKCanvas canvas, AnimatedTimerColumn column)
        {
            SKPath innerSegmentPath = _pathBuilder.CreateRectanglePath(column);

            using (var innerPaint = _paintManager.CreateInnerSegmentPaint(column))
            {
                canvas.Save();
                canvas.Translate(0, +column.ScrollOffset);
                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.Restore();
            }
        }




        #region WorkingMethods





        public int WORKINGCalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType)
        {
            int totalSeconds = (int)elapsed.TotalSeconds;
            int minutes = totalSeconds / 60;
            int hours = totalSeconds / 3600;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return totalSeconds % 10;
                case ColumnUnitType.SecondsLeadingDigit:
                    return (totalSeconds / 10) % 6;
                case ColumnUnitType.MinutesSingleDigits:
                    return minutes % 10;
                case ColumnUnitType.MinutesLeadingDigits:
                    return (minutes / 10) % 6;
                case ColumnUnitType.HoursSinglesDigits:
                    return hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return (hours / 10) % 10;
                default:
                    return 0;
            }
        }



 


        private bool WORKINGShouldColumnAnimate(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            // Subtract the result of the module operation to get a value between 0 and the animation interval.
            double secondsUntilChange = column.AnimationInterval.TotalSeconds - (elapsed.TotalSeconds % column.AnimationInterval.TotalSeconds);

            return secondsUntilChange <= 1.0;  
        }




  



        public void WORKINGSetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue)
        {
            var focusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);

         
        }



        public float WORKINGCalculateAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }


        public float WORKINGCalculateColumnScrollProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }





        private float WORKINGCalculateScrollOffset(AnimatedTimerColumn column)
        {
           

            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = CalculateEasingValue(column);
            return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }


        private float CalculateEasingValue(AnimatedTimerColumn column)
        {
            float baseAnimationProgress = column.BaseAnimationProgress;
            return baseAnimationProgress < 0.5f
                ? 4f * baseAnimationProgress * baseAnimationProgress * baseAnimationProgress
                : 1f - MathF.Pow(-2f * baseAnimationProgress + 2f, 3f) / 2f;


        }





        public void WORKINGDrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan elapsed,bool overLayIsAnimating, ColumnAnimationSetting animationDirection)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;

            _pathBuilder.CreateTimerPaths(column, out innerSegmentPath, out outerOverlayPath, elapsed, overLayIsAnimating);

            using (var innerPaint = _paintManager.CreateInnerSegmentPaint(column))
            using (var outerPaint = _paintManager.CreateOuterSegmentPaint(column))
            {



                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.DrawPath(outerOverlayPath, outerPaint);

                canvas.Restore();
            }
        }




        public void WORKINGDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);


            bool isCircleStatic = true;

            foreach (var column in columns)
            {



                int newValue = WORKINGCalculateColumnValue(elapsed, column.ColumnType);
                column.CurrentValue = newValue;


       
                // Update focused Segment to match the current value. Check if needed.
                WORKINGSetFocusedSegmentByValue(column, newValue);


                

                // 2. Determine if this column should animate right now.
                column.IsAnimating = WORKINGShouldColumnAnimate(elapsed, column);



                if (column.IsAnimating) 
                {


                    isCircleStatic = false;

              

                    float animationProgress = WORKINGCalculateAnimationProgress(elapsed);
                    float normalizedProgress = WORKINGCalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateNormalizedAnimationProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    column.ScrollOffset = WORKINGCalculateScrollOffset(column);

                    if (column.ColumnType == ColumnUnitType.SecondsLeadingDigit && elapsed > TimeSpan.FromSeconds(8))
                    {
                        _appLogger.Debug($"CURRENT VALUE : {column.CurrentValue} \n  elapsed : {elapsed}.  ScrollOFfset calculated : {column.ScrollOffset} \n \n ");
                    }

          

                }
                else
                {
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                }

                NEWDrawColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                WORKINGDrawTimerPaths(canvas, column, elapsed,isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                WORKINGNumberDraw(canvas, column, ColumnAnimationSetting.IsMovingUp);
 

            }
        }



        public void WORKINGNumberDraw(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection)
        {
     

            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
                canvas.Save();

                if (animationDirection == ColumnAnimationSetting.IsMovingUp)
                {
                    canvas.Translate(0, -column.ScrollOffset);
                }
                else if (animationDirection == ColumnAnimationSetting.IsMovingDown)
                {
                    canvas.Translate(0, + column.ScrollOffset);
                }


                for (int currentSegmentIndex = 0; currentSegmentIndex < column.TotalSegmentCount; currentSegmentIndex++)
                {
                    var segment = column.TimerSegments[currentSegmentIndex];
                    float baseY = column.Location.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight);

                    float oldSegmentY = segment.Location.Y;
                    float newSegmentY = segment.Location.Y - column.ScrollOffset; 


                    segment.UpdatePositionAndCenterPoint(column.Location.X, newSegmentY);

                    // Calculate text center position
                    float centerX = column.Location.X + (segment.SegmentWidth / 2f);
                    float centerY = baseY + (segment.SegmentHeight / 2f) + (segment.TextSize / 2f);

                    // Draw at the base position - translation handles the scroll
                    canvas.DrawText(segment.Value.ToString(), centerX, centerY, font, paint);
                }

                canvas.Restore();
            }
        }





























        #region TESTMethods




        private float TESTCalculateScrollOffset(AnimatedTimerColumn column)
        {
            if(column.CurrentValue == column.MaxValue)
            {

            }

            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = CalculateEasingValue(column);
            return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }







    }
}
#endregion






#endregion