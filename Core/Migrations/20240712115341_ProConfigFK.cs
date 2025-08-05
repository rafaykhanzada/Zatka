using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class ProConfigFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "tblProductConfig",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductConfig_ProductId",
                table: "tblProductConfig",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductConfig_tblProduct_ProductId",
                table: "tblProductConfig",
                column: "ProductId",
                principalTable: "tblProduct",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductConfig_tblProduct_ProductId",
                table: "tblProductConfig");

            migrationBuilder.DropIndex(
                name: "IX_tblProductConfig_ProductId",
                table: "tblProductConfig");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "tblProductConfig",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
