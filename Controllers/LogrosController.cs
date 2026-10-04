using Microsoft.AspNetCore.Mvc;
using WebServiceGeometria.Servicios.Logros;

namespace WebServiceGeometria.Controllers
{
    [ApiController]
    [Route("api/Logros")]
    public class LogrosController : Controller
    {
        private readonly ServiceLogros _servicio;

        public LogrosController(ServiceLogros servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("Logros")]
        public IActionResult GetLogros()
        {
            try
            {
                var servicio = _servicio.GetLogros();
                return Ok(servicio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
