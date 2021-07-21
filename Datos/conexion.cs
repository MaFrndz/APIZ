using System.IO;

namespace APIZ.Datos
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
