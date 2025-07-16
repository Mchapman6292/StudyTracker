using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using static System.Formats.Asn1.AsnWriter;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IGradientManager
    {
        SKShader CreateInnerSegmentGradient(AnimatedTimerColumn column);
        SKShader CreateColumnGradientByIsColumnActive(AnimatedTimerColumn column);
        SKShader CreateOuterSegmentGradient(AnimatedTimerColumn column);
        SKShader CreateNumberGradientByIsColumnActive(AnimatedTimerSegment segment, bool isActive);
        SKShader CreateBackgroundCanvasGradient(SKRect bounds);
        SKShader CreateFocusedNumberGradient(AnimatedTimerColumn column);
    }

    public class GradientManager : IGradientManager
    {
        private static readonly SKColor BackgroundDeepDark = new SKColor(26, 26, 46);
        private static readonly SKColor BackgroundMidDark = new SKColor(22, 33, 62);
        private static readonly SKColor BackgroundSurface = new SKColor(15, 52, 96);
        private static readonly SKColor BackgroundAccentHint = new SKColor(245, 194, 231, 25);

        private static readonly SKColor InnerSegmentBrightCore = new SKColor(245, 194, 231);
        private static readonly SKColor InnerSegmentGlowPrimary = new SKColor(203, 166, 247);
        private static readonly SKColor InnerSegmentGlowSecondary = new SKColor(137, 180, 250);

        private static readonly SKColor OuterOverlayBrightFlash = new SKColor(245, 194, 231, 120);
        private static readonly SKColor OuterOverlayGlowHalo = new SKColor(203, 166, 247, 80);
        private static readonly SKColor OuterOverlayAtmospheric = new SKColor(137, 180, 250, 40);

        private static readonly SKColor ActiveColumnPrimary = new SKColor(255, 255, 255, 8);
        private static readonly SKColor ActiveColumnSecondary = new SKColor(255, 255, 255, 5);
        private static readonly SKColor ActiveColumnBase = new SKColor(255, 255, 255, 3);

        private static readonly SKColor InactiveColumnPrimary = new SKColor(255, 255, 255, 5);
        private static readonly SKColor InactiveColumnBase = new SKColor(255, 255, 255, 2);

        private static readonly SKColor ActiveNumberPrimary = new SKColor(245, 194, 231);
        private static readonly SKColor ActiveNumberSecondary = new SKColor(203, 166, 247);

        private static readonly SKColor ActiveNumberGlow = new SKColor(245, 194, 231);

        private static readonly SKColor InactiveNumberPrimary = new SKColor(205, 214, 244, 153);
        private static readonly SKColor InactiveNumberSecondary = new SKColor(205, 214, 244, 102);



        private static readonly SKColor FocusedNumberPrimary = new SKColor(245, 194, 231);
        private static readonly SKColor FocusedNumberSecondary = new SKColor(203, 166, 247);


        private static readonly SKColor SnakeLoopBrightMagenta = new SKColor(245, 194, 231);
        private static readonly SKColor SnakeLoopLavenderPurple = new SKColor(203, 166, 247);
        private static readonly SKColor SnakeLoopVioletPurple = new SKColor(137, 180, 250);

        private byte SegmentHighlightOpacityMultiplier = 200;
        private byte CircleOverlayOpacityMultiplier = 100;
        private byte NumberGlowOpacityMultiplier = 150;
        private float CircleRadiusMultiplier = 2.0f;
        private float NumberGlowRadiusMultiplier = 1.2f;

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
               ActiveColumnPrimary.WithAlpha(20), ActiveColumnSecondary, ActiveColumnBase.WithAlpha(8)
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
                InactiveNumberPrimary, InactiveNumberSecondary
            };
        }

        public SKColor[] CreateFocusedNumberColors()
        {
            return new SKColor[]
            {
                FocusedNumberPrimary, FocusedNumberSecondary
            };
        }

        public SKShader CreateFocusedNumberGradient(AnimatedTimerColumn column)
        {
            SKColor[] focusedColors = CreateFocusedNumberColors();

            return SKShader.CreateLinearGradient(column.Location, new SKPoint(column.Location.X, column.Location.Y + column.Height), focusedColors, twoColorStops, SKShaderTileMode.Clamp);
        }


        public SKShader CreateNumberGradientByIsColumnActive(AnimatedTimerSegment segment, bool isActive)
        {
            SKColor[] colors = isActive ? CreateInvactiveNumberColors() : CreateInvactiveNumberColors();

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