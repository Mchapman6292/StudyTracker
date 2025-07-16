using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers;
using CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Runtime.CompilerServices;
using Uno.UI.Xaml;

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
        private List<SKPoint> basePath = new List<SKPoint>();
        private int pathIndex = 0;
        private int SnakeLength { get; set; } = 10;



        private AnimatedTimerColumn CurrentColumnWithSnake {  get; set; }
        private AnimatedTimerColumn? TargetColumn{ get; set; }
        public bool? TravelToNewColumn { get; set; }



        private const float segmentWidth = AnimatedColumnSettings.SegmentWidth;
        private const float segmentHeight = AnimatedColumnSettings.SegmentHeight;


        private readonly IApplicationLogger _appLogger;
        private readonly IAnimatedColumnStateManager _animatedColumnStateManager;
        private readonly IRenderingCalculator _renderingCalculator;


        public SnakePathManager(IApplicationLogger appLogger, IAnimatedColumnStateManager animatedColumnStateManager, IRenderingCalculator renderingCalculator)
        {
            _appLogger = appLogger;
            _animatedColumnStateManager = animatedColumnStateManager;

        }




        public void UpdateSnakeMovement()
        {
            SKPoint relativePath = GetCurrentPathPosition();
            SKPoint worldPosition = CalculateSnakeCurrentPosition();
            AddPositionToQueue(worldPosition);
            AdvancePathIndex();
        }


        public void AdvancePathIndex()
        {
            pathIndex = (pathIndex + 1) % basePath.Count;
        }

        private SKPoint GetCurrentPathPosition()
        {
            return basePath[pathIndex];
        }

        private SKPoint CalculateSnakeCurrentPosition()
        {
            SKPoint basePosition = GetCurrentPathPosition();

            return new SKPoint(CurrentColumnWithSnake.Location.X + basePosition.X, CurrentColumnWithSnake.Location.Y + basePosition.Y - CurrentColumnWithSnake.YTranslation);
        }



        public void AddPositionToQueue(SKPoint position)
        {
            SnakePositions.Enqueue(position);

            if (SnakePositions.Count > SnakeLength)
            {
                SnakePositions.Dequeue();
            }
        }






        public SKPoint GetCurrentSnakeHead()
        {
            return SnakePositions.Count > 0 ? SnakePositions.Last() : new SKPoint();
        }








    }
}
