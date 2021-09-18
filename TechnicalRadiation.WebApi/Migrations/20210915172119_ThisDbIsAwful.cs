using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class ThisDbIsAwful : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorsNewsItems");

            migrationBuilder.CreateTable(
                name: "AuthorNewsItems",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorNewsItems", x => new { x.AuthorsId, x.NewsItemsId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorNewsItems");

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
        }
    }
}
