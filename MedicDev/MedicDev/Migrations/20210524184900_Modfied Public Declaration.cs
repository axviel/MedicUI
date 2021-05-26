using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicDev.Migrations
{
    public partial class ModfiedPublicDeclaration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "PublicDeclaration");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "PublicDeclaration");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PublicDeclaration",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PublicDeclaration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PublicDeclaration",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PublicDeclaration");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PublicDeclaration");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "PublicDeclaration",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "PublicDeclaration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "PublicDeclaration",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
