
using LNegocio;
using Microsoft.AspNetCore.Mvc;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        #region ALMACEN

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
        public JsonResult listarProductos([FromBody] productoBuscar param)
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ProductoLN().listarProducto(param);
            return Json(result);
        }
        [HttpPost]
        [Route("listarProductosContar")]
        public JsonResult listarProductosCont([FromBody] productoBuscar param)
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ProductoLN().listarProductoCont(param);
            return Json(result);
        }


        [HttpPost]
        [Route("listarCategoriasProducto")]
        public JsonResult listarCategoriasProducto()
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ProductoLN().listaCategoriaProducto();
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
        [HttpPost]
        [Route("listarPedidos")]
        public JsonResult listarPedidos()
        {
            var result = new ProductoLN().listarPedidos();
            return Json(result);
        }
        [HttpPost]
        [Route("insertarPedido")]
        public JsonResult insertarPedido([FromBody] pedido param)
        {
            var result = new ProductoLN().insertarPedido(param);
            return Json(result);
        }
        [HttpPost]
        [Route("listaCompraProductoPorPedido")]
        public JsonResult listaCompraProductoPorPedido([FromBody] int param)
        {
            var result = new ProductoLN().listaCompraProductoPorPedido(param);
            return Json(result);
        }
        #endregion

        #region PRODUCCION
        [HttpPost]
        [Route("listaConsumo")]
        public JsonResult obtenerListaPedidos()
        {
            ProductoLN obj = new ProductoLN();
            return Json(obj.listaConsumo());
        }

        [HttpPost]
        [Route("insertarConsumo")]
        public JsonResult insertarConsumo([FromBody] consumo param)
        {
            ProductoLN obj = new ProductoLN();
            return Json(obj.insertarConsumo(param));
        }

        [HttpPost]
        [Route("detalleConsumo")]
        public JsonResult detalleConsumo([FromBody] consumo param)
        {
            ProductoLN obj = new ProductoLN();
            return Json(obj.obtenerDetalleConsumo(param));
        }

        [HttpPost]
        [Route("insertarDetalleConsumo")]
        public JsonResult insertarDetalleConsumo([FromBody] detalleConsumo param)
        {
            ProductoLN obj = new ProductoLN();
            return Json(obj.insertarDetalleConsumo(param));
        }
        #endregion
    }
}