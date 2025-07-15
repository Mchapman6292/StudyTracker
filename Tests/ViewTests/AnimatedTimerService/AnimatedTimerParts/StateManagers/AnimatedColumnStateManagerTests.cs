using CodingTracker.View.Forms.Services.AnimatedTimerService;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LibVLCSharp.Shared;
using SkiaSharp;
using Xunit;

namespace Tests.ViewTests.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public class AnimatedColumnStateManagerTests
    {
        private readonly IAnimatedColumnStateManager _stateManager;
        private readonly IAnimatedTimerColumnFactory _columnFactory;



        public AnimatedColumnStateManagerTests(IAnimatedColumnStateManager _columnStateManager, IAnimatedTimerColumnFactory columnFactory)
        {
            _stateManager = _columnStateManager;
            _columnFactory = columnFactory;
        }


        /*
        [Theory]
        [InlineData(9.5, 0.0f)]    // Before animation starts
        [InlineData(9.7, 0.0f)]    // Animation just starting
        [InlineData(9.85, 0.5f)]   // Halfway through
        [InlineData(10.0, 1.0f)]   // Animation complete
        public void CalculateAnimationProgress_VariousTimePoints_ReturnsExpectedProgress(double elapsedSeconds, float expectedProgress)
        {
            {
                AnimatedTimerColumn testSecondsLeadingDigit = _columnFactory.CreateColumnWithSegments(AnimatedColumnSettings.ZeroToNineDigit, (new SKPoint(100, 150)), ColumnUnitType.SecondsLeadingDigit);

                testSecondsLeadingDigit.AnimationStartTime = AnimatedColumnSettings.UnitTypesToAnimationTimeSpans[testSecondsLeadingDigit.ColumnType];
                testSecondsLeadingDigit.CurrentAnimationEndTime = testSecondsLeadingDigit.AnimationStartTime + testSecondsLeadingDigit.AnimationInterval;

                var elapsed = TimeSpan.FromSeconds(elapsedSeconds);

                float progress = _stateManager.NEWCalculateAnimationProgress(testSecondsLeadingDigit, elapsed);

  
                Assert.Equal(expectedProgress, progress, 3);
            }
        }
*/


    }
} 