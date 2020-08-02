
using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        [HttpPost]
        [Route("obtenerListaProductos")]
        public JsonResult obtenerUnidadMedida([FromBody] paramsListaMenu param)
        {
            
            var result = new VentaLN().lsitaProductos( param);
            return Json(result); 
        }
        
    }
}