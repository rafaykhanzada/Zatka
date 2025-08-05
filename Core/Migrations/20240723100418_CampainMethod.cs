using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class CampainMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DealerRebate",
                table: "tblDiscountPolicyLine",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProductPrice",
                table: "tblDiscountPolicyLine",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "tblDiscountPolicyLine",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                table: "tblDiscountPolicyLine",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealerRebate",
                table: "tblDiscountPolicyLine");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "tblDiscountPolicyLine");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "tblDiscountPolicyLine");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "tblDiscountPolicyLine");
        }
    }
}
