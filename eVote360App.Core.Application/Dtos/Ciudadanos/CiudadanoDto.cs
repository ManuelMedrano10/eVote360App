namespace eVote360App.Core.Application.Dtos.Ciudadanos
{
    public class CiudadanoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string DocumentoIdentidad { get; set; }
        public bool IsActive { get; set; }
    }
}
