using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebotani.GabrieleTagliani.Soccer.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnoSquadra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Anno",
                table: "Squadre",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anno",
                table: "Squadre");
        }
    }
}
