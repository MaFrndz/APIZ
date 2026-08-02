using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
    [Produces("aplication/json")]
    [Route("api/planilla")]
    [ApiController]
    public class PlanillaController : Controller
    {
        [HttpGet]
        [Route("obtenerPlanilla")]
        public JsonResult obtenerPlanilla()
        {
            var result = new PlanillaLN().obtenerPlanilla();


            return Json(result);
        }
    }
}
