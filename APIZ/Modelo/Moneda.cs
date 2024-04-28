using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class Moneda
    {
        public Moneda()
        {
            CompraProducto = new HashSet<CompraProducto>();
            Producto = new HashSet<Producto>();
        }

        public int IdMoneda { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }

        public virtual ICollection<CompraProducto> CompraProducto { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
