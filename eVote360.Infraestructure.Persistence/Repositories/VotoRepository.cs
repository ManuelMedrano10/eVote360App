using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using eVote360App.Infraestructure.Persistence.Contexts;

namespace eVote360App.Infraestructure.Persistence.Repositories
{
    public class VotoRepository :GenericRepository<Voto>, IVotoRepository
    {
        public VotoRepository(EVote360AppContext context) : base(context) { }
    }
}
