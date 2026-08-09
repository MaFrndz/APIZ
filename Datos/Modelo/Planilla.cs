using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class Planilla
    {
        public Planilla()
        {
            AsistenciaPlanilla = new HashSet<AsistenciaPlanilla>();
        }

        public int IdPlanilla { get; set; }
        public int? IdGrupoPlanilla { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cargo { get; set; }
        public string Dni { get; set; }
        public string Celularcuenta { get; set; }
        public decimal? DiasTrabajados { get; set; }
        public decimal? TarifaDia { get; set; }
        public bool? Borrado { get; set; }

        public virtual GrupoPlanilla IdGrupoPlanillaNavigation { get; set; }
        public virtual ICollection<AsistenciaPlanilla> AsistenciaPlanilla { get; set; }
    }
}
