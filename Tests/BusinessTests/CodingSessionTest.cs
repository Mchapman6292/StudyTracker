using CodingTracker.Common.CodingSessions;
using Xunit;

namespace Tests.BusinessTests.CodingSessionTests
{
    public class CodingSessionTest
    {

        // CodingSession object is created once the user logs in with only the userId defined, the other values are populated once the user starts a session & then converted to a CodingSessionEntity before being added to the database. 
        [Fact]
        public void CodingSessionInitializedWithOnlyUserId()
        {
            int userId = 42;
            var session = new CodingSession { UserId = userId };

            Assert.Equal(userId, session.UserId);
            Assert.Null(session.SessionId);
            Assert.Null(session.StartDate);
            Assert.Null(session.StartTime);
            Assert.Null(session.EndDate);
            Assert.Null(session.EndTime);
            Assert.Null(session.DurationSeconds);
            Assert.Equal(string.Empty, session.DurationHHMM);
            Assert.False(session.GoalSet);
            Assert.Null(session.GoalSeconds);
            Assert.Null(session.GoalReached);
        }


        [Fact]
        public void CodingSessionStartedCorrectly()
        {
            int userId = 42;
            DateTime currentDateTime = DateTime.Now;
            DateOnly currentDate = DateOnly.FromDateTime(currentDateTime);
            bool goalSet = true;
            int goalMinutes = 60;

            var session = new CodingSession
            {
                UserId = userId,
                StartDate = currentDate,
                StartTime = currentDateTime,
                GoalSet = goalSet,
                GoalSeconds = goalMinutes
            };

            Assert.Equal(userId, session.UserId);
            Assert.Equal(currentDate, session.StartDate);
            Assert.Equal(currentDateTime, session.StartTime);
            Assert.Equal(goalSet, session.GoalSet);
            Assert.Equal(goalMinutes, session.GoalSeconds);
            Assert.Null(session.EndDate);
            Assert.Null(session.EndTime);
            Assert.Null(session.GoalReached);
            Assert.Null(session.DurationSeconds);
            Assert.Empty(session.DurationHHMM);

        }


        [Fact]
        public void CompletingGoal_SetsGoalReachedToTrue()
        {
            var now = DateTime.Now;
            var session = new CodingSession
            {
                UserId = 42,
                StartDate = DateOnly.FromDateTime(now.AddMinutes(-60)),
                StartTime = now.AddMinutes(-60),
                GoalSet = true,
                GoalSeconds = 45
            };

            session.EndDate = DateOnly.FromDateTime(now);
            session.EndTime = now;
            session.DurationSeconds = 60 * 60;
            session.GoalReached = session.DurationSeconds >= session.GoalSeconds * 60;

            Assert.True(session.GoalReached);
        }

        [Fact]
        public void SessionGoalSetToFalseWhenNoGoalSet()
        {
            var session = new CodingSession
            {
                UserId = 42,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                StartTime = DateTime.Now,
                GoalSet = false,
                GoalReached = false,
                GoalSeconds = 0
            };
        }
    }
}
