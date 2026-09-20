using WebServiceGeometria.Respuestas.Minijuegos.Vistas;
namespace WebServiceGeometria.Servicios.Minijuegos
{
    public class ServiceMinijuegos
    {
        private readonly string _connectionString;
        public ServiceMinijuegos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            //public List<vConsultarMinijuegos>

        }
    }
}
