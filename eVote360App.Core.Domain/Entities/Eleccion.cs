using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class Eleccion : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required DateTime Fecha { get; set; }
        public bool Finalizada { get; set; } = false;
    }
}
