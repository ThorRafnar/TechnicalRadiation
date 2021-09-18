using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class WhenWillThisEnd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorNewsItems",
                table: "AuthorNewsItems");

            migrationBuilder.RenameTable(
                name: "AuthorNewsItems",
                newName: "AuthorNewsItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorNewsItem",
                table: "AuthorNewsItem",
                columns: new[] { "AuthorsId", "NewsItemsId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorNewsItem",
                table: "AuthorNewsItem");

            migrationBuilder.RenameTable(
                name: "AuthorNewsItem",
                newName: "AuthorNewsItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorNewsItems",
                table: "AuthorNewsItems",
                columns: new[] { "AuthorsId", "NewsItemsId" });
        }
    }
}
