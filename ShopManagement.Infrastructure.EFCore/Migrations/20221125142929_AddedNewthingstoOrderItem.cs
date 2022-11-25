using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddedNewthingstoOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "OrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "HasDiscount",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ItemPayAmount",
                table: "OrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalItemPrice",
                table: "OrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UnitPriceWithDiscount",
                table: "OrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "HasDiscount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ItemPayAmount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalItemPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UnitPriceWithDiscount",
                table: "OrderItems");
        }
    }
}
