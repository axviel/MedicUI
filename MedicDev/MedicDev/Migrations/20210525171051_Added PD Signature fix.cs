using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicDev.Migrations
{
    public partial class AddedPDSignaturefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicDeclarationSignature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicDeclarationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicDeclarationSignature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicDeclarationSignature_PublicDeclaration_PublicDeclarationId",
                        column: x => x.PublicDeclarationId,
                        principalTable: "PublicDeclaration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicDeclarationSignature_PublicDeclarationId",
                table: "PublicDeclarationSignature",
                column: "PublicDeclarationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicDeclarationSignature");
        }
    }
}
