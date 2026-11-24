using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServiceGeometria.Servicios.Minijuegos;
using WebServiceGeometria.Servicios.Niveles;

namespace WebServiceGeometria.Controllers
{
    [ApiController]
    [Route("api/NivelesMatchShadow")]
    public class Niveles_MatchShadowController : Controller
    {
        private readonly ServiceNivelMatchShadow _niveles;
        private readonly ServiceDetalleNivelesMatchShadow _detalles;
        public Niveles_MatchShadowController(ServiceNivelMatchShadow nivels, ServiceDetalleNivelesMatchShadow detalles)
        {
            _niveles = nivels;
            _detalles = detalles;
        }

        [HttpGet("Niveles")]
        public IActionResult getNiveles(int id_minijuego)
        {
            try
            {
                var niveles = _niveles.getNiveles(id_minijuego);
                return Ok(niveles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("Detalles")] //JCorresponde al emparejamiento dinamico, se diseño sin pensar a futuro por eso todo se llama matchshadow
        public IActionResult getDetalleNivel(int id_nivel, int numero_nivel)
        {
            try
            {
                var detalles = _detalles.getDetalleNivel(id_nivel, numero_nivel);
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("Detalles_Cazador")]
        public IActionResult getDetalleCazaAreas(int id_nivel, int orden)
        {
            try
            {
                var cazador = _detalles.getDetalleCazaAreas(id_nivel, orden);
                return Ok(cazador);
            }
            catch (Exception ex)
            { 
                return BadRequest(new {mensaje = ex.Message });
            }
        }

        [HttpGet("Detalle_AreavsPerimetro")]
        public IActionResult getDetalleAreavsPerimetro(int id_nivel, int orden)
        {
            try
            {
                var areaperimetro = _detalles.getDetalleAreavsPerimetro(id_nivel, orden); 
                return Ok(areaperimetro);
            }
            catch (Exception ex)
            { 
                return BadRequest(new {mensaje = ex.Message});
            }
        }

        [HttpGet("Detalle_Clasificador")]
        public IActionResult getClasificador(int id_nivel, int orden)
        {
            try
            {
                var clasificador = _detalles.getClasificador(id_nivel, orden);
                return Ok(clasificador);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
