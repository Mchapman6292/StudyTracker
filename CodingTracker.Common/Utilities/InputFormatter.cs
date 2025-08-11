using CodingTracker.Common.LoggingInterfaces;


namespace CodingTracker.Common.Utilities
{
    public interface IInputFormatter
    {

    }

    public class InputFormatter : IInputFormatter
    {
        private readonly IApplicationLogger _appLogger;



        public InputFormatter(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }



        public TimeSpan FormatTimeSpanToString(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
            {
                return TimeSpan.Zero;
            }
            string cleanTime = new string(timeString.Where(char.IsDigit).ToArray());

            switch (cleanTime.Length)
            {
                case 1:
                case 2:
                    {
                        // Interpret as hours only (e.g., "2" or "02" = 2 hours)
                        if (int.TryParse(cleanTime, out int hours))
                        {
                            TimeSpan hoursResult = new TimeSpan(hours, 0, 0);
                            string timeSpanForLogging = LoggerHelper.FormatElapsedTimeSpan(hoursResult);
                            _appLogger.Info($"Timespan returned: {timeSpanForLogging}.");
                            return hoursResult;
                        }
                        break;
                    }
                case 3:
                    {
                        string hourStr = cleanTime.Substring(0, 1);
                        string minsStr = cleanTime.Substring(1, 2);
                        if (int.TryParse(hourStr, out int h) && (int.TryParse(minsStr, out int m)))
                        {
                            h = int.Parse(hourStr);
                            m = int.Parse(minsStr);
                            TimeSpan hoursMinsResult = new TimeSpan(h, m, 0);
                            string timeSpanForLogging = LoggerHelper.FormatElapsedTimeSpan(hoursMinsResult);
                            _appLogger.Info($"Timespan returned: {timeSpanForLogging}.");
                            return hoursMinsResult;
                        }
                        break;
                    }
                case 4:
                    {
                        string hourStr = timeString.Substring(0, 2);
                        string minsStr = timeString.Substring(cleanTime.Length - 2);
                        if (int.TryParse(hourStr, out int h) && (int.TryParse(minsStr, out int m)))
                        {
                            h = int.Parse(hourStr);
                            m = int.Parse(minsStr);
                            TimeSpan fullHoursMinsResult = new TimeSpan(h, m, 0);
                            string timeSpanForLogging = LoggerHelper.FormatElapsedTimeSpan(fullHoursMinsResult);
                            _appLogger.Info($"Timespan returned: {timeSpanForLogging}.");
                            return fullHoursMinsResult;
                        }
                        break;
                    }
            }
            _appLogger.Info($"Unable to parse time returning Timespan 0");
            return TimeSpan.Zero;
        }

       


    }
}
