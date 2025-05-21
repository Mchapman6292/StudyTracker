using CodingTracker.Common.LoggingInterfaces;

namespace CodingTracker.View.Forms.Services.CountdownTimerService
{
    public class CountdownTimerManager
    {
        private readonly IApplicationLogger _appLogger;


        public CountdownTimerManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }




    }
}
