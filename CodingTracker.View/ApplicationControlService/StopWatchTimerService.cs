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
        void StartSessionTimer();
        void RestartSessionTimer();
        void StartRestartTimer();


        void ResetRestartTimer();
        void StopRestartTimer();

        TimeSpan GetRestartElapsedTimeCappedAtOneSecond();
        double GetRestartStopwatchSeconds();
    }

    /// <summary>
    /// This is needed so other classes can stop the timer for the active coding session.
    /// This can happen if a coding session is active and the user goes to exit etc.
    public class StopWatchTimerService : IStopWatchTimerService
    {
        private Stopwatch sessionTimer { get; } = new Stopwatch();

        private Stopwatch restartStopwatch { get; set; } = new Stopwatch();

        private TimeSpan maxRestartStopwatchDuration { get; } = TimeSpan.FromSeconds(1); 


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

        public void StartSessionTimer()
        {
            sessionTimer.Start();
        }

        public void StopTimer()
        {
            sessionTimer.Stop();
        }

        public void RestartSessionTimer()
        {
            sessionTimer.Restart();
        }






        public void ResetRestartTimer()
        {
            restartStopwatch.Restart();

        }

        public void StopRestartTimer()
        {
            restartStopwatch.Stop();
        }

        public void StartRestartTimer()
        {
            if(restartStopwatch == null)
            {
                restartStopwatch = new Stopwatch();
            }

            restartStopwatch.Start();
        }

        public TimeSpan GetRestartElapsedTimeCappedAtOneSecond()
        {
            TimeSpan elapsed = TimeSpan.FromMicroseconds(restartStopwatch.Elapsed.TotalMilliseconds);

            if (elapsed > maxRestartStopwatchDuration)
            {
                return maxRestartStopwatchDuration;
            }

            return elapsed; 
        }


        public double GetRestartStopwatchSeconds()
        {
            double elpased = restartStopwatch.Elapsed.TotalSeconds;

            if (elpased > maxRestartStopwatchDuration.Seconds)
            {
                return maxRestartStopwatchDuration.Seconds;
            }
            return elpased;
        }
 



    }
}



