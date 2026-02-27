using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddClassificaFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DifferenzaReti",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GolFatti",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GolSubiti",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pareggi",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartiteGiocate",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Punti",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sconfitte",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vittorie",
                table: "SquadreCampionati",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifferenzaReti",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "GolFatti",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "GolSubiti",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "Pareggi",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "PartiteGiocate",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "Punti",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "Sconfitte",
                table: "SquadreCampionati");

            migrationBuilder.DropColumn(
                name: "Vittorie",
                table: "SquadreCampionati");
        }
    }
}
