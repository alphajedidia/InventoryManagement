using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionStock.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    NumBon = table.Column<string>(type: "TEXT", nullable: true),
                    DateActivite = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TypeActivite = table.Column<string>(type: "TEXT", nullable: true),
                    NomClientFournisseur = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Entrees",
                columns: table => new
                {
                    NumBonEntre = table.Column<string>(type: "TEXT", nullable: false),
                    DateEntree = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NomFournisseur = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrees", x => x.NumBonEntre);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyRankResults",
                columns: table => new
                {
                    IdProduit = table.Column<int>(type: "INTEGER", nullable: false),
                    Designation = table.Column<string>(type: "TEXT", nullable: true),
                    Descriptions = table.Column<string>(type: "TEXT", nullable: true),
                    Img = table.Column<string>(type: "TEXT", nullable: true),
                    QuantiteStock = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalSorties = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    IdProduit = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Designation = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descriptions = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Img = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    QuantiteStock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.IdProduit);
                });

            migrationBuilder.CreateTable(
                name: "Sorties",
                columns: table => new
                {
                    NumBonSortie = table.Column<string>(type: "TEXT", nullable: false),
                    DateSortie = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NomClient = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorties", x => x.NumBonSortie);
                });

            migrationBuilder.CreateTable(
                name: "StatisticsResults",
                columns: table => new
                {
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    NbrEntree = table.Column<int>(type: "INTEGER", nullable: false),
                    NbrSortie = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "StockersWithDesignation",
                columns: table => new
                {
                    IdStock = table.Column<int>(type: "INTEGER", nullable: false),
                    NumBonEntre = table.Column<string>(type: "TEXT", nullable: true),
                    IdProduit = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantiteEntree = table.Column<int>(type: "INTEGER", nullable: false),
                    Designation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Stockers",
                columns: table => new
                {
                    IdStock = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumBonEntre = table.Column<string>(type: "TEXT", nullable: false),
                    IdProduit = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantiteEntree = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockers", x => x.IdStock);
                    table.ForeignKey(
                        name: "FK_Stockers_Entrees_NumBonEntre",
                        column: x => x.NumBonEntre,
                        principalTable: "Entrees",
                        principalColumn: "NumBonEntre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stockers_Produits_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produits",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destockers",
                columns: table => new
                {
                    IdDestock = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumBonSortie = table.Column<string>(type: "TEXT", nullable: false),
                    IdProduit = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantiteSortie = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destockers", x => x.IdDestock);
                    table.ForeignKey(
                        name: "FK_Destockers_Produits_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produits",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Destockers_Sorties_NumBonSortie",
                        column: x => x.NumBonSortie,
                        principalTable: "Sorties",
                        principalColumn: "NumBonSortie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destockers_IdProduit",
                table: "Destockers",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Destockers_NumBonSortie",
                table: "Destockers",
                column: "NumBonSortie");

            migrationBuilder.CreateIndex(
                name: "IX_Stockers_IdProduit",
                table: "Stockers",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Stockers_NumBonEntre",
                table: "Stockers",
                column: "NumBonEntre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Destockers");

            migrationBuilder.DropTable(
                name: "MonthlyRankResults");

            migrationBuilder.DropTable(
                name: "StatisticsResults");

            migrationBuilder.DropTable(
                name: "Stockers");

            migrationBuilder.DropTable(
                name: "StockersWithDesignation");

            migrationBuilder.DropTable(
                name: "Sorties");

            migrationBuilder.DropTable(
                name: "Entrees");

            migrationBuilder.DropTable(
                name: "Produits");
        }
    }
}
