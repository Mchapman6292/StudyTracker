using CodingTracker.Common.LoggingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers
{
    public interface IAnimatedLogHelper
    {

    }

    public class AnimatedLogHelper : IAnimatedLogHelper
    {
        private readonly IApplicationLogger _appLogger;


        public AnimatedLogHelper(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }

    }
}
