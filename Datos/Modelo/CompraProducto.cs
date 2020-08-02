using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class CompraProducto
    {
        public int IdCompraProducto { get; set; }
        public int? IdProducto { get; set; }
        public int? IdMoneda { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }

        public virtual Moneda IdMonedaNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
