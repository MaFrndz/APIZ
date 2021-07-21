using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LNegocio;

namespace APIZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        UsuarioLN obj = new UsuarioLN();

        [HttpPost]
        [Route("listaUsuarios")]
        public JsonResult listaUsuarios(){
            
            var result = obj.listaUsuarios();
            return Json( result );
        }
        [HttpPost]
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
        [HttpPost]
        [Route("listarSede")]
        public JsonResult listarSede()
        {
            var result = obj.listarSede();
            return Json(result);
        }
    }
}