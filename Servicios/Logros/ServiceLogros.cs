using MySql.Data.MySqlClient;
using WebServiceGeometria.Respuestas.Logros.Vistas;

namespace WebServiceGeometria.Servicios.Logros
{
    public class ServiceLogros
    {
        public readonly string _connectionString;
        public ServiceLogros(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //========================================================================================================================================

        public List<vConsultarLogros> GetLogros()
        {
            try
            {
                List<vConsultarLogros> logros = new List<vConsultarLogros>();

                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();

                    string query = @"SELECT * FROM v_consultar_logros";

                    MySqlCommand cmd = new(query, consulta);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        logros.Add(new vConsultarLogros
                        {
                            id_logro = Convert.ToInt32(reader["id_logro"]),
                            nombre_logro = reader["nombre_logro"].ToString(),
                            descripcion_logro = reader["descripcion_logro"].ToString(),
                            icono = reader["icono"].ToString()
                        });
                    }
                }

                return logros;
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
