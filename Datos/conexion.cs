
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIZ.Controllers
{
    public class conexion
    {
        public  string getConexionString()
        {
            string ccConexion = "";
            using (StreamReader r = new StreamReader("cadenaCn.json"))
            {
                var json = r.ReadToEnd();


                ccConexion = json;
                

            }

            return ccConexion;
        }
        private class c
        {
            public string cConexion { get; set; }
        }
    }
}
