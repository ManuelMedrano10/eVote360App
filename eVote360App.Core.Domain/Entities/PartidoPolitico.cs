using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class PartidoPolitico : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required string Siglas { get; set; }
        public required string Logo { get; set; }

        public ICollection<Candidato>? Candidatos { get; set; }
        public ICollection<Usuario>? Dirigentes { get; set; }
    }
}
