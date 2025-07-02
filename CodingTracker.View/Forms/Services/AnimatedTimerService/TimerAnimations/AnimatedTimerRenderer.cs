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

                float segmentY = timerColumn.Location.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight) - timerColumn.ScrollOffset;

                DrawNumber(canvas, targetSegment, timerColumn.Location.X, segmentY);
            }
        }






















        private void NEWDrawColumn(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.ScrollOffset;


            SKPoint newLocation = new SKPoint(column.Location.X, newY);
            SKSize rectangleSize = new SKSize(column.Width, column.Height);





            SKRect columnRectangle = SKRect.Create(newLocation, rectangleSize);




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
                column.IsTimeToAnimate = HasColumnIntervalElapsed(elapsed, column);

                // 3. Calculate scroll offset.
                if (column.IsTimeToAnimate)
                {
                    float animationProgress = _segmentOverlayCalculator.CalculateAnimationProgress(elapsed);
                    float circleAnimationProgress = _segmentOverlayCalculator.CalculateInvertedNormalizedProgress(animationProgress);


                    column.ScrollOffset = CalculateScrollOffset(column, animationProgress);
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
                canvas.Translate(0, +column.ScrollOffset);


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
                bool withinAnimationInterval = _columnStateManager.IsTimeToAnimate(column ,elapsed);

                // 3. Calculate scroll offset.
                if (withinAnimationInterval)
                {
            

                    float animationProgress = _columnStateManager.GetColumnAnimationProgress(elapsed);
                    circleAnimationProgress = _columnStateManager.CalculateCircleAnimationProgress(elapsed, animationProgress);
                    float scrollOffset = _columnStateManager.CalculateScrollOffset(column, animationProgress);





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


                    column.ScrollOffset = _columnStateManager.CalculateScrollOffset(column, animationProgress);

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
            float startY = column.Location.Y - column.ScrollOffset;


            for (int currentSegmentCount = 0; currentSegmentCount < column.TotalSegmentCount; currentSegmentCount++)
            {

                var segment = column.TimerSegments[currentSegmentCount];


                float YNew = startY + (currentSegmentCount * digitHeight);

                segment.UpdatePosition(column.Location.X, YNew);

                DrawNumber(canvas, segment, column.Location.X, YNew);
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


        /*
        public void UPDATEDNEWDraw(SKCanvas canvas, List<AnimatedTimerColumn> columns, TimeSpan elapsed)
        {
            _appLogger.Debug($"\n########## DRAW FRAME START - Elapsed: {elapsed.TotalSeconds:F3}s ##########");

            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            foreach (AnimatedTimerColumn column in columns)
            {
                _appLogger.Debug($"\n--- Processing {column.ColumnType} ---");

                float oldScrollOffset = column.ScrollOffset;
                _columnStateManager.UpdateAnimationState(column, elapsed);

                // Calculate scroll offset
                if (column.IsAnimating)
                {
                    float animationProgress = _columnStateManager.TESTGetColumnAnimationProgress(column, elapsed);
                    _appLogger.Debug($"Animation progress: {animationProgress:F3}");

                    column.ScrollOffset = _columnStateManager.CalculateScrollOffset(column, animationProgress);
                    _appLogger.Debug($"Animated ScrollOffset: {oldScrollOffset:F1} -> {column.ScrollOffset:F1} (diff: {column.ScrollOffset - oldScrollOffset:F1})");
                }
                else
                {
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                    _appLogger.Debug($"Static ScrollOffset: {column.ScrollOffset:F1} (value {column.CurrentValue} * height {AnimatedColumnSettings.SegmentHeight})");
                }

                _segmentStateManager.UpdateSegmentPositions(column);
                _appLogger.Debug($"Segment positions updated");
            }

            _appLogger.Debug($"\n--- Starting Draw Phase ---");

            foreach (AnimatedTimerColumn column in columns)
            {
                _appLogger.Debug($"Drawing column {column.ColumnType}");
                NEWDrawColumn(canvas, column);
                DrawTimerPaths(canvas, column, elapsed, 0.0f, isCircleStatic: true);
            }

            // Have to draw the numbers last to ensure that other paint methods do not draw over them
            _appLogger.Debug($"\n--- Drawing Numbers ---");
            foreach (AnimatedTimerColumn column in columns)
            {
                _appLogger.Debug($"Drawing numbers for {column.ColumnType}");
                NEWDrawNumbers(canvas, column);
            }

            _appLogger.Debug($"########## DRAW FRAME END ##########\n");
        }
        */


        public void UPDATEDNEWDraw(SKCanvas canvas, List<AnimatedTimerColumn> columns, TimeSpan elapsed)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            foreach (AnimatedTimerColumn column in columns)
            {
                // Has elapsed passed the next animation interval
                if(_columnStateManager.IsTimeToAnimate(column,elapsed))
                {
                    _appLogger.Debug($"ANIMATION INTERVAL PASSED initial transition time: {column.NextTransitionTime}, elapsed: {elapsed}.");

                    bool withinAnimationWindow = _columnStateManager.NEWIsStillWithinAnimationWindow(column, elapsed);
                    
                    // If not within the animation window then start the logic to animate and update the next animation times etc.
                    if(!withinAnimationWindow)
                    {
                        _columnStateManager.NEWUpdateIsAnimating(column, true);

                        // Update the last transition time first as we use this value to calculate the next transition time. 
                        _columnStateManager.NEWUpdateLastAnimationStartTime(column, elapsed);
                        _columnStateManager.NEWUpdateNextTransitionTime(column, elapsed);
             
                    
                    }

                    
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
    }
}
