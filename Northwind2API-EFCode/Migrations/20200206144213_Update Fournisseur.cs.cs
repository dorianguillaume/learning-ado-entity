using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind2API_EFCode.Migrations
{
    public partial class UpdateFournisseurcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageAccueil",
                table: "Fournisseurs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitreContact",
                table: "Fournisseurs",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageAccueil",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "TitreContact",
                table: "Fournisseurs");
        }
    }
}
