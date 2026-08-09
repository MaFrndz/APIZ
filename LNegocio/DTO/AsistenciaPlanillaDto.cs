using System;

namespace LNegocio.DTO
{
    public class AsistenciaPlanillaDto
    {
        public int IdAsistenciaPlanilla { get; set; }
        public int? IdPlanilla { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Asistencia { get; set; }
        public bool? Borrado { get; set; }
    }
}
