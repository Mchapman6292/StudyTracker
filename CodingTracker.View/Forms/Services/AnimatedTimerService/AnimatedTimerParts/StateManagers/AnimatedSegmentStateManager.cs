using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedSegmentStateManager
    {
 
    }
    public class AnimatedSegmentStateManager : IAnimatedSegmentStateManager
    {
        private readonly IApplicationLogger _appLogger;


        public AnimatedSegmentStateManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }











    }
}
