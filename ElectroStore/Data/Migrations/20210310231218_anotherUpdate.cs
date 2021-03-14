using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroStore.Data.Migrations
{
    public partial class anotherUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_Parent_id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Parent_id",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PrimaryCategories",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SourceURLs",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Parent_id",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Upc",
                table: "Products",
                newName: "Category");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "Upc");

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCategories",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceURLs",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Parent_id",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Parent_id",
                table: "Categories",
                column: "Parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_Parent_id",
                table: "Categories",
                column: "Parent_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
