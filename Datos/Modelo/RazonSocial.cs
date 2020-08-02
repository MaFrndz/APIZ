using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class RazonSocial
    {
        public RazonSocial()
        {
            Producto = new HashSet<Producto>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdRazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Ruc { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
