using System;
using System.Collections.Generic;

namespace Datos.Modelo
{
    public partial class GrupoPlanilla
    {
        public GrupoPlanilla()
        {
            Planilla = new HashSet<Planilla>();
        }

        public int IdGrupoPlanilla { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Planilla> Planilla { get; set; }
    }
}
