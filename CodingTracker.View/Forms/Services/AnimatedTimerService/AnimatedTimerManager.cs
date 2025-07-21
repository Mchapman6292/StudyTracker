using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
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

        void UpdateColumnStatesOnRestartButtonClick();




    }

    public class AnimatedTimerManager : IAnimatedTimerManager
    {
        public SKControl SkTimerControl { get; private set; }
        private readonly IStopWatchTimerService _stopWatchService;
        private readonly IAnimatedTimerRenderer _animatedRenderer;
        private readonly IAnimatedTimerColumnFactory _animatedColumnFactory;
        private readonly IApplicationLogger _appLogger;
        private readonly IPaintManager _circleHighlight;
        private readonly IRenderingCalculator _renderingCalculator;
        private List<AnimatedTimerColumn> _columns;





        public AnimatedTimerManager(IStopWatchTimerService stopWatchService, IAnimatedTimerRenderer animatedRenderer, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IApplicationLogger appLogger, IPaintManager circleHighLight, IRenderingCalculator renderingCalculator)
        {
            _stopWatchService = stopWatchService;
            _animatedRenderer = animatedRenderer;
            _animatedColumnFactory = animatedTimerColumnFactory;
            _appLogger = appLogger;
            _circleHighlight = circleHighLight;
            _renderingCalculator = renderingCalculator;
            _columns = new List<AnimatedTimerColumn>(); 
  
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

            _animatedRenderer.NEWTESTDraw(canvas, elapsed, _columns);
        }



        

        private float CalculateStartingYLocation(float formHeight)
        {
            return formHeight / 2 - 100;
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

















        public void LogColumn(AnimatedTimerColumn column, TimeSpan elapsed, float? initialYLocation, float? easedProgress, float? yTranslation)
        {
            string logMessage = $"\n \n"
                                + $"\n-----LOGGING COLUMN {column.ColumnType} AT ELAPSED {FormatElapsedTimeSPan(elapsed)}-----"
                                + $"\n-----Current Value : {column.CurrentValue}, Target Value : {column.TargetSegmentValue}.-----"
                                + $"\n-----IsAnimating: {column.IsAnimating}.-----"
                                + $"\n-----BaseAnimationProgress: {column.BaseAnimationProgress}, ColumnScrollProgress: {column.ColumnScrollProgress}, CircleAnimationProgress: {column.CircleAnimationProgress}.-----"
                                + $"\n-----Max Value: {column.MaxValue}, TotalSegmentCount: {column.TotalSegmentCount}, TimerSegments.Count: {column.TimerSegments.Count()}.-----"
                                + $"\n------PassedFirstTransition: {column.PassedFirstTransition.ToString()}."
                                + $"\n------- IsRestarting: {column.IsRestarting}. RestartYLocation: {column.YLocationAtRestart}. ";


            if (initialYLocation != null && easedProgress != null && yTranslation != null)
            {
                logMessage +=
                $"\n---- OFFSET CALCULATION FOR COLUMN: {column.ColumnType}.-----"
                + $"\n---- InitialYLocation: {initialYLocation}, Easing Value: {easedProgress}, YTranslation: {yTranslation}.-----\n\n";
            }

            _appLogger.Info(logMessage);
        }

        public string FormatElapsedTimeSPan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }





        public List<AnimatedTimerColumn> ReturnTimerColumns()
        {
            return _columns;
        }










        public void UpdateColumnStatesOnRestartButtonClick()
        {
            _stopWatchService.ResetRestartTimer();
            var span = _stopWatchService.ReturnElapsedTimeSpan();

            foreach (var column in _columns)
            {
                column.IsRestarting = true;
                column.YLocationAtRestart = column.YTranslation; 
                column.IsAnimating = true;
                column.IsColumnActive = true;


                LogColumn(column, span, null, null, null);
            }
        }
    }










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
    
