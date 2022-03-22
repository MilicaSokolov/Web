using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_Projekat.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lokal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Musterija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BrTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musterija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Radnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LokalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Radnici_Lokal_LokalID",
                        column: x => x.LokalID,
                        principalTable: "Lokal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StvariZaIznajmljivanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    LokalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StvariZaIznajmljivanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StvariZaIznajmljivanje_Lokal_LokalID",
                        column: x => x.LokalID,
                        principalTable: "Lokal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Iznajmljivanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrOsoba = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    MusterijaID = table.Column<int>(type: "int", nullable: true),
                    StvarID = table.Column<int>(type: "int", nullable: true),
                    LokalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iznajmljivanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanje_Lokal_LokalID",
                        column: x => x.LokalID,
                        principalTable: "Lokal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanje_Musterija_MusterijaID",
                        column: x => x.MusterijaID,
                        principalTable: "Musterija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanje_StvariZaIznajmljivanje_StvarID",
                        column: x => x.StvarID,
                        principalTable: "StvariZaIznajmljivanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IznajmljivanjeRadnici",
                columns: table => new
                {
                    IznajmljivanjeID = table.Column<int>(type: "int", nullable: false),
                    RadniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IznajmljivanjeRadnici", x => new { x.IznajmljivanjeID, x.RadniciID });
                    table.ForeignKey(
                        name: "FK_IznajmljivanjeRadnici_Iznajmljivanje_IznajmljivanjeID",
                        column: x => x.IznajmljivanjeID,
                        principalTable: "Iznajmljivanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IznajmljivanjeRadnici_Radnici_RadniciID",
                        column: x => x.RadniciID,
                        principalTable: "Radnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanje_LokalID",
                table: "Iznajmljivanje",
                column: "LokalID");

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanje_MusterijaID",
                table: "Iznajmljivanje",
                column: "MusterijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanje_StvarID",
                table: "Iznajmljivanje",
                column: "StvarID");

            migrationBuilder.CreateIndex(
                name: "IX_IznajmljivanjeRadnici_RadniciID",
                table: "IznajmljivanjeRadnici",
                column: "RadniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Radnici_LokalID",
                table: "Radnici",
                column: "LokalID");

            migrationBuilder.CreateIndex(
                name: "IX_StvariZaIznajmljivanje_LokalID",
                table: "StvariZaIznajmljivanje",
                column: "LokalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IznajmljivanjeRadnici");

            migrationBuilder.DropTable(
                name: "Iznajmljivanje");

            migrationBuilder.DropTable(
                name: "Radnici");

            migrationBuilder.DropTable(
                name: "Musterija");

            migrationBuilder.DropTable(
                name: "StvariZaIznajmljivanje");

            migrationBuilder.DropTable(
                name: "Lokal");
        }
    }
}
