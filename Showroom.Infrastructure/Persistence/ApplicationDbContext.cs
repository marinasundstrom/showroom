
using Showroom.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;

namespace Showroom.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<CompetenceArea> CompetenceAreas { get; set; }

        public DbSet<ManagerProfile> ManagersProfiles { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<ConsultantProfile> ConsultantProfiles { get; set; }

        public DbSet<ManagerCompetenceArea> ManagerCompetenceAreas { get; set; }

        public DbSet<UserProfileLink> UserProfileLink { get; set; }

        public DbSet<UserProfileLinkType> UserProfileLinkTypes { get; set; }

        public DbSet<ClientCase> ClientCases { get; set; }

        public DbSet<ConsultantRecommendation> ConsultantRecommendations { get; set; }

        public DbSet<ClientConsultantInterest> ClientConsultantInterests { get; set; }
    }
}
