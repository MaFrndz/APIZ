using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Producto
    {
        public Producto()
        {
            CompraProducto = new HashSet<CompraProducto>();
            DetalleConsumo = new HashSet<DetalleConsumo>();
            Ingreso = new HashSet<Ingreso>();
        }

        public int IdProducto { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdCategoriaProducto { get; set; }
        public string Nombre { get; set; }
        public decimal? UltimoPrecio { get; set; }
        public int? Stock { get; set; }
        public decimal? CostoCompras { get; set; }
        public decimal? CostoGasto { get; set; }
        public bool? ProductoCompra { get; set; }
        public bool? ProductoVenta { get; set; }

        public virtual CategoriaProducto IdCategoriaProductoNavigation { get; set; }
        public virtual Moneda IdMonedaNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; }
        public virtual ICollection<CompraProducto> CompraProducto { get; set; }
        public virtual ICollection<DetalleConsumo> DetalleConsumo { get; set; }
        public virtual ICollection<Ingreso> Ingreso { get; set; }
    }
}
