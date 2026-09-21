using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebServiceGeometria.Servicios.Minijuegos;
using WebServiceGeometria.Servicios.Usuarios;
namespace WebServiceGeometria.Contexto
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Inyecta la cadena de conexión
            services.AddSingleton(configuration.GetConnectionString("DefaultConnection"));

            // Inyecta el servicio de usuario
            services.AddScoped<ServiceUsuarios>();
            services.AddScoped<ServiceMinijuegos>();
            return services;
        }
    }
}
