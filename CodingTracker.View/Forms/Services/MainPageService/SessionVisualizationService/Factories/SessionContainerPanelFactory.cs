using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories
{
    public interface ISessionContainerPanelFactory
    {
        SessionContainerPanel CreateSessionContainerPanel(DateOnly panelDateLocal);
    }

    public class SessionContainerPanelFactory : ISessionContainerPanelFactory
    {
        private Size ContainerPanelSize = new Size(443, 19);

        public SessionContainerPanel CreateSessionContainerPanel(DateOnly panelDateLocal)
        {
            var sessionContainerPanel = new SessionContainerPanel()
            {
                Size = ContainerPanelSize,
                BackColor = Color.Transparent,
                FillColor = Color.Transparent,
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 8, 15, 8),
                AutoSize = false
            };

            return sessionContainerPanel;
        }
    }
}