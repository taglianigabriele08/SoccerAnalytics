using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Squadre_SquadraCasaId",
                table: "Partite");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Squadre_SquadraTrasfertaId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_SquadraCasaId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_SquadraTrasfertaId",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "Giocata",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "SquadraCasaId",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "SquadraTrasfertaId",
                table: "Partite");

            migrationBuilder.AlterColumn<int>(
                name: "GolTrasferta",
                table: "Partite",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GolCasa",
                table: "Partite",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SquadraCasa",
                table: "Partite",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SquadraTrasferta",
                table: "Partite",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SquadraCasa",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "SquadraTrasferta",
                table: "Partite");

            migrationBuilder.AlterColumn<int>(
                name: "GolTrasferta",
                table: "Partite",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "GolCasa",
                table: "Partite",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "Giocata",
                table: "Partite",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SquadraCasaId",
                table: "Partite",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SquadraTrasfertaId",
                table: "Partite",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Partite_SquadraCasaId",
                table: "Partite",
                column: "SquadraCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_SquadraTrasfertaId",
                table: "Partite",
                column: "SquadraTrasfertaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Squadre_SquadraCasaId",
                table: "Partite",
                column: "SquadraCasaId",
                principalTable: "Squadre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Squadre_SquadraTrasfertaId",
                table: "Partite",
                column: "SquadraTrasfertaId",
                principalTable: "Squadre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
