using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddGol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Partite_CampionatoId",
                table: "Partite",
                column: "CampionatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Campionati_CampionatoId",
                table: "Partite",
                column: "CampionatoId",
                principalTable: "Campionati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Campionati_CampionatoId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_CampionatoId",
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
        }
    }
}
