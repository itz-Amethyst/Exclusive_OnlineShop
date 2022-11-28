using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountManagement.Infrastructure.EFCore.Migrations
{
    public partial class changedDiscountRateToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Discounts");

            migrationBuilder.AddColumn<int>(
                name: "DiscountRate",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountRate",
                table: "Discounts");

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "Discounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
