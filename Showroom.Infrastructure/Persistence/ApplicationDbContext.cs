
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;
using Showroom.Domain.Entities;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ManagerProfile>()
                .HasMany(x => x.ConsultantProfiles)
                .WithOne(x => x.Manager)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ManagerProfile>()
               .HasMany(x => x.ClientProfiles)
               .WithOne(x => x.Reference)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ManagerProfile>()
               .HasMany(x => x.ClientCases)
               .WithOne(x => x.Manager)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ManagerProfile>()
               .HasMany(x => x.Recommendations)
               .WithOne(x => x.Manager)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ManagerProfile>()
                .HasMany(x => x.ManagerCompetenceAreas)
                .WithOne(x => x.Manager)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ClientProfile>()
               .HasMany(x => x.Interests)
               .WithOne(x => x.Client)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ManagerCompetenceArea>()
                .HasOne(x => x.Manager)
                .WithMany(x => x.ManagerCompetenceAreas)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ConsultantRecommendation>()
                 .HasOne(x => x.Consultant)
                 .WithMany(x => x.ConsultantRecommendations)
                 .OnDelete(DeleteBehavior.NoAction);
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
