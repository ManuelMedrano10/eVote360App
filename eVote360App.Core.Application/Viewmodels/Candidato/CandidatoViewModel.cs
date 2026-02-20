namespace eVote360App.Core.Application.Viewmodels.Candidato
{
    public class CandidatoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Foto { get; set; }
        public string? PuestoNombre { get; set; }
        public bool IsActive { get; set; }
        public string? PartidoNombre { get; set; }
        public string? PartidoLogo { get; set; }
    }
}
