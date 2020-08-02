using Microsoft.AspNetCore.Mvc;
using System.IO;
using LNegocio;
using System;
using Microsoft.Extensions.Configuration;

namespace APIZ.Controllers
{
    [Produces("aplication/json")]
    [Route("api/fLogin")]
    [ApiController]
    public class fLoginController : Controller
    {
        private IConfiguration _config;
        public void Startup(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("autenticacion")]
        public JsonResult listaUsuarios( [FromBody] autenticacionReq param)
        {
            

             var resul = Autenticacion.fn(param);
            //string t = DateTime.Now.Ticks.ToString();
            //  DateTime t1 = new DateTime(Convert.ToInt64(t));
            return Json(resul);
        }


        private bool validTokem(string tokem)
        {
            bool result = false; 
            StreamReader file = new StreamReader("Modelo/tokems.txt");
            string tokems = file.ReadLine();
            file.Close();
            if (tokems.Contains(tokem)) { result = true; }

            return result;
        }

        private void addTokem(string val)
        {
          
            using (StreamWriter writer = System.IO.File.AppendText("Modelo/tokems.txt"))
            {
                writer.Write(val);
            }
        }

    }
}