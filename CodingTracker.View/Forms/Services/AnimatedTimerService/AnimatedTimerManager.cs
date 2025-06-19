using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory.CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing.Text;

// TODO: Change AnimationPhaseCalculator, define a single animation method, apply at timer intervals.


namespace CodingTracker.View.Forms.Services.AnimatedTimerService
{
    public interface IAnimatedTimerManager
    {
        void InitializeColumns();
        void UpdateAndRender(SKControl skControl);
        void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e);

        List<AnimatedTimerColumn> ReturnTimerColumns();
    }

    public class AnimatedTimerManager : IAnimatedTimerManager
    {
        public SKControl SkTimerControl { get; private set; }
        private readonly IStopWatchTimerService _stopWatchService;
        private readonly IAnimatedTimerRenderer _animatedRenderer;
        private readonly IAnimatedTimerColumnFactory _animatedColumnFactory;
        private readonly IApplicationLogger _appLogger;
        private List<AnimatedTimerColumn> _columns;


        public AnimatedTimerManager(IStopWatchTimerService stopWatchService, IAnimatedTimerRenderer animatedRenderer, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IApplicationLogger appLogger)
        {
            _stopWatchService = stopWatchService;
            _animatedRenderer = animatedRenderer;
            _animatedColumnFactory = animatedTimerColumnFactory;
            _appLogger = appLogger;
            _columns = new List<AnimatedTimerColumn>(); 
  
        }



        public void UpdateAndRender(SKControl skControl)
        {
            skControl.Invalidate();
        }

  

        public void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {



            var canvas = e.Surface.Canvas;
            var bounds = e.Info.Rect;
            var elapsed = _stopWatchService.ReturnElapsedTimeSpan();

            _animatedRenderer.Draw(canvas, bounds, elapsed, _columns);
        }


        public void LogColumnPosition(AnimatedTimerColumn column)
        {
            _appLogger.Debug($"{column.ColumnType.ToString()} location: X = {column.Location.X.ToString()}, Y = {column.Location.Y.ToString()}");
        }

        public void LogXPosition(float spacing)
        {
            _appLogger.Debug($"Spacing: {spacing}");    
        }

        public void InitializeColumns()
        {
            float xPosition = 50;
            float spacing = 150;

            List<AnimatedTimerColumn> columns = new List<AnimatedTimerColumn>();

            var secondsSingleDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, 100)), ColumnUnitType.SecondsSingleDigits);
            xPosition += spacing;
            LogXPosition(xPosition);

            LogColumnPosition(secondsSingleDigits);

            var secondsLeadingDigit = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, 100)), ColumnUnitType.SecondsLeadingDigit);
            xPosition += spacing;
      

            var minutesSingleDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, 100)), ColumnUnitType.MinutesSingleDigits);
            xPosition += spacing;

            var minutesLeadingDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, spacing)), ColumnUnitType.MinutesLeadingDigits);
            xPosition += spacing;

            var hoursSinlgeDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, spacing)), ColumnUnitType.HoursSinglesDigits);
            xPosition += spacing;
            var hoursLeadingDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, spacing)), ColumnUnitType.HoursLeadingDigits);

            _columns.Add(secondsSingleDigits);
            _columns.Add(secondsLeadingDigit);
            _columns.Add(minutesSingleDigits);
            _columns.Add(minutesLeadingDigits);
            _columns.Add(hoursSinlgeDigits);
            _columns.Add(hoursLeadingDigits);

            _appLogger.Debug($"InitializeColumns complete number of columns in _columns: {_columns.Count}");

            var segmentValuess = secondsSingleDigits.TimerSegments.Select(x => x.Value).ToList();

            _appLogger.Debug($"Values for secondsColumn: {string.Join("", segmentValuess)} ");

            







            // Remmove after tests!!!!!!!!!
            _stopWatchService.StartTimer();



        }


     

        public List<AnimatedTimerColumn> ReturnTimerColumns()
        {
            return _columns;
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
}