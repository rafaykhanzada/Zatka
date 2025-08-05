using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class MarketOnDiscountFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblDiscountPolicyLine_tblDealerMarket_MarketId",
                table: "tblDiscountPolicyLine");

            migrationBuilder.RenameColumn(
                name: "MarketId",
                table: "tblDiscountPolicyLine",
                newName: "DealerMarketId");

            migrationBuilder.RenameIndex(
                name: "IX_tblDiscountPolicyLine_MarketId",
                table: "tblDiscountPolicyLine",
                newName: "IX_tblDiscountPolicyLine_DealerMarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblDiscountPolicyLine_tblDealerMarket_DealerMarketId",
                table: "tblDiscountPolicyLine",
                column: "DealerMarketId",
                principalTable: "tblDealerMarket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblDiscountPolicyLine_tblDealerMarket_DealerMarketId",
                table: "tblDiscountPolicyLine");

            migrationBuilder.RenameColumn(
                name: "DealerMarketId",
                table: "tblDiscountPolicyLine",
                newName: "MarketId");

            migrationBuilder.RenameIndex(
                name: "IX_tblDiscountPolicyLine_DealerMarketId",
                table: "tblDiscountPolicyLine",
                newName: "IX_tblDiscountPolicyLine_MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblDiscountPolicyLine_tblDealerMarket_MarketId",
                table: "tblDiscountPolicyLine",
                column: "MarketId",
                principalTable: "tblDealerMarket",
                principalColumn: "Id");
        }
    }
}
