using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.View.FormManagement;

namespace CodingTracker.View.Forms.Session
{
    public partial class SessionRatingForm : Form
    {
        private readonly IFormNavigator _formNavigator;
        private readonly ICodingSessionManager _codingSessionManager;
        public SessionRatingForm(IFormNavigator formNavigator, ICodingSessionManager codingSessionManager)
        {
            _formNavigator = formNavigator;
            _codingSessionManager = codingSessionManager;
            InitializeComponent();
        }



        private void starRating_ValueChanged(object sender, EventArgs e)
        {
            ratingStarTool.Value = (float)Math.Round(ratingStarTool.Value);
            _codingSessionManager.SetSessionStarRating((int)ratingStarTool.Value);

            _formNavigator.SwitchToForm(FormPageEnum.MainContainerForm);
        }
    }
}
