using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Producto
    {
        public Producto()
        {
            CompraProducto = new HashSet<CompraProducto>();
        }

        public int IdProducto { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdRazonSocial { get; set; }
        public int? IdMoneda { get; set; }
        public string Nombre { get; set; }
        public int? Stock { get; set; }
        public decimal? Precio { get; set; }

        public virtual Moneda IdMonedaNavigation { get; set; }
        public virtual RazonSocial IdRazonSocialNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<CompraProducto> CompraProducto { get; set; }
    }
}
