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
			var result = new IngresosLN().obtenerIngresos();
			return Json(result);
		}

		[HttpPost]
		[Route ("insertar")]
		public JsonResult insertarIngreso(IngresoFront param)
		{
			var result = new IngresosLN().insertarIngreso(param);
			return Json(result);
		}

		[HttpDelete]
		[Route("eliminar/{id}")]
		public JsonResult eliminar(int id)
		{
			return Json( new IngresosLN().eliminar(id) );
		}
	}
}
