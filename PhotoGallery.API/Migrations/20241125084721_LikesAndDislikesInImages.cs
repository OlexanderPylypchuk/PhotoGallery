using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoGallery.API.Migrations
{
    /// <inheritdoc />
    public partial class LikesAndDislikesInImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Photos");
        }
    }
}
