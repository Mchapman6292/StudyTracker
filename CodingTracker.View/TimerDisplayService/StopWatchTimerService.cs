using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.TimerDisplayService.StopWatchTimerServices
{
    public interface IStopWatchTimerService
    {
        Stopwatch ReturnStopWatch();
        TimeSpan ReturnElapsedTimeSpan();
        double ReturnElapsedSeconds();
        double ReturnElapsedMilliseconds();
    }

    public class StopWatchTimerService : IStopWatchTimerService
    {
        private Stopwatch stopwatchTimer {  get; } = new Stopwatch();


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



    }
}
