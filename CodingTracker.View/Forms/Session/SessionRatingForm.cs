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
            starRating.Value = (float)Math.Round(starRating.Value);
            _codingSessionManager.SetSessionStarRating((int)starRating.Value);

            _formNavigator.SwitchToForm(FormPageEnum.OldMainPage);
        }
    }
}
