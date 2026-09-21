using MySql.Data.MySqlClient;
using WebServiceGeometria.Respuestas.Minijuegos.Vistas;
namespace WebServiceGeometria.Servicios.Minijuegos
{
    public class ServiceMinijuegos
    {
        private readonly string _connectionString;
        public ServiceMinijuegos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<vConsultarMinijuegos> getMenu()
        {
            try
            {
                List<vConsultarMinijuegos> minijuegos = new List<vConsultarMinijuegos>();

                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();

                    string query = @" SELECT * FROM v_consultar_minijuego WHERE estado = 1";

                    MySqlCommand cmd = new(query, consulta);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        minijuegos.Add(new vConsultarMinijuegos
                        {
                            id_minijuego = Convert.ToInt32(reader["id_minijuego"]),
                            nombre_minijuego = reader["nombre_minijuego"].ToString(),
                            icono = reader["icono"].ToString(),
                            descripcion = reader["descripcion"].ToString()
                        });
                    }

                }

                return minijuegos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
