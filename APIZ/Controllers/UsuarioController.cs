using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LNegocio;
using Datos.Modelo;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        UsuarioLN obj = new UsuarioLN();

        [HttpGet]
        [Route("listaUsuarios")]
        public JsonResult listaUsuarios(){
            
            var result = obj.listaUsuarios();
            return Json( result );
        }

        [HttpGet]
        [Route("listaPerfil")]
        public JsonResult listaPerfil()
        {
            var result = obj.lsitarPerfil();
            return Json(result);
        }

        [HttpPost]
        [Route("insertarEditarUsuario")]
        public JsonResult insertarEditarUsuario([FromBody] usuaio param)
        {
            var result = obj.insertarEditarUsuario(param);
            return Json(result);
        }

        [HttpGet]
        [Route("listarSede")]
        public JsonResult listarSede()
        {
            var result = obj.listarSede();
            return Json(result);
        }

        [HttpGet]
        [Route("listarMenuPerfil/{idPerfil}")]
        public JsonResult listMenuPerfil(int idPerfil)
        {
            var result = obj.listMenuPerfil(idPerfil);
            return Json(result);    
		}

		[HttpDelete]
		[Route("eliminar/{idUsuario}")]
		public JsonResult eliminar(int idUsuario)
		{
			var result = obj.eliminarUsuario(idUsuario);
			return Json(result);
		}

		[HttpPost]
        [Route("actualizarMenuPerfil")]
        public JsonResult actualizarMenuPerfil([FromBody] List<LNegocio.MenuPerfil> menu)
        {
			var result = obj.actualizarMenuPerfil(menu);
			return Json(result);
		}
    }
}