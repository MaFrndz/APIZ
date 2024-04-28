using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class DetalleConsumo
    {
        public int IdDetalleConsumo { get; set; }
        public int? IdConsumo { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }

        public virtual Consumo IdConsumoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
