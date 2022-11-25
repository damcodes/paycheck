using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaycheckBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Company = table.Column<string>(type: "text", nullable: false),
                    PayRate = table.Column<double>(type: "double precision", nullable: false),
                    PayPeriodLength = table.Column<TimeSpan>(type: "interval", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paychecks",
                columns: table => new
                {
                    PaycheckId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    JobId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paychecks", x => x.PaycheckId);
                    table.ForeignKey(
                        name: "FK_Paychecks_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paychecks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workdays",
                columns: table => new
                {
                    WorkdayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tips = table.Column<double>(type: "double precision", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PaycheckId = table.Column<int>(type: "integer", nullable: false),
                    JobId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workdays", x => x.WorkdayId);
                    table.ForeignKey(
                        name: "FK_Workdays_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workdays_Paychecks_PaycheckId",
                        column: x => x.PaycheckId,
                        principalTable: "Paychecks",
                        principalColumn: "PaycheckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workdays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                table: "Jobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Paychecks_JobId",
                table: "Paychecks",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Paychecks_UserId",
                table: "Paychecks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_JobId",
                table: "Workdays",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_PaycheckId",
                table: "Workdays",
                column: "PaycheckId");

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_UserId",
                table: "Workdays",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workdays");

            migrationBuilder.DropTable(
                name: "Paychecks");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
