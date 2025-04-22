using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.Entities.UserCredentialEntities;
using Microsoft.EntityFrameworkCore;

namespace CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts
{
    public interface ICodingTrackerDbContext
    {
        DbSet<CodingSessionEntity> CodingSessions { get; set; }
        DbSet<UserCredentialEntity> UserCredentials { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
