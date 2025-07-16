using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.Painting;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    // Opactiy ratings 0 fully transparent, 255 fully opaque




    public interface IPaintManager
    {
        SKPaint CreateInnerSegmentPaint(AnimatedTimerColumn column);
        SKPaint CreateOuterSegmentPaint(AnimatedTimerColumn column);
        SKPaint TESTCreateOuterSegmentPaint(AnimatedTimerColumn column);
        SKPaint CreateColumnPaint(AnimatedTimerColumn column);
        SKPaint CreateActiveNumberPaintAndGradient(AnimatedTimerColumn column);
        SKFont CreateNumberFont();
        SKPaint CreateTopLeftLightShadowPaint(ShadowIntensity intensity, SKColor lightColor);
        SKPaint CreateRightSideDarkShadowPaint(ShadowIntensity intensity, SKColor darkColor);
        SKPaint CreateFocusedNumberPaintAndGradient(AnimatedTimerColumn column);


        SKPaint TESTCreateInnerSegmentPaint(AnimatedTimerColumn column);

    }


    public class PaintManager : IPaintManager
    {

        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedColumnStateManager _columnStateManager;
       private readonly IGradientManager _gradientManager;


        private byte InactiveColumnOpacity = 122;

        private SKColor ColumnColor = new SKColor(49, 50, 68);


        public PaintManager(IApplicationLogger appLogger, IAnimatedColumnStateManager columnStateManager, IGradientManager gradientManager)
        {
            _appLogger = appLogger;
            _columnStateManager = columnStateManager;
            _gradientManager = gradientManager;
        }


        private float CalculateSegmentOpacityMultiplier(AnimatedTimerColumn column)
        {
            return 1.0f - column.CircleAnimationProgress;
        }




        public SKPaint TESTCreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            SKShader gradientShader = _gradientManager.CreateInnerSegmentGradient(column);

            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {

                Shader = gradientShader,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,

            };
        }


        public SKPaint CreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {

                Color = ColumnColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true

            };
        }



        public SKPaint CreateOuterSegmentPaint(AnimatedTimerColumn column)
        {

            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);

            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {


                /*
                Color = new SKColor(49, 50, 68).WithAlpha(alpha),
                */
                Color = AnimatedColumnSettings.CatppuccinPink,
                Style = SKPaintStyle.Fill,

                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true


            };
        }






        public SKPaint TESTCreateOuterSegmentPaint(AnimatedTimerColumn column)
        {
            SKShader outerSegmentGradient = _gradientManager.CreateOuterSegmentGradient(column);

            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte shadowAlpha = (byte)(opacityMultiplier * 40);

            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {
               Shader = outerSegmentGradient,
                Style = SKPaintStyle.Fill,

                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true
            };
        }



        public SKPaint CreateColumnPaint(AnimatedTimerColumn column)
        {
            SKShader columnGradinet = _gradientManager.CreateColumnGradientByIsColumnActive(column);

    
            return new SKPaint()
            {
                StrokeWidth = 1f,
                Shader = columnGradinet,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
        }

 


        // To handle blue occuring on first transition add a bool 
        public SKPaint CreateActiveNumberPaintAndGradient(AnimatedTimerColumn column)
        {
            SKShader numberGradient = _gradientManager.CreateNumberGradientByIsColumnActive(column.FocusedSegment, column.IsColumnActive);


            if (!column.IsColumnActive)
            {
                return new SKPaint()
                {
                    Shader = numberGradient,
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Center,
                    MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Outer, 0.5f)
                };
            }
            else if (column.IsColumnActive && column.NumberBlurringStartAnimationActive)
            {
                float pulse = (float)(Math.Sin(column.BaseAnimationProgress * 2 * Math.PI) * 0.5 + 0.5);

                return new SKPaint()
                {
                    Shader = numberGradient,
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Center,
                    MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, pulse * 2f),

                };
            }

            else
            {
                return new SKPaint()
                {
                    Shader = numberGradient,
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Center,
                };
                }

          }



        public SKPaint CreateFocusedNumberPaintAndGradient(AnimatedTimerColumn column)
        {
            SKShader focusedNumberGradient = _gradientManager.CreateFocusedNumberGradient(column);

            return new SKPaint()
            {
                Shader = focusedNumberGradient,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center
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
    }
}