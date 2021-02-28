using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Showroom.Domain.Entities;

namespace Showroom.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Organization> Organizations { get; set; }
        DbSet<CompetenceArea> CompetenceAreas { get; set; }
        DbSet<ManagerProfile> ManagersProfiles { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<ClientProfile> ClientProfiles { get; set; }
        DbSet<ConsultantProfile> ConsultantProfiles { get; set; }
        DbSet<ManagerCompetenceArea> ManagerCompetenceAreas { get; set; }
        DbSet<UserProfileLink> UserProfileLink { get; set; }
        DbSet<UserProfileLinkType> UserProfileLinkTypes { get; set; }
        DbSet<ClientCase> ClientCases { get; set; }
        DbSet<ConsultantRecommendation> ConsultantRecommendations { get; set; }
        DbSet<ClientConsultantInterest> ClientConsultantInterests { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    }
}
