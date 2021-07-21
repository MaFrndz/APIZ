using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Consumo
    {
        public Consumo()
        {
            DetalleConsumo = new HashSet<DetalleConsumo>();
        }

        public int IdConsumo { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nombre { get; set; }
        public int? IdSede { get; set; }

        public virtual ICollection<DetalleConsumo> DetalleConsumo { get; set; }
    }
}
