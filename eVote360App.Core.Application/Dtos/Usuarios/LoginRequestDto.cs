namespace eVote360App.Core.Application.Dtos.Usuarios
{
    public class LoginRequestDto
    {
        public required string NombreUsuario { get; set; }
        public required string Password { get; set; }
    }
}
