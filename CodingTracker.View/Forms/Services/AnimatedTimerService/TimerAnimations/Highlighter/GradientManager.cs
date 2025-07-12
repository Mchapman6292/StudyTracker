using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Guna.UI2.Material.Animation.AnimationManager;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public interface IGradientManager
    {
        SKShader CreateSegmentHighlightGradient(AnimatedTimerColumn column, float animationProgress);
        SKShader CreateColumnBackgroundGradient(AnimatedTimerColumn column, bool isActive);
        SKShader CreateCircleOverlayGradient(SKPoint center, float radius, float animationProgress);
        SKShader CreateNumberGlowGradient(AnimatedTimerSegment segment, bool isActive);
        SKShader CreateBackgroundGradient(SKRect bounds);
    }

    public class GradientManager : IGradientManager
    {
        private byte SegmentHighlightOpacityMultiplier = 255;
        private byte CircleHighlightOpacityMultiplier = 60;


        private readonly SKColor SegmentHighlightGradient = new SKColor[] { AnimatedColumnSettings.CatppuccinPink.WithAlpha(alpha), }
        


        public GradientManager()
        {

        }

        public SKColor CreateSegmentHighlightGradient(AnimatedTimerColumn column)
        {
            byte alpha = (byte)((1.0f - column.BaseAnimationProgress) * SegmentHighlightOpacityMultiplier);

            return new SKColor[] {AnimatedColumnSettings.CatppuccinPink.WithAlpha(alpha),       AnimatedColumnSettings.CatppuccinMauve.WithAlpha((byte)(alpha * 0.7f)),         AnimatedColumnSettings.CatppuccinBase.WithAlpha((byte)(alpha * 0.3f))
        }


        public SKShader CreateSegmentHighlightGradient(AnimatedTimerColumn column, float animationProgress)
        {
        

            return SKShader.CreateRadialGradient(new SKPoint(AnimatedColumnSettings.SegmentWidth / 2, AnimatedColumnSettings.SegmentHeight / 2), AnimatedColumnSettings.MaxRadius,
                new SKColor[] { AnimatedColumnSettings.CatppuccinPink.WithAlpha(alpha), AnimatedColumnSettings.CatppuccinMauve.WithAlpha((byte)(alpha * 0.7f)), AnimatedColumnSettings.CatppuccinBase.WithAlpha((byte)(alpha * 0.3f))
                },
                new float[] { 0f, 0.6f, 1f },
                SKShaderTileMode.Clamp
            );
        }

        public SKShader CreateColumnBackgroundGradient(AnimatedTimerColumn column, bool isActive)
        {
            if (!isActive)
            {
                return SKShader.CreateLinearGradient( new SKPoint(0, 0),new SKPoint(0, AnimatedColumnSettings.SegmentHeight), new SKColor[] {
                       new SKColor(45, 42, 62, 200),
                       new SKColor(35, 34, 50, 220)
                    },
                    new float[] { 0f, 1f },
                    SKShaderTileMode.Clamp
                );
            }

            return SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(0, AnimatedColumnSettings.SegmentHeight),
                new SKColor[] {
                   AnimatedColumnSettings.CatppuccinSurface1.WithAlpha(240),
                   AnimatedColumnSettings.CatppuccinSurface0,
                   AnimatedColumnSettings.CatppuccinBase.WithAlpha(250)
                },
                new float[] { 0f, 0.5f, 1f },
                SKShaderTileMode.Clamp
            );
        }

        public SKShader CreateCircleOverlayGradient(SKPoint center, float radius, float animationProgress)
        {
            byte alpha = (byte)((1.0f - animationProgress) * CircleHighlightOpacityMultiplier);

            return SKShader.CreateRadialGradient(
                center,
                radius * 1.5f,
                new SKColor[] {
                   SKColors.Transparent,
                   AnimatedColumnSettings.CatppuccinBase.WithAlpha(alpha),
                   AnimatedColumnSettings.CatppuccinBase.WithAlpha((byte)(alpha * 1.5f))
                },
                new float[] { 0f, 0.7f, 1f },
                SKShaderTileMode.Clamp
            );
        }

        public SKShader CreateNumberGlowGradient(AnimatedTimerSegment segment, bool isActive)
        {
            if (!isActive)
            {
                return null;
            }

            return SKShader.CreateRadialGradient(
                new SKPoint(segment.SegmentWidth / 2, segment.SegmentHeight / 2),
                segment.SegmentHeight * 0.8f,
                new SKColor[] {
                   AnimatedColumnSettings.CatppuccinText,
                   AnimatedColumnSettings.CatppuccinText.WithAlpha(100),
                   SKColors.Transparent
                },
                new float[] { 0f, 0.3f, 1f },
                SKShaderTileMode.Clamp
            );
        }

        public SKShader CreateBackgroundGradient(SKRect bounds)
        {
            return SKShader.CreateLinearGradient(
                new SKPoint(bounds.Left, bounds.Top),
                new SKPoint(bounds.Left, bounds.Bottom),
                new SKColor[] {
                   AnimatedColumnSettings.CatppuccinBase,
                   new SKColor(49, 40, 68),
                   new SKColor(89, 66, 102),
                   new SKColor(120, 85, 130)
                },
                new float[] { 0f, 0.4f, 0.7f, 1f },
                SKShaderTileMode.Clamp
            );
        }
    }
}