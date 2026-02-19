using Microsoft.EntityFrameworkCore;

namespace eVote360App.Infraestructure.Persistence.Contexts
{
    public class EVote360AppContext : DbContext
    {
        public EVote360AppContext(DbContextOptions<EVote360AppContext> options) : base(options)
        {
            
        }
    }
}
