using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class CategoriaProducto
    {
        public CategoriaProducto()
        {
            Producto = new HashSet<Producto>();
        }

        public int IdCategoriaProducto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
