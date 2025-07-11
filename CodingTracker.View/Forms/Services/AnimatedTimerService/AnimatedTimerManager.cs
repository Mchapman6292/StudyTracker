﻿using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Collections.Generic;
using System.Drawing.Text;

// TODO: Change AnimationPhaseCalculator, define a single animation method, apply at timer intervals.


namespace CodingTracker.View.Forms.Services.AnimatedTimerService
{
    public interface IAnimatedTimerManager
    {
        void InitializeColumns(Form targetForm);
        void UpdateAndRender(SKControl skControl);
        void DrawColumnsOnTick(object sender, SKPaintSurfaceEventArgs e);

        void LogColumnBools();
        List<AnimatedTimerColumn> ReturnTimerColumns();
    }

    public class AnimatedTimerManager : IAnimatedTimerManager
    {
        public SKControl SkTimerControl { get; private set; }
        private readonly IStopWatchTimerService _stopWatchService;
        private readonly IAnimatedTimerRenderer _animatedRenderer;
        private readonly IAnimatedTimerColumnFactory _animatedColumnFactory;
        private readonly IApplicationLogger _appLogger;
        private readonly IPaintManager _circleHighlight;
        private List<AnimatedTimerColumn> _columns;


        public AnimatedTimerManager(IStopWatchTimerService stopWatchService, IAnimatedTimerRenderer animatedRenderer, IAnimatedTimerColumnFactory animatedTimerColumnFactory, IApplicationLogger appLogger, IPaintManager circleHighLight)
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

            _animatedRenderer.TestDraw(canvas, elapsed, _columns);
        }




        private void DefineStartingYPosition(Form targetForm)
        {
            float startingX = 50;

            int startingY = targetForm.Location.Y + 150;
        }

        public void InitializeColumns(Form targetForm)
        {
            float startingX = 350;

            int startingY = targetForm.Location.Y + 150;



            _columns = _animatedColumnFactory.CreateColumnsWithPositions(startingX, startingY);

            foreach (var column in _columns)
            {
                var max = column.TimerSegments.Max(segment => segment.Value);
                var min = column.TimerSegments.Min(segment => segment.Value);
                _appLogger.Debug($"Column: {column.ColumnType} created with SegmentCount: {column.TimerSegments.Count}, min Value : {min}, Max value : {max}.");
            }

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