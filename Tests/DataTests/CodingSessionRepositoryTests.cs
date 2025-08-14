using CodingTracker.Common.DataInterfaces.DbContextService;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.Utilities;
using CodingTracker.Data.Repositories.CodingSessionRepositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CodingTracker.Tests.Repositories
{
    public class CodingSessionRepositoryTests
    {
        private readonly Mock<IApplicationLogger> _mockLogger;
        private readonly Mock<ICodingTrackerDbContext> _mockDbContext;
        private readonly Mock<DbSet<CodingSessionEntity>> _mockDbSet;
        private readonly Mock<IUtilityService> _mockUtilityService;

        public CodingSessionRepositoryTests()
        {
            _mockLogger = new Mock<IApplicationLogger>();
            _mockDbContext = new Mock<ICodingTrackerDbContext>();
            _mockDbSet = new Mock<DbSet<CodingSessionEntity>>();
            _mockUtilityService = new Mock<IUtilityService>();
            _mockDbContext.Setup(x => x.CodingSessions).Returns(_mockDbSet.Object);
        }

        [Fact]
        public async Task AddCodingSessionEntityAsync_WhenSessionDoesNotExist_ShouldReturnTrueAndAddSession()
        {
            var testSession = new CodingSessionEntity
            {
                SessionId = 1,
                UserId = 100,
                StartDateUTC = DateOnly.FromDateTime(DateTime.UtcNow),
                DurationSeconds = 3600
            };

            _mockDbSet.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<CodingSessionEntity, bool>>>(), default))
                     .ReturnsAsync(false);

            _mockDbContext.Setup(x => x.SaveChangesAsync(default))
                          .ReturnsAsync(1);

            var repository = new CodingSessionRepository(_mockLogger.Object, _mockDbContext.Object);

            var result = await repository.AddCodingSessionEntityAsync(testSession);

            Assert.True(result);
            _mockDbSet.Verify(x => x.Add(testSession), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
            _mockLogger.Verify(x => x.Debug(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task AddCodingSessionEntityAsync_WhenSessionAlreadyExists_ShouldThrowInvalidOperationException()
        {
            var testSession = new CodingSessionEntity
            {
                SessionId = 1,
                UserId = 100
            };

            _mockDbSet.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<CodingSessionEntity, bool>>>(), default))
                     .ReturnsAsync(true);

            var repository = new CodingSessionRepository(_mockLogger.Object, _mockDbContext.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repository.AddCodingSessionEntityAsync(testSession));

            Assert.Contains("A CodingSession with SessionId 1 already exists.", exception.Message);
            _mockDbSet.Verify(x => x.Add(It.IsAny<CodingSessionEntity>()), Times.Never);
            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Never);
        }
    }
}