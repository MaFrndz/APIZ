using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Ingreso
    {
        public int IdIngreso { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProducto { get; set; }
        public string Fecha { get; set; }
        public decimal? Monto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? ProcioUnidad { get; set; }
        public string Descripcion { get; set; }
        public bool? Borrado { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
    }
}
