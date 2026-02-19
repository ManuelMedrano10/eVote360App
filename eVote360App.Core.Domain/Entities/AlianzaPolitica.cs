using eVote360App.Core.Domain.Common;
using eVote360App.Core.Domain.Common.Enums;

namespace eVote360App.Core.Domain.Entities
{
    public class AlianzaPolitica : BasicEntity<int>
    {
        public int PartidoSolicitanteId { get; set; }
        public PartidoPolitico? PartidoSolicitante { get; set; }
        public int PartidoReceptorId { get; set; }
        public PartidoPolitico? PartidoReceptor { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
        public DateTime? FechaAceptacion { get; set; }
        public EstadoAlianza Estado { get; set; } = EstadoAlianza.Pendiente;
    }
}
