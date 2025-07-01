using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IPaintManager
    {
        SKPaint CreateInnerSegmentPaint();
        SKPaint CreateOuterSegmentPaint(float circleAnimationProgress);
    }

    public class PaintManager : IPaintManager
    {
        public bool IsEnabled { get; set; }

        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmwentOverlayCalculator;
        private readonly IAnimatedColumnStateManager _columnStateManager;


        public PaintManager(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverlayCalculator, IAnimatedColumnStateManager columnStateManager)
        {
            _appLogger = appLogger;
            _segmwentOverlayCalculator = segmentOverlayCalculator;
            _columnStateManager = columnStateManager;
        }


        private float CalculateOpacityMultiplier(float circleAnimationProgress)
        {
            return 1.0f - circleAnimationProgress;
        }

        private float TESTCalculateOpacityMultiplier(float circleAnimationProgress)
        {
            if (circleAnimationProgress < 0.7f)
            {
                return 1.0f; 
            }
            else if (circleAnimationProgress < 0.9f)
            {
                float fadeProgress = (circleAnimationProgress - 0.7f) / 0.2f; 
                return Single.Lerp(1.0f, 0.0f, fadeProgress);
            }
            else
            {
                return 0.0f; 
            }
        }



        public SKPaint CreateInnerSegmentPaint()
        {
            return new SKPaint()
            {
                Color = new SKColor(255, 255, 255, 40).WithAlpha(AnimatedColumnSettings.OuterCircleOpacity),
                Style = SKPaintStyle.Fill,
                IsAntialias = true

            };
        }

        public SKPaint CreateOuterSegmentPaint(float circleAnimationProgress)
        {
            float opacityMultiplier = CalculateOpacityMultiplier(circleAnimationProgress);
            
            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {
                Color = new SKColor(255, 255, 255, 40),
                Style = SKPaintStyle.Fill,

                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true,


            };
        }

    

    }
}
