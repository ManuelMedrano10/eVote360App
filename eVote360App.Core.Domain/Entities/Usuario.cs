using eVote360App.Core.Domain.Common;
using eVote360App.Core.Domain.Common.Enums;

namespace eVote360App.Core.Domain.Entities
{
    public class Usuario : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Password { get; set; }
        public required RolUsuario Rol { get; set; }

        public int? PartidoPoliticoId { get; set; }
        public PartidoPolitico? PartidoPolitico { get; set; }
    }
}
