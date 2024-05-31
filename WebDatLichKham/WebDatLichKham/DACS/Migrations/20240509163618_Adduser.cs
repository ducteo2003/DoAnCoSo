using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class Adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BacSis",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BacSis_UserId",
                table: "BacSis",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BacSis_AspNetUsers_UserId",
                table: "BacSis",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BacSis_AspNetUsers_UserId",
                table: "BacSis");

            migrationBuilder.DropIndex(
                name: "IX_BacSis_UserId",
                table: "BacSis");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BacSis");
        }
    }
}
