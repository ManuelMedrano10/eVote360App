using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Entities
{
    public class RegistroVotante : BasicEntity<int>
    {
        public int EleccionId { get; set; }
        public Eleccion? Eleccion { get; set; }
        public int CiudadanoId { get; set; }
        public Ciudadano? Ciudadano { get; set; }
    }
}
