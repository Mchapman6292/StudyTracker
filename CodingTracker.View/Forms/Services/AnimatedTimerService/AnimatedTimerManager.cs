using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory.CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing.Text;

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

        public void InitializeColumns()
        {
            float xPosition = 50;
            float spacing = 80;

            List<AnimatedTimerColumn> columns = new List<AnimatedTimerColumn>();

            var secondsSingleDigits = _animatedColumnFactory.CreateColumn(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, 100)));
            xPosition += spacing;
            var secondsLeadingDigit = _animatedColumnFactory.CreateColumn(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, 100)));
            xPosition += spacing;

            var minutesSingleDigits = _animatedColumnFactory.CreateColumn(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, 100)));
            xPosition -= spacing;
            var minutesLeadingDigits = _animatedColumnFactory.CreateColumn(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, spacing)));
            xPosition -= spacing;

            var hoursSinlgeDigits = _animatedColumnFactory.CreateColumn(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, spacing)));
            xPosition -= spacing;
            var hoursLeadingDigits = _animatedColumnFactory.CreateColumn(AnimatedColumnSettings.OneToNineDigit, (new SKPoint(xPosition, spacing)));

            _columns.Add(secondsSingleDigits);
            _columns.Add(secondsLeadingDigit);
            _columns.Add(minutesSingleDigits);
            _columns.Add(minutesLeadingDigits);
            _columns.Add(hoursSinlgeDigits);
            _columns.Add(hoursLeadingDigits);

            _appLogger.Debug($"InitializeColumns complete number of columns in _columns: {_columns.Count}");

       

            



        }


        private void CreateTimerColumns()
        {
            /*
            var columns = new List<AnimatedTimerColumn>();

            float xPosition = 50;
            float spacing = 80;

            columns.Add(new AnimatedTimerColumn
            {
                TimerLocation = new SKPoint(xPosition, 100),
                ColumnType = TimeUnit.HoursTens
            });
            xPosition += spacing;

            columns.Add(new AnimatedTimerColumn
            {
                TimerLocation = new SKPoint(xPosition, 100),
                ColumnType = TimeUnit.HoursOnes
            });
            xPosition += spacing + 20;

            columns.Add(new AnimatedTimerColumn
            {
                TimerLocation = new SKPoint(xPosition, 100),
                ColumnType = TimeUnit.MinutesTens
            });
            xPosition += spacing;

            columns.Add(new AnimatedTimerColumn
            {
                TimerLocation = new SKPoint(xPosition, 100),
                ColumnType = TimeUnit.MinutesOnes
            });
            xPosition += spacing + 20;

            columns.Add(new AnimatedTimerColumn
            {
                TimerLocation = new SKPoint(xPosition, 100),
                ColumnType = TimeUnit.SecondsTens
            });
            xPosition += spacing;

            columns.Add(new AnimatedTimerColumn
            {
                TimerLocation = new SKPoint(xPosition, 100),
                ColumnType = TimeUnit.SecondsOnes
            });

            return columns;
            */
        }

        public List<AnimatedTimerColumn> ReturnTimerColumns()
        {
            return _columns;
        }
    }

 

    public enum TimeUnit
    {
        HoursTens,
        HoursOnes,
        MinutesTens,
        MinutesOnes,
        SecondsTens,
        SecondsOnes
    }
}