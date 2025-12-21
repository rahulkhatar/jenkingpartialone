using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revision_Part_1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressMarker",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "X");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dishes",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressMarker",
                table: "Restaurants");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dishes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}
