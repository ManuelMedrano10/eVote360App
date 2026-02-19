namespace eVote360App.Core.Application.Viewmodels.Ciudadano
{
    public class CiudadanoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string DocumentoIdentidad { get; set; }
        public bool IsActive { get; set; }
    }
}
