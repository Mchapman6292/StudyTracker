using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.Painting;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IPaintManager
    {
        SKPaint CreateInnerSegmentPaint(AnimatedTimerColumn column);
        SKPaint CreateOuterSegmentPaint(AnimatedTimerColumn column);
        SKPaint CreateColumnPaint();
        SKPaint CreateNumberPaint();
        SKFont CreateNumberFont();
    }

    public class PaintManager : IPaintManager
    {

        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedColumnStateManager _columnStateManager;


        public PaintManager(IApplicationLogger appLogger, IAnimatedColumnStateManager columnStateManager)
        {
            _appLogger = appLogger;
            _columnStateManager = columnStateManager;
        }


        private float CalculateOpacityMultiplier(AnimatedTimerColumn column)
        {
            return 1.0f - column.CircleAnimationProgress;
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



        public SKPaint CreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {
                /*
                Color = AnimatedColumnSettings.CatppuccinPink,
                */
                Color = SKColors.Green,
                Style = SKPaintStyle.Fill,
                IsAntialias = true

            };
        }






        public SKPaint CreateOuterSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateOpacityMultiplier(column);
            byte shadowAlpha = (byte)(opacityMultiplier * 40);
            
            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {
                
                Color = new SKColor(49, 50, 68).WithAlpha(alpha),
                Style = SKPaintStyle.Fill,

                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true,


            };
        }

        

        public SKPaint CreateColumnPaint()
        {
            return new SKPaint()
            {
                Color = AnimatedColumnSettings.TESTColumnColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
        }

        public SKPaint CreateNumberPaint()
        {
            return new SKPaint()
            {
                Color = SKColors.Lavender,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
            };
        }

        public SKFont CreateNumberFont()
        {
            return new SKFont()
            {
                Size = AnimatedColumnSettings.TextSize
            };
        }





        public SKPaint CreateTopLeftLightShadowPaint(ShadowIntensity intensity, SKColor lightColor)
        {
          return new SKPaint
            {
                Color = lightColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, AnimatedColumnSettings.TopLeftShadowBlurRadius)
            };

    }


        public SKPaint CreateRightSideDarkShadowPaint(ShadowIntensity intensity, SKColor darkColor)
        {
           return new SKPaint
            {
                Color = darkColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, AnimatedColumnSettings.BottomRightShadowBlurRadius)
            };
}
