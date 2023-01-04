using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trustme.MigrationConsole.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrustFrameworkPolicies",
                columns: table => new
                {
                    DbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustFrameworkPolicies", x => x.DbKey);
                });

            migrationBuilder.CreateTable(
                name: "SubJourneys",
                columns: table => new
                {
                    DbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyDbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubJourneys", x => x.DbKey);
                    table.ForeignKey(
                        name: "FK_SubJourneys_TrustFrameworkPolicies_PolicyDbKey",
                        column: x => x.PolicyDbKey,
                        principalTable: "TrustFrameworkPolicies",
                        principalColumn: "DbKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserJourneys",
                columns: table => new
                {
                    DbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyDbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssuranceLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJourneys", x => x.DbKey);
                    table.ForeignKey(
                        name: "FK_UserJourneys_TrustFrameworkPolicies_PolicyDbKey",
                        column: x => x.PolicyDbKey,
                        principalTable: "TrustFrameworkPolicies",
                        principalColumn: "DbKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrchestrationSteps",
                columns: table => new
                {
                    DbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OrchestrationStepJourney = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    JourneyDbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrchestrationStepUserJourneyJourneyDbKey = table.Column<Guid>(name: "OrchestrationStepUserJourney_JourneyDbKey", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrchestrationSteps", x => x.DbKey);
                    table.ForeignKey(
                        name: "FK_OrchestrationSteps_SubJourneys_JourneyDbKey",
                        column: x => x.JourneyDbKey,
                        principalTable: "SubJourneys",
                        principalColumn: "DbKey");
                    table.ForeignKey(
                        name: "FK_OrchestrationSteps_UserJourneys_OrchestrationStepUserJourney_JourneyDbKey",
                        column: x => x.OrchestrationStepUserJourneyJourneyDbKey,
                        principalTable: "UserJourneys",
                        principalColumn: "DbKey");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    DbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubJourneyDbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubJourneyReferenceId = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    OrchestrationStepDbKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.DbKey);
                    table.ForeignKey(
                        name: "FK_Candidates_OrchestrationSteps_OrchestrationStepDbKey",
                        column: x => x.OrchestrationStepDbKey,
                        principalTable: "OrchestrationSteps",
                        principalColumn: "DbKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_SubJourneys_SubJourneyDbKey",
                        column: x => x.SubJourneyDbKey,
                        principalTable: "SubJourneys",
                        principalColumn: "DbKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_OrchestrationStepDbKey",
                table: "Candidates",
                column: "OrchestrationStepDbKey");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_SubJourneyDbKey",
                table: "Candidates",
                column: "SubJourneyDbKey");

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationSteps_JourneyDbKey",
                table: "OrchestrationSteps",
                column: "JourneyDbKey");

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationSteps_OrchestrationStepUserJourney_JourneyDbKey",
                table: "OrchestrationSteps",
                column: "OrchestrationStepUserJourney_JourneyDbKey");

            migrationBuilder.CreateIndex(
                name: "IX_SubJourneys_PolicyDbKey",
                table: "SubJourneys",
                column: "PolicyDbKey");

            migrationBuilder.CreateIndex(
                name: "IX_UserJourneys_PolicyDbKey",
                table: "UserJourneys",
                column: "PolicyDbKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "OrchestrationSteps");

            migrationBuilder.DropTable(
                name: "SubJourneys");

            migrationBuilder.DropTable(
                name: "UserJourneys");

            migrationBuilder.DropTable(
                name: "TrustFrameworkPolicies");
        }
    }
}
