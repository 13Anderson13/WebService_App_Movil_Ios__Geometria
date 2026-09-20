using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace WebServiceGeometria.Controllers
{
    public class ValidarController : Controller
    {
        private readonly string _connectionString;

        public ValidarController(string connectionString)
        {
            _connectionString = connectionString;
        }
        [HttpGet("PROBAR_CONEXION")]
        public ActionResult ProbarConexion()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return Ok("¡Conexión exitosa a la base de datos!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al conectar a la base de datos: {ex.Message}");
            }
        }
    }
}
