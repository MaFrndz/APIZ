using Datos.Modelo;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
    public class VentaLN
    {
        SitemaZContext bd = new SitemaZContext();

        public  List<ProductoResult> lsitaProductos  (paramsListaMenu param) {

            var result = (from x in bd.Producto
                        where x.Nombre.Contains( param.nombreProducto) &&   x.UltimoPrecio < param.costoMaximo  
                          select (new ProductoResult {  IdProducto = x.IdProducto, 
                              Nombre=x.Nombre, Precio=x.UltimoPrecio.Value,
                              SimboloMoneda=x.IdMonedaNavigation.Simbolo,
                              Stock= 0, UnidadMedida=x.IdUnidadMedidaNavigation.Nombre}
                          )).ToList();
            return result;
        }
    }

    public class paramsListaMenu
    {
        public int idRazonSocial { get; set; }
        public string nombreProducto { get; set; }
        public int costoMaximo { get; set; }
    }

    public class ProductoResult
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int? Stock { get; set; }
        public decimal? Precio { get; set; }
        public string SimboloMoneda { get; set; }
        public string UnidadMedida { get; set; }
    }
}
