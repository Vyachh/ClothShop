using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothShop.Migrations
{
    public partial class CartItemCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartItemCount",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartItemCount",
                table: "CartItems");
        }
    }
}
