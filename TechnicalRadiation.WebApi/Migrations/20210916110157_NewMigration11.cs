using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class NewMigration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoryNewsItem_NewsItemsId",
                table: "CategoryNewsItem",
                column: "NewsItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorNewsItem_NewsItemsId",
                table: "AuthorNewsItem",
                column: "NewsItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorNewsItem_Authors_AuthorsId",
                table: "AuthorNewsItem",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorNewsItem_NewsItems_NewsItemsId",
                table: "AuthorNewsItem",
                column: "NewsItemsId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryNewsItem_Categories_CategoriesId",
                table: "CategoryNewsItem",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryNewsItem_NewsItems_NewsItemsId",
                table: "CategoryNewsItem",
                column: "NewsItemsId",
                principalTable: "NewsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorNewsItem_Authors_AuthorsId",
                table: "AuthorNewsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorNewsItem_NewsItems_NewsItemsId",
                table: "AuthorNewsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryNewsItem_Categories_CategoriesId",
                table: "CategoryNewsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryNewsItem_NewsItems_NewsItemsId",
                table: "CategoryNewsItem");

            migrationBuilder.DropIndex(
                name: "IX_CategoryNewsItem_NewsItemsId",
                table: "CategoryNewsItem");

            migrationBuilder.DropIndex(
                name: "IX_AuthorNewsItem_NewsItemsId",
                table: "AuthorNewsItem");
        }
    }
}
