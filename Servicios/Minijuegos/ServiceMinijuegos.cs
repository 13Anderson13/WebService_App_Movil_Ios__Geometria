using MySql.Data.MySqlClient;
using System.Data;
using WebServiceGeometria.Respuestas.Minijuegos.DTO;
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
        public string Insertar(dtoInsertarMinijuego dto)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var command = new MySqlCommand("SP_Insertar_Minijuego"))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("sp_nombre_minijuego", dto.nombre_minijuego);
                        command.Parameters.AddWithValue("sp_icono", dto.icono);
                        command.Parameters.AddWithValue("sp_descripcion", dto.descripcion);
                        command.Parameters.AddWithValue("sp_estado", dto.estado);
                        command.Parameters.AddWithValue("sp_ventana", dto.ventana);

                        command.ExecuteNonQuery();
                        return "Registro completado con exito";
                    }
                }
            }
            catch (Exception ex)
            {
                return ("Error: " + ex.Message);
            }
        }
    }
}
