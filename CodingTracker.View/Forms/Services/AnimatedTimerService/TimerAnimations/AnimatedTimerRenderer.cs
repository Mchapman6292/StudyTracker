﻿using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.PathBuilders;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CSharpMarkup.WinUI;
using Guna.UI2.AnimatorNS;
using Microsoft.UI.Xaml;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawColumnNumbers(SKCanvas canvas, AnimatedTimerColumn column, ColumnAnimationSetting animationDirection);
        void WorkingDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);

        void TESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
        void NEWTESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
        void RefactoredDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);
        void TESTRefactoredDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns);

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
        private readonly IAnimationPhaseCalculator _phaseCalculator;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IPaintManager _paintManager;
        private readonly IPathBuilder _pathBuilder;
        private readonly IAnimatedColumnStateManager _columnStateManager;
        private readonly IAnimatedSegmentStateManager _segmentStateManager;
        private readonly IShadowBuilder _shadowBuilder;
        private readonly IAnimationCalculator _animationCalculator;
        private readonly IAnimatedLogHelper _animatedLogHelper;


        public int tickCountsBeforeOneSecond = 0;

        public AnimatedTimerRenderer(IApplicationLogger appLogger, IAnimationPhaseCalculator phaseCalculator, IStopWatchTimerService stopwWatchTimerService, IPaintManager circleHighlight, IPathBuilder pathBuilder, IAnimatedColumnStateManager columnStateManager, IAnimatedSegmentStateManager segmentStateManager, IShadowBuilder shadowBuilder, IAnimationCalculator renderingCalculator, IAnimatedLogHelper animatedLogHelper)
        {
            _appLogger = appLogger;
            _phaseCalculator = phaseCalculator;
            _stopWatchTimerService = stopwWatchTimerService;
            _paintManager = circleHighlight;
            _pathBuilder = pathBuilder;
            _columnStateManager = columnStateManager;
            _segmentStateManager = segmentStateManager;
            _shadowBuilder = shadowBuilder;
            _animationCalculator = renderingCalculator;
            _animatedLogHelper = animatedLogHelper;
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




        private bool TimeToBeginStandardAnimation(TimeSpan elapsed, AnimatedTimerColumn column)
        {

            if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                return true;
            }
            
            double secondsBeforeNextAnimationInterval = _animationCalculator.CalculateSecondsUntilNextAnimationInterval(column,elapsed);

            bool isTimeToAnimate = secondsBeforeNextAnimationInterval <= 1.0;

       

            return isTimeToAnimate;
        }









        public void SetFocusedSegmentByColumnTargetValue(AnimatedTimerColumn column, int newValue)
        {
            var newFocusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);

            column.FocusedSegment = newFocusedSegment;


        }






















        // Animation starting at correct time 2025-07-10 06:46:01.438 +10:00 [DBG] SECONDS LEADING DIGITS ANIMATION STARTED AT: 00:09
        // Focused segment assignment incorrect? 2025-07-10 06:46:01.438 +10:00 [DBG] Focused segment updated to 0 (00:00:09.0298016)











        // REVIEW ISPASSEDFIRSTRANSITION LOGIC, always set to true, but timer animating correctly.

        public void WorkingDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

  

            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                SKRect leftShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);
                SKRect rightShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);

                // Animation begins one second before the column changes so we add one second.
                //TODO
                // Change ColumnAnimationInterval initialization by -1 to fix.
                //This band aid does not handle 0-1 animations.




                // The columnTargetValue == currentValue + 1 = TargetSegmentValue?
                int liveTargetValue = _animationCalculator.CalculateColumnTargetValueByElapsed(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);

   





                // Default values initialized as 0 and 1 for current and target
                // When the restartTimerElapsed was <1 , new value != targetValue
                // This mean that the current value was updated to 1 and the target updated to 0.
                // this will need reviewed and changed, what happens if timer stops and starts before one second etc. 


                /*
                if (columnTargetValue != column.TargetSegmentValue && restartTimerElapsed < column.AnimationInterval && column.PassedFirstTransition == false)
                {
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = columnTargetValue;

                }
                */



                // this will need reviewed and changed, what happens if timer stops and starts before one second etc. 
                if (liveTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                   
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = liveTargetValue;

                    column.PassedFirstTransition = true;
                    SetFocusedSegmentByColumnTargetValue(column, liveTargetValue);

                }



        

                else
                {
                    SetFocusedSegmentByColumnTargetValue(column, liveTargetValue);
                }

      

   

                // 2. Determine if this column should animate right now.
                column.IsStandardAnimationOccuring = TimeToBeginStandardAnimation(elapsed, column);

                if (column.IsStandardAnimationOccuring)
                {

                    if (column.IsColumnActive == false)
                    {
                        column.IsColumnActive = true;
                        _columnStateManager.UpdateIsNumberBlurringActive(column, true);
                        _appLogger.Debug($"IsCOlumnACtive changed from false to: {column.IsColumnActive} at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
                    }

                    if (column.IsNumberBlurringActive && elapsed >= column.AnimationInterval)
                    {
                        _columnStateManager.UpdateIsNumberBlurringActive(column, false);
                    }



                    isCircleStatic = false;

                    float animationProgress = _animationCalculator.calculateBaseAnimationProgress(elapsed);
                    float normalizedProgress = _animationCalculator.CalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.UpdateBaseAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float animatingYTranslation = _animationCalculator.CalculateYTranslation(column, elapsed, column.BaseAnimationProgress);




                    column.YTranslation = animatingYTranslation;


                    /*
                        _shadowBuilder.DrawColumnLeftShadow(canvas, leftShadowRectangle);
                    
                        _shadowBuilder.DrawColumnRightShadow(canvas, rightShadowRectangle);
                        




                    // Are timer paths being draw over each other / at the same time?
                    /*
                    WORKINGDrawTimerPaths(canvas, column, restartTimerElapsed, isCircleStatic, ColumnAnimationSetting.IsMovingUp);
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
                    WORKINGDrawTimerPaths(canvas, column, restartTimerElapsed, isCircleStatic, ColumnAnimationSetting.IsMovingUp);
                    */
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);


                    }
                }
            }








  




        public void TESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                SKRect leftShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);
                SKRect rightShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);

                if (column.IsRestarting)
                {
                    double restartElpasedSeconds = _stopWatchTimerService.GetRestartStopwatchSeconds();

                    float restartProgress = Math.Min(1f, (float)restartElpasedSeconds / 0.5f);
                    float easedProgress = _animationCalculator.CalculateEasingValue(restartProgress);
                    column.YTranslation = column.YLocationAtRestart * (1f - easedProgress);

                    if (restartProgress >= 1f)
                    {
                        column.IsRestarting = false;
                        column.YTranslation = 0;
                        column.CurrentValue = 0;
                        column.TargetSegmentValue = 1;
                        column.PassedFirstTransition = false;

                        if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                        {
                            column.IsColumnActive = true;
                            column.IsNumberBlurringActive = true;
                        }
                    }

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    continue;
                }

                int liveTargetValue = _animationCalculator.CalculateColumnTargetValueByElapsed(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);

                if (liveTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = liveTargetValue;
                    column.PassedFirstTransition = true;
                    SetFocusedSegmentByColumnTargetValue(column, liveTargetValue);
                }
                else
                {
                    SetFocusedSegmentByColumnTargetValue(column, liveTargetValue);
                }

                column.IsStandardAnimationOccuring = TimeToBeginStandardAnimation(elapsed, column);

                if (column.IsStandardAnimationOccuring)
                {
                    if (column.IsColumnActive == false)
                    {
                        column.IsColumnActive = true;
                        _columnStateManager.UpdateIsNumberBlurringActive(column, true);
                        _appLogger.Debug($"IsCOlumnACtive changed from false to: {column.IsColumnActive} at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
                    }

                    if (column.IsNumberBlurringActive && elapsed >= column.AnimationInterval)
                    {
                        _columnStateManager.UpdateIsNumberBlurringActive(column, false);
                    }

                    isCircleStatic = false;

                    float animationProgress = _animationCalculator.calculateBaseAnimationProgress(elapsed);
                    float normalizedProgress = _animationCalculator.CalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.UpdateBaseAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float animatingYTranslation = _animationCalculator.CalculateYTranslation(column, elapsed, column.BaseAnimationProgress);

                    column.YTranslation = animatingYTranslation;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                }
                else
                {
                    column.YTranslation = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
                    isCircleStatic = true;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                }
            }
        }






      




        public void NEWTESTDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            // Check if restart animation has just completed. 
            bool restartComplete = _columnStateManager.CheckAllColumnsFinishedRestart(columns);

            if (restartComplete)
            {
                _appLogger.Debug($"Restart Complete Current restartTimerElapsed : {elapsed}");
            } 


            bool isCircleStatic = true;

            foreach (var column in columns)
            {
                SKRect leftShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);
                SKRect rightShadowRectangle = _shadowBuilder.CreateRectangleForShadow(column);

                if (column.IsRestarting)
                {

                    TimeSpan restartTimerElapsed = _stopWatchTimerService.GetRestartElapsedTimeCappedAtOneSecond();
                    float restartAnimationProgress = _animationCalculator.CalculateRestartAnimationProgress(restartTimerElapsed);

                    _columnStateManager.UpdateBaseAnimationProgress(column, restartAnimationProgress);

    

                    _appLogger.Debug($"Animation progress Calculated for restart: {restartAnimationProgress}.");

                    column.YTranslation = _animationCalculator.TESTCalculateYTranslation(column, elapsed, restartAnimationProgress);

             


                    if (restartAnimationProgress >= 1f)
                    {
                        if (column.ColumnType != ColumnUnitType.SecondsSingleDigits)
                        {
                            column.IsRestarting = false;
                            column.YTranslation = 0;
                            column.CurrentValue = 0;
                            column.TargetSegmentValue = 1;
                            column.PassedFirstTransition = false;
                            column.IsColumnActive = false;
                            column.IsStandardAnimationOccuring = false;
                            column.IsNumberBlurringActive = true;
                        }

                        if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                        {
                            {
                                column.IsRestarting = false;
                                column.IsColumnActive = true;
                                column.IsStandardAnimationOccuring = true;
                                column.IsNumberBlurringActive = false;
                                column.PassedFirstTransition = false;
                                column.CurrentValue = 0;
                                column.TargetSegmentValue = 1;

                            }
                        }
                    }


                    if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {
                        _animatedLogHelper.LogColumn(column, restartTimerElapsed, ColumnAnimationType.Restart,null, null, null);
                    }

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    continue;
                }

                int liveTargetValue = _animationCalculator.CalculateColumnTargetValueByElapsed(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);

                if (liveTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                    column.CurrentValue = column.TargetSegmentValue;
                    column.TargetSegmentValue = liveTargetValue;
                    column.PassedFirstTransition = true;
                    SetFocusedSegmentByColumnTargetValue(column, liveTargetValue);
                }
                else
                {
                    SetFocusedSegmentByColumnTargetValue(column, liveTargetValue);
                }

                column.IsStandardAnimationOccuring = TimeToBeginStandardAnimation(elapsed, column);

                if (column.IsStandardAnimationOccuring)
                {
                    if (column.IsColumnActive == false)
                    {
                        column.IsColumnActive = true;
                        _columnStateManager.UpdateIsNumberBlurringActive(column, true);
                        _appLogger.Debug($"IsCOlumnACtive changed from false to: {column.IsColumnActive} at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
                    }

                    if (column.IsNumberBlurringActive && elapsed >= column.AnimationInterval)
                    {
                        _columnStateManager.UpdateIsNumberBlurringActive(column, false);
                    }

                    isCircleStatic = false;

                    float animationProgress = _animationCalculator.calculateBaseAnimationProgress(elapsed);
                    float normalizedProgress = _animationCalculator.CalculateColumnScrollProgress(animationProgress);
                    float circleAnimationProgress = _columnStateManager.TESTCalculateCircleAnimationProgress(column);

                    _columnStateManager.UpdateBaseAnimationProgress(column, animationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, normalizedProgress);
                    _columnStateManager.UpdateCircleAnimationProgress(column, circleAnimationProgress);

                    float animatingYTranslation = _animationCalculator.TESTCalculateYTranslation(column, elapsed, column.BaseAnimationProgress);

                    column.YTranslation = animatingYTranslation;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                }
                else
                {
                    column.YTranslation = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
                    isCircleStatic = true;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
                }
            }
        }



        public void RefactoredDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            bool areColumnsInRestartState = _columnStateManager.ReturnAreColumnsInRestartState();

            if (areColumnsInRestartState)
            {

                // Check is restart timer is complete(1 second).
                TimeSpan restartTimerElapsed = _stopWatchTimerService.GetRestartElapsedTimeCappedAtOneSecond();
                float restartAnimationProgress = _animationCalculator.CalculateRestartAnimationProgress(restartTimerElapsed);

                if (restartAnimationProgress >= 1 )
                {
                    // Set the properties of each indiviaul column to its correct state
                    _columnStateManager.UpdateColumnStateWhenRestartComplete(columns);

                    // Check each indivdual column is no longer isRestarting
                    bool allColumnsFinishedRestarting = _columnStateManager.CheckAllColumnsOutOfRestartState(columns);

               
                    if (allColumnsFinishedRestarting)
                    {
                        // Update global/controller bool IsTimerRestarting and stop restart time & restart session timer
                        _columnStateManager.UpdateStateAndTimerWhenRestartComplete();
                    }
                }
                DrawRestartAnimationForAllColumns(canvas, restartTimerElapsed, columns, restartAnimationProgress);
 
                return;
            }

            // Restart animation is calculated with the same values for all columns, since normal animation works differently for each we need to loop through each one. 
            foreach (AnimatedTimerColumn column in columns)
            {
                int columnTargetValue = _animationCalculator.CalculateColumnTargetValueByElapsed(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);


                // Handle assigning the correct target value in scenario outlined in SetColumnStateForFirstTransitionAnimation definition.

                if (columnTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                    SetColumnStateForFirstTransitionAnimation(column, columnTargetValue, elapsed);
                }
              

                else
                {
                    // Set the columnTargetValue to the one calculated by 
                    SetFocusedSegmentByColumnTargetValue(column, columnTargetValue);
                }
                  


                // Now check to see if it is the colummns time to animate(is restartTimerElapsed >= Column.animationInerval - 1
                column.IsStandardAnimationOccuring = TimeToBeginStandardAnimation(elapsed, column);


                if (column.IsStandardAnimationOccuring)
                {
                    // Record the point when column changes from inactive to active, the active flag is used for showing column active colors
                    if(column.IsColumnActive == false)
                    {
                        _appLogger.Info($"Column : {column.ColumnType} changed to active at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
                    }

                    // Update the column to active and disable number blurring.
                    _columnStateManager.UpdateIsColumnActive(column, true);
                    _columnStateManager.UpdateIsNumberBlurringActive(column, false);

                    // Calculate the 
                    float baseAnimationProgress = _animationCalculator.calculateBaseAnimationProgress(elapsed);
                    float columnScrollProgress = _animationCalculator.CalculateColumnScrollProgress(baseAnimationProgress);


                    _columnStateManager.UpdateBaseAnimationProgress(column, baseAnimationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, columnScrollProgress);

              
                    column.YTranslation = _animationCalculator.TESTCalculateYTranslation(column, elapsed, column.BaseAnimationProgress);

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);
         
                }
                else
                {
                    // Else we just draw columns at the same location.

                    

                    column.YTranslation = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
               
                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);

                    if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {
                        
                        _animatedLogHelper.LogColumn(column, elapsed, ColumnAnimationType.Static,null,null, null);
                    }
           
                }


            }
        }





        // Default values initialized as 0 and 1 for current and target
        // When the restartTimerElapsed was <1 , new value != targetValue
        // This mean that the current value was updated to 1 and the target updated to 0.
        public void SetColumnStateForFirstTransitionAnimation(AnimatedTimerColumn column, int columnTargetValue, TimeSpan elapsed)
        {
      


            column.CurrentValue = column.TargetSegmentValue;
            column.TargetSegmentValue = columnTargetValue;
            column.PassedFirstTransition = true;
            SetFocusedSegmentByColumnTargetValue(column, columnTargetValue);


        }


        public void TESTSetColumnStateForFirstTransitionAnimation(AnimatedTimerColumn column, int columnTargetValue, TimeSpan elapsed)
        {

            if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                int newCurrentValue = column.TargetSegmentValue - 1;

                column.CurrentValue = newCurrentValue;
                column.TargetSegmentValue = columnTargetValue;
                column.PassedFirstTransition = true;
                SetFocusedSegmentByColumnTargetValue(column, newCurrentValue);
            }
            else
            {
                column.CurrentValue = column.TargetSegmentValue;
                column.TargetSegmentValue = columnTargetValue;
                column.PassedFirstTransition = true;
                SetFocusedSegmentByColumnTargetValue(column, columnTargetValue);
            }

       

        }



  




        public void DrawRestartAnimationForAllColumns(SKCanvas canvas, TimeSpan restartTimerElapsed, List<AnimatedTimerColumn> columns, float restartAnimationProgress)
        {
    
            foreach (var column in columns)
            {
                _columnStateManager.UpdateRestartAnimationProgress(column, restartAnimationProgress);

                column.YTranslation = _animationCalculator.TESTCalculateYTranslation(column, restartTimerElapsed, restartAnimationProgress);

                DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);


                if(column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                {
                    _animatedLogHelper.LogColumn(column, restartTimerElapsed, ColumnAnimationType.Restart ,null, null, null);
                }
            }



        }






        public void TESTRefactoredDraw(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
        {
            canvas.Clear(AnimatedColumnSettings.FormBackgroundColor);

            bool areColumnsInRestartState = _columnStateManager.ReturnAreColumnsInRestartState();

            if (areColumnsInRestartState)
            {

                // Check is restart timer is complete(1 second).
                TimeSpan restartTimerElapsed = _stopWatchTimerService.GetRestartElapsedTimeCappedAtOneSecond();
                float restartAnimationProgress = _animationCalculator.CalculateRestartAnimationProgress(restartTimerElapsed);

                if (restartAnimationProgress >= 1)
                {
                    // Set the properties of each indiviaul column to its correct state
                    _columnStateManager.UpdateColumnStateWhenRestartComplete(columns);

                    // Check each indivdual column is no longer isRestarting
                    bool allColumnsFinishedRestarting = _columnStateManager.CheckAllColumnsOutOfRestartState(columns);


                    if (allColumnsFinishedRestarting)
                    {
                        // Update global/controller bool IsTimerRestarting and stop restart time & restart session timer
                        _columnStateManager.UpdateStateAndTimerWhenRestartComplete();
                    }
                }
                DrawRestartAnimationForAllColumns(canvas, restartTimerElapsed, columns, restartAnimationProgress);

                return;
            }

            // Restart animation is calculated with the same values for all columns, since normal animation works differently for each we need to loop through each one. 
            foreach (AnimatedTimerColumn column in columns)
            {
                int columnTargetValue = _animationCalculator.CalculateColumnTargetValueByElapsed(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);


                // Handle assigning the correct target value in scenario outlined in SetColumnStateForFirstTransitionAnimation definition.

                if (columnTargetValue != column.TargetSegmentValue || elapsed < TimeSpan.FromSeconds(1) || column.PassedFirstTransition != true)
                {
                    SetColumnStateForFirstTransitionAnimation (column, columnTargetValue, elapsed);
                }


                else
                {
                    // Set the columnTargetValue to the one calculated by 
                    SetFocusedSegmentByColumnTargetValue(column, columnTargetValue);
                }



                // Now check to see if it is the colummns time to animate(is restartTimerElapsed >= Column.animationInerval - 1
                column.IsStandardAnimationOccuring = TimeToBeginStandardAnimation(elapsed, column);


                if (column.IsStandardAnimationOccuring)
                {
                    // Record the point when column changes from inactive to active, the active flag is used for showing column active colors
                    if (column.IsColumnActive == false)
                    {
                        _appLogger.Info($"Column : {column.ColumnType} changed to active at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
                    }

                    // Update the column to active and disable number blurring.
                    _columnStateManager.UpdateIsColumnActive(column, true);
                    _columnStateManager.UpdateIsNumberBlurringActive(column, false);

                    // Calculate the 
                    float baseAnimationProgress = _animationCalculator.calculateBaseAnimationProgress(elapsed);
                    float columnScrollProgress = _animationCalculator.CalculateColumnScrollProgress(baseAnimationProgress);


                    _columnStateManager.UpdateBaseAnimationProgress(column, baseAnimationProgress);
                    _columnStateManager.WORKINGUpdateColumnScrollProgress(column, columnScrollProgress);


                    column.YTranslation = _animationCalculator.TESTCalculateYTranslation(column, elapsed, column.BaseAnimationProgress);

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);

                }
                else
                {
                    // Else we just draw columns at the same location.



                    column.YTranslation = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;

                    DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);

                    if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
                    {

                        _animatedLogHelper.LogColumn(column, elapsed, ColumnAnimationType.Static, null, null, null);
                    }

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



