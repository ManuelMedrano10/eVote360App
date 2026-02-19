using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class PuestoElectivo : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }

        public ICollection<Candidato>? Candidatos { get; set; }
    }
}
