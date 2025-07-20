using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CSharpMarkup.WinUI;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawColumnNumbers(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection);
        void WorkingDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);

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
        private readonly IShadowBuilder _shadowBuilder;
        private readonly IRenderingCalculator _renderingCalculator;

        public AnimatedTimerRenderer(IApplicationLogger appLogger, IAnimationPhaseCalculator phaseCalculator, IStopWatchTimerService stopwWatchTimerService, IPaintManager circleHighlight, IPathBuilder pathBuilder, IAnimatedColumnStateManager columnStateManager, IAnimatedSegmentStateManager segmentStateManager, IShadowBuilder shadowBuilder, IRenderingCalculator renderingCalculator)
        {
            _appLogger = appLogger;
            _phaseCalculator = phaseCalculator;
            _stopWatchTimerService = stopwWatchTimerService;
            _paintManager = circleHighlight;
            _pathBuilder = pathBuilder;
            _columnStateManager = columnStateManager;
            _segmentStateManager = segmentStateManager;
            _shadowBuilder = shadowBuilder;
            _renderingCalculator = renderingCalculator;
        }




        private void DrawRoundedColumn(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection)
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



        // This is where the value for number animations is set
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

                    if (segment.Value == column.TargetSegmentValue && column.IsColumnActive && column.BaseAnimationProgress > AnimatedColumnSettings.NumberHighlightActivationThreshold)
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




        private bool WORKINGShouldColumnAnimate(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            
            double secondsBeforeNextAnimationInterval = _renderingCalculator.CalculateSecondsUntilNextAnimationInterval(column,elapsed);

            bool isTimeToAnimate = secondsBeforeNextAnimationInterval <= 1.0;

            if (column.ColumnType == ColumnUnitType.SecondsLeadingDigit && isTimeToAnimate == true)
            {
                _appLogger.Debug($"SECONDS LEADING DIGITS ANIMATION STARTED AT: {elapsed.ToString(@"mm\:ss")}");
            }

            return isTimeToAnimate;
        }









        public void WORKINGSetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue)
        {
            var newFocusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);

            column.FocusedSegment = newFocusedSegment;


        }






















        // Animation starting at correct time 2025-07-10 06:46:01.438 +10:00 [DBG] SECONDS LEADING DIGITS ANIMATION STARTED AT: 00:09
        // Focused segment assignment incorrect? 2025-07-10 06:46:01.438 +10:00 [DBG] Focused segment updated to 0 (00:00:09.0298016)













        public void WorkingDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            int tickCountsBeforeOneSecond = 0;

            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                SKRect leftShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);
                SKRect rightShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);

                // Animation begins one second before the column changes so we add one second.
                //TODO
                // Change ColumnAnimationInterval initialization by -1 to fix.
                //This band aid does not handle 0-1 animations.




                // The liveTargetValue == currentValue + 1 = TargetSegmentValue?
                int liveTargetValue = _renderingCalculator.CalculateTargetValue(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);

                if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    _appLogger.Debug($"Targetvalue calculated : {liveTargetValue}.");
                }






                // Default values initialized as 0 and 1 for current and target
                // When the elapsed was <1 , new value != targetValue
                // This mean that the current value was updated to 1 and the target updated to 0.
                // this will need reviewed and changed, what happens if timer stops and starts before one second etc. 


                /*
                if (liveTargetValue != column.TargetSegmentValue && elapsed < column.AnimationInterval && column.PassedFirstTransition == false)
                {
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = liveTargetValue;

                }
                */



                // this will need reviewed and changed, what happens if timer stops and starts before one second etc. 
                if (liveTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                   
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = liveTargetValue;

                    column.PassedFirstTransition = true;
                    WORKINGSetFocusedSegmentByValue(column, liveTargetValue);

                    if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {
                        LogColumn(column, elapsed, null, null, null);
                    }
                }



                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits && column.PassedFirstTransition == false)
                {
                    tickCountsBeforeOneSecond++;
                }


                if (column.ColumnType == ColumnUnitType.SecondsSingleDigits && column.PassedFirstTransition == true)
                {
                    _appLogger.Info($"Total ticks for one second: {tickCountsBeforeOneSecond}.");

                }



                else
                {
                    WORKINGSetFocusedSegmentByValue(column, liveTargetValue);
                }

      

   

                // 2. Determine if this column should animate right now.
                column.IsAnimating = WORKINGShouldColumnAnimate(elapsed, column);

                if (column.IsAnimating)
                {

                    if (column.IsColumnActive == false)
                    {
                        column.IsColumnActive = true;
                        _columnStateManager.UpdatedNumberBlurringStartAnimationActive(column, true);
                        _appLogger.Debug($"IsCOlumnACtive changed from false to: {column.IsColumnActive} at {FormatElapsedTimeSPan(elapsed)}");
                    }

                    if (column.NumberBlurringStartAnimationActive && elapsed >= column.AnimationInterval)
                    {
                        _columnStateManager.UpdatedNumberBlurringStartAnimationActive(column, false);
                    }



                    isCircleStatic = false;

                    float animationProgress = _renderingCalculator.CalculateAnimationProgress(elapsed);
                    float normalizedProgress = _renderingCalculator.CalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float animatingYTranslation = _renderingCalculator.CalculateYTranslation(column, elapsed);




                    column.YTranslation = animatingYTranslation;


                    /*
                        _shadowBuilder.DrawColumnLeftShadow(canvas, leftShadowRectangle);
                    
                        _shadowBuilder.DrawColumnRightShadow(canvas, rightShadowRectangle);
                        




                    // Are timer paths being draw over each other / at the same time?
                    /*
                    WORKINGDrawTimerPaths(canvas, column, elapsed, isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                    */

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);

                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);



                    }
                    else
                    {
                        // shadow should be drawn before rectangle?
                        column.YTranslation = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
                        isCircleStatic = true;


                    /*
                _shadowBuilder.TESTDrawColumnLeftShadow(canvas, leftShadowRectangle);


                _shadowBuilder.DrawColumnRightShadow(canvas, rightShadowRectangle);

            */

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);

                    /*
                    WORKINGDrawTimerPaths(canvas, column, elapsed, isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                    */
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);


                    }
                }
            }









        public void LogColumn(AnimatedTimerColumn column, TimeSpan elapsed, float? baseY, float? easedProgress, float? yTranslation)
        {
            string logMessage = $"\n \n"
                                + $"\n-----LOGGING COLUMN {column.ColumnType} AT ELAPSED {FormatElapsedTimeSPan(elapsed)}-----"
                                + $"\n-----Current Value : {column.CurrentValue}, Target Value : {column.TargetSegmentValue}.-----"
                                + $"\n-----IsAnimating: {column.IsAnimating}.-----"
                                + $"\n-----BaseAnimationProgress: {column.BaseAnimationProgress}, ColumnScrollProgress: {column.ColumnScrollProgress}, CircleAnimationProgress: {column.CircleAnimationProgress}.-----"
                                + $"\n-----Max Value: {column.MaxValue}, TotalSegmentCount: {column.TotalSegmentCount}, TimerSegments.Count: {column.TimerSegments.Count()}.-----"
                                + $"\n------PassedFirstTransition: {column.PassedFirstTransition.ToString()}.";


            if (baseY != null && easedProgress != null && yTranslation != null)
            {
                logMessage +=
                $"\n---- OFFSET CALCULATION FOR COLUMN: {column.ColumnType}.-----"
                + $"\n---- BASE Y: {baseY}, Easing Value: {easedProgress}, animatingYTranslation: {yTranslation}.-----\n\n";
            }

            _appLogger.Info(logMessage);
        }

        public string FormatElapsedTimeSPan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }














        // Used for old circle overlay animation and defining a unique path inside each segment, can be used to give focused segment distinct color etc. 


        /*
        public void WORKINGDrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan elapsed, bool isCircleStatic, ColumnAnimationSetting animationDirection)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;

            _pathBuilder.CreateTimerPaths(column, out innerSegmentPath, out outerOverlayPath, elapsed, isCircleStatic);

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



