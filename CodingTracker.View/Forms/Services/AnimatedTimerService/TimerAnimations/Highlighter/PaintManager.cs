using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.Painting;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    // Opactiy ratings 0 fully transparent, 255 fully opaque
    



    public interface IPaintManager
    {
        SKPaint CreateInnerSegmentPaint(AnimatedTimerColumn column);
        SKPaint CreateOuterSegmentPaint(AnimatedTimerColumn column);
        SKPaint TESTCreateOuterSegmentPaint(AnimatedTimerColumn column);
        SKPaint CreateColumnPaint(bool IsColumnActive);
        SKPaint CreateNumberPaint(bool isColumnActive);
        SKFont CreateNumberFont();
        SKPaint CreateTopLeftLightShadowPaint(ShadowIntensity intensity, SKColor lightColor);
        SKPaint CreateRightSideDarkShadowPaint(ShadowIntensity intensity, SKColor darkColor);


        SKPaint TESTCreateInnerSegmentPaint(AnimatedTimerColumn column);

    }

    
    public class PaintManager : IPaintManager
    {

        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedColumnStateManager _columnStateManager;


        private byte InactiveColumnOpacity = 122;

        private SKColor ColumnColor = new SKColor(49, 50, 68);


        public PaintManager(IApplicationLogger appLogger, IAnimatedColumnStateManager columnStateManager)
        {
            _appLogger = appLogger;
            _columnStateManager = columnStateManager;
        }


        private float CalculateSegmentOpacityMultiplier(AnimatedTimerColumn column)
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


        /*
        public SKPaint TESTCreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 200);

            var shader = SKShader.CreateLinearGradient(
                new SKPoint(column.Location.X, column.Location.Y),
                new SKPoint(column.Location.X + column.Width, column.Location.Y + column.Height),
                new SKColor[] {
                    AnimatedColumnSettings.CatppuccinMauve.WithAlpha((byte)(alpha * 0.6f)),
                    AnimatedColumnSettings.CatppuccinPink.WithAlpha((byte)(alpha * 0.8f))
                },
                SKShaderTileMode.Clamp
            );

            return new SKPaint()
            {
                Shader = shader,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
        }
        */


        public SKPaint TESTCreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {

                Color = ColumnColor,
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
                Color =AnimatedColumnSettings.CatppuccinPink,
                Style = SKPaintStyle.Fill,

                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true


            };
        }






        public SKPaint TESTCreateOuterSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte shadowAlpha = (byte)(opacityMultiplier * 40);

            byte alpha = (byte)(opacityMultiplier * 255);

            return new SKPaint()
            {
                Color = new SKColor(0, 0, 0).WithAlpha(shadowAlpha),
                Style = SKPaintStyle.Fill,

                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true
            };
        }



        public SKPaint CreateColumnPaint(bool isColumnActive)
        {
            SKColor columnColor = ColumnColor;

            if(!isColumnActive)
            {
                columnColor = new SKColor(60, 60, 80);
            }

            return new SKPaint()
            {
                Color = columnColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
        }

        public SKPaint CreateNumberPaint(bool isColumnActive)
        {
            SKColor textColour = ColumnColor;

            if (!isColumnActive)
            {
                textColour = SKColors.Gray;
            }

            else
            {
                textColour = AnimatedColumnSettings.CatppuccinText;
            }

                return new SKPaint()
                {
                    Color = textColour,
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
    }
}
