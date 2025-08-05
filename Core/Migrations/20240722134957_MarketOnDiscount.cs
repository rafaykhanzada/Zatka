using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class MarketOnDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "tblRedemption",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "tblDiscountPolicyLine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblDiscountPolicyLine_MarketId",
                table: "tblDiscountPolicyLine",
                column: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblDiscountPolicyLine_tblDealerMarket_MarketId",
                table: "tblDiscountPolicyLine",
                column: "MarketId",
                principalTable: "tblDealerMarket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblDiscountPolicyLine_tblDealerMarket_MarketId",
                table: "tblDiscountPolicyLine");

            migrationBuilder.DropIndex(
                name: "IX_tblDiscountPolicyLine_MarketId",
                table: "tblDiscountPolicyLine");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "tblRedemption");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "tblDiscountPolicyLine");
        }
    }
}
