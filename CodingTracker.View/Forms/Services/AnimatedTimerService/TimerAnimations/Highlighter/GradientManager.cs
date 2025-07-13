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
               AnimatedColumnSettings.CatppuccinPink.WithAlpha(alpha),  AnimatedColumnSettings.CatppuccinMauve.WithAlpha((byte)(alpha * 0.7f)),    AnimatedColumnSettings.CatppuccinBase.WithAlpha((byte)(alpha * 0.3f))
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
               SKColors.Transparent, AnimatedColumnSettings.CatppuccinBase.WithAlpha(alpha),  AnimatedColumnSettings.CatppuccinBase.WithAlpha((byte)(alpha * 1.5f))
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
               AnimatedColumnSettings.CatppuccinSurface1.WithAlpha(240), AnimatedColumnSettings.CatppuccinSurface0,   AnimatedColumnSettings.CatppuccinBase.WithAlpha(250)
            };
        }

        private SKColor[] CreateInactiveColumnColors()
        {
            return new SKColor[]
            {
                AnimatedColumnSettings.CatppuccinSurface0.WithAlpha(200), AnimatedColumnSettings.CatppuccinBase.WithAlpha(220)
            };
        }
        private SKColor[] CreateBackgroundColors()
        {
            return new SKColor[]
            {
               AnimatedColumnSettings.CatppuccinBase, AnimatedColumnSettings.CatppuccinSurface0,AnimatedColumnSettings.CatppuccinSurface1,  AnimatedColumnSettings.CatppuccinMauve.WithAlpha(50) };
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
                AnimatedColumnSettings.CatppuccinText,  AnimatedColumnSettings.CatppuccinText.WithAlpha(NumberGlowOpacityMultiplier), SKColors.Transparent
            };
        }

        private SKColor[] CreateInvactiveNumberColors()
        {
            return new SKColor[]
            {
                AnimatedColumnSettings.InactiveColumnColor, AnimatedColumnSettings.CatppuccinText
            };
        }


        public SKShader CreateNumberGradientByIsColumnActive(AnimatedTimerSegment segment, bool isActive)
        {
            SKColor[] colors = isActive ? CreateActiveNumberColors() : CreateInvactiveNumberColors();

            float radius = segment.SegmentHeight * NumberGlowRadiusMultiplier;

            return SKShader.CreateRadialGradient(segment.LocationCenterPoint, radius, colors, threeColorStops,  SKShaderTileMode.Clamp);
        }





        public SKShader CreateBackgroundCanvasGradient(SKRect bounds)
        {
            SKColor[] backgroundColors = CreateBackgroundColors();

            return SKShader.CreateLinearGradient(new SKPoint(bounds.Left, bounds.Top), new SKPoint(bounds.Left, bounds.Bottom),  backgroundColors, fourColorStops,  SKShaderTileMode.Clamp);
        }
    }
}