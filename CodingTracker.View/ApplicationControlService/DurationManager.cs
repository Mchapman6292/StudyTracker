using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.ApplicationControlService.DurationManagers
{
    public interface IDurationManager
    {
        void SetSessionDuration(TimeSpan duration);
        TimeSpan ReturnSessionDuration();
    }

    public class DurationManager : IDurationManager
    {
        private TimeSpan _sessionDuration {  get; set; } 
        

        public void SetSessionDuration(TimeSpan sessionDuration)
        {
            _sessionDuration = sessionDuration;
        }

        public TimeSpan ReturnSessionDuration()
        {
            return _sessionDuration;
        }
    }
}
