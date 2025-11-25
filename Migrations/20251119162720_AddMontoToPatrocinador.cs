using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatrocinioZoneProyecto.Migrations
{
    /// <inheritdoc />
    public partial class AddMontoToPatrocinador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Monto",
                table: "Patrocinadores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Patrocinadores");
        }
    }
}
