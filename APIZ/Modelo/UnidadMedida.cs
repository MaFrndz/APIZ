using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            Producto = new HashSet<Producto>();
        }

        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
