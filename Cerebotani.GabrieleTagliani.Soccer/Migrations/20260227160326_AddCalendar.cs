using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marcatore_Partite_PartitaId",
                table: "Marcatore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marcatore",
                table: "Marcatore");

            migrationBuilder.RenameTable(
                name: "Marcatore",
                newName: "Marcatori");

            migrationBuilder.RenameIndex(
                name: "IX_Marcatore_PartitaId",
                table: "Marcatori",
                newName: "IX_Marcatori_PartitaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marcatori",
                table: "Marcatori",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marcatori_Partite_PartitaId",
                table: "Marcatori",
                column: "PartitaId",
                principalTable: "Partite",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marcatori_Partite_PartitaId",
                table: "Marcatori");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marcatori",
                table: "Marcatori");

            migrationBuilder.RenameTable(
                name: "Marcatori",
                newName: "Marcatore");

            migrationBuilder.RenameIndex(
                name: "IX_Marcatori_PartitaId",
                table: "Marcatore",
                newName: "IX_Marcatore_PartitaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marcatore",
                table: "Marcatore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marcatore_Partite_PartitaId",
                table: "Marcatore",
                column: "PartitaId",
                principalTable: "Partite",
                principalColumn: "Id");
        }
    }
}
