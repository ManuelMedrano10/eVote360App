using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class Candidato : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Foto { get; set; }
        public required int PartidoPoliticoId { get; set; }
        public int? PuestoElectivoId { get; set; }
        public PartidoPolitico? PartidoPolitico { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }
    }
}
