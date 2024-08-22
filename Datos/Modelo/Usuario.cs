using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdPerfil { get; set; }
        public int? IdSede { get; set; }
        public string UsuarioSistema { get; set; }
        public string Contrasenia { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Correo { get; set; }
        public bool? Borrado { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
        public virtual Sede IdSedeNavigation { get; set; }
    }
}
