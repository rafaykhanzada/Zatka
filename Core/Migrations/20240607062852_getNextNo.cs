using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class getNextNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "tblRedemption",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPolicyId",
                table: "tblRedemption",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "tblRedemption",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "tblRedemption",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblNextNo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocLength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Increment = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_tblNextNo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblNextNo");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "tblRedemption");

            migrationBuilder.DropColumn(
                name: "DiscountPolicyId",
                table: "tblRedemption");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "tblRedemption");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "tblRedemption");
        }
    }
}
