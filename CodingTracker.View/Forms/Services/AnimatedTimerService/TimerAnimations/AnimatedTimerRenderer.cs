using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.Measure;
using SkiaSharp;
using Uno.UI.Xaml;
using static Guna.UI2.Material.Animation.AnimationManager;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CSharpMarkup.WinUI;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y);
        void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas);

        /*
        void NEWDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
        */

        void UPDATEDNEWDraw(SKCanvas canvas, List<AnimatedTimerColumn> columns, TimeSpan elapsed);

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

                float segmentY = timerColumn.CurrentLocation.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight) - timerColumn.ScrollOffset;

                DrawNumber(canvas, targetSegment, timerColumn.CurrentLocation.X, segmentY);
            }
        }






















        private void NEWDrawColumn(SKCanvas canvas, AnimatedTimerColumn column)
        {
            SKSize rectangleSize = new SKSize(column.Width, column.Height);



            SKRect columnRectangle = SKRect.Create(column.CurrentLocation, rectangleSize);

            using (var rectPaint = _paintManager.CreateColumnPaint())
            {
                canvas.DrawRect(columnRectangle, rectPaint);
            }
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
                DrawTimerPaths(canvas, column, elapsed);

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



        public void DrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan elapsed, float circleAnimationProgress, bool isCircleStatic)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;



            _pathBuilder.CreateTimerPaths(column, out outerOverlayPath, out innerSegmentPath, elapsed, isCircleStatic);



            using (var innerPaint = _paintManager.CreateInnerSegmentPaint())
            using (var outerPaint = _paintManager.CreateOuterSegmentPaint(circleAnimationProgress))
            {



                // Apply scroll offset to transform the Y axis of the paths and move them downward.
                canvas.Save();
                canvas.Translate(0, + column.ScrollOffset);


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

                    DrawTimerPaths(canvas, column, elapsed, circleAnimationProgress, isCircleStatic: false);
                    DrawSegments(canvas, column);


                    _columnStateManager.UpdateScrollOffset(column, animationProgress);
                    _columnStateManager.UpdateAnimationPogress(column, animationProgress);
                }
                else
                {

                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;

                    NEWDrawColumn(canvas, column);

                    DrawTimerPaths(canvas, column, elapsed, 0.0f, isCircleStatic: true);
                    DrawSegments(canvas, column);


                }
            }
        }

        */



        private void DrawSegments(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float digitHeight = AnimatedColumnSettings.SegmentHeight;
            float startY = column.CurrentLocation.Y - column.ScrollOffset;


            for (int currentSegmentCount = 0; currentSegmentCount < column.TotalSegmentCount; currentSegmentCount++)
            {

                var segment = column.TimerSegments[currentSegmentCount];


                float YNew = startY + (currentSegmentCount * digitHeight);

                segment.UpdatePosition(column.CurrentLocation.X, YNew);

                DrawNumber(canvas, segment, column.CurrentLocation.X, YNew);
            }
        }





        public void NEWDrawNumbers(SKCanvas canvas, AnimatedTimerColumn column)
        {
            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
                for (int currentSegmentIndex = 0; currentSegmentIndex < column.TotalSegmentCount; currentSegmentIndex++)
                {
                    AnimatedTimerSegment currentSegment = column.TimerSegments[currentSegmentIndex];

                    var x = currentSegment.LocationCenterPoint.X;
                    var y = currentSegment.LocationCenterPoint.Y;


                    canvas.DrawText(currentSegment.Value.ToString(), x, y, font, paint);

                }
            }
        }


       




        public void UPDATEDNEWDraw(SKCanvas canvas, List<AnimatedTimerColumn> columns, TimeSpan elapsed)
        {
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

                    //Needed? When should focus segment update?
                    _columnStateManager.UpdateFocusedSegment(column, newSegment);


                    _appLogger.Debug($"Updating column current value to {timeDigit} newPrevious value: {column.PreviousValue}.");

    


                }

                /// Handles when the calculations for animating between column values & updates IsAnimating when animation is complete. 

                if(column.IsAnimating)
                {
                    // First we use the elapsed time to calculate the animation progress
                    float animationProgress = _columnStateManager.NEWCalculateAnimationProgress(column, elapsed);
                    _columnStateManager.UpdateAnimationPogress(column, animationProgress);
                    
                    //
                    circleAnimationProgress = _columnStateManager.CalculateCircleAnimationProgress(elapsed, animationProgress);
                    float normalizedColumnAnimationProgress = _columnStateManager.CalculateEasingForVertialOffSet(animationProgress);

                    float scrollOffSet = _columnStateManager.TESTCalculateScrollOffset(column, column.ColumnAnimationProgress);
                    _columnStateManager.UpdateScrollOffset(column, scrollOffSet);


               
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);
                    _columnStateManager.UpdateNormalizedColumnAnimationProgress(column, normalizedColumnAnimationProgress);


                    SKPoint newLocation = _columnStateManager.CalculateNewColumnLocationDuringAnimation(column);
                    _columnStateManager.UpdateColumnCurrentLocation(column, newLocation);

                    if (elapsed >= column.CurrentAnimationEndTime)
                    {
                        _columnStateManager.NEWUpdateIsAnimating(column, false);
                    }

                    
           
         

                   

         
                    NEWDrawColumn(canvas, column);
                    DrawTimerPaths(canvas, column, elapsed, column.CircleAnimationProgress, isCircleStatic: false);
                    NEWDrawNumbers(canvas, column);

                }

                else
                {
                    float scrollOffSet = _columnStateManager.CalculateVerticalOffset(column, column.IsAnimating);
                    _columnStateManager.UpdateScrollOffset(column, scrollOffSet);
                    NEWDrawColumn(canvas, column);
                    DrawTimerPaths(canvas, column, elapsed, column.CircleAnimationProgress, isCircleStatic: true);
                    NEWDrawNumbers(canvas, column);
                }
            }
        }




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



        public void UpdateAllLocations(AnimatedTimerColumn column, SKPoint location)
        {

        }
    }
}
