using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Pedido
    {
        public Pedido()
        {
            CompraProducto = new HashSet<CompraProducto>();
        }

        public int IdPedido { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nombre { get; set; }
        public int? IdSede { get; set; }

        public virtual ICollection<CompraProducto> CompraProducto { get; set; }
    }
}
