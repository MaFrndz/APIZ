
using LNegocio.Inico;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
    [Produces("aplication/json")]
    [Route("api/home")]
    [ApiController]
    public class sHomeController : Controller
    {
      
        [HttpPost]
        [Route("menuPorPerfil")]
        public JsonResult listaUsuarios( [FromBody] obtenerMenuPorUsuarioReq param)
        {
            var resul = obtenerMenuPorUsuario.fn(param);
            return Json(resul);
        }

    }
}