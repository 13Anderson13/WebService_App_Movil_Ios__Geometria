using Microsoft.AspNetCore.Mvc;
using WebServiceGeometria.Servicios.Minijuegos;

namespace WebServiceGeometria.Controllers
{
    [ApiController]
    [Route("api/Minijuego")]
    public class MinijuegoController : Controller
    {
        private readonly ServiceMinijuegos _servicio;
        public MinijuegoController(ServiceMinijuegos minijuegos) 
        {
            _servicio = minijuegos;
        }


        [HttpGet("Menu")]
        public IActionResult getMenu()
        {
            try
            {
                var minijuegos = _servicio.getMenu();
                return Ok(minijuegos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
