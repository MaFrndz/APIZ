using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class EntidadEgreso
    {
        public EntidadEgreso()
        {
            Egreso = new HashSet<Egreso>();
        }

        public int IdEntidadEgreso { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public bool? PagoPeriodico { get; set; }
        public int? DiaPagoPeriodico { get; set; }
        public decimal? MontoPagoPeriodico { get; set; }

        public virtual ICollection<Egreso> Egreso { get; set; }
    }
}
