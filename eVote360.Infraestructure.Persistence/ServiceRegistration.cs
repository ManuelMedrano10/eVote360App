using eVote360App.Core.Domain.Interfaces;
using eVote360App.Infraestructure.Persistence.Contexts;
using eVote360App.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eVote360App.Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
        {
            #region Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<EVote360AppContext>(opt => opt.UseInMemoryDatabase("AppDB"));
            }
            else
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<EVote360AppContext>(opt => opt.UseSqlServer(connectionString,
                m => m.MigrationsAssembly(typeof(EVote360AppContext).Assembly.FullName)), ServiceLifetime.Transient);
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAlianzaPoliticaRepository, AlianzaPoliticaRepository>();
            services.AddTransient<ICandidatoRepository, CandidatoRepository>();
            services.AddTransient<ICiudadanoRepository, CiudadanoRepository>();
            services.AddTransient<IEleccionRepository, EleccionRepository>();
            services.AddTransient<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
            services.AddTransient<IPuestoElectivoRepository, PuestoElectivoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IVotoRepository, VotoRepository>();
            services.AddTransient<IRegistroVotanteRepository, RegistroVotanteRepository>();
            #endregion
        }
    }
}
