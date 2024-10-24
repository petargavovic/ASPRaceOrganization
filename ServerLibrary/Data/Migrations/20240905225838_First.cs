using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKategorije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangListaID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrkaID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vozaci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrkaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangListaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasmanID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UcinakId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozaci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RangListe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KrajSezone = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VozacID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasmanID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UcinakId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategorijaId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangListe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangListe_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojKrugova = table.Column<int>(type: "int", nullable: false),
                    DatumTrke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Staza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VozacId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UcinakId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategorijaId = table.Column<int>(type: "int", nullable: false),
                    TrkaId = table.Column<int>(type: "int", nullable: true),
                    VozacId1 = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trke_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trke_Trke_TrkaId",
                        column: x => x.TrkaId,
                        principalTable: "Trke",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trke_Vozaci_VozacId1",
                        column: x => x.VozacId1,
                        principalTable: "Vozaci",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plasmani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPoena = table.Column<int>(type: "int", nullable: false),
                    VozacID = table.Column<int>(type: "int", nullable: false),
                    RangListaID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plasmani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plasmani_RangListe_RangListaID",
                        column: x => x.RangListaID,
                        principalTable: "RangListe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plasmani_Vozaci_VozacID",
                        column: x => x.VozacID,
                        principalTable: "Vozaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RangListaVozac",
                columns: table => new
                {
                    RangListaId = table.Column<int>(type: "int", nullable: false),
                    VozacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangListaVozac", x => new { x.RangListaId, x.VozacId });
                    table.ForeignKey(
                        name: "FK_RangListaVozac_RangListe_RangListaId",
                        column: x => x.RangListaId,
                        principalTable: "RangListe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RangListaVozac_Vozaci_VozacId",
                        column: x => x.VozacId,
                        principalTable: "Vozaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ucinci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartnaPozicija = table.Column<int>(type: "int", nullable: false),
                    Plasman = table.Column<int>(type: "int", nullable: false),
                    Vremena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VozacID = table.Column<int>(type: "int", nullable: false),
                    TrkaID = table.Column<int>(type: "int", nullable: false),
                    RangListaId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucinci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucinci_RangListe_RangListaId",
                        column: x => x.RangListaId,
                        principalTable: "RangListe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ucinci_Trke_TrkaID",
                        column: x => x.TrkaID,
                        principalTable: "Trke",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ucinci_Vozaci_VozacID",
                        column: x => x.VozacID,
                        principalTable: "Vozaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plasmani_RangListaID",
                table: "Plasmani",
                column: "RangListaID");

            migrationBuilder.CreateIndex(
                name: "IX_Plasmani_VozacID",
                table: "Plasmani",
                column: "VozacID");

            migrationBuilder.CreateIndex(
                name: "IX_RangListaVozac_VozacId",
                table: "RangListaVozac",
                column: "VozacId");

            migrationBuilder.CreateIndex(
                name: "IX_RangListe_KategorijaId",
                table: "RangListe",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trke_KategorijaId",
                table: "Trke",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trke_TrkaId",
                table: "Trke",
                column: "TrkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trke_VozacId1",
                table: "Trke",
                column: "VozacId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ucinci_RangListaId",
                table: "Ucinci",
                column: "RangListaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucinci_TrkaID",
                table: "Ucinci",
                column: "TrkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucinci_VozacID",
                table: "Ucinci",
                column: "VozacID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plasmani");

            migrationBuilder.DropTable(
                name: "RangListaVozac");

            migrationBuilder.DropTable(
                name: "Ucinci");

            migrationBuilder.DropTable(
                name: "RangListe");

            migrationBuilder.DropTable(
                name: "Trke");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "Vozaci");
        }
    }
}
