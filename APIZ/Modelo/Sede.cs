using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class Sede
    {
        public Sede()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdSede { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
