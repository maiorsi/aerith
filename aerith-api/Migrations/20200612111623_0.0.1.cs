using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aerith.Api.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationRoles",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalisedName = table.Column<string>(maxLength: 256, nullable: true),
                    concurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationRoles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationUsers",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(maxLength: 256, nullable: true),
                    normalisedUsername = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalisedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    emailConfirmed = table.Column<bool>(nullable: false),
                    passwordHash = table.Column<string>(nullable: true),
                    securityStamp = table.Column<string>(nullable: true),
                    concurrencyStamp = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    phoneNumberConfirmed = table.Column<bool>(nullable: false),
                    twoFactorEnabled = table.Column<bool>(nullable: false),
                    lockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    lockoutEnabled = table.Column<bool>(nullable: false),
                    accessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "codes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: true),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.id);
                    table.UniqueConstraint("AK_seasons_value", x => x.value);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    value = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 128, nullable: true),
                    badgeSVG = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.UniqueConstraint("AK_teams_value", x => x.value);
                });

            migrationBuilder.CreateTable(
                name: "roleClaims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleId = table.Column<long>(nullable: false),
                    claimType = table.Column<string>(nullable: true),
                    claimValue = table.Column<string>(nullable: true),
                    ApplicationRoleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roleClaims", x => x.id);
                    table.ForeignKey(
                        name: "FK_roleClaims_applicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "applicationRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_roleClaims_applicationRoles_roleId",
                        column: x => x.roleId,
                        principalTable: "applicationRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userClaims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<long>(nullable: false),
                    claimType = table.Column<string>(nullable: true),
                    claimValue = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userClaims", x => x.id);
                    table.ForeignKey(
                        name: "FK_userClaims_applicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userClaims_applicationUsers_userId",
                        column: x => x.userId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userLogins",
                columns: table => new
                {
                    loginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    providerKey = table.Column<string>(maxLength: 128, nullable: false),
                    providerDisplayName = table.Column<string>(nullable: true),
                    userId = table.Column<long>(nullable: false),
                    ApplicationUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userLogins", x => new { x.loginProvider, x.providerKey });
                    table.ForeignKey(
                        name: "FK_userLogins_applicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userLogins_applicationUsers_userId",
                        column: x => x.userId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userRoles",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    roleId = table.Column<long>(nullable: false),
                    UserId1 = table.Column<long>(nullable: true),
                    RoleId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoles", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "FK_userRoles_applicationRoles_roleId",
                        column: x => x.roleId,
                        principalTable: "applicationRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRoles_applicationRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "applicationRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userRoles_applicationUsers_userId",
                        column: x => x.userId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRoles_applicationUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userTokens",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    loginProvider = table.Column<string>(maxLength: 2048, nullable: false),
                    name = table.Column<string>(maxLength: 2048, nullable: false),
                    value = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTokens", x => new { x.userId, x.loginProvider, x.name });
                    table.ForeignKey(
                        name: "FK_userTokens_applicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userTokens_applicationUsers_userId",
                        column: x => x.userId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leagues",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    codeId = table.Column<long>(nullable: false),
                    value = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leagues", x => x.id);
                    table.UniqueConstraint("AK_leagues_value", x => x.value);
                    table.ForeignKey(
                        name: "FK_leagues_codes_codeId",
                        column: x => x.codeId,
                        principalTable: "codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    identityId = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    groupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_applicationUsers_identityId",
                        column: x => x.identityId,
                        principalTable: "applicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "byes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    roundId = table.Column<long>(nullable: false),
                    teamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_byes", x => x.id);
                    table.ForeignKey(
                        name: "FK_byes_teams_teamId",
                        column: x => x.teamId,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    leagueId = table.Column<long>(nullable: false),
                    seasonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournaments", x => x.id);
                    table.ForeignKey(
                        name: "FK_tournaments_leagues_leagueId",
                        column: x => x.leagueId,
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tournaments_seasons_seasonId",
                        column: x => x.seasonId,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groupUsers",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    userId = table.Column<long>(nullable: false),
                    groupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_groupUsers_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupUsers_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "competitions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    groupId = table.Column<long>(nullable: false),
                    tournamentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_competitions", x => x.id);
                    table.ForeignKey(
                        name: "FK_competitions_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_competitions_tournaments_tournamentId",
                        column: x => x.tournamentId,
                        principalTable: "tournaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    value = table.Column<int>(nullable: false),
                    tournamentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rounds", x => x.id);
                    table.ForeignKey(
                        name: "FK_rounds_tournaments_tournamentId",
                        column: x => x.tournamentId,
                        principalTable: "tournaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixtures",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    roundId = table.Column<long>(nullable: false),
                    matchState = table.Column<string>(maxLength: 64, nullable: true),
                    venue = table.Column<string>(maxLength: 256, nullable: true),
                    url = table.Column<string>(maxLength: 1024, nullable: true),
                    homeTeamId = table.Column<long>(nullable: false),
                    awayTeamId = table.Column<long>(nullable: false),
                    teamId = table.Column<long>(nullable: true),
                    homeTeamScore = table.Column<int>(nullable: false),
                    awayTeamScore = table.Column<int>(nullable: false),
                    kickoffTime = table.Column<DateTime>(nullable: false),
                    gameMinutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fixtures", x => x.id);
                    table.ForeignKey(
                        name: "FK_fixtures_teams_awayTeamId",
                        column: x => x.awayTeamId,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fixtures_teams_homeTeamId",
                        column: x => x.homeTeamId,
                        principalTable: "teams",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_fixtures_rounds_roundId",
                        column: x => x.roundId,
                        principalTable: "rounds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fixtures_teams_teamId",
                        column: x => x.teamId,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tips",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    userId = table.Column<long>(nullable: false),
                    fixtureId = table.Column<long>(nullable: false),
                    competitionId = table.Column<long>(nullable: false),
                    selectedTeamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tips", x => x.id);
                    table.ForeignKey(
                        name: "FK_tips_competitions_competitionId",
                        column: x => x.competitionId,
                        principalTable: "competitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tips_fixtures_fixtureId",
                        column: x => x.fixtureId,
                        principalTable: "fixtures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tips_teams_selectedTeamId",
                        column: x => x.selectedTeamId,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tips_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "applicationRoles",
                columns: new[] { "id", "concurrencyStamp", "name", "normalisedName" },
                values: new object[,]
                {
                    { 1L, "dfde25b6-1ae7-4b6d-9c3b-49a137130137", "Administrators", "ADMINISTRATORS" },
                    { 2L, "a3b2c7e6-c86a-451f-8060-425717e6be70", "Users", "USERS" }
                });

            migrationBuilder.InsertData(
                table: "applicationUsers",
                columns: new[] { "id", "accessFailedCount", "concurrencyStamp", "email", "emailConfirmed", "lockoutEnabled", "lockoutEnd", "normalisedEmail", "normalisedUsername", "passwordHash", "phoneNumber", "phoneNumberConfirmed", "securityStamp", "twoFactorEnabled", "username" },
                values: new object[] { 1L, 0, "6ab64169-26f6-456c-9b24-cc05c1cf1b4a", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEB2PdvJYyIJBrL5CrstJcZRU92tMiz5HBW1MP2kFejCEpPEzFIKofLIABoE+xHxGew==", null, false, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "codes",
                columns: new[] { "id", "createdDate", "isInactive", "name" },
                values: new object[] { 1L, new DateTime(2020, 6, 12, 21, 16, 23, 50, DateTimeKind.Local).AddTicks(2387), false, "Rugby League" });

            migrationBuilder.InsertData(
                table: "userRoles",
                columns: new[] { "userId", "roleId", "RoleId1", "UserId1" },
                values: new object[] { 1L, 1L, null, null });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "applicationRoles",
                column: "normalisedName",
                unique: true,
                filter: "[normalisedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "applicationUsers",
                column: "normalisedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "applicationUsers",
                column: "normalisedUsername",
                unique: true,
                filter: "[normalisedUsername] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_byes_teamId",
                table: "byes",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "IX_competitions_groupId",
                table: "competitions",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_competitions_tournamentId",
                table: "competitions",
                column: "tournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_fixtures_awayTeamId",
                table: "fixtures",
                column: "awayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_fixtures_homeTeamId",
                table: "fixtures",
                column: "homeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_fixtures_roundId",
                table: "fixtures",
                column: "roundId");

            migrationBuilder.CreateIndex(
                name: "IX_fixtures_teamId",
                table: "fixtures",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "IX_groupUsers_groupId",
                table: "groupUsers",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_groupUsers_userId",
                table: "groupUsers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_leagues_codeId",
                table: "leagues",
                column: "codeId");

            migrationBuilder.CreateIndex(
                name: "IX_roleClaims_ApplicationRoleId",
                table: "roleClaims",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_roleClaims_roleId",
                table: "roleClaims",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_tournamentId_value",
                table: "rounds",
                columns: new[] { "tournamentId", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teams_value_name",
                table: "teams",
                columns: new[] { "value", "name" },
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tips_competitionId",
                table: "tips",
                column: "competitionId");

            migrationBuilder.CreateIndex(
                name: "IX_tips_fixtureId",
                table: "tips",
                column: "fixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_tips_selectedTeamId",
                table: "tips",
                column: "selectedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_tips_userId_fixtureId_competitionId",
                table: "tips",
                columns: new[] { "userId", "fixtureId", "competitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_seasonId",
                table: "tournaments",
                column: "seasonId");

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_leagueId_seasonId",
                table: "tournaments",
                columns: new[] { "leagueId", "seasonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userClaims_ApplicationUserId",
                table: "userClaims",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userClaims_userId",
                table: "userClaims",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userLogins_ApplicationUserId",
                table: "userLogins",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userLogins_userId",
                table: "userLogins",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_roleId",
                table: "userRoles",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_RoleId1",
                table: "userRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_UserId1",
                table: "userRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_users_groupId",
                table: "users",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_users_identityId",
                table: "users",
                column: "identityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userTokens_ApplicationUserId",
                table: "userTokens",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "byes");

            migrationBuilder.DropTable(
                name: "groupUsers");

            migrationBuilder.DropTable(
                name: "roleClaims");

            migrationBuilder.DropTable(
                name: "tips");

            migrationBuilder.DropTable(
                name: "userClaims");

            migrationBuilder.DropTable(
                name: "userLogins");

            migrationBuilder.DropTable(
                name: "userRoles");

            migrationBuilder.DropTable(
                name: "userTokens");

            migrationBuilder.DropTable(
                name: "competitions");

            migrationBuilder.DropTable(
                name: "fixtures");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "applicationRoles");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "applicationUsers");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "leagues");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "codes");
        }
    }
}
