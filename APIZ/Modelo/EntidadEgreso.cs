using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class EntidadEgreso
    {
        public int IdEntidadEgreso { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public bool? PagoPeriodico { get; set; }
        public int? DiaPagoPeriodico { get; set; }
        public decimal? MontoPagoPeriodico { get; set; }
        public bool? Borrado { get; set; }
    }
}
