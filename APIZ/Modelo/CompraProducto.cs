using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class CompraProducto
    {
        public int IdCompraProducto { get; set; }
        public int? IdProducto { get; set; }
        public int? IdMoneda { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public decimal? PrecioTotal { get; set; }
        public int? IdPedido { get; set; }
        public DateTime? FechaCompra { get; set; }
        public bool? Agotado { get; set; }
        public int? CantidadConsumida { get; set; }

        public virtual Moneda IdMonedaNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
