using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.SnakeAnimation
{
    public interface ISnakePathManager 
    {
    }

    public enum SnakePathType
    {
        ColumnOutline,
        SegmentOutline,
        ZigZag
    }



    public class SnakePathManager : ISnakePathManager
    {

        public SKPoint SnakeHead { get; set; }
        public SKPoint SnakeTail { get; set; }

        Queue<SKPoint> SnakePositions = new Queue<SKPoint>();


        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedColumnStateManager _animatedColumnStateManager;


        public SnakePathManager(IApplicationLogger appLogger, IAnimatedColumnStateManager animatedColumnStateManager)
        {
            _appLogger = appLogger;
            _animatedColumnStateManager = animatedColumnStateManager;
        }








    }
}
