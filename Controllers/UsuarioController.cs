using Microsoft.AspNetCore.Mvc;
using WebServiceGeometria.Respuestas.Usuarios.DTO;
using WebServiceGeometria.Respuestas.Usuarios.Vistas;
using WebServiceGeometria.Servicios.Usuarios;

namespace WebServiceGeometria.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController : Controller
    {
        private readonly ServiceUsuarios _usuario;
        
        public UsuarioController(ServiceUsuarios servicio)
        {
            _usuario = servicio;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Logear")]
        public ActionResult<vConsultarUsuarios> Logear([FromBody] dtoValidarUsuario dto)
        {
            var usuario = _usuario.Logear(dto);

            if (usuario == null)
                return NotFound("Usuario no encontrado o credenciales incorrectas");

            usuario.password = null;

            return Ok(usuario);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Insertar")]

        public ActionResult Insertar([FromBody] dtoInsertarUsuario dto)
        {
            var resultado = _usuario.Insertar(dto);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

    }
}
