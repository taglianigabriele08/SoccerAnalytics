using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddNazioneCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Campionati",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nazione",
                table: "Campionati",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Campionati");

            migrationBuilder.DropColumn(
                name: "Nazione",
                table: "Campionati");
        }
    }
}
