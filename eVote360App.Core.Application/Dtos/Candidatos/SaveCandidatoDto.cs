namespace eVote360App.Core.Application.Dtos.Candidatos
{
    public class SaveCandidatoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Foto { get; set; }
        public int PartidoPoliticoId { get; set; }
        public int PuestoElectivoId { get; set; }
    }
}
