using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatrocinioZoneProyecto.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ubicacion",
                table: "ZonasPatrocinio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Patrocinadores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clubes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "ZonasPatrocinio");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Patrocinadores");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clubes");
        }
    }
}
