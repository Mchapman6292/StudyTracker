using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService
{
    public interface IAnimatedTimerManager
    {
        void Initialize(SKControl control, IStopWatchTimerService stopWatchService);
        void UpdateAndRender();
        void SetHighlight(ITimerHighlight highlight);
    }

    public class AnimatedTimerManager : IAnimatedTimerManager
    {
        public SKControl SkTimerControl { get; private set; }
        private readonly IStopWatchTimerService _stopWatchService;
        private readonly IAnimatedTimerRenderer _animatedRenderer;
        private readonly IHighlightManager _highlightManager;
        private List<AnimatedTimerColumn> _columns;


        public AnimatedTimerManager(SKControl skTimerControl, IStopWatchTimerService stopWatchService, IAnimatedTimerRenderer animatedRenderer, IHighlightManager highlightManager, List<AnimatedTimerColumn> columns)
        {
            SkTimerControl = skTimerControl;
            _stopWatchService = stopWatchService;
            _animatedRenderer = animatedRenderer;
            _highlightManager = highlightManager;
            _columns = columns;
        }

        public void Initialize(SKControl control, IStopWatchTimerService stopWatchService)
        {
            SkTimerControl = control;

            _columns = CreateTimerColumns();
            SkTimerControl.PaintSurface += OnPaintSurface;
        }

        public void UpdateAndRender()
        {
            SkTimerControl.Invalidate();
        }

        public void SetHighlight(ITimerHighlight highlight)
        {
            _highlightManager?.SetHighlight(highlight);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var bounds = e.Info.Rect;
            var elapsed = _stopWatchService.ReturnElapsedTimeSpan();

            _animatedRenderer.Draw(canvas, bounds, elapsed);
        }

        private List<AnimatedTimerColumn> CreateTimerColumns()
        {
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