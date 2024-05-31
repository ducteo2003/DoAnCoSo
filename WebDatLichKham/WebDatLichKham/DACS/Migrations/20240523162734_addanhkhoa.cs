using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class addanhkhoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Khoas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KhoaId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_KhoaId",
                table: "Images",
                column: "KhoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Khoas_KhoaId",
                table: "Images",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Khoas_KhoaId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_KhoaId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Khoas");

            migrationBuilder.DropColumn(
                name: "KhoaId",
                table: "Images");
        }
    }
}
