using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Common.Utilities;

public static class LoggerHelper
{
    public static string FormatElapsedTimeSpan(TimeSpan elapsed)
    {
        return elapsed.ToString(@"mm\:ss\.fff");
    }

    public static string FormatMinuteTimeSpanToHHMMSS(TimeSpan minuteElapsed)
    {
        return minuteElapsed.ToString(@"hh\:mm\:ss");
    }


    public static string FormatAllElapsedTimeSpan(TimeSpan elapsed)
    {
        return elapsed.TotalMinutes switch
        {
            < 1 => elapsed.ToString(@"ss\.fff\s"),        
            < 60 => elapsed.ToString(@"mm\:ss"),             
            < 1440 => elapsed.ToString(@"hh\:mm\:ss"),       
            _ => elapsed.ToString(@"d\.hh\:mm\:ss")        
        };
    }





}
