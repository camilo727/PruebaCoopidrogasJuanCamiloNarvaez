


using Infraestructura.Datos;
using Infraestructura.Repositorio.Interfaz;
using Infraestructura.Repositorio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructura.DependenciaInjenction
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrasctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"),
         b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
         ServiceLifetime.Scoped);
                
            services.AddScoped<IUsuarioRepo, UsuarioRepo>();
            services.AddScoped<IClienteRepo, ClienteRepo>();
            return services;
        }
    }
}
