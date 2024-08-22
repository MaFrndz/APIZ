using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResumenController : Controller
	{
		[HttpPost]
		[Route("resumenMes")]
		public JsonResult resumenMes([FromBody] PeridoResumen periodo)
		{
			//var resul = obtenerMenuPorUsuario.fn(null);
			var result = new ResumenLN().resumenMes(periodo);
			return Json(result);
		}
	}
}
