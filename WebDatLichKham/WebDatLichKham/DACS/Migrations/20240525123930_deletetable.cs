using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class deletetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatLichKhams_LichLamViecs_LichLamViecId",
                table: "DatLichKhams");

            migrationBuilder.DropForeignKey(
                name: "FK_LichLamViecs_BacSis_BacSiId",
                table: "LichLamViecs");

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichKhams_LichLamViecs_LichLamViecId",
                table: "DatLichKhams",
                column: "LichLamViecId",
                principalTable: "LichLamViecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LichLamViecs_BacSis_BacSiId",
                table: "LichLamViecs",
                column: "BacSiId",
                principalTable: "BacSis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatLichKhams_LichLamViecs_LichLamViecId",
                table: "DatLichKhams");

            migrationBuilder.DropForeignKey(
                name: "FK_LichLamViecs_BacSis_BacSiId",
                table: "LichLamViecs");

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichKhams_LichLamViecs_LichLamViecId",
                table: "DatLichKhams",
                column: "LichLamViecId",
                principalTable: "LichLamViecs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichLamViecs_BacSis_BacSiId",
                table: "LichLamViecs",
                column: "BacSiId",
                principalTable: "BacSis",
                principalColumn: "Id");
        }
    }
}
