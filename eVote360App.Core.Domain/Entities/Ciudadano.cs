using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class Ciudadano : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string DocumentoIdentidad { get; set; }
    }
}
