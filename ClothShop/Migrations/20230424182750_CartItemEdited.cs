using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothShop.Migrations
{
    public partial class CartItemEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CartItems");
        }
    }
}
