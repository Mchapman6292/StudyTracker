using CodingTracker.Common.DataInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodingTracker.View.Forms.Services.MainPageService.DonutChartManagers
{
    public class DonutChartManager
    {
        private readonly ICodingSessionRepository _codingSessionRepository;

        public DonutChartManager(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }


    }
}
