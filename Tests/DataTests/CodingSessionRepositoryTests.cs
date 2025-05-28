using CodingTracker.Common.DataInterfaces.DbContextService;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Moq;

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
            _mockUtilityService = new Mock<IUtilityService>();

            _mockDbContext.Setup(x => x.CodingSessions).Returns(_mockDbSet.Object);
        }




    }
}
