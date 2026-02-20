using System.Reflection;
using eVote360App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eVote360App.Infraestructure.Persistence.Contexts
{
    public class EVote360AppContext : DbContext
    {
        public EVote360AppContext(DbContextOptions<EVote360AppContext> options) 
        : base(options) { }

        public DbSet<AlianzaPolitica> AlianzasPoliticas { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Ciudadano> Ciudadanos { get; set; }
        public DbSet<Eleccion> Elecciones { get; set; }
        public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }
        public DbSet<PuestoElectivo> PuestosElectivos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Voto> Votos { get; set; }
        public DbSet<RegistroVotante> RegistrosVotantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
