using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CSharpMarkup.WinUI;
using LiveChartsCore.Drawing.Segments;
using OpenTK.Graphics.ES11;
using SkiaSharp;
using System.Buffers.Text;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {

        /*
        void NEWDraw(SKCanvas canvas, SKRect bounds, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
        */


        void TESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
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
                canvas.Translate(0, - column.YTranslation);
            }

            if (animationDirection == ColumnAnimationSetting.IsMovingDown)
            {
                canvas.Translate(0, + column.YTranslation);
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
                canvas.Translate(0, +column.YTranslation);
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

            var result = secondsUntilChange <= 1.0;

            if (column.ColumnType == ColumnUnitType.SecondsLeadingDigit && result == true)
            {
                _appLogger.Debug($"SECONDS LEADING DIGITS ANIMATION STARTED AT: {elapsed.ToString(@"mm\:ss")}");
            }

            return result;
        }



        private bool TESTShouldColumnAnimate(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                return true;
            }

            // Subtract the result of the module operation to get a value between 0 and the animation interval.
            double secondsUntilChange = column.AnimationInterval.TotalSeconds - (elapsed.TotalSeconds % column.AnimationInterval.TotalSeconds);

            var result = secondsUntilChange <= 1.0;

            if (column.ColumnType == ColumnUnitType.SecondsLeadingDigit && result == true)
            {
                _appLogger.Debug($"SECONDS LEADING DIGITS ANIMATION STARTED AT: {elapsed.ToString(@"mm\:ss")}");
            }

            return result;
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
            float baseOffSet = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = CalculateEasingValue(column);
            return baseOffSet + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }


        private float CalculateEasingValue(AnimatedTimerColumn column)
        {
            float baseAnimationProgress = column.BaseAnimationProgress;

            var result = baseAnimationProgress < 0.5f
                ? 4f * baseAnimationProgress * baseAnimationProgress * baseAnimationProgress
                : 1f - MathF.Pow(-2f * baseAnimationProgress + 2f, 3f) / 2f;

      
  

            return result;



        }





        public void WORKINGDrawTimerPaths(SKCanvas canvas, AnimatedTimerColumn column, TimeSpan elapsed,bool isCircleStatic, ColumnAnimationSetting animationDirection)
        {

            SKPath innerSegmentPath;
            SKPath outerOverlayPath;

            _pathBuilder.CreateTimerPaths(column, out innerSegmentPath, out outerOverlayPath, elapsed, isCircleStatic);

            using (var innerPaint = _paintManager.CreateInnerSegmentPaint(column))
            using (var outerPaint = _paintManager.CreateOuterSegmentPaint(column))
            {



                canvas.DrawPath(innerSegmentPath, innerPaint);
                canvas.DrawPath(outerOverlayPath, outerPaint);

                canvas.Restore();
            }
        }




        public void OLDDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);


            bool isCircleStatic = true;

            foreach (var column in columns)
            {


                // This sets the TargetValue to 0 
                int newValue = WORKINGCalculateColumnValue(elapsed, column.ColumnType);

        

                column.CurrentValue = column.TargetValue;
                column.TargetValue = newValue;
      



                // Update focused Segment to match the current value. Check if needed.
                WORKINGSetFocusedSegmentByValue(column, newValue);


                

                // 2. Determine if this column should animate right now.
                column.IsAnimating = WORKINGShouldColumnAnimate(elapsed, column);



                if (column.IsAnimating) 
                {
                    // TODO check if secondsleading digit is stuck in ISAnimating at wrong times, circle not shwoing as static. 

                    isCircleStatic = false;

              

                    float animationProgress = WORKINGCalculateAnimationProgress(elapsed);
                    float normalizedProgress = WORKINGCalculateColumnScrollProgress(animationProgress); 
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);




                    column.YTranslation = TESTCaclulateColumnYTranslation(column, elapsed);

                    if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {
                        _appLogger.Debug($"CURRENT VALUE : {column.TargetValue}  elapsed : {elapsed}.  {column.YTranslation}\n \n ");
                    }

          

                }
                else
                {
                    column.YTranslation = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
                    isCircleStatic = true;
                }

                NEWDrawColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                WORKINGDrawTimerPaths(canvas, column, elapsed,isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                WORKINGNumberDrawANDUpdateSegmentPosition(canvas, column, ColumnAnimationSetting.IsMovingUp);
 

            }
        }



        public void WORKINGNumberDrawANDUpdateSegmentPosition(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection)
        {
     

            using (var paint = _paintManager.CreateNumberPaint())
            using (var font = _paintManager.CreateNumberFont())
            {
                canvas.Save();

                if (animationDirection == ColumnAnimationSetting.IsMovingUp)
                {
                    canvas.Translate(0, -column.YTranslation);
                }
                else if (animationDirection == ColumnAnimationSetting.IsMovingDown)
                {
                    canvas.Translate(0, + column.YTranslation);
                }


                for (int currentSegmentIndex = 0; currentSegmentIndex < column.TotalSegmentCount; currentSegmentIndex++)
                {
                    var segment = column.TimerSegments[currentSegmentIndex];
                    float baseY = column.Location.Y + (currentSegmentIndex * AnimatedColumnSettings.SegmentHeight);

                    float oldSegmentY = segment.Location.Y;
                    float newSegmentY = segment.Location.Y - column.YTranslation; 


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





        private float TESTCaclulateColumnYTranslation(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float easedProgress = CalculateEasingValue(column);

            float baseY;
            float yTranslation;

            // Handle when we reach the top of the column & need to scroll upwards back to start, elapsed check is to stop this occuring on the first 0 - 1 transition.
            if (column.TargetValue == 0 && column.IsAnimating && elapsed > column.AnimationInterval)
            {
                // Wrapping from max to 0, so animate downward from max position.
                baseY = column.MaxValue * AnimatedColumnSettings.SegmentHeight;
   
                return baseY - (easedProgress * baseY);
            }

            else
            {
                if(elapsed < column.AnimationInterval && column.CurrentValue == 0 && column.TargetValue == 1)
                {
                    baseY = column.Location.Y;
                    yTranslation = baseY + (easedProgress * AnimatedColumnSettings.SegmentHeight);
                    return yTranslation;

                }




                baseY = (column.TargetValue * AnimatedColumnSettings.SegmentHeight);
                yTranslation = baseY + (easedProgress * AnimatedColumnSettings.SegmentHeight);

   
                return yTranslation;
            }
        }


        // 1 extra animation, wrong animtion from 0-1 after first
        private float YTranslatioExtraAnimationBug(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float easedProgress = CalculateEasingValue(column);

            float baseY;
            float yTranslation;

            // Handle when we reach the top of the column & need to scroll upwards back to start, elapsed check is to stop this occuring on the first 0 - 1 transition.
            if (column.TargetValue == 0 && column.CurrentValue == column.MaxValue && column.IsAnimating && elapsed > column.AnimationInterval)
            {
                // Wrapping from max to 0, so animate downward from max position.
                baseY = column.MaxValue * AnimatedColumnSettings.SegmentHeight;
                yTranslation = baseY - (easedProgress * baseY);
            
            }
            else
            {

                baseY = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
                yTranslation = baseY + (easedProgress * AnimatedColumnSettings.SegmentHeight);
           
            }

            if(column.ColumnType == ColumnUnitType.SecondsSingleDigits && elapsed > TimeSpan.FromSeconds(9))
            {
                LogAnimatedTimerColumn(column, elapsed, baseY, easedProgress, yTranslation);
            }

            return yTranslation;
        }


        // Are the drawing and values not matched after 9?

        public void WORKINGDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);


            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                int newValue = WORKINGCalculateColumnValue(elapsed, column.ColumnType);


                // Default values initialized as 0 and 1 for current and target
                // When the elapsed was <1 , new value != targetValue
                // This mean that the current value was updated to 1 and the target updated to 0.

                // this will need reviewed and changed, what happens if timer stops and starts before one second etc. 
                if (newValue != column.TargetValue || elapsed < TimeSpan.FromSeconds(1))
                {
                    column.CurrentValue = column.TargetValue;
                    column.TargetValue = newValue;

                    WORKINGSetFocusedSegmentByValue(column, newValue);
                }


                // 2. Determine if this column should animate right now.
                column.IsAnimating = TESTShouldColumnAnimate(elapsed, column);

                if (column.IsAnimating)
                {

                    isCircleStatic = false;

                    float animationProgress = WORKINGCalculateAnimationProgress(elapsed);
                    float normalizedProgress = WORKINGCalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float yTranslation = WORKINGCalculateYTranslation(column, elapsed);


                    if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {
                        _appLogger.Debug($"Old Y Translation: {column.YTranslation}, new: {yTranslation}");
                    }


                    column.YTranslation = WORKINGCalculateYTranslation(column, elapsed);


              

                }
                else
                { 

                    column.YTranslation = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
                    isCircleStatic = true;
                }

                NEWDrawColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                WORKINGDrawTimerPaths(canvas, column, elapsed, isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                WORKINGNumberDrawANDUpdateSegmentPosition(canvas, column, ColumnAnimationSetting.IsMovingUp);


            }
        }
        



        public void LogAnimatedTimerColumn(AnimatedTimerColumn column, TimeSpan elapsed, float? baseY, float? easedProgress, float? yTranslation)
        {
            string logMessage = $"\n \n"
                                +$"\n-----LOGGING COLUMN {column.ColumnType} AT ELAPSED {FormatElapsedTimeSPan(elapsed)}-----"
                                + $"\n-----Previous Value : {column.CurrentValue}, Target Value : {column.TargetValue}.-----"
                                + $"\n-----IsAnimating: {column.IsAnimating}.-----"
                                + $"\n-----BaseAnimationProgress: {column.BaseAnimationProgress}, ColumnScrollProgress: {column.ColumnScrollProgress}, CircleAnimationProgress: {column.CircleAnimationProgress}.-----"
                                + $"\n-----Max Value: {column.MaxValue}, TotalSegmentCount: {column.TotalSegmentCount}, TimerSegments.Count: {column.TimerSegments.Count()}.-----";


                                if (baseY != null && easedProgress != null && yTranslation != null)
                                {
                                    logMessage +=
                                    $"\n---- OFFSET CALCULATION FOR COLUMN: {column.ColumnType}.-----"
                                    + $"\n---- BASE Y: {baseY}, Easing Value: {easedProgress}, yTranslation: {yTranslation}.-----\n\n";
                                }

            _appLogger.Info(logMessage);
        }

        public string FormatElapsedTimeSPan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }










        private float WORKINGCalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float easedProgress = CalculateEasingValue(column);

            float baseY;
            float yTranslation;

            // Handle when we reach the top of the column & need to scroll upwards back to start, elapsed check is to stop this occuring on the first 0 - 1 transition.
            if (column.TargetValue == 0 && column.CurrentValue == column.MaxValue && column.IsAnimating && elapsed > column.AnimationInterval)
            {
                _appLogger.Debug($"Wrap around started for column: {column.ColumnType} at {(FormatElapsedTimeSPan(elapsed))}");

                // Wrapping from max to 0, so animate downward from max position.
                baseY = column.MaxValue * AnimatedColumnSettings.SegmentHeight;
                yTranslation = baseY - (easedProgress * baseY);
                return yTranslation;

            }

            else 
            {

                baseY = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                float endY = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
                float distance = endY - baseY;
                yTranslation = baseY + (easedProgress * distance);
                

            }

            if (column.ColumnType == ColumnUnitType.SecondsSingleDigits && elapsed > TimeSpan.FromSeconds(9))
            {
                LogAnimatedTimerColumn(column, elapsed, baseY, easedProgress, yTranslation);
            }

  

            return yTranslation;
        }















        public void TESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);


            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                int newValue = WORKINGCalculateColumnValue(elapsed, column.ColumnType);


                // Default values initialized as 0 and 1 for current and target
                // When the elapsed was <1 , new value != targetValue
                // This mean that the current value was updated to 1 and the target updated to 0.

                // this will need reviewed and changed, what happens if timer stops and starts before one second etc. 
                if (newValue != column.TargetValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                    column.CurrentValue = column.TargetValue;
                    column.TargetValue = newValue;

                    column.PassedFirstTransition = true;
                    WORKINGSetFocusedSegmentByValue(column, newValue);
                }


                // 2. Determine if this column should animate right now.
                column.IsAnimating = TESTShouldColumnAnimate(elapsed, column);

                if (column.IsAnimating)
                {

                    isCircleStatic = false;

                    float animationProgress = WORKINGCalculateAnimationProgress(elapsed);
                    float normalizedProgress = WORKINGCalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.WORKINGUpdateAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float yTranslation = WORKINGCalculateYTranslation(column, elapsed);


                    if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {
                        _appLogger.Debug($"Old Y Translation: {column.YTranslation}, new: {yTranslation}");
                    }


                    column.YTranslation = WORKINGCalculateYTranslation(column, elapsed);




                }
                else
                {

                    column.YTranslation = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
                    isCircleStatic = true;
                }

                NEWDrawColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                WORKINGDrawTimerPaths(canvas, column, elapsed, isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                WORKINGNumberDrawANDUpdateSegmentPosition(canvas, column, ColumnAnimationSetting.IsMovingUp);


            }
        }

    }
}
#endregion






#endregion