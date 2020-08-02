
using Datos.Modelo;
using System.Linq;

namespace LNegocio
{
    public class Autenticacion
    {
        public static autenticacionRes fn(autenticacionReq param)
        {

            SitemaZContext bd = new SitemaZContext();
            param.contrasenia = Funciones.codificarB64(param.contrasenia);
            autenticacionRes result = new autenticacionRes(); 
            

            var us = (from x in bd.Usuario
                          where (x.UsuarioSistema == param.usuarioSistema && x.Contrasenia == param.contrasenia)
                          select ( new autenticacionRes {apMaterno = x.ApMaterno,
                                                        apPaterno = x.ApPaterno,
                                                        autorizado = true,
                                                        idPerfil = x.IdPerfil.Value,
                                                        nombres = x.Nombres,
                                                        idUsuario = x.IdUsuario,
                                                        IdRazonSocial = x.IdRazonSocialNavigation.IdRazonSocial
                          } )
                          ).SingleOrDefault();


            if(us != null) {
                result = us;
            }
            else {
                result.autorizado = false;
            }

            return result;
        }
    }

    public class autenticacionReq
    {
        public string usuarioSistema { get; set; }
        public string contrasenia { get; set; }
    }

    public class autenticacionRes
    {
        public bool autorizado { get; set; }
        public int idPerfil { get; set; }
        public int idUsuario { get; set; }
        public int IdRazonSocial { get; set; }

        public string nombres { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }

    }
}
