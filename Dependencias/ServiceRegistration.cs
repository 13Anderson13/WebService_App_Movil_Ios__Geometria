using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            /*services.AddScoped<ServicioImagen>();
            services.AddScoped<ServicioInstalacion>();
            services.AddScoped<ServicioHabitacion>();
            services.AddScoped<ServicioHorarios>();
            services.AddScoped<ServicioServicio>();
            services.AddScoped<ServicioTipo_Instalacion>();
            services.AddScoped<ServicioDuenoUsuario>();*/
            return services;
        }
    }
}
