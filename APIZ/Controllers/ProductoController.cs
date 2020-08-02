
using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        // GENERAL
        [HttpPost]
        [Route("obtenerUnidadMedida")]
        public JsonResult obtenerUnidadMedida()
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ProductoLN().ListarUnidadMedida();
            return Json(result); 
        }
        //GENERAL
        [HttpPost]
        [Route("listarTipoMoneda")]
        public JsonResult listarTipoMoneda()
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ProductoLN().listarTipoMoneda();
            return Json(result);
        }


        [HttpPost]
        [Route("listarProductos")]
        public JsonResult listarProductos()
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ProductoLN().listarProducto();
            return Json(result);
        }

        [HttpPost]
        [Route("insertarProductos")]
        public JsonResult insertarProductos([FromBody] producto param)
        {
            var result = new ProductoLN().insertarProducto(param);
            return Json(result);
        }

        [HttpPost]
        [Route("insertarCompra")]
        public JsonResult insertarCompra([FromBody] producto param)
        {
            var result = new ProductoLN().insertarCompra(param);
            return Json(result);
        }
    }
}