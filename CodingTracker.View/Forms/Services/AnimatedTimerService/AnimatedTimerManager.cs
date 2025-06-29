using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing.Text;

// TODO: Change AnimationPhaseCalculator, define a single animation method, apply at timer intervals.


namespace CodingTracker.View.Forms.Services.AnimatedTimerService
{
    public interface IAnimatedTimerManager
    {
        void InitializeColumns(Form targetForm);
        void UpdateAndRender(SKControl skControl);
        void DrawColumnsOnTick(object sender, SKPaintSurfaceEventArgs e);

        List<AnimatedTimerColumn> ReturnTimerColumns();
    }

    public class AnimatedTimerManager : IAnimatedTimerManager
    {
        public SKControl SkTimerControl { get; private set; }
        private readonly IStopWatchTimerService _stopWatchService;
        private readonly IAnimatedTimerRenderer _animatedRenderer;
        private readonly IAnimatedTimerColumnFactory _animatedColumnFactory;
        private readonly IApplicationLogger _appLogger;
        private readonly ICircleHighLight _circleHighlight;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalculator;
        private List<AnimatedTimerColumn> _columns;


        public AnimatedTimerManager(IStopWatchTimerService stopWatchService, IAnimatedTimerRenderer animatedRenderer, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IApplicationLogger appLogger, ICircleHighLight circleHighLight,ISegmentOverlayCalculator segmentOverlayCalculator)
        {
            _stopWatchService = stopWatchService;
            _animatedRenderer = animatedRenderer;
            _animatedColumnFactory = animatedTimerColumnFactory;
            _appLogger = appLogger;
            _circleHighlight = circleHighLight;
            _columns = new List<AnimatedTimerColumn>(); 
  
        }



        public void UpdateAndRender(SKControl skControl)
        {
            skControl.Invalidate();
        }

  

        public void DrawColumnsOnTick(object sender, SKPaintSurfaceEventArgs e)
        {

            var canvas = e.Surface.Canvas;
            var bounds = e.Info.Rect;
            var elapsed = _stopWatchService.ReturnElapsedTimeSpan();

            _animatedRenderer.NEWDraw(canvas, bounds, elapsed, _columns);
        }


        public void LogColumnPosition(AnimatedTimerColumn column)
        {
            _appLogger.Debug($"{column.ColumnType.ToString()} location: X = {column.Location.X.ToString()}, Y = {column.Location.Y.ToString()} \n AnimationInterval: {column.AnimationInterval.ToString()}");
        }

        public void LogXPosition(float spacing)
        {
            _appLogger.Debug($"Spacing: {spacing}");    
        }

        public void InitializeColumns(Form targetForm)
        {
            float xPosition = 50;
            float spacing = 100;

            int pageHalfwayPoint = targetForm.Location.Y + 150;


            List<AnimatedTimerColumn> columns = new List<AnimatedTimerColumn>();

            var secondsSingleDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, pageHalfwayPoint)), ColumnUnitType.SecondsSingleDigits);
            xPosition += spacing;

            _appLogger.Debug($"\n \n ANIMATION INTERVAL SET TO  {secondsSingleDigits.AnimationInterval} FOR secondsSingleDigits.");
            LogXPosition(xPosition);

            LogColumnPosition(secondsSingleDigits);

            var secondsLeadingDigit = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToSixDigit, (new SKPoint(xPosition, pageHalfwayPoint)), ColumnUnitType.SecondsLeadingDigit);
            xPosition += spacing;
      

            var minutesSingleDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, pageHalfwayPoint)), ColumnUnitType.MinutesSingleDigits);
            xPosition += spacing;

            _appLogger.Debug($"\n \n ANIMATION INTERVAL SET TO  {minutesSingleDigits.AnimationInterval} FOR minutesSingleDigits.");

            var minutesLeadingDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToSixDigit, (new SKPoint(xPosition, pageHalfwayPoint)), ColumnUnitType.MinutesLeadingDigits);
            xPosition += spacing;

 


            var hoursSinlgeDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, pageHalfwayPoint)), ColumnUnitType.HoursSinglesDigits);


            _appLogger.Debug($"\n \n ANIMATION INTERVAL SET TO  {hoursSinlgeDigits.AnimationInterval} FOR hoursSinlgeDigits.");
            xPosition += spacing;
            var hoursLeadingDigits = _animatedColumnFactory.CreateColumnWithSegments(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, pageHalfwayPoint)), ColumnUnitType.HoursLeadingDigits);

            LogColumnPosition(hoursLeadingDigits);

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






        public void DrawCircleSegmentHighlight()
        {

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