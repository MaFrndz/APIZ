﻿using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Egreso
    {
        public int IdEgreso { get; set; }
        public int IdUsuario { get; set; }
        public int IdEntidadEgreso { get; set; }
        public string Fecha { get; set; }
        public decimal? Monto { get; set; }
        public string Descripcion { get; set; }
        public bool? Borrado { get; set; }

        public virtual EntidadEgreso IdEntidadEgresoNavigation { get; set; }
        public virtual EvidenciaEgreso EvidenciaEgreso { get; set; }
    }
}
