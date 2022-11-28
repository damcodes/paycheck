using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaycheckBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountToPaycheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Paychecks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Paychecks");
        }
    }
}
