using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaycheckBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddWagesEarnedToWorkday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WagesEarned",
                table: "Workdays",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WagesEarned",
                table: "Workdays");
        }
    }
}
