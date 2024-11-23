using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoGallery.API.Migrations
{
    /// <inheritdoc />
    public partial class GalleriesNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoInGallery_Gallerys_GalleryId",
                table: "PhotoInGallery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gallerys",
                table: "Gallerys");

            migrationBuilder.RenameTable(
                name: "Gallerys",
                newName: "Galleries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Galleries",
                table: "Galleries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoInGallery_Galleries_GalleryId",
                table: "PhotoInGallery",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoInGallery_Galleries_GalleryId",
                table: "PhotoInGallery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Galleries",
                table: "Galleries");

            migrationBuilder.RenameTable(
                name: "Galleries",
                newName: "Gallerys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gallerys",
                table: "Gallerys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoInGallery_Gallerys_GalleryId",
                table: "PhotoInGallery",
                column: "GalleryId",
                principalTable: "Gallerys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
