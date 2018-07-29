using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyExchange.Migrations
{
    public partial class ChangedRatesPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyRate_Currency_CurrencyId",
                table: "DailyRate");

            migrationBuilder.DropIndex(
                name: "IX_DailyRate_CurrencyId",
                table: "DailyRate");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "DailyRate");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "DailyRate");

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Composite",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Composite");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "DailyRate",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "DailyRate",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_DailyRate_CurrencyId",
                table: "DailyRate",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyRate_Currency_CurrencyId",
                table: "DailyRate",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
