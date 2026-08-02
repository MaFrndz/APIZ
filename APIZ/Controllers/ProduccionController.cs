
using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionController : Controller
    {

		#region PRODUCCION

		[HttpGet]
		[Route("listaProductoStock")]
		public JsonResult obtenerProductosConStock()
		{
			ProduccionLN obj = new ProduccionLN();
			return Json(obj.lsitaProductosConStock());
		}

		[HttpGet]
        [Route("listaConsumo")]
        public JsonResult listaConsumo()
        {
			ProduccionLN obj = new ProduccionLN();
            return Json(obj.listaConsumo());
        }

        [HttpPost]
        [Route("insertarConsumo")]
        public JsonResult insertarConsumo([FromBody] consumo param)
        {
			ProduccionLN obj = new ProduccionLN();
            return Json(obj.insertarConsumo(param));
        }

        [HttpPost]
        [Route("detalleConsumo")]
        public JsonResult detalleConsumo([FromBody] consumo param)
        {
			ProduccionLN obj = new ProduccionLN();
            return Json(obj.obtenerDetalleConsumo(param));
        }

        [HttpPost]
        [Route("insertarDetalleConsumo")]
        public JsonResult insertarDetalleConsumo([FromBody] detalleConsumo param)
        {
			ProduccionLN obj = new ProduccionLN();
            return Json(obj.insertarDetalleConsumo(param));
        }

        [HttpPatch]
        [Route("actualizarDetalleConsumo/{id}")]
        public JsonResult actualizarDetalleConsumo([FromRoute] int id, [FromBody] detalleConsumo detalleConsumo)
        {
            ProduccionLN obj = new ProduccionLN();
            return Json(obj.actualizarDetalleConsumo(id, detalleConsumo));
        }

        [HttpGet]
        [Route("obtenerDetalleConsumoConDevolucion")]
        public JsonResult obtenerDetalleConsumoConDevolucion()
        {
            ProduccionLN obj = new ProduccionLN();
            return Json(obj.obtenerDetalleConsumoConDevolucion());
        }

        #endregion
    }
}