using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LNegocio.Inico
{
    public class obtenerMenuPorUsuario
    {
        public static List< obtenerMenuPorUsuarioRes> fn (obtenerMenuPorUsuarioReq param)
        {
            SitemaZContext bd = new SitemaZContext();
            
            var result = (from x in bd.MenuPerfil
                          where (x.IdPerfil == param.idPerfil)
                          select (new obtenerMenuPorUsuarioRes { menu = x.IdMenuNavigation.NomMenu ,visible = x.Visible.Value} )
                            ).ToList();

            return result;
        }
    }

    public class obtenerMenuPorUsuarioReq
    {
        public int idPerfil { get; set; }
    }

    public class obtenerMenuPorUsuarioRes
    {
        public string menu { get; set; }
        public bool visible { get; set; }
    }
}
