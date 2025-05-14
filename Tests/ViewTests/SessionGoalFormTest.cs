using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ButtonHighlighterServices;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.PopUpFormService;
using Moq;
using Xunit;

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
            var mockUtilityService = new Mock<IUtilityService>();
            var mockLogger = new Mock<IApplicationLogger>();
            var mockFormStateManagement = new Mock<IFormStateManagement>();
            var mockButtonHighlighterService = new Mock<IButtonHighlighterService>();
            var exitFlowManager = new Mock<IExitFlowManager>();

            var form = new SessionGoalPage(
                mockSessionManager.Object,
                mockFormSwitcher.Object,
                mockInputValidator.Object,
                mockNotificationManager.Object,
                mockUtilityService.Object,
                mockLogger.Object,
                mockFormStateManagement.Object,
                mockButtonHighlighterService.Object,
                exitFlowManager.Object
            );
        


        var session = new CodingSession
            {
                UserId = 42,
                StartDateLocal = DateOnly.FromDateTime(DateTime.Now),
                StartTimeLocal = DateTime.Now,
                GoalSet = false,
                GoalReached = false,
                GoalSeconds = 0
            };

            form.HandleSkipButton();
            mockFormSwitcher.Verify(m => m.SwitchToForm(FormPageEnum.OrbitalTimerPage), Times.Once);


        }
    }
}
