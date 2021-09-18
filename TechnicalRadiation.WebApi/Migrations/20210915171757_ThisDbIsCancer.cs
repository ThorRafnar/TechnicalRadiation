using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class ThisDbIsCancer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsItemsAuthors");

            migrationBuilder.DropTable(
                name: "NewsItemsCategories");

            migrationBuilder.CreateTable(
                name: "AuthorsNewsItems",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsNewsItems", x => new { x.AuthorsId, x.NewsItemsId });
                });

            migrationBuilder.CreateTable(
                name: "CategoryNewsItem",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNewsItem", x => new { x.CategoriesId, x.NewsItemsId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorsNewsItems");

            migrationBuilder.DropTable(
                name: "CategoryNewsItem");

            migrationBuilder.CreateTable(
                name: "NewsItemsAuthors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemsAuthors", x => new { x.AuthorId, x.NewsItemId });
                });

            migrationBuilder.CreateTable(
                name: "NewsItemsCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemsCategories", x => new { x.CategoryId, x.NewsItemId });
                });
        }
    }
}
