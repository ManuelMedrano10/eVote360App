using System.Reflection;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eVote360App.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUsuarioService, UsuarioService>();
        }
    }
}
