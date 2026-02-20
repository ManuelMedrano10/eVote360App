namespace eVote360App.Core.Application.Viewmodels.Votacion
{
    public class CandidatoPapeletaViewModel
    {
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public required string Foto { get; set; }
        public required string PartidoNombre { get; set; }
        public required string PartidoLogo { get; set; }
    }
}
