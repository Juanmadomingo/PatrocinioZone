using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatrocinioZoneProyecto.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deporte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patrocinadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrocinadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZonasPatrocinio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ubicacion = table.Column<int>(type: "int", nullable: false),
                    Tamanio = table.Column<int>(type: "int", nullable: false),
                    EstadoReservado = table.Column<bool>(type: "bit", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    PatrocinadorId = table.Column<int>(type: "int", nullable: true),
                    PatrocinadorId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonasPatrocinio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZonasPatrocinio_Clubes_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZonasPatrocinio_Patrocinadores_PatrocinadorId",
                        column: x => x.PatrocinadorId,
                        principalTable: "Patrocinadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZonasPatrocinio_Patrocinadores_PatrocinadorId1",
                        column: x => x.PatrocinadorId1,
                        principalTable: "Patrocinadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZonasPatrocinio_ClubId",
                table: "ZonasPatrocinio",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonasPatrocinio_PatrocinadorId",
                table: "ZonasPatrocinio",
                column: "PatrocinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonasPatrocinio_PatrocinadorId1",
                table: "ZonasPatrocinio",
                column: "PatrocinadorId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZonasPatrocinio");

            migrationBuilder.DropTable(
                name: "Clubes");

            migrationBuilder.DropTable(
                name: "Patrocinadores");
        }
    }
}
