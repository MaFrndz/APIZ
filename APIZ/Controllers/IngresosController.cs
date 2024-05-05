using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngresosController : Controller
	{

		[HttpGet]
		[Route("obtenerIngresos")]
		public JsonResult obtenerIngresos()
		{
			
			var result = new ProductoLN().ListarUnidadMedida();
			return Json(result);
		}

	}
}
