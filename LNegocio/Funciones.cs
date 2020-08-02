using System;
using System.Collections.Generic;
using System.Text;

namespace LNegocio
{
    public class Funciones
    {
        public static string codificarB64(string val)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(val);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public  static string decodificarB64(string val)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(val);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


    }
}
