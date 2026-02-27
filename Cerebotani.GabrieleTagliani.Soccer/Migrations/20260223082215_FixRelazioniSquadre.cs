using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class FixRelazioniSquadre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SquadreCampionati_Campionati_CampionatiId",
                table: "SquadreCampionati");

            migrationBuilder.DropForeignKey(
                name: "FK_SquadreCampionati_Squadre_SquadreId",
                table: "SquadreCampionati");

            migrationBuilder.RenameColumn(
                name: "SquadreId",
                table: "SquadreCampionati",
                newName: "SquadraId");

            migrationBuilder.RenameColumn(
                name: "CampionatiId",
                table: "SquadreCampionati",
                newName: "CampionatoId");

            migrationBuilder.RenameIndex(
                name: "IX_SquadreCampionati_SquadreId",
                table: "SquadreCampionati",
                newName: "IX_SquadreCampionati_SquadraId");

            migrationBuilder.RenameIndex(
                name: "IX_SquadreCampionati_CampionatiId",
                table: "SquadreCampionati",
                newName: "IX_SquadreCampionati_CampionatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SquadreCampionati_Campionati_CampionatoId",
                table: "SquadreCampionati",
                column: "CampionatoId",
                principalTable: "Campionati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SquadreCampionati_Squadre_SquadraId",
                table: "SquadreCampionati",
                column: "SquadraId",
                principalTable: "Squadre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SquadreCampionati_Campionati_CampionatoId",
                table: "SquadreCampionati");

            migrationBuilder.DropForeignKey(
                name: "FK_SquadreCampionati_Squadre_SquadraId",
                table: "SquadreCampionati");

            migrationBuilder.RenameColumn(
                name: "SquadraId",
                table: "SquadreCampionati",
                newName: "SquadreId");

            migrationBuilder.RenameColumn(
                name: "CampionatoId",
                table: "SquadreCampionati",
                newName: "CampionatiId");

            migrationBuilder.RenameIndex(
                name: "IX_SquadreCampionati_SquadraId",
                table: "SquadreCampionati",
                newName: "IX_SquadreCampionati_SquadreId");

            migrationBuilder.RenameIndex(
                name: "IX_SquadreCampionati_CampionatoId",
                table: "SquadreCampionati",
                newName: "IX_SquadreCampionati_CampionatiId");

            migrationBuilder.AddForeignKey(
                name: "FK_SquadreCampionati_Campionati_CampionatiId",
                table: "SquadreCampionati",
                column: "CampionatiId",
                principalTable: "Campionati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SquadreCampionati_Squadre_SquadreId",
                table: "SquadreCampionati",
                column: "SquadreId",
                principalTable: "Squadre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
