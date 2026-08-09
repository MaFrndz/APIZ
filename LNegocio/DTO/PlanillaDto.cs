using System;

namespace LNegocio.DTO
{
    public class PlanillaDto
    {
        public int IdPlanilla { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cargo { get; set; }
        public string Dni { get; set; }
        public string CelularCuenta { get; set; }
        public decimal DiasTrabajados { get; set; }
        public decimal TarifaDia { get; set; }
        public bool Borrado { get; set; }

        public GrupoPlanillaDto grupoPlanilla { get; set; }
    }

    
}
