using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Logging
{
    public static class LoggerHelpers
    {
        public static string FormatElapsedTimeSpan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }



   
    }
}
