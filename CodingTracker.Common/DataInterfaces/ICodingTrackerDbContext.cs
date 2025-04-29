using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.Entities.UserCredentialEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts
{
    public interface ICodingTrackerDbContext
    {
        DbSet<CodingSessionEntity> CodingSessions { get; set; }
        DbSet<UserCredentialEntity> UserCredentials { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
