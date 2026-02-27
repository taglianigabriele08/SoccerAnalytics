using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CampionatoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Giornata = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SquadraCasaId = table.Column<int>(type: "INTEGER", nullable: false),
                    SquadraTrasfertaId = table.Column<int>(type: "INTEGER", nullable: false),
                    GolCasa = table.Column<int>(type: "INTEGER", nullable: true),
                    GolTrasferta = table.Column<int>(type: "INTEGER", nullable: true),
                    Giocata = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partite_Squadre_SquadraCasaId",
                        column: x => x.SquadraCasaId,
                        principalTable: "Squadre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partite_Squadre_SquadraTrasfertaId",
                        column: x => x.SquadraTrasfertaId,
                        principalTable: "Squadre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partite_SquadraCasaId",
                table: "Partite",
                column: "SquadraCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_SquadraTrasfertaId",
                table: "Partite",
                column: "SquadraTrasfertaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partite");
        }
    }
}
