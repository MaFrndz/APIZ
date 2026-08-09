using System;
using System.Collections.Generic;
using System.Text;

namespace LNegocio.DTO
{
    public class GrupoPlanillaDto
    {
        public int idGrupoPlanilla { get; set; }
        public string nombre { get; set; }

        public GrupoPlanillaDto() { }

        public GrupoPlanillaDto(int idGrupoPlanilla, string nombre)
        {
            this.idGrupoPlanilla = idGrupoPlanilla;
            this.nombre = nombre;
        }
    }
}
