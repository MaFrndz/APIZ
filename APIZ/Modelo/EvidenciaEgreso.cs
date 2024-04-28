using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class EvidenciaEgreso
    {
        public int IdEgreso { get; set; }
        public string B64 { get; set; }

        public virtual Egreso IdEgresoNavigation { get; set; }
    }
}
