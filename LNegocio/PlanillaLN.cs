using Datos.Modelo;
using LNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LNegocio
{
    public class PlanillaLN
    {
        SitemaZContext bd = new SitemaZContext();
        public List<PlanillaDto> obtenerPlanilla()
        {
            //var result = (from x in bd.pk
            //              select (new PlanillaResult
            //              {
            //                  IdPlanilla = x.IdPlanilla,
            //                  Nombre = x.Nombre,
            //                  Fecha = x.Fecha.Value
            //              })).ToList();
            //return result;
            return null;
        }
    }
}
