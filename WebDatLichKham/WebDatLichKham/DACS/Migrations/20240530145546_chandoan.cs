using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class chandoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChanDoan_Khoas_KhoaId",
                table: "ChanDoan");

            migrationBuilder.DropForeignKey(
                name: "FK_DieuTri_ChanDoan_ChanDoanId",
                table: "DieuTri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DieuTri",
                table: "DieuTri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChanDoan",
                table: "ChanDoan");

            migrationBuilder.RenameTable(
                name: "DieuTri",
                newName: "dieuTris");

            migrationBuilder.RenameTable(
                name: "ChanDoan",
                newName: "chanDoans");

            migrationBuilder.RenameIndex(
                name: "IX_DieuTri_ChanDoanId",
                table: "dieuTris",
                newName: "IX_dieuTris_ChanDoanId");

            migrationBuilder.RenameIndex(
                name: "IX_ChanDoan_KhoaId",
                table: "chanDoans",
                newName: "IX_chanDoans_KhoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dieuTris",
                table: "dieuTris",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chanDoans",
                table: "chanDoans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chanDoans_Khoas_KhoaId",
                table: "chanDoans",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dieuTris_chanDoans_ChanDoanId",
                table: "dieuTris",
                column: "ChanDoanId",
                principalTable: "chanDoans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chanDoans_Khoas_KhoaId",
                table: "chanDoans");

            migrationBuilder.DropForeignKey(
                name: "FK_dieuTris_chanDoans_ChanDoanId",
                table: "dieuTris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dieuTris",
                table: "dieuTris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chanDoans",
                table: "chanDoans");

            migrationBuilder.RenameTable(
                name: "dieuTris",
                newName: "DieuTri");

            migrationBuilder.RenameTable(
                name: "chanDoans",
                newName: "ChanDoan");

            migrationBuilder.RenameIndex(
                name: "IX_dieuTris_ChanDoanId",
                table: "DieuTri",
                newName: "IX_DieuTri_ChanDoanId");

            migrationBuilder.RenameIndex(
                name: "IX_chanDoans_KhoaId",
                table: "ChanDoan",
                newName: "IX_ChanDoan_KhoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DieuTri",
                table: "DieuTri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChanDoan",
                table: "ChanDoan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChanDoan_Khoas_KhoaId",
                table: "ChanDoan",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DieuTri_ChanDoan_ChanDoanId",
                table: "DieuTri",
                column: "ChanDoanId",
                principalTable: "ChanDoan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
