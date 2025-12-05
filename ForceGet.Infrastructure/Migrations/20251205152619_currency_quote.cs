using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForceGet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class currency_quote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Quotes",
                newName: "ToCurrency");

            migrationBuilder.RenameColumn(
                name: "ConvertedUSD",
                table: "Quotes",
                newName: "ConvertedAmount");

            migrationBuilder.AddColumn<int>(
                name: "FromCurrency",
                table: "Quotes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromCurrency",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "ToCurrency",
                table: "Quotes",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "ConvertedAmount",
                table: "Quotes",
                newName: "ConvertedUSD");
        }
    }
}
