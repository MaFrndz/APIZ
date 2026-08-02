using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class AsistenciaPlanilla
    {
        public int IdAsistenciaPlanilla { get; set; }
        public int? IdPlanilla { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Asistencia { get; set; }
        public bool? Borrado { get; set; }

        public virtual Planilla IdPlanillaNavigation { get; set; }
    }
}
