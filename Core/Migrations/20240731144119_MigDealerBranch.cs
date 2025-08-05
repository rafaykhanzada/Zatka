using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class MigDealerBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "tblQRCode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblDealerBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    DealerId = table.Column<int>(type: "int", nullable: true),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDealerBranch", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblQRCode_BranchId",
                table: "tblQRCode",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblQRCode_tblDealerBranch_BranchId",
                table: "tblQRCode",
                column: "BranchId",
                principalTable: "tblDealerBranch",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblQRCode_tblDealerBranch_BranchId",
                table: "tblQRCode");

            migrationBuilder.DropTable(
                name: "tblDealerBranch");

            migrationBuilder.DropIndex(
                name: "IX_tblQRCode_BranchId",
                table: "tblQRCode");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "tblQRCode");
        }
    }
}
