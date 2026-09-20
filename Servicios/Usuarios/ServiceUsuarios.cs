using MySql.Data.MySqlClient;
using System.Data;
using WebServiceGeometria.Respuestas.Usuarios.Vistas;
using WebServiceGeometria.Respuestas.Usuarios.DTO;
namespace WebServiceGeometria.Servicios.Usuarios
{
    public class ServiceUsuarios
    {
        private readonly string _connectionString;
        public ServiceUsuarios(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //========================================================================================================================================

        public vConsultarUsuarios Logear (dtoValidarUsuario dto)
        {
            vConsultarUsuarios usuario = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var command = new MySqlCommand("SP_Validar_Usuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("sp_login", dto.login);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hash = reader["password"].ToString();

                            if (BCrypt.Net.BCrypt.Verify(dto.password, hash))
                            {
                                usuario = new vConsultarUsuarios()
                                {
                                    id_usuarios = Convert.ToInt32(reader["id_usuarios"]),
                                    login = reader["login"].ToString(),
                                    nombre = reader["nombre"].ToString(),
                                    apellido = reader["apellido"].ToString()
                                };
                            }
                        }
                    }
                }
            }

            return usuario;
        }

        //========================================================================================================================================

        public string Insertar(dtoInsertarUsuario dto)
        {

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var command = new MySqlCommand("SP_Insertar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        string passwordTemporal = BCrypt.Net.BCrypt.HashPassword(dto.login);
                        // Agregar los parámetros al procedimiento almacenado
                        command.Parameters.AddWithValue("sp_login", dto.login);
                        command.Parameters.AddWithValue("sp_nombre", dto.nombre);
                        command.Parameters.AddWithValue("sp_apellido", dto.apellido);
                        command.Parameters.AddWithValue("sp_password", passwordTemporal);
                        command.Parameters.AddWithValue("sp_email", dto.email);
                        command.Parameters.AddWithValue("sp_telefono", dto.telefono);

                        // Abrir la conexión a la base de datos
                        connection.Open();

                        // Ejecutar el SP para insertar el usuario
                        command.ExecuteNonQuery();
                        return "Registro completo";
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
