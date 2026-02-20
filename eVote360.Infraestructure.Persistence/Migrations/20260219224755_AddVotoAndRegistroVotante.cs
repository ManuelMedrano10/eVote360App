using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360App.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVotoAndRegistroVotante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosVotantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    CiudadanoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosVotantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosVotantes_Ciudadanos_CiudadanoId",
                        column: x => x.CiudadanoId,
                        principalTable: "Ciudadanos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrosVotantes_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "int", nullable: false),
                    CandidatoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votos_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votos_PuestosElectivos_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestosElectivos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVotantes_CiudadanoId",
                table: "RegistrosVotantes",
                column: "CiudadanoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVotantes_EleccionId",
                table: "RegistrosVotantes",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CandidatoId",
                table: "Votos",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_EleccionId",
                table: "Votos",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_PuestoElectivoId",
                table: "Votos",
                column: "PuestoElectivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosVotantes");

            migrationBuilder.DropTable(
                name: "Votos");
        }
    }
}
