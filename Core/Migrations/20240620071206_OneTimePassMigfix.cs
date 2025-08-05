using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class OneTimePassMigfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OTP",
                table: "OTP");

            migrationBuilder.RenameTable(
                name: "OTP",
                newName: "tblOTP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblOTP",
                table: "tblOTP",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblOTP",
                table: "tblOTP");

            migrationBuilder.RenameTable(
                name: "tblOTP",
                newName: "OTP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OTP",
                table: "OTP",
                column: "Id");
        }
    }
}
