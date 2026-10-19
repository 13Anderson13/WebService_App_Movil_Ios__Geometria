using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using WebServiceGeometria.Respuestas.MatchShadow.View;

namespace WebServiceGeometria.Servicios.Niveles
{
    public class ServiceDetalleNivelesMatchShadow
    {
        private readonly string _connectionString;
        public ServiceDetalleNivelesMatchShadow(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<vDetalle_minijuego_MatchShadow> getDetalleNivel(int id_nivel, int numero_nivel)
        {
            try
            {
                List<vDetalle_minijuego_MatchShadow> detalle = new List<vDetalle_minijuego_MatchShadow>();
                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();
                    string query = @"SELECT  id_detalle, id_nivel,numero_nivel, nombre_figura, icono, coordenada_sombra_x, coordenada_sombra_y, coordenada_ficha_x, coordenada_ficha_y " +
                    "FROM v_Detalle_Minijuego_matchshadow WHERE id_nivel = @id_nivel AND numero_nivel = @numero_nivel";

                    using (MySqlCommand cmd = new(query, consulta))
                    {
                        cmd.Parameters.AddWithValue("@id_nivel", id_nivel);
                        cmd.Parameters.AddWithValue("@numero_nivel", numero_nivel);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                detalle.Add(new vDetalle_minijuego_MatchShadow
                                {
                                    id_detalle = Convert.ToInt32(reader["id_detalle"]),
                                    id_nivel = Convert.ToInt32(reader["id_nivel"]),
                                    numero_nivel = Convert.ToInt32(reader["numero_nivel"]),
                                    nombre_figura = reader["nombre_figura"].ToString(),
                                    icono = reader["icono"].ToString(),
                                    coordenada_sombra_x = Convert.ToDouble(reader["coordenada_sombra_x"]),
                                    coordenada_sombra_y = Convert.ToDouble(reader["coordenada_sombra_y"]),
                                    coordenada_ficha_x = Convert.ToDouble(reader["coordenada_ficha_x"]),
                                    coordenada_ficha_y = Convert.ToDouble(reader["coordenada_ficha_y"])
                                });
                            }
                        }
                    }
                }
                return detalle;
            }
            catch (Exception x)
            {
                throw;
            }
        }
    }
}
