using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddMarcatori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcatore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Minuto = table.Column<int>(type: "INTEGER", nullable: false),
                    Rigore = table.Column<bool>(type: "INTEGER", nullable: false),
                    SquadraNome = table.Column<string>(type: "TEXT", nullable: true),
                    PartitaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcatore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marcatore_Partite_PartitaId",
                        column: x => x.PartitaId,
                        principalTable: "Partite",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marcatore_PartitaId",
                table: "Marcatore",
                column: "PartitaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marcatore");
        }
    }
}
