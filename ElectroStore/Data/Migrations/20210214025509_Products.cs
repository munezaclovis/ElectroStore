using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroStore.Data.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Asins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ean = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keys = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsDateSeen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsDoRecommend = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsNumHelpful = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsSourceURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewsUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                table: "User");
        }
    }
}
