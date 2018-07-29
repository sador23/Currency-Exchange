using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyExchange.Migrations
{
    public partial class ChangedIdNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Composite");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DailyRate",
                newName: "DailyRateId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Currency",
                newName: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailyRateId",
                table: "DailyRate",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Currency",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Composite",
                nullable: false,
                defaultValue: 0);
        }
    }
}
