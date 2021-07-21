using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
    public class UsuarioLN
    {
        SitemaZContext bd = new SitemaZContext();

        public List<usuaio> listaUsuarios()
        {
            var result = (from x in bd.Usuario
                          select(new usuaio { idUsuario= x.IdUsuario , idPerfil = x.IdPerfil.Value, UsuarioSistema = x.UsuarioSistema,
                          Nombres =x.Nombres, ApPaterno = x.ApPaterno, ApMaterno = x.ApMaterno, 
                          Correo = x.Correo, nPerfil = x.IdPerfilNavigation.Nombre, idSede = x.IdSede.Value,nSede = x.IdSedeNavigation.Nombre})).ToList();

            return result; 
        }

        public List<perfil> lsitarPerfil() {
            var result = (from x in bd.Perfil
                          select (new perfil { idPerfil= x.IdPerfil, Nombre = x.Nombre})).ToList();

            return result; 
        }

        public bool insertarEditarUsuario(usuaio param)
        {
            bool result = false;
            try { 
                if(param.idUsuario != 0) {
                    var usuario = (from x in bd.Usuario where x.IdUsuario == param.idUsuario select x).SingleOrDefault();
                    usuario.Nombres = param.Nombres;
                    usuario.ApPaterno = param.ApPaterno;
                    usuario.ApMaterno = param.ApMaterno;
                    usuario.Correo = param.Correo;
                    usuario.UsuarioSistema = param.UsuarioSistema;
                    if(param.ContraseniaAnt != null) { 
                        if(usuario.Contrasenia == Funciones.codificarB64(param.ContraseniaAnt)){
                            usuario.Contrasenia = Funciones.codificarB64(param.ContraseniaNueva);
                            result = true;
                        }
                    }

                    usuario.IdPerfil = param.idPerfil;
                    usuario.IdSede = param.idSede;
                    bd.SaveChanges();
                    bd.Dispose();
                }
                else {
                    Usuario nuevo = new Usuario();
                    nuevo.ApMaterno = param.ApMaterno;
                    nuevo.ApPaterno = param.ApPaterno;
                    nuevo.Nombres = param.Nombres;
                    nuevo.IdSede = param.idSede;
                    nuevo.IdPerfil = param.idPerfil;
                    nuevo.UsuarioSistema = param.UsuarioSistema;
                
                    if(param.ContraseniaNueva != null) {
                        nuevo.Contrasenia = Funciones.codificarB64(param.ContraseniaNueva);
                        result = true;
                    }
                
                    bd.Usuario.Add(nuevo);
                    bd.SaveChanges(); bd.Dispose();
                }
            }
            catch (Exception e) { result = false; throw e; }
            return result;
        }
        public List<sede> listarSede()
        {
            var result = (from x in bd.Sede 
                          select (new sede { idSede = x.IdSede, Nombre = x.Nombre , Detalle = x.Detalle}) ).ToList();
            return result;
        }

        //public List<Perfil> lsitarSede() { }
    }

    public class perfil
    {
        public int idPerfil { get; set; }
        public string Nombre { get; set; }
    }
    public  class sede { 
        public int idSede { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set;  }
    }

    public class usuaio
    {
        public int idUsuario { get; set; }
        public int idPerfil { get; set; }
        public int idSede { get; set; }

        public string UsuarioSistema { get; set; }
        
        public string ContraseniaNueva { get; set; }
        public string ContraseniaAnt { get; set; }

        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Correo { get; set; }
        public string nPerfil { get; set; }
        public string nSede { get; set; }
    }
}
