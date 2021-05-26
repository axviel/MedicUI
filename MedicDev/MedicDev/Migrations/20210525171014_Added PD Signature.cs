using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicDev.Migrations
{
    public partial class AddedPDSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeclarationDate",
                table: "PublicDeclaration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "PublicDeclaration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "PublicDeclaration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeclarationDate",
                table: "PublicDeclaration");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PublicDeclaration");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "PublicDeclaration");
        }
    }
}
