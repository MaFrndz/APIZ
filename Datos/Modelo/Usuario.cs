using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Usuario
    {
        public Usuario()
        {
            Producto = new HashSet<Producto>();
        }

        public int IdUsuario { get; set; }
        public int? IdPerfil { get; set; }
        public int? IdRazonSocial { get; set; }
        public string UsuarioSistema { get; set; }
        public string Contrasenia { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Correo { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
        public virtual RazonSocial IdRazonSocialNavigation { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
