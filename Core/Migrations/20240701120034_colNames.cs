using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class colNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tblProduct",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tblCategory",
                newName: "CategoryName");

            migrationBuilder.AddColumn<int>(
                name: "DealerMarketId",
                table: "tblQRCode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblDealerMarket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDealerMarket", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblQRCode_DealerMarketId",
                table: "tblQRCode",
                column: "DealerMarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblQRCode_tblDealerMarket_DealerMarketId",
                table: "tblQRCode",
                column: "DealerMarketId",
                principalTable: "tblDealerMarket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblQRCode_tblDealerMarket_DealerMarketId",
                table: "tblQRCode");

            migrationBuilder.DropTable(
                name: "tblDealerMarket");

            migrationBuilder.DropIndex(
                name: "IX_tblQRCode_DealerMarketId",
                table: "tblQRCode");

            migrationBuilder.DropColumn(
                name: "DealerMarketId",
                table: "tblQRCode");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "tblProduct",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "tblCategory",
                newName: "Name");
        }
    }
}
