using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class Perfil
    {
        public Perfil()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public bool? SesionUnica { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
