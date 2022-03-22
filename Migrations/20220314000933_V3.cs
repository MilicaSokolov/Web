using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_Projekat.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolicina",
                table: "Iznajmljivanje");

            migrationBuilder.AddColumn<int>(
                name: "MaxBrOsoba",
                table: "StvariZaIznajmljivanje",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxBrOsoba",
                table: "StvariZaIznajmljivanje");

            migrationBuilder.AddColumn<int>(
                name: "Kolicina",
                table: "Iznajmljivanje",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
