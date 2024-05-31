using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class addchuandoandieutri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChanDoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhoaId = table.Column<int>(type: "int", nullable: false),
                    MoTaChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChanDoan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChanDoan_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DieuTri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChanDoanId = table.Column<int>(type: "int", nullable: false),
                    MoTaDieuTri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieuTri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DieuTri_ChanDoan_ChanDoanId",
                        column: x => x.ChanDoanId,
                        principalTable: "ChanDoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChanDoan_KhoaId",
                table: "ChanDoan",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_DieuTri_ChanDoanId",
                table: "DieuTri",
                column: "ChanDoanId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DieuTri");

            migrationBuilder.DropTable(
                name: "ChanDoan");
        }
    }
}
