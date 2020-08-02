using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class MenuPerfil
    {
        public int? IdMenu { get; set; }
        public int? IdPerfil { get; set; }
        public bool? Visible { get; set; }

        public virtual Menu IdMenuNavigation { get; set; }
        public virtual Perfil IdPerfilNavigation { get; set; }
    }
}
