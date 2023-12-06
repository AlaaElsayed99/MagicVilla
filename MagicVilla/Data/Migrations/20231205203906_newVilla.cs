using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Data.Migrations
{
    /// <inheritdoc />
    public partial class newVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "villaAPIs",
                newName: "UpdatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "villaAPIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Amenity",
                table: "villaAPIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "villaAPIs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "villaAPIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "villaAPIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Occupancy",
                table: "villaAPIs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "villaAPIs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Sqft",
                table: "villaAPIs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amenity",
                table: "villaAPIs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "villaAPIs");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "villaAPIs");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "villaAPIs");

            migrationBuilder.DropColumn(
                name: "Occupancy",
                table: "villaAPIs");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "villaAPIs");

            migrationBuilder.DropColumn(
                name: "Sqft",
                table: "villaAPIs");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "villaAPIs",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "villaAPIs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
