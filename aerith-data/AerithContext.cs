using Aerith.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerith.Data
{
    public class AerithContext : DbContext
    {
        private const string SQL_DEFAULT_DATE = "GETDATE()";

        public AerithContext(DbContextOptions<AerithContext> options) : base(options)
        {
        }

        public DbSet<Code> Codes { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Bye> Byes { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tip> Tips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Custom Keys
            modelBuilder.Entity<League>(entity =>
            {
                entity.HasAlternateKey(_ => _.Value);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasAlternateKey(_ => _.Value);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasAlternateKey(_ => _.Value);
            });

            // Defaults
            modelBuilder.Entity<Code>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasData(new Code { Id = 1, Name = "Rugby League" });
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasIndex(_ => new { _.LeagueId, _.SeasonId}).IsUnique();
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasIndex(_ => new { _.TournamentId, _.Value}).IsUnique();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasIndex(_ => new { _.Value, _.Name }).IsUnique();
            });

            modelBuilder.Entity<Fixture>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasOne(_ => _.AwayTeam)
                    .WithMany(_ => _.AwayFixtures)
                    .HasForeignKey(_ => _.AwayTeamId)
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(_ => _.HomeTeam)
                    .WithMany(_ => _.HomeFixtures)
                    .HasForeignKey(_ => _.HomeTeamId)
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Bye>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasIndex(_ => new { _.UserId, _.FixtureId, _.CompetitionId }).IsUnique();

                entity.HasOne(_ => _.Fixture)
                    .WithMany(_ => _.Tips)
                    .HasForeignKey(_ => _.FixtureId)
                    .HasPrincipalKey(_ => _.Id)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
