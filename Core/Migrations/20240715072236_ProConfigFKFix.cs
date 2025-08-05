using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class ProConfigFKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "tblProductConfig",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProductConfig_CategoryId",
                table: "tblProductConfig",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductConfig_tblCategory_CategoryId",
                table: "tblProductConfig",
                column: "CategoryId",
                principalTable: "tblCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProductConfig_tblCategory_CategoryId",
                table: "tblProductConfig");

            migrationBuilder.DropIndex(
                name: "IX_tblProductConfig_CategoryId",
                table: "tblProductConfig");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "tblProductConfig");
        }
    }
}
