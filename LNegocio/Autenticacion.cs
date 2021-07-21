
using Datos.Modelo;
using System;
using System.Linq;

namespace LNegocio
{
    public class Autenticacion
    {
        

        public static autenticacionRes fn(autenticacionReq param)
        {
            autenticacionRes result = new autenticacionRes();
            
            try { 
            SitemaZContext bd = new SitemaZContext();
            param.contrasenia = Funciones.codificarB64(param.contrasenia);
            
            

            var us = (from x in bd.Usuario
                          where (x.UsuarioSistema == param.usuarioSistema && x.Contrasenia == param.contrasenia)
                          select ( new autenticacionRes {apMaterno = x.ApMaterno,
                                                        apPaterno = x.ApPaterno,
                                                        autorizado = true,
                                                        idPerfil = x.IdPerfil.Value,
                                                        nombres = x.Nombres,
                                                        idUsuario = x.IdUsuario,
                                                        idSede = x.IdSedeNavigation.IdSede
                          } )
                          ).SingleOrDefault();


            if(us != null) {
                result = us;
            }
            else {
                result.autorizado = false;
            }
            }catch(Exception e) { result.nombres = e.Message; }
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
        public int idSede { get; set; }

        public string nombres { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }

    }
}
