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
        void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y);
        void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas);

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




        // Old method, remove once working.

        public void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas)
        {
            for (int currentSegmentIndex = 0; currentSegmentIndex < timerColumn.TotalSegmentCount; currentSegmentIndex++)
            {
                AnimatedTimerSegment targetSegment = timerColumn.TimerSegments[currentSegmentIndex];

                float segmentY = timerColumn.Location.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight) - timerColumn.ScrollOffset;

                DrawNumber(canvas, targetSegment, timerColumn.Location.X, segmentY);
            }
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















        /*

        public void OldDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            SKColor FormBackgroundColor = new SKColor(35, 34, 50);

            canvas.Clear(FormBackgroundColor);



            foreach (var column in columns)
            {

                int newValue = CalculateColumnValue(elapsed, column.ColumnType);
                column.CurrentValue = newValue;

                // Update focused Segment to match the current value.

                column.SetFocusedSegmentByValue(newValue);

                // 2. Determine if this column should animate right now.
                column.IsElapsedGreaterThanNextTransitionTime = HasColumnIntervalElapsed(elapsed, column);

                // 3. Calculate scroll offset.
                if (column.IsElapsedGreaterThanNextTransitionTime)
                {
                    float animationProgress = _segmentOverlayCalculator.CalculateAnimationProgress(elapsed);
                    float circleAnimationProgress = _segmentOverlayCalculator.CalculateInvertedNormalizedProgress(animationProgress);


                    column.ScrollOffset = CalculateVerticalOffset(column, animationProgress);
                }
                else
                {
                    // We always have to draw the numbers even when that column is not animating or else canvas.Clear will remove.
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                }


                DrawSegments(canvas, column);
                WORKINGDrawTimerPaths(canvas, column, elapsed);

            }
        }


        */


        public void DrawInnerPathOnly(SKCanvas canvas, AnimatedTimerColumn column)
        {
            SKPath innerSegmentPath = _pathBuilder.CreateRectanglePath(column);

            using (var innerPaint = _paintManager.CreateInnerSegmentPaint())
            {
                canvas.Save();
                canvas.Translate(0, +column.ScrollOffset);
                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.Restore();
            }
        }








        private bool HasColumnIntervalElapsed(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            // Check if we're within 1 second of when this column changes.
            double secondsUntilChange = column.AnimationInterval.TotalSeconds -
                                       (elapsed.TotalSeconds % column.AnimationInterval.TotalSeconds);

            return secondsUntilChange <= 1.0;  // Animate during last second before change.
        }


        /*
        public void NEWDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {

            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            float? circleAnimationProgress;

            foreach (var column in columns)
            {
                int newValue = _columnStateManager.CalculateColumnValue(elapsed, column.ColumnType);
                _columnStateManager.UpdateColumnCurrentValue(column, newValue);


                // Update focused Segment to match the current value.

                _columnStateManager.SetFocusedSegmentByValue(column, newValue);

                // 2. Determine if this column should animate right now.
                bool withinAnimationInterval = _columnStateManager.IsElapsedGreaterThanNextTransitionTime(column ,elapsed);

                // 3. Calculate scroll offset.
                if (withinAnimationInterval)
                {
            

                    float animationProgress = _columnStateManager.GetColumnAnimationProgress(elapsed);
                    circleAnimationProgress = _columnStateManager.CalculateCircleAnimationProgress(elapsed, animationProgress);
                    float scrollOffset = _columnStateManager.CalculateVerticalOffset(column, animationProgress);





                }
                else
                {
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                }

                NEWDrawColumn(canvas, column);
                DrawSegments(canvas, column);


            }
        }
        */


        /*
        public void UPDATEDNEWDraw(SKCanvas canvas, List<AnimatedTimerColumn> columns, TimeSpan elapsed)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            foreach (AnimatedTimerColumn column in columns)
            {
                float circleAnimationProgress;
                _columnStateManager.UpdateAnimationState(column, elapsed);


                if (column.IsAnimating)
                {
                    float animationProgress = _columnStateManager.GetColumnAnimationProgress(elapsed);
                    circleAnimationProgress = _columnStateManager.CalculateCircleAnimationProgress(elapsed, animationProgress);


                    column.ScrollOffset = _columnStateManager.CalculateVerticalOffset(column, animationProgress);

                    NEWDrawColumn(canvas, column);

                    WORKINGDrawTimerPaths(canvas, column, elapsed, circleAnimationProgress, isCircleStatic: false);
                    DrawSegments(canvas, column);


                    _columnStateManager.UpdateScrollOffset(column, animationProgress);
                    _columnStateManager.UpdateAnimationPogress(column, animationProgress);
                }
                else
                {

                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;

                    NEWDrawColumn(canvas, column);

                    WORKINGDrawTimerPaths(canvas, column, elapsed, 0.0f, isCircleStatic: true);
                    DrawSegments(canvas, column);


                }
            }
        }

        */



        private void DrawSegments(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float digitHeight = AnimatedColumnSettings.SegmentHeight;
            float startY = column.Location.Y - column.ScrollOffset;


            for (int currentSegmentCount = 0; currentSegmentCount < column.TotalSegmentCount; currentSegmentCount++)
            {

                float currentY = currentSegmentCount * AnimatedColumnSettings.SegmentHeight;


                var segment = column.TimerSegments[currentSegmentCount];


                float YNew = startY + (currentSegmentCount * digitHeight);

                segment.UpdatePositionAndCenterPoint(column.Location.X, YNew);

                DrawNumber(canvas, segment, column.Location.X, YNew);
            }
        }



        /*

        public void NEWDrawNumbers(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float startY = column.Location.Y - column.ScrollOffset;

            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
                for (int currentSegmentIndex = 0; currentSegmentIndex < column.TotalSegmentCount; currentSegmentIndex++)
                {
                    AnimatedTimerSegment currentSegment = column.TimerSegments[currentSegmentIndex];

                    float YNew = startY + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight);


                    canvas.DrawText(currentSegment.Value.ToString(), x, y, font, paint);

                }
            }
        }







        public void UPDATEDNEWDraw(SKCanvas canvas, List<AnimatedTimerColumn> columns, TimeSpan elapsed)
        {
            /*
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            foreach (AnimatedTimerColumn column in columns)
            {
                float circleAnimationProgress;

                int timeDigit = _columnStateManager.ExtractTimeDigitForColumn(column, elapsed);

                ///First block handles when to animate.

                if (timeDigit != column.CurrentValue && !column.IsAnimating)
                {
                    int newPrevious = column.CurrentValue;

                    // Update is Animating and record the time the animation starts, when it should end etc
                    _columnStateManager.NEWUpdateIsAnimating(column, true);
                    _columnStateManager.NEWUpdateAnimationStartTime(column, elapsed);
                    _columnStateManager.NEWUpdateCurrentAnimationEndTime(column, elapsed);
                    _columnStateManager.UpdateColumnPreviousValue(column, newPrevious);
                    _columnStateManager.UpdateColumnCurrentValue(column, timeDigit);

                    var newSegment = _columnStateManager.NEWFindNewTimeSegmentByTimeDigit(column, timeDigit);
                    _columnStateManager.UpdateFocusedSegment(column, newSegment);


                    _appLogger.Debug($"Updating column current value to {timeDigit} newPrevious value: {column.PreviousValue}.");




                }

                /// Handles when the calculations for animating between column values & updates IsAnimating when animation is complete. 

                if (column.IsAnimating)
                {
                    // First we use the elapsed time to calculate the animation progress
                    float animationProgress = _columnStateManager.NEWCalculateAnimationProgress(column, elapsed);
                    _columnStateManager.UpdateAnimationPogress(column, animationProgress);

                    //
                    circleAnimationProgress = _columnStateManager.CalculateCircleAnimationProgress(elapsed, animationProgress);
                    float scrollOffSet = _columnStateManager.TESTCalculateScrollOffset(column, column.AnimationProgress);

                    float scrollOffSet = _columnStateManager.TESTCalculateScrollOffset(column, column.ColumnAnimationProgress);
                    _columnStateManager.UpdateScrollOffset(column, scrollOffSet);



                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);
                    _columnStateManager.UpdateNormalizedColumnAnimationProgress(column, normalizedColumnAnimationProgress);


                    SKPoint newLocation = _columnStateManager.CalculateNewColumnLocationDuringAnimation(column);
                    _columnStateManager.UpdateColumnLocation(column, newLocation);

                    if (elapsed >= column.CurrentAnimationEndTime)
                    {
                        _columnStateManager.NEWUpdateIsAnimating(column, false);
                    }








                    NEWDrawColumn(canvas, column);
                    WORKINGDrawTimerPaths(canvas, column, elapsed, column.CircleAnimationProgress, isCircleStatic: false);
                    NEWDrawNumbers(canvas, column);

                }

                else
                {
                    float scrollOffSet = _columnStateManager.CalculateVerticalOffset(column, column.IsAnimating);
                    NEWDrawColumn(canvas, column);
                    WORKINGDrawTimerPaths(canvas, column, elapsed, column.CircleAnimationProgress, isCircleStatic: true);
                    NEWDrawNumbers(canvas, column);
                }
            }
            
        }
        */



        public void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y)
        {
            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {

                // Calculate center positions.
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.Value.ToString(), centerX, centerY, font, paint);
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
            float animationDurationFloat = AnimatedColumnSettings.AnimationDurationFloat;

            switch (column.ColumnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    double secondsCycle = elapsed.TotalSeconds % 1.0;
                    return secondsCycle >= (1.0 - animationDurationFloat);
                case ColumnUnitType.SecondsLeadingDigit:
                    double tenSecondsCycle = elapsed.TotalSeconds % 10.0;
                    return tenSecondsCycle >= (10.0 - animationDurationFloat);
                case ColumnUnitType.MinutesSingleDigits:
                    double minutesCycle = elapsed.TotalSeconds % 60.0;
                    return minutesCycle >= (60.0 - animationDurationFloat);
                case ColumnUnitType.MinutesLeadingDigits:
                    double tenMinutesCycle = elapsed.TotalSeconds % 600.0;
                    return tenMinutesCycle >= (600.0 - animationDurationFloat);
                case ColumnUnitType.HoursSinglesDigits:
                    double hoursCycle = elapsed.TotalSeconds % 3600.0;
                    return hoursCycle >= (3600.0 - animationDurationFloat);
                case ColumnUnitType.HoursLeadingDigits:
                    double tenHoursCycle = elapsed.TotalSeconds % 36000.0;
                    return tenHoursCycle >= (36000.0 - animationDurationFloat);
                default:
                    return false;
            }
        }



        public float WORKINGCalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            ColumnUnitType columnType = column.ColumnType;

            float animationDurationFloat = AnimatedColumnSettings.AnimationDurationFloat;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    double secondsCycle = elapsed.TotalSeconds % 1.0;
                    return secondsCycle >= 0.7 ? (float)((secondsCycle - 0.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.SecondsLeadingDigit:
                    double tenSecondsCycle = elapsed.TotalSeconds % 10.0;
                    return tenSecondsCycle >= 9.7 ? (float)((tenSecondsCycle - 9.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.MinutesSingleDigits:
                    double minutesCycle = elapsed.TotalSeconds % 60.0;
                    return minutesCycle >= 59.7 ? (float)((minutesCycle - 59.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.MinutesLeadingDigits:
                    double tenMinutesCycle = elapsed.TotalSeconds % 600.0;
                    return tenMinutesCycle >= 599.7 ? (float)((tenMinutesCycle - 599.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.HoursSinglesDigits:
                    double hoursCycle = elapsed.TotalSeconds % 3600.0;
                    return hoursCycle >= 3599.7 ? (float)((hoursCycle - 3599.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.HoursLeadingDigits:
                    double tenHoursCycle = elapsed.TotalSeconds % 36000.0;
                    return tenHoursCycle >= 35999.7 ? (float)((tenHoursCycle - 35999.7) / animationDurationFloat) : 0.0f;
                default:
                    return 0.0f;
            }
        }



        public void WORKINGSetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue)
        {
            var focusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);

            if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                _appLogger.Debug($"Focused segment updated to {focusedSegment.Value}");
            }
        }




        public float WORKINGCalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }



        public float WORKINGCalculateEasingValue(float animationProgress)
        {
            const float midpoint = 0.5f;

            if (animationProgress < midpoint)
            {
                float scaledProgress = animationProgress;
                float easeInValue = 4f * scaledProgress * scaledProgress * scaledProgress;
                return easeInValue;
            }
            else
            {

                float adjustedProgress = -2f * animationProgress + 2f; // Maps 0.5→1 to 1→0

                // Cubic ease-out: 1 - ((1-t)^3)
                float powerOfThree = MathF.Pow(adjustedProgress, 3f);
                float normalizedAnimationProgress = 1f - (powerOfThree / 2f);

                return normalizedAnimationProgress;
            }
        }


        private float WORKINGCalculateScrollOffset(AnimatedTimerColumn column, float animationProgress)
        {
            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = WORKINGCalculateEasingValue(animationProgress);
            return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }




        private void WORKINGDrawSegments(SKCanvas canvas, AnimatedTimerColumn column)
        {
            // Step 1: Get the fixed height for each segment
            float digitHeight = AnimatedColumnSettings.SegmentHeight;

            // Step 2: Calculate the starting Y position
            // This combines the column's base Y position with the current scroll offset
            float startY = column.Location.Y + column.ScrollOffset;


            for (int currentSegmentCount = 0; currentSegmentCount < column.TotalSegmentCount; currentSegmentCount++)
            {

                var segment = column.TimerSegments[currentSegmentCount];
                _appLogger.Debug($"old segment location: X = {segment.Location.X}, Y = {segment.Location.Y}");

                float YNew = startY + (currentSegmentCount * digitHeight);

                segment.UpdatePositionAndCenterPoint(column.Location.X, YNew);

                _appLogger.Debug($"new segment location: X = {segment.Location.X}, Y = {segment.Location.Y}");

                WORKINGDrawNumber(canvas, segment, column.Location.X, YNew);
            }
        }






        public void TESTTWODrawNumbers(SKCanvas canvas, AnimatedTimerColumn column)
        {
            canvas.Save();
            canvas.Translate(0, column.ScrollOffset);

            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
                for (int currentSegmentIndex = 0; currentSegmentIndex < column.TotalSegmentCount; currentSegmentIndex++)
                {
                    AnimatedTimerSegment currentSegment = column.TimerSegments[currentSegmentIndex];
                    float x = currentSegment.LocationCenterPoint.X;
                    float y = column.Location.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight);
                    canvas.DrawText(currentSegment.Value.ToString(), x, y, font, paint);
                }
            }

            canvas.Restore();
        }



        public void WORKINGDrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y)
        {
            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
      
                // Calculate center positions.
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.Value.ToString(), centerX, centerY, font, paint);
            }
        }


 


        public void TESTWORKINGDrawNumber(SKCanvas canvas, AnimatedTimerColumn column, float x, float y)
        {
            AnimatedTimerSegment timerSegment = column.FocusedSegment;

            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
                canvas.Save();
                canvas.Translate(x, y + column.ScrollOffset);

                // Calculate center positions.
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.Value.ToString(), centerX, centerY, font, paint);
            }
        }





        public void WORKINGDrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan elapsed, float circleAnimationProgress, bool isCircleStatic, ColumnAnimationSetting animationDirection)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;

            _pathBuilder.CreateTimerPaths(column, out outerOverlayPath, out innerSegmentPath, elapsed, isCircleStatic);

            using (var innerPaint = _paintManager.CreateInnerSegmentPaint())
            using (var outerPaint = _paintManager.CreateOuterSegmentPaint(circleAnimationProgress))
            {




                // Apply scroll offset to transform the Y axis of the paths and move them downward.
                canvas.Save();
                if (animationDirection == ColumnAnimationSetting.IsMovingUp)
                {
                    canvas.Translate(0, - column.ScrollOffset);
 
                }

                if (animationDirection == ColumnAnimationSetting.IsMovingDown)
                {
                    canvas.Translate(0, + column.ScrollOffset);
                }


                // OldDraw at transformed position.
                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.DrawPath(outerOverlayPath, outerPaint);

                /*
                var innerBounds = innerSegmentPath.Bounds;
                var outerBounds = outerOverlayPath.Bounds;
                _appLogger.Debug($"Inner path innerBounds: {innerBounds}");
                _appLogger.Debug($"Inner path innerBounds: {outerBounds}");
                */

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

                _appLogger.Debug($"CurrentValue updated to {column.CurrentValue}, FocusedSegment set to {column.FocusedSegment.Value}.");



                // 2. Determine if this column should animate right now.
                column.IsAnimating = WORKINGShouldColumnAnimate(elapsed, column);

                if (column.IsAnimating)
                {
                    isCircleStatic = false;

                    float animationProgress = WORKINGCalculateAnimationProgress(column, elapsed);
                    float normalizedProgress = WORKINGCalculateNormalizedProgress(animationProgress);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateNormalizedAnimationProgress(column, normalizedProgress);

                    column.ScrollOffset = WORKINGCalculateScrollOffset(column, animationProgress);


                }
                else
                {
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                }

                NEWDrawColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                WORKINGDrawTimerPaths(canvas, column, elapsed, column.CircleAnimationProgress, isCircleStatic = false, ColumnAnimationSetting.IsMovingUp);
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




    }
}
#endregion