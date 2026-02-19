using eVote360App.Core.Domain.Entities;
using eVote360App.Core.Domain.Interfaces;
using eVote360App.Infraestructure.Persistence.Contexts;

namespace eVote360App.Infraestructure.Persistence.Repositories
{
    public class AlianzaPoliticaRepository : GenericRepository<AlianzaPolitica>, IAlianzaPoliticaRepository
    {
        public AlianzaPoliticaRepository(EVote360AppContext context) : base(context)
        {
        }
    }
}
