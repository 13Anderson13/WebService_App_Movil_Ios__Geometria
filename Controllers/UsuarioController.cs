using Microsoft.AspNetCore.Mvc;
using WebServiceGeometria.DTO.Usuarios.Parametros;
using WebServiceGeometria.DTO.Usuarios.Vistas;
using WebServiceGeometria.Servicios.Usuarios;

namespace WebServiceGeometria.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController : Controller
    {
        private readonly ServiceUsuarios obj1;
        private readonly IConfiguration _configuration;
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string Code, DateTime Expiration)> _verificationCodes = new();

        public UsuarioController(ServiceUsuarios servicio, IConfiguration configuration)
        {
            obj1 = servicio;
            _configuration = configuration;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Logear")]
        public ActionResult<vConsultarUsuarios> Logear([FromBody] dtoValidarUsuario dto)
        {
            var usuario = obj1.Logear(dto);

            if (usuario == null)
                return NotFound("Usuario no encontrado o credenciales incorrectas");

            usuario.password = null;

            return Ok(usuario);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Insertar")]

        public ActionResult Insertar([FromBody] dtoInsertarUsuario dto)
        {
            var resultado = obj1.Insertar(dto);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

    }
}
