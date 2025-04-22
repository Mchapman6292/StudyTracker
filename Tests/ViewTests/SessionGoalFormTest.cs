using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.FormService;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;
using Moq;
using Xunit;
using CodingTracker.View.PopUpFormService;
using CodingTracker.View.FormPageEnums;

namespace Tests.ViewTests.SessionGoalFormTests
{
    public class SessionGoalFormTest
    {


        [Fact]
        public void SessionGoalSetToFalseWhenNoGoalSet()
        {
            var mockSessionManager = new Mock<ICodingSessionManager>();
            var mockFormSwitcher = new Mock<IFormSwitcher>();
            var mockInputValidator = new Mock<IInputValidator>();
            var mockNotificationManager = new Mock<INotificationManager>();
            var mockFormStateManager = new Mock<IFormStatePropertyManager>();
            var mockUtilityService = new Mock<IUtilityService>();
            var mockLogger = new Mock<IApplicationLogger>();

            var form = new SessionGoalPage
            (
                mockSessionManager.Object,
                mockFormSwitcher.Object,
                mockInputValidator.Object,
                mockNotificationManager.Object,
                mockFormStateManager.Object,
                mockUtilityService.Object,
                mockLogger.Object
            );

            var session = new CodingSession
            {
                UserId = 42,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                StartTime = DateTime.Now,
                GoalSet = false,
                GoalReached = false,
                GoalSeconds = 0
            };

            form.HandleSkipButton();
            mockFormSwitcher.Verify(m => m.SwitchToForm(FormPageEnum.OrbitalTimerPage), Times.Once);


        }
    }
}
