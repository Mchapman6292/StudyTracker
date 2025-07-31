using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using Microsoft.EntityFrameworkCore.Storage.Json;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;
using System.Runtime.InteropServices;

// TODO: Change AnimationPhaseCalculator, define a single animation method, apply at timer intervals.


namespace CodingTracker.View.Forms.Services.AnimatedTimerService
{
    public interface IAnimatedTimerManager
    {
        void InitializeColumns(Form targetForm, TimerPlaceHolderForm timerPlaceHolderForm);
        void UpdateAndRender(SKControl skControl);
        void DrawColumnsOnTick(object sender, SKPaintSurfaceEventArgs e);


        void LogColumnBools();
        List<AnimatedTimerColumn> ReturnTimerColumns();






    }



    public enum ColumnUnitType
    {
        HoursLeadingDigits,
        HoursSinglesDigits,
        MinutesLeadingDigits,
        MinutesSingleDigits,
        SecondsLeadingDigit,
        SecondsSingleDigits
    }


    public class AnimatedTimerManager : IAnimatedTimerManager
    {
        public SKControl SkTimerControl { get; private set; }
        private readonly IStopWatchTimerService _stopWatchService;
        private readonly IAnimatedTimerRenderer _animatedRenderer;
        private readonly IAnimatedTimerColumnFactory _animatedColumnFactory;
        private readonly IApplicationLogger _appLogger;
        private readonly IPaintManager _circleHighlight;
        private readonly IAnimationCalculator _animationCalculator;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IAnimatedColumnStateManager _columnStateManager;
        private readonly IAnimatedTimerRenderer _animatedTimerRenderer;
        private readonly IAnimatedLogHelper _animatedLogHelper;

        private List<AnimatedTimerColumn> _columns;



       




        public AnimatedTimerManager(IStopWatchTimerService stopWatchService, IAnimatedTimerRenderer animatedRenderer, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IApplicationLogger appLogger, IPaintManager circleHighLight, IAnimationCalculator renderingCalculator, IStopWatchTimerService stopWatchTimerService, IAnimatedColumnStateManager animatedColumnStateManager, IAnimatedTimerRenderer animatedTimerRenderer, IAnimatedLogHelper animatedLogHelper)
        {
            _stopWatchService = stopWatchService;
            _animatedRenderer = animatedRenderer;
            _animatedColumnFactory = animatedTimerColumnFactory;
            _appLogger = appLogger;
            _circleHighlight = circleHighLight;
            _animationCalculator = renderingCalculator;
            _columnStateManager = animatedColumnStateManager;
            _stopWatchTimerService = stopWatchTimerService; 
            _animatedTimerRenderer = animatedTimerRenderer;
            _animatedLogHelper = animatedLogHelper;
            _columns = new List<AnimatedTimerColumn>();

        }






        public void DrawTimer(SKCanvas canvas, TimeSpan elapsed, List<AnimatedTimerColumn> columns)
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
                    _columnStateManager.UpdateColumnsWhenRestartComplete(columns);

                    // Check each indivdual column is no longer isRestarting
                    bool allColumnsFinishedRestarting = _columnStateManager.CheckAllColumnsOutOfRestartState(columns);


                    if (allColumnsFinishedRestarting)
                    {
                        // Update global/controller bool IsTimerRestarting and stop restart time & restart session timer
                        _columnStateManager.UpdateStateAndTimerWhenRestartComplete();
                        return;
                    }
                }
                _animatedTimerRenderer.DrawRestartAnimationForAllColumns(canvas, restartTimerElapsed, columns, restartAnimationProgress);
                return;
            }

            // Restart animation is calculated with the same values for all columns, since normal animation works differently for each we need to loop through each one. 
            foreach (AnimatedTimerColumn column in columns)
            {

                // We use this method to extract the time digit for the corresponding column, this method will always return a value between 0-9, we can calculate this for each column inside the loop as we use the same elapsed value. 
                int targetDigitAtCurrentElapsedTime = _animationCalculator.CalculateTargetDigitAtCurrentElapsedTime(elapsed + TimeSpan.FromSeconds(1), column.ColumnType);

                // If the targetDigitAtCurrentElapsed update the column & segment values. 
                if (targetDigitAtCurrentElapsedTime != column.TargetDigit && !column.IsRestarting)
                {
                    // These are used to update the column position, numbers etc.
                    _columnStateManager.UpdateColumnActiveDigit(column, column.TargetDigit);
                    _columnStateManager.UpdateTargetSegmentValue(column, targetDigitAtCurrentElapsedTime);
                }


                // Now check to see if it is the colummns time to animate(is restartTimerElapsed >= Column.animationInerval - 1
                column.IsStandardAnimationOccurring = _animatedTimerRenderer.TimeToBeginStandardAnimation(elapsed, column);


                if (column.IsStandardAnimationOccurring)
                {
                    // Record the point when column changes from inactive to active, the active flag is used for showing column active colors
                    if (column.IsColumnActive == false)
                    {
                        _columnStateManager.UpdateIsColumnActive(column, true);
                        _columnStateManager.UpdateIsNumberBlurringActive(column, false);
                        _appLogger.Info($"Column : {column.ColumnType} changed to active at {_animatedLogHelper.FormatElapsedTimeSpan(elapsed)}");
                    }



                    // Update the column to active and disable number blurring.


                    // Calculate the progress values for the the animation over one second and the column scroll progress which is baseAnimation + easing method.
                    float baseAnimationProgress = _animationCalculator.calculateBaseAnimationProgress(elapsed);
                    float columnScrollProgress = _animationCalculator.CalculateColumnScrollProgress(baseAnimationProgress);



                    _columnStateManager.UpdateBaseAnimationProgress(column, baseAnimationProgress);
                    _columnStateManager.UpdateColumnScrollProgress(column, columnScrollProgress);


                    column.YTranslation = _animationCalculator.TESTCalculateYTranslation(column, elapsed, column.BaseAnimationProgress);

                    _animatedTimerRenderer.DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    _animatedTimerRenderer.DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);

                }
                else
                {
                    // Else we just draw columns at the same location.
                    _animatedTimerRenderer.DrawRoundedColumn(canvas, column, ColumnAnimationSetting.IsMovingUp);
                    _animatedTimerRenderer.DrawColumnNumbers(canvas, column, ColumnAnimationSetting.IsMovingUp);


                }
            }
        }








        public void UpdateAndRender(SKControl skControl)
        {
            skControl.Invalidate();

        }

        public void TestLogSegmentPosition()
        {
            var segment = _columns.FirstOrDefault(c => c.ColumnType == ColumnUnitType.SecondsSingleDigits)?.TimerSegments.FirstOrDefault(s => s.Value == 2);

            _appLogger.Debug($"Segment position : X = {segment.Location.X}   Y = {segment.Location.Y}");
        }

        public void LogColumnBools()
        {
            string message = string.Empty;

            foreach (var column in _columns)
            {
                message += $"ColumnType: {column.ColumnType}, PassedFirstTransition: {column.PassedFirstTransition.ToString()}\n";
            }
            _appLogger.Info(message);
        }


        public void SortColumnsList()
        {

        }


        public void DrawColumnsOnTick(object sender, SKPaintSurfaceEventArgs e)
        {

            var canvas = e.Surface.Canvas;
            var bounds = e.Info.Rect;
            var elapsed = _stopWatchService.ReturnElapsedTimeSpan();
  

            DrawTimer(canvas, elapsed, _columns);
        }








        public void InitializeColumns(Form targetForm, TimerPlaceHolderForm timerPlaceHolderForm)
        {
            float startingX = AnimatedColumnSettings.StartingXPosition;

            int startingY = targetForm.Height / 2 - 100;



            _columns = _animatedColumnFactory.TESTCreateColumnsWithPositions(startingX, startingY, timerPlaceHolderForm);

            foreach (var column in _columns)
            {
                var max = column.TimerSegments.Max(segment => segment.Value);
                var min = column.TimerSegments.Min(segment => segment.Value);
                _appLogger.Debug($"Column: {column.ColumnType} created with SegmentCount: {column.TimerSegments.Count}, startingY: {startingY}. Location: X: {column.Location.X} Y: {column.Location.Y}. ");
            }
        }





        public List<AnimatedTimerColumn> ReturnTimerColumns()
        {
            return _columns;
        }





   















    }



}
 
    
