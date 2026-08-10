using LNegocio;
using LNegocio.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APIZ.Controllers
{
    [Produces("aplication/json")]
    [Route("api/planilla")]
    [ApiController]
    public class PlanillaController : Controller
    {
        [HttpGet("obtenerPlanilla/{periodo?}")]
        public JsonResult obtenerPlanilla([FromRoute] string periodo)
        {
            var result = new PlanillaLN().obtenerPlanilla(periodo);
            return Json(result);
        }

        [HttpPost]
        [Route("insertarPlanilla")]
        public JsonResult insertarPlanilla([FromBody] PlanillaDto param)
        {
            var result = new PlanillaLN().insertarPlanilla(param);
            return Json(result);
        }

        [HttpPut]
        [Route("actualizarPlanilla/{id}")]
        public JsonResult actualizarPlanilla([FromRoute] int id, [FromBody] PlanillaDto param)
        {
            var result = new PlanillaLN().actualizarPlanilla(id, param);
            return Json(result);
        }

        [HttpGet]
        [Route("obtenerGrupoPlanilla")]
        public JsonResult obtenerGrupoPlanilla()
        {
            var result = new PlanillaLN().obtenerGrupoPlanilla();
            return Json(result);
        }

        [HttpDelete]
        [Route("eliminarPlanilla/{id}")]
        public JsonResult eliminarPlanilla([FromRoute] int id)
        {
            var result = new PlanillaLN().eliminarPlanilla(id);
            return Json(result);
        }

        [HttpPost]
        [Route("guardarAsistencia")]
        public JsonResult guardarAsistencia([FromBody] List<AsistenciaPlanillaDto> listaAsistencia)
        {
            var result = new PlanillaLN().guardarAsistencias(listaAsistencia);
            return Json(result);
        }

        [HttpGet]
        [Route("obtenerAsistenciasPorFecha/{fecha}")]
        public JsonResult obtenerAsistenciasPorFecha([FromRoute] string fecha)
        {
            System.DateTime fechaConvertida = System.DateTime.Parse(fecha);
            var result = new PlanillaLN().obtenerAsistenciasPorFecha(fechaConvertida);
            return Json(result);
        }

        [HttpGet]
        [Route("obtenerAsistenciasPorIdPlanilla/{idPlanilla}")]
        public JsonResult obtenerAsistenciasPorIdPlanilla([FromRoute] int idPlanilla)
        {
            var result = new PlanillaLN().obtenerAsistenciasPorIdPlanilla(idPlanilla);
            return Json(result);
        }
    }
}
