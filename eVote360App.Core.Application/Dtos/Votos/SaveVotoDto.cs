namespace eVote360App.Core.Application.Dtos.Votos
{
    public class SaveVotoDto
    {
        public int EleccionId { get; set; }
        public int PuestoElectivoId { get; set; }
        public int CandidatoId { get; set; }
    }
}
