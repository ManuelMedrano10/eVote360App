namespace eVote360App.Core.Application.Dtos.Candidatos
{
    public class CandidatoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Foto { get; set; }
        public int PartidoPoliticoId { get; set; }
        public int PuestoElectivoId { get; set; }
        public string? PuestoNombre { get; set; }
        public string? PartidoNombre { get; set; }
        public string? PartidoLogo { get; set; }
        public bool IsActive { get; set; }
    }
}
