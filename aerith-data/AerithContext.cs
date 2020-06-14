using Aerith.Common.Models;
using Aerith.Common.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aerith.Data
{
    public class AerithContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        private const string SQL_DEFAULT_DATE = "GETDATE()";

        public AerithContext(DbContextOptions<AerithContext> options)
        : base(options)
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
        public DbSet<Tip> Tips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity
            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.ToTable("applicationUsers");

                // Primary key
                e.HasKey(u => u.Id);
                e.Property(u => u.Id).HasColumnName("id");

                // Indexes for "normalized" username and email, to allow efficient lookups
                e.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                e.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                // A concurrency token for use with the optimistic concurrency checking
                e.Property(u => u.ConcurrencyStamp)
                    .HasColumnName("concurrencyStamp")
                    .IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                e.Property(u => u.UserName)
                    .HasColumnName("username")
                    .HasMaxLength(256);

                e.Property(u => u.NormalizedUserName)
                    .HasColumnName("normalisedUsername")
                    .HasMaxLength(256);

                e.Property(u => u.Email)
                    .HasColumnName("email")
                    .HasMaxLength(256);

                e.Property(u => u.NormalizedEmail)
                    .HasColumnName("normalisedEmail")
                    .HasMaxLength(256);

                e.Property(u => u.EmailConfirmed).HasColumnName("emailConfirmed");
                e.Property(u => u.PasswordHash).HasColumnName("passwordHash");
                e.Property(u => u.SecurityStamp).HasColumnName("securityStamp");
                e.Property(u => u.PhoneNumber).HasColumnName("phoneNumber");
                e.Property(u => u.PhoneNumberConfirmed).HasColumnName("phoneNumberConfirmed");
                e.Property(u => u.TwoFactorEnabled).HasColumnName("twoFactorEnabled");
                e.Property(u => u.LockoutEnd).HasColumnName("lockoutEnd");
                e.Property(u => u.LockoutEnabled).HasColumnName("lockoutEnabled");
                e.Property(u => u.AccessFailedCount).HasColumnName("accessFailedCount");

                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                e.HasMany<ApplicationUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                e.HasMany<ApplicationUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                e.HasMany<ApplicationUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                e.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable("applicationRoles");

                // Primary key
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).HasColumnName("id");

                // Index for "normalized" role name to allow efficient lookups
                entity.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // A concurrency token for use with the optimistic concurrency checking
                entity.Property(r => r.ConcurrencyStamp)
                    .HasColumnName("concurrencyStamp")
                    .IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                entity.Property(u => u.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256);

                entity.Property(u => u.NormalizedName)
                    .HasColumnName("normalisedName")
                    .HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                entity.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                entity.HasMany<ApplicationRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            modelBuilder.Entity<ApplicationUserClaim>(entity =>
            {
                entity.ToTable("userClaims");

                // Primary key
                entity.HasKey(uc => uc.Id);
                entity.Property(uc => uc.Id).HasColumnName("id");

                entity.Property(uc => uc.UserId).HasColumnName("userId");
                entity.Property(uc => uc.ClaimType).HasColumnName("claimType");
                entity.Property(uc => uc.ClaimValue).HasColumnName("claimValue");
            });

            modelBuilder.Entity<ApplicationUserLogin>(e =>
            {
                e.ToTable("userLogins");

                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                e.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                e.Property(l => l.LoginProvider)
                    .HasColumnName("loginProvider")
                    .HasMaxLength(128);

                e.Property(l => l.ProviderKey)
                    .HasColumnName("providerKey")
                    .HasMaxLength(128);

                e.Property(l => l.ProviderDisplayName).HasColumnName("providerDisplayName");
                e.Property(l => l.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<ApplicationUserToken>(e =>
            {
                e.ToTable("userTokens");

                // Composite primary key consisting of the UserId, LoginProvider and Name
                e.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                e.Property(t => t.LoginProvider)
                    .HasColumnName("loginProvider")
                    .HasMaxLength(2048);

                e.Property(t => t.Name)
                    .HasColumnName("name")
                    .HasMaxLength(2048);   

                e.Property(t => t.UserId).HasColumnName("userId");
                e.Property(t => t.Value).HasColumnName("value");        
            });

            modelBuilder.Entity<ApplicationRoleClaim>(e =>
            {
                e.ToTable("roleClaims");

                // Primary key
                e.HasKey(rc => rc.Id);
                e.Property(rc => rc.Id).HasColumnName("id");

                e.Property(rc => rc.RoleId).HasColumnName("roleId");
                e.Property(rc => rc.ClaimType).HasColumnName("claimType");
                e.Property(rc => rc.ClaimValue).HasColumnName("claimValue");
            });

            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("userRoles");

                // Primary key
                entity.HasKey(r => new { r.UserId, r.RoleId });

                entity.Property(r => r.RoleId).HasColumnName("roleId");
                entity.Property(r => r.UserId).HasColumnName("userId");
            });

            // Roles
            modelBuilder.Entity<ApplicationRole>(e => {
                e.HasData(new ApplicationRole[]{
                    new ApplicationRole {
                        Id = 1L,
                        Name = "Administrators",
                        NormalizedName = "ADMINISTRATORS"
                    },
                    new ApplicationRole {
                        Id = 2L,
                        Name = "Users",
                        NormalizedName = "USERS"
                    }
                });
            });

            var passwordHasher = new PasswordHasher<ApplicationUser>(); 

            modelBuilder.Entity<ApplicationUser>(e => {
                e.HasData(new ApplicationUser[] {
                    new ApplicationUser {
                        Id = 1L,
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        PasswordHash = passwordHasher.HashPassword(null, "P@ssword01")
                    }
                });
            });

            modelBuilder.Entity<ApplicationUserRole>(e => {
                e.HasData(new ApplicationUserRole[]{
                    new ApplicationUserRole {
                        RoleId = 1L,
                        UserId = 1L
                    }
                });
            });

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

                entity.HasIndex(_ => new { _.LeagueId, _.SeasonId }).IsUnique();
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.Property(_ => _.CreatedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.CreatedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);
                entity.Property(_ => _.ModifiedBy).HasDefaultValue("AERITH");
                entity.Property(_ => _.ModifiedDate).HasDefaultValueSql(SQL_DEFAULT_DATE);

                entity.HasIndex(_ => new { _.TournamentId, _.Value }).IsUnique();
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
