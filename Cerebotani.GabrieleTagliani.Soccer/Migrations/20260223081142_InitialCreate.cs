using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Squadre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Citta = table.Column<string>(type: "TEXT", nullable: false),
                    Stadio = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squadre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stagioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stagioni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campionati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false),
                    Inizio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StagioneId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campionati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campionati_Stagioni_StagioneId",
                        column: x => x.StagioneId,
                        principalTable: "Stagioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SquadreCampionati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CampionatiId = table.Column<int>(type: "INTEGER", nullable: false),
                    SquadreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadreCampionati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SquadreCampionati_Campionati_CampionatiId",
                        column: x => x.CampionatiId,
                        principalTable: "Campionati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquadreCampionati_Squadre_SquadreId",
                        column: x => x.SquadreId,
                        principalTable: "Squadre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campionati_StagioneId",
                table: "Campionati",
                column: "StagioneId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadreCampionati_CampionatiId",
                table: "SquadreCampionati",
                column: "CampionatiId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadreCampionati_SquadreId",
                table: "SquadreCampionati",
                column: "SquadreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SquadreCampionati");

            migrationBuilder.DropTable(
                name: "Campionati");

            migrationBuilder.DropTable(
                name: "Squadre");

            migrationBuilder.DropTable(
                name: "Stagioni");
        }
    }
}
