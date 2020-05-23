﻿// <auto-generated />
using System;
using Aerith.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aerith.Api.Migrations
{
    [DbContext(typeof(AerithContext))]
    [Migration("20200522011733_0.0.1")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aerith.Common.Models.Bye", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RoundId")
                        .HasColumnName("roundId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("byes");
                });

            modelBuilder.Entity("Aerith.Common.Models.Code", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("codes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsInactive = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rugby League"
                        });
                });

            modelBuilder.Entity("Aerith.Common.Models.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("GroupId")
                        .HasColumnName("groupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("TournamentId")
                        .HasColumnName("tournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TournamentId");

                    b.ToTable("competitions");
                });

            modelBuilder.Entity("Aerith.Common.Models.Fixture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayTeamId")
                        .HasColumnName("awayTeamId")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamScore")
                        .HasColumnName("awayTeamScore")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("GameMinutes")
                        .HasColumnName("gameMinutes")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnName("homeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnName("homeTeamScore")
                        .HasColumnType("int");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("KickoffTime")
                        .HasColumnName("kickoffTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MatchState")
                        .HasColumnName("matchState")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RoundId")
                        .HasColumnName("roundId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .HasColumnName("url")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Venue")
                        .HasColumnName("venue")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("RoundId");

                    b.HasIndex("TeamId");

                    b.ToTable("fixtures");
                });

            modelBuilder.Entity("Aerith.Common.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("groups");
                });

            modelBuilder.Entity("Aerith.Common.Models.GroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("GroupId")
                        .HasColumnName("groupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("UserId")
                        .HasColumnName("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("groupUsers");
                });

            modelBuilder.Entity("Aerith.Common.Models.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodeId")
                        .HasColumnName("codeId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Value");

                    b.HasIndex("CodeId");

                    b.ToTable("leagues");
                });

            modelBuilder.Entity("Aerith.Common.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TournamentId")
                        .HasColumnName("tournamentId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("rounds");
                });

            modelBuilder.Entity("Aerith.Common.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Value");

                    b.ToTable("seasons");
                });

            modelBuilder.Entity("Aerith.Common.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BadgeSVG")
                        .HasColumnName("badgeSVG")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("SilhoetteSVG")
                        .HasColumnName("silhoetteSVG")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Value");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("Aerith.Common.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<int>("LeagueId")
                        .HasColumnName("leagueId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("SeasonId");

                    b.ToTable("tournaments");
                });

            modelBuilder.Entity("Aerith.Common.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("GroupId")
                        .HasColumnName("groupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsInactive")
                        .HasColumnName("isInactive")
                        .HasColumnType("bit");

                    b.Property<string>("LoginId")
                        .IsRequired()
                        .HasColumnName("loginId")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("modifiedBy")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue("AERITH");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("modifiedDate")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Aerith.Common.Models.Bye", b =>
                {
                    b.HasOne("Aerith.Common.Models.Team", "Team")
                        .WithMany("Byes")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aerith.Common.Models.Competition", b =>
                {
                    b.HasOne("Aerith.Common.Models.Group", "Group")
                        .WithMany("Competitions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aerith.Common.Models.Tournament", "Tournament")
                        .WithMany("Competitions")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aerith.Common.Models.Fixture", b =>
                {
                    b.HasOne("Aerith.Common.Models.Team", "AwayTeam")
                        .WithMany("AwayFixtures")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aerith.Common.Models.Team", "HomeTeam")
                        .WithMany("HomeFixtures")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Aerith.Common.Models.Round", "Round")
                        .WithMany("Fixtures")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aerith.Common.Models.Team", null)
                        .WithMany("Fixtures")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Aerith.Common.Models.GroupUser", b =>
                {
                    b.HasOne("Aerith.Common.Models.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aerith.Common.Models.User", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aerith.Common.Models.League", b =>
                {
                    b.HasOne("Aerith.Common.Models.Code", "Code")
                        .WithMany("Leagues")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aerith.Common.Models.Round", b =>
                {
                    b.HasOne("Aerith.Common.Models.Tournament", "Tournament")
                        .WithMany("Rounds")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aerith.Common.Models.Tournament", b =>
                {
                    b.HasOne("Aerith.Common.Models.League", "League")
                        .WithMany("Tournaments")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aerith.Common.Models.Season", "Season")
                        .WithMany("Tournaments")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aerith.Common.Models.User", b =>
                {
                    b.HasOne("Aerith.Common.Models.Group", null)
                        .WithMany("Users")
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
