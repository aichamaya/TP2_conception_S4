using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCryo.Migrations
{
    /// <inheritdoc />
    public partial class moussa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Localisation = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bateaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Modele = table.Column<string>(type: "TEXT", nullable: false),
                    AnneeConstruction = table.Column<int>(type: "INTEGER", nullable: false),
                    Classe = table.Column<int>(type: "INTEGER", nullable: false),
                    Etat = table.Column<int>(type: "INTEGER", nullable: false),
                    StationProcheId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bateaux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bateaux_Stations_StationProcheId",
                        column: x => x.StationProcheId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    StationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Materiels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    BateauId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materiels_Bateaux_BateauId",
                        column: x => x.BateauId,
                        principalTable: "Bateaux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BateauId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationDestinationId = table.Column<int>(type: "INTEGER", nullable: false),
                    AutonomiePourcentage = table.Column<double>(type: "REAL", nullable: false),
                    DerniereMiseAJour = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TypeMission = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcours_Bateaux_BateauId",
                        column: x => x.BateauId,
                        principalTable: "Bateaux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcours_Stations_StationDestinationId",
                        column: x => x.StationDestinationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bateaux_StationProcheId",
                table: "Bateaux",
                column: "StationProcheId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiels_BateauId",
                table: "Materiels",
                column: "BateauId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcours_BateauId",
                table: "Parcours",
                column: "BateauId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcours_StationDestinationId",
                table: "Parcours",
                column: "StationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StationId",
                table: "Users",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materiels");

            migrationBuilder.DropTable(
                name: "Parcours");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bateaux");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
