using System.Diagnostics;

namespace CodingTracker.View.TimerDisplayService.StopWatchTimerServices
{
    public interface IStopWatchTimerService
    {
        Stopwatch ReturnStopWatch();
        TimeSpan ReturnElapsedTimeSpan();
        double ReturnElapsedSeconds();
        double ReturnElapsedMilliseconds();
        void StopTimer();
        void StartTimer();
    }

    public class StopWatchTimerService : IStopWatchTimerService
    {
        private Stopwatch stopwatchTimer { get; } = new Stopwatch();


        public Stopwatch ReturnStopWatch()
        {
            return stopwatchTimer;
        }

        public TimeSpan ReturnElapsedTimeSpan()
        {
            return stopwatchTimer.Elapsed;
        }

        public double ReturnElapsedSeconds()
        {
            return stopwatchTimer.Elapsed.TotalSeconds;
        }

        public double ReturnElapsedMilliseconds()
        {
            return stopwatchTimer.Elapsed.TotalMilliseconds;
        }

        public void StartTimer()
        {
            stopwatchTimer.Start();
        }

        public void StopTimer()
        {
            stopwatchTimer.Stop();
        }




    }
}
