using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.Measure;
using SkiaSharp;
using Uno.UI.Xaml;
using static Guna.UI2.Material.Animation.AnimationManager;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y);
        void DrawAllSegments(AnimatedTimerColumn timerColumn, SKCanvas canvas);

        void OldDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns);

        void NEWDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
    }

    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IAnimationPhaseCalculator _phaseCalculator;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly ICircleHighLight _circleHighlight;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalculator;
        private readonly IPathBuilder _pathBuilder;

        public AnimatedTimerRenderer(IApplicationLogger appLogger, IAnimationPhaseCalculator phaseCalculator, IStopWatchTimerService stopwWatchTimerService, ICircleHighLight circleHighlight, ISegmentOverlayCalculator segmentOverlayCalculator, IPathBuilder pathBuilder)
        {
            _appLogger = appLogger;
            _phaseCalculator = phaseCalculator;
            _stopWatchTimerService = stopwWatchTimerService;
            _circleHighlight = circleHighlight;
            _segmentOverlayCalculator = segmentOverlayCalculator;
            _pathBuilder = pathBuilder;
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

        




















        private int CalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType)
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



        
        private void NEWDrawColumn(SKCanvas canvas, AnimatedTimerColumn column)
        {
            float newY = column.Location.Y - column.ScrollOffset;

      
            SKPoint newLocation = new SKPoint(column.Location.X, newY);    
            SKSize rectangleSize = new SKSize(column.Width, column.Height);

            if (newLocation.X != column.Location.X || newLocation.Y != column.Location.Y)
            {
                _appLogger.Fatal($"newLocation and columnLocation not matching  X: {newLocation.X} Y: {newLocation.Y} \n columnLocation X: {column.Location.X}, Y: {column.Location.Y}.");
            }

            

            SKRect columnRectangle = SKRect.Create(newLocation, rectangleSize);

            _appLogger.Debug($"Drawing column of size {rectangleSize.Width}  {rectangleSize.Height} at X:{newLocation.X}, Y:{newLocation.Y}.");


            using (var rectPaint = new SKPaint())
            {
                rectPaint.Color = AnimatedColumnSettings.MainPageFadedColor;
                rectPaint.Style = SKPaintStyle.Fill;
                rectPaint.IsAntialias = true;

                canvas.DrawRect(columnRectangle, rectPaint);
            }
        }
        


       


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

        // Value to determine the progress between one Segment and the next. 
        private float CalculateScrollOffset(AnimatedTimerColumn column,float animationProgress)
        {
            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = CalculateEasingValue(animationProgress);
            return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }

        public void DrawNumber(SKCanvas canvas, AnimatedTimerSegment timerSegment, float x, float y)
        {
            using (var paint = new SKPaint())
            using (var font = new SKFont())
            {
                paint.Color = SKColors.White;
                paint.IsAntialias = true;
                paint.TextAlign = SKTextAlign.Center;
                
                font.Size = timerSegment.TextSize;

                // Calculate center positions.
                float centerX = x + (timerSegment.SegmentWidth / 2f);
                float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);

                canvas.DrawText(timerSegment.Value.ToString(), centerX, centerY, font, paint);
            }
        }


        private float CalculateEasingValue(float t)
        {
            return t < 0.5f
                ? 4f * t * t * t
                : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

            
        }

        private float CaclulatePhaseValue(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }








        public void OldDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            SKColor formBackgroundColor = new SKColor(35, 34, 50);

            canvas.Clear(formBackgroundColor);



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
                    float normalizedProgress = _segmentOverlayCalculator.CalculateInvertedNormalizedProgress(animationProgress);


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







        public void DrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan elapsed)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;



            _pathBuilder.CreateTimerPaths(column, out outerOverlayPath, out innerSegmentPath, elapsed);

            _appLogger.Debug($"DrawTimerPaths Inner path innerBounds: {innerSegmentPath.Bounds}");
            _appLogger.Debug($"DrawTimerPaths Inner path innerBounds: {outerOverlayPath.Bounds}");

            using (var innerPaint = new SKPaint())
            using (var outerPaint = new SKPaint())
            {

                outerPaint.Style = SKPaintStyle.Fill;
                outerPaint.Color = new SKColor(255, 255, 255, 40); 



                innerPaint.Color = new SKColor(255, 255, 255, 40).WithAlpha(AnimatedColumnSettings.OuterCircleOpacity);
                innerPaint.Style = SKPaintStyle.Fill;
                innerPaint.IsAntialias = true;

                
                innerPaint.StrokeWidth = 1f;
                innerPaint.StrokeCap = SKStrokeCap.Round;
                innerPaint.StrokeJoin = SKStrokeJoin.Round;

               


                // Apply scroll offset to transform the Y axis of the paths and move them downward.
                canvas.Save();
                canvas.Translate(0, +column.ScrollOffset);
                

                // OldDraw at transformed position.
                canvas.DrawPath(innerSegmentPath, outerPaint);
                canvas.DrawPath(outerOverlayPath, innerPaint);

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



        public void NEWDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            SKColor formBackgroundColor = new SKColor(35, 34, 50);

            canvas.Clear(formBackgroundColor);



            foreach (var column in columns)
            {

                int newValue = CalculateColumnValue(elapsed, column.ColumnType);
                column.CurrentValue = newValue;

                // Update focused Segment to match the current value.

                column.SetFocusedSegmentByValue(newValue);

                // 2. Determine if this column should animate right now.
                bool shouldTranisitionBetweenNumbers = IsWithinAnimationInterval(column ,elapsed);

                // 3. Calculate scroll offset.
                if (shouldTranisitionBetweenNumbers)
                {
                    float animationProgress = column.GetColumnAnimationProgress(elapsed);
                    float normalizedProgress = column.GetCircleHighlightAnimationProgress(elapsed);


                    column.ScrollOffset = CalculateScrollOffset(column, animationProgress);
                }
                else
                {
                    column.ScrollOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                }

                NEWDrawColumn(canvas, column);
                DrawSegments(canvas, column);
                DrawTimerPaths(canvas, column, elapsed);

            }
        }




        private bool IsWithinAnimationInterval(AnimatedTimerColumn column ,TimeSpan elapsed)
        {
            double secondsUntilChange = AnimationInterval.TotalSeconds - (elapsed.TotalSeconds / AnimationInterval.TotalSeconds);
            return secondsUntilChange <= 1.0;

        }








    }
}

