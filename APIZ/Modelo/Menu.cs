using System;
using System.Collections.Generic;

namespace APIZ.Modelo
{
    public partial class Menu
    {
        public int IdMenu { get; set; }
        public string NomMenu { get; set; }
        public string Icon { get; set; }
        public int? Orden { get; set; }
    }
}
