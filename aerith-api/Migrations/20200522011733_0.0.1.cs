using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aerith.Api.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "codes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    value = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 128, nullable: true),
                    badgeSVG = table.Column<string>(maxLength: 128, nullable: true),
                    silhoetteSVG = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.UniqueConstraint("AK_teams_value", x => x.value);
                });

            migrationBuilder.CreateTable(
                name: "leagues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    codeId = table.Column<int>(nullable: false),
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    loginId = table.Column<string>(maxLength: 256, nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    groupId = table.Column<int>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "byes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    roundId = table.Column<int>(nullable: false),
                    teamId = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    leagueId = table.Column<int>(nullable: false),
                    seasonId = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    groupId = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    groupId = table.Column<int>(nullable: false),
                    tournamentId = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    value = table.Column<int>(nullable: false),
                    tournamentId = table.Column<int>(nullable: false)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    createdDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    modifiedBy = table.Column<string>(maxLength: 128, nullable: true, defaultValue: "AERITH"),
                    modifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    isInactive = table.Column<bool>(nullable: false),
                    roundId = table.Column<int>(nullable: false),
                    matchState = table.Column<string>(maxLength: 64, nullable: true),
                    venue = table.Column<string>(maxLength: 256, nullable: true),
                    url = table.Column<string>(maxLength: 1024, nullable: true),
                    homeTeamId = table.Column<int>(nullable: false),
                    awayTeamId = table.Column<int>(nullable: false),
                    teamId = table.Column<int>(nullable: true),
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

            migrationBuilder.InsertData(
                table: "codes",
                columns: new[] { "id", "isInactive", "name" },
                values: new object[] { 1, false, "Rugby League" });

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
                name: "IX_rounds_tournamentId",
                table: "rounds",
                column: "tournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_leagueId",
                table: "tournaments",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_seasonId",
                table: "tournaments",
                column: "seasonId");

            migrationBuilder.CreateIndex(
                name: "IX_users_groupId",
                table: "users",
                column: "groupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "byes");

            migrationBuilder.DropTable(
                name: "competitions");

            migrationBuilder.DropTable(
                name: "fixtures");

            migrationBuilder.DropTable(
                name: "groupUsers");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "leagues");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "codes");
        }
    }
}
