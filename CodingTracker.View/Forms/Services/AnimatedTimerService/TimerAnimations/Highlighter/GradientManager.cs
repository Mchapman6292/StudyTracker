using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IGradientManager
    {
        SKShader CreateInnerSegmentGradient(AnimatedTimerColumn column);
        SKShader CreateColumnGradientByIsColumnActive(AnimatedTimerColumn column);
        SKShader CreateOuterSegmentGradient(AnimatedTimerColumn column);
        SKShader CreateNumberGradientByIsColumnActive(AnimatedTimerSegment segment, bool isActive);
        SKShader CreateBackgroundCanvasGradient(SKRect bounds);
    }

    public class GradientManager : IGradientManager
    {
        private static readonly SKColor BackgroundDeepDark = new SKColor(20, 20, 30);
        private static readonly SKColor BackgroundMidDark = new SKColor(30, 32, 45);
        private static readonly SKColor BackgroundSurface = new SKColor(40, 42, 55);
        private static readonly SKColor BackgroundAccentHint = new SKColor(255, 81, 195, 15);

        private static readonly SKColor InnerSegmentBrightCore = new SKColor(255, 255, 255);
        private static readonly SKColor InnerSegmentGlowPrimary = new SKColor(255, 81, 195);
        private static readonly SKColor InnerSegmentGlowSecondary = new SKColor(168, 228, 255);

        private static readonly SKColor OuterOverlayBrightFlash = new SKColor(255, 255, 255, 102);
        private static readonly SKColor OuterOverlayGlowHalo = new SKColor(205, 214, 244, 51);
        private static readonly SKColor OuterOverlayAtmospheric = SKColors.Transparent;

        private static readonly SKColor ActiveColumnPrimary = new SKColor(50, 52, 65);
        private static readonly SKColor ActiveColumnSecondary = new SKColor(40, 42, 55);
        private static readonly SKColor ActiveColumnBase = new SKColor(30, 32, 45);

        private static readonly SKColor InactiveColumnPrimary = new SKColor(35, 37, 50, 180);
        private static readonly SKColor InactiveColumnBase = new SKColor(25, 27, 40, 200);


        private static readonly SKColor ActiveNumberPrimary = new SKColor(127, 202, 209);
        private static readonly SKColor ActiveNumberSecondary = new SKColor(147, 206, 209);
        /*255, 255, 255
        private static readonly SKColor ActiveNumberPrimary = new SKColor(255, 255, 255);
        */
        private static readonly SKColor ActiveNumberGlow = new SKColor(204, 84, 144);

        private static readonly SKColor InactiveNumberPrimary = new SKColor(120, 120, 140);
        private static readonly SKColor InactiveNumberSecondary = new SKColor(180, 180, 200);



        private static readonly SKColor SnakeLoopBrightMagenta = new SKColor(255, 0, 255);
        private static readonly SKColor SnakeLoopLavenderPurple = new SKColor(155, 89, 182);
        private static readonly SKColor SnakeLoopVioletPurple = new SKColor(138, 43, 226);



        private byte SegmentHighlightOpacityMultiplier = 255;
        private byte CircleOverlayOpacityMultiplier = 60;
        private byte NumberGlowOpacityMultiplier = 100;
        private float CircleRadiusMultiplier = 1.5f;
        private float NumberGlowRadiusMultiplier = 0.8f;

        private float[] threeColorStops = { 0f, 0.5f, 1f };
        private float[] twoColorStops = { 0f, 1f };
        private float[] fourColorStops = { 0f, 0.33f, 0.66f, 1f };

        public GradientManager()
        {
        }

        private byte CalculateAlpha(float baseAnimationProgress, byte multiplier)
        {
            return (byte)((1.0f - baseAnimationProgress) * multiplier);
        }

        private SKColor[] CreateInnerSegmentColors(AnimatedTimerColumn column)
        {
            byte alpha = CalculateAlpha(column.BaseAnimationProgress, SegmentHighlightOpacityMultiplier);
            return new SKColor[]
            {
               InnerSegmentBrightCore.WithAlpha(alpha), InnerSegmentGlowPrimary.WithAlpha((byte)(alpha * 0.8f)), InnerSegmentGlowSecondary.WithAlpha((byte)(alpha * 0.4f))
            };
        }

        public SKShader CreateInnerSegmentGradient(AnimatedTimerColumn column)
        {
            if (column.FocusedSegment == null) return null;

            SKColor[] highlightColors = CreateInnerSegmentColors(column);
            return SKShader.CreateRadialGradient(column.FocusedSegment.LocationCenterPoint, AnimatedColumnSettings.MaxRadius, highlightColors, threeColorStops, SKShaderTileMode.Clamp);
        }

        private SKColor[] CreateOuterSegmentColors(AnimatedTimerColumn column)
        {
            byte alpha = CalculateAlpha(column.CircleAnimationProgress, CircleOverlayOpacityMultiplier);
            return new SKColor[]
            {
               OuterOverlayBrightFlash.WithAlpha((byte)(alpha * 2.0f)), OuterOverlayGlowHalo.WithAlpha((byte)(alpha * 1.2f)), OuterOverlayAtmospheric.WithAlpha((byte)(alpha * 0.6f))
            };
        }

        public SKShader CreateOuterSegmentGradient(AnimatedTimerColumn column)
        {
            if (column.FocusedSegment == null) return null;

            SKColor[] overlayColors = CreateOuterSegmentColors(column);
            float radius = AnimatedColumnSettings.MaxRadius * CircleRadiusMultiplier;

            return SKShader.CreateRadialGradient(column.FocusedSegment.LocationCenterPoint, radius, overlayColors, threeColorStops, SKShaderTileMode.Clamp);
        }

        private SKColor[] CreateActiveColumnColors()
        {
            return new SKColor[]
            {
               ActiveColumnPrimary.WithAlpha(240), ActiveColumnSecondary, ActiveColumnBase.WithAlpha(250)
            };
        }

        private SKColor[] CreateInactiveColumnColors()
        {
            return new SKColor[]
            {
                InactiveColumnPrimary, InactiveColumnBase
            };
        }

        private SKColor[] CreateBackgroundColors()
        {
            return new SKColor[]
            {
               BackgroundDeepDark, BackgroundMidDark, BackgroundSurface, BackgroundAccentHint
            };
        }

        public SKShader CreateColumnGradientByIsColumnActive(AnimatedTimerColumn column)
        {
            SKColor[] colors = column.IsColumnActive ? CreateActiveColumnColors() : CreateInactiveColumnColors();
            float[] stops = column.IsColumnActive ? threeColorStops : twoColorStops;

            return SKShader.CreateLinearGradient(column.Location, new SKPoint(column.Location.X, column.Location.Y + column.Height), colors, stops, SKShaderTileMode.Clamp);
        }

        private SKColor[] CreateActiveNumberColors()
        {
            return new SKColor[]
            {
                ActiveNumberPrimary, ActiveNumberSecondary
            };
        }

        private SKColor[] CreateInvactiveNumberColors()
        {
            return new SKColor[]
            {
                ActiveNumberPrimary, ActiveNumberSecondary
            };
        }

        public SKShader CreateNumberGradientByIsColumnActive(AnimatedTimerSegment segment, bool isActive)
        {
            SKColor[] colors = isActive ? CreateActiveNumberColors() : CreateInvactiveNumberColors();

            float radius = segment.SegmentHeight * NumberGlowRadiusMultiplier;

  
     


            return SKShader.CreateRadialGradient(segment.LocationCenterPoint, radius, colors, twoColorStops, SKShaderTileMode.Clamp);
        }

        public SKShader CreateBackgroundCanvasGradient(SKRect bounds)
        {
            SKColor[] backgroundColors = CreateBackgroundColors();

            return SKShader.CreateLinearGradient(new SKPoint(bounds.Left, bounds.Top), new SKPoint(bounds.Left, bounds.Bottom), backgroundColors, fourColorStops, SKShaderTileMode.Clamp);
        }
    }
}