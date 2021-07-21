using Datos.Modelo;
using LNegocio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContabilidadController : Controller
    {
        [HttpPost]
        [Route("insertarEditarEntidad")]
        public JsonResult insertarEditarEntidad(EntidadEgreso param)
        {
            //var resul = obtenerMenuPorUsuario.fn(null);
            var result = new ContabilidadLN().insertarEditarEntidad(param);
            return Json(result);
        }
        [HttpPost]
        [Route("listarEgresos")]
        public JsonResult listarEgresos(paramsBusquedaEgresos param)
        {
            var result = new ContabilidadLN().listarEgresos(param);
            return Json(result);
        }
        [HttpPost]
        [Route("listarEgresosContar")]
        public JsonResult listarEgresosContar(paramsBusquedaEgresos param)
        {
            var result = new ContabilidadLN().listarEgresosContar(param);
            return Json(result);
        }
        [HttpPost]
        [Route("obtenerEvidencia")]
        public JsonResult obtenerEvidencia(egresoFront param)
        {
            var result = new ContabilidadLN().obtenerEvidencia(param);
            return Json(result);
        }


        [HttpPost]
        [Route("listarEntidadEgreso")]
        public JsonResult listarEntidadEgreso()
        {
            var result = new ContabilidadLN().listarEntidadEgreso();
            return Json(result);
        }
        [HttpPost]
        [Route("insertarEgreso")]
        public JsonResult insertarEgreso(insertarEgreso param)
        {
            var result = new ContabilidadLN().insertarEgreso(param);
            return Json(result);
        }
    }
}