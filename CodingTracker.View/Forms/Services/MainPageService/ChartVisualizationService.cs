using CodingTracker.Common.DataInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodingTracker.View.Forms.Services.MainPageService.DonutChartManagers
{
    public class ChartVisualizationService
    {
        private readonly ICodingSessionRepository _codingSessionRepository;

        public ChartVisualizationService(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }


    }
}
