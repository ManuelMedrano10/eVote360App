using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class Voto : BasicEntity<int>
    {
        public int EleccionId { get; set; }
        public Eleccion? Eleccion { get; set; }
        public int PuestoElectivoId { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }
        public int CandidatoId { get; set; }
        public Candidato? Candidato { get; set; }
    }
}
