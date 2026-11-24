using MySql.Data.MySqlClient;
using System.Text.Json;
using WebServiceGeometria.Respuestas.MatchShadow.View;
using WebServiceGeometria.Respuestas.Niveles.View;

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

        public List<vPerimetrovsArea> getDetalleAreavsPerimetro(int id_nivel, int orden)
        {
            try
            {
                List<vPerimetrovsArea> detalle = new List<vPerimetrovsArea>();
                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();
                    string query = @"SELECT id_detalle, id_nivel, orden, base, altura, modo, opciones_json, respuesta_correcta 
                             FROM v_Detalle_Minijuego_PerimetroArea 
                             WHERE id_nivel = @id_nivel AND orden = @orden
                             ORDER BY orden ASC";

                    using (MySqlCommand cmd = new(query, consulta))
                    {
                        cmd.Parameters.AddWithValue("@id_nivel", id_nivel);
                        cmd.Parameters.AddWithValue("@orden", orden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string opcionesRaw = reader["opciones_json"]?.ToString() ?? "[]";
                                var opciones = JsonSerializer.Deserialize<List<int>>(opcionesRaw) ?? new List<int>();

                                detalle.Add(new vPerimetrovsArea
                                {
                                    id_detalle = Convert.ToInt32(reader["id_detalle"]),
                                    id_nivel = Convert.ToInt32(reader["id_nivel"]),
                                    orden = Convert.ToInt32(reader["orden"]),
                                    base_disponible = Convert.ToInt32(reader["base"]),
                                    altura = Convert.ToInt32(reader["altura"]),
                                    modo = reader["modo"].ToString(),
                                    opciones_json = opciones,
                                    respuesta_correcta = Convert.ToInt32(reader["respuesta_correcta"])
                                });
                            }
                        }
                    }
                }
                return detalle;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<vCazaAngulos> getDetalleCazaAreas(int id_nivel, int orden)
        {
            try
            {
                List<vCazaAngulos> cazador = new List<vCazaAngulos>();
                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();
                    string query = @"SELECT id_detalle,
                                        id_nivel,
                                        orden,
                                        grados,
                                        mostrar_grados,
                                        tipo_esperado
                                    FROM v_Detalle_Minijuego_Angulos
                                    WHERE id_nivel = @id_nivel AND orden = @orden";
                    using (MySqlCommand cmd = new(query, consulta))
                    {
                        cmd.Parameters.AddWithValue("@id_nivel", id_nivel);
                        cmd.Parameters.AddWithValue("@orden", orden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cazador.Add(new vCazaAngulos
                                {
                                    id_detalle = Convert.ToInt32(reader["id_detalle"]),
                                    id_nivel = Convert.ToInt32(reader["id_nivel"]),
                                    orden = Convert.ToInt32(reader["orden"]),
                                    grados = Convert.ToDouble(reader["grados"]),
                                    mostrar_grados = Convert.ToBoolean(reader["mostrar_grados"]),
                                    tipo_esperado = reader["tipo_esperado"].ToString()
                                });
                            }
                        }
                    }
                }
                return cazador;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<vNivelClasificador> getClasificador(int id_nivel, int orden)
        {
            try
            {
                List<vNivelClasificador> clasificador = new List<vNivelClasificador>();
                using (MySqlConnection consulta = new MySqlConnection(_connectionString))
                {
                    consulta.Open();
                    string query = @"SELECT 
                                        id_detalle,
                                        id_nivel,
                                        orden,
                                        enunciado,
                                        icono,
                                        opciones_json,
                                        respuesta_correcta
                                    FROM v_Detalle_Minijuego_Clasificador";
                    using (MySqlCommand cmd = new(query, consulta))
                    {
                        cmd.Parameters.AddWithValue("@id_nivel", id_nivel);
                        cmd.Parameters.AddWithValue("@orden", orden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string opcionesRaw = reader["opciones_json"]?.ToString() ?? "[]";
                                var opciones = JsonSerializer.Deserialize<List<string>>(opcionesRaw) ?? new List<string>();

                                clasificador.Add(new vNivelClasificador
                                {
                                    id_detalle = Convert.ToInt32(reader["id_detalle"]),
                                    id_nivel = Convert.ToInt32(reader["id_nivel"]),
                                    orden = Convert.ToInt32(reader["orden"]),
                                    enunciado = reader["enunciado"].ToString(),
                                    icono = reader["icono"].ToString(),
                                    opciones_json = opciones,
                                    respuesta_correcta = reader["respuesta_correcta"].ToString()
                                });
                            }
                        }
                    }
                }
                return clasificador;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
