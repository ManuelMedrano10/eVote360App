namespace eVote360App.Core.Application.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string NombreUsuario { get; set; }
        public int Rol { get; set; }
        public bool IsActive { get; set; }
        public int? PartidoPoliticoId { get; set; }
    }
}
