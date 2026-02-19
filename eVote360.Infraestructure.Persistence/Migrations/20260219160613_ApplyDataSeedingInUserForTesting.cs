using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360App.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ApplyDataSeedingInUserForTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Email", "IsActive", "Nombre", "NombreUsuario", "PartidoPoliticoId", "Password", "Rol" },
                values: new object[] { 1, "Principal", "admin@gmail.com", true, "Admin", "AdminJose", null, "1234", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
