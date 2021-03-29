using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroStore.Data.Migrations
{
    public partial class categories_class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_Parent_id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Parent_id",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Parent_id",
                table: "Categories");
        }
    }
}
