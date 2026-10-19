using MySql.Data.MySqlClient;
using WebServiceGeometria.Respuestas.MatchShadow.View;

namespace WebServiceGeometria.Servicios.Niveles
{
    public class ServiceNivelMatchShadow
    {
        private readonly string _connectionString;
        public ServiceNivelMatchShadow(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<vNivel_MatchShadow> getNiveles(int id_minijuego)
        {
            try
            {
                List<vNivel_MatchShadow> niveles = new List<vNivel_MatchShadow>();

                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();

                    string query = @"SELECT id_nivel,numero_nivel, id_minijuego, titulo_nivel, dificultad, estado FROM v_Consultar_Nivel " +
                    "WHERE estado = true AND id_minijuego = @id_minijuego";

                    using (MySqlCommand cmd = new(query, consulta))
                    {
                        cmd.Parameters.AddWithValue("@id_minijuego", id_minijuego);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                niveles.Add(new vNivel_MatchShadow
                                {
                                    id_nivel = Convert.ToInt32(reader["id_nivel"]),
                                    numero_nivel = Convert.ToInt32(reader["numero_nivel"]),
                                    id_minijuego = Convert.ToInt32(reader["id_minijuego"]),
                                    titulo_nivel = reader["titulo_nivel"].ToString(),
                                    dificultad = reader["dificultad"].ToString(),
                                    estado = Convert.ToBoolean(reader["estado"])
                                });
                            }
                        }


                    }
                }
                return niveles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}