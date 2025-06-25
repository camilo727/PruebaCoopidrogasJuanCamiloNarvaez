
using Aplicación.Servicios.Interfaz;
using Aplicación.Servicios.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using Infraestructura.DependenciaInjenction;
using Microsoft.Extensions.Configuration;
using Aplicación.Maper;

namespace Aplicación.DependenciaInjenction
{
    public static class AplicacionContainer
    {
        public static IServiceCollection AplicacionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApiMaper));

            //Soporte para CORS
            //Se pueden habilitar: 1-Un dominio, 2-multiples dominios,
            //3-cualquier dominio (Tener en cuenta seguridad)
            //Usamos de ejemplo el dominio: http://localhost:3223, se debe cambiar por el correcto
            //Se usa (*) para todos los dominios

            //services.AddCors(p => p.AddPolicy("PoliticaCors", build =>
            //{
            //    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            //}));

            services.InfrasctureServices(configuration);

            services.AddScoped<IUsuarioServer, UsuarioServer>();
            services.AddScoped<IClienteServe, ClienteService>();
            return services;
        }
    }
}
