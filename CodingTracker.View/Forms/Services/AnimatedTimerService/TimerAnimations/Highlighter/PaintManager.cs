using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Shadows;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.Painting;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
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

        public SKPaint TESTCreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 255);

            var shader = SKShader.CreateRadialGradient(new SKPoint(column.Location.X + column.Width / 2,column.Location.Y + AnimatedColumnSettings.SegmentHeight / 2),
                AnimatedColumnSettings.MaxRadius,
                new SKColor[] {
                   AnimatedColumnSettings.CatppuccinPink.WithAlpha(alpha),
                   AnimatedColumnSettings.CatppuccinMauve.WithAlpha((byte)(alpha * 0.7f))
                },
                new float[] { 0f, 1f },
                SKShaderTileMode.Clamp
            );

            return new SKPaint()
            {
                Shader = shader,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                BlendMode = SKBlendMode.Screen
            };
        }

        public SKPaint CreateInnerSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 255);

            var shader = SKShader.CreateLinearGradient(
                new SKPoint(column.Location.X, column.Location.Y),
                new SKPoint(column.Location.X + column.Width, column.Location.Y + column.Height),
                new SKColor[] {
                   AnimatedColumnSettings.CatppuccinMauve.WithAlpha((byte)(alpha * 0.8f)),
                   AnimatedColumnSettings.CatppuccinPink.WithAlpha(alpha)
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

        public SKPaint CreateOuterSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte alpha = (byte)(opacityMultiplier * 60);

            return new SKPaint()
            {
                Color = AnimatedColumnSettings.CatppuccinBase.WithAlpha(alpha),
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 8f)
            };
        }

        public SKPaint TESTCreateOuterSegmentPaint(AnimatedTimerColumn column)
        {
            float opacityMultiplier = CalculateSegmentOpacityMultiplier(column);
            byte shadowAlpha = (byte)(opacityMultiplier * 40);

            return new SKPaint()
            {
                Color = new SKColor(0, 0, 0).WithAlpha(shadowAlpha),
                Style = SKPaintStyle.Fill,
                StrokeWidth = 1f,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 6f)
            };
        }

        public SKPaint CreateColumnPaint(bool isColumnActive)
        {
            if (!isColumnActive)
            {
                return new SKPaint()
                {
                    Color = new SKColor(45, 42, 62, 200),
                    Style = SKPaintStyle.Fill,
                    IsAntialias = true
                };
            }

            var shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(0, AnimatedColumnSettings.SegmentHeight),
                new SKColor[] {
                   new SKColor(69, 71, 90, 240),
                   new SKColor(49, 50, 68, 255)
                },
                new float[] { 0f, 1f },
                SKShaderTileMode.Clamp
            );

            return new SKPaint()
            {
                Shader = shader,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };
        }

        public SKPaint CreateNumberPaint(bool isColumnActive)
        {
            if (!isColumnActive)
            {
                return new SKPaint()
                {
                    Color = AnimatedColumnSettings.CatppuccinSubtext.WithAlpha(180),
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Center,
                };
            }

            return new SKPaint()
            {
                Color = AnimatedColumnSettings.CatppuccinText,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Outer, 2f)
            };
        }

        public SKFont CreateNumberFont()
        {
            return new SKFont()
            {
                Size = AnimatedColumnSettings.TextSize,
                Typeface = SKTypeface.FromFamilyName("Segoe UI", SKFontStyleWeight.Medium, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            };
        }

        public SKPaint CreateTopLeftLightShadowPaint(ShadowIntensity intensity, SKColor lightColor)
        {
            float blurRadius = intensity switch
            {
                ShadowIntensity.Subtle => 4f,
                ShadowIntensity.Normal => 6f,
                ShadowIntensity.Strong => 10f,
                _ => 6f
            };

            return new SKPaint
            {
                Color = AnimatedColumnSettings.CatppuccinMauve.WithAlpha(40),
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, blurRadius)
            };
        }

        public SKPaint CreateRightSideDarkShadowPaint(ShadowIntensity intensity, SKColor darkColor)
        {
            float blurRadius = intensity switch
            {
                ShadowIntensity.Subtle => 4f,
                ShadowIntensity.Normal => 8f,
                ShadowIntensity.Strong => 12f,
                _ => 8f
            };

            byte alpha = intensity switch
            {
                ShadowIntensity.Subtle => 80,
                ShadowIntensity.Normal => 120,
                ShadowIntensity.Strong => 160,
                _ => 120
            };

            return new SKPaint
            {
                Color = AnimatedColumnSettings.CatppuccinBase.WithAlpha(alpha),
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, blurRadius)
            };
        }
    }
}