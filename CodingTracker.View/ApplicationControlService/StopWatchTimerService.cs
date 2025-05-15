using System.Diagnostics;

namespace CodingTracker.View.ApplicationControlService
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

    /// <summary>
    /// This is needed so other classes can stop the timer for the active coding session.
    /// This can happen if a coding session is active and the user goes to exit etc.
    public class StopWatchTimerService : IStopWatchTimerService
    {
        private Stopwatch sessionTimer { get; } = new Stopwatch();


        public Stopwatch ReturnStopWatch()
        {
            return sessionTimer;
        }

        public TimeSpan ReturnElapsedTimeSpan()
        {
            return sessionTimer.Elapsed;
        }

        public double ReturnElapsedSeconds()
        {
            return sessionTimer.Elapsed.TotalSeconds;
        }

        public double ReturnElapsedMilliseconds()
        {
            return sessionTimer.Elapsed.TotalMilliseconds;
        }

        public void StartTimer()
        {
            sessionTimer.Start();
        }

        public void StopTimer()
        {
            sessionTimer.Stop();
        }




    }
}
