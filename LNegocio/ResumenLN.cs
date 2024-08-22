using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace LNegocio
{
	public class ResumenLN
	{
		SitemaZContext bd = new SitemaZContext();
		public ResultResumenMes resumenMes( PeridoResumen periodo)
		{
			//Gastos: pedidos
			var comprasProductos = (from x in bd.Pedido 
									where x.Fecha.Value.Year == Int32.Parse( periodo.anio) && x.Fecha.Value.Month == Int32.Parse( periodo.mes)  
									select x.CompraProducto).SelectMany( cp => cp).ToList();

			//Gastos: egresos registrados
			var egresos = (from x in bd.Egreso where x.Fecha.StartsWith(periodo.anio+"-"+periodo.mes) select x).ToList();

			//ingresos: ventas
			var ingresos = (from x in bd.Ingreso where x.Fecha.StartsWith(periodo.anio + "-" + periodo.mes) select x).ToList();

			var resumen = elaborarResumen(comprasProductos, egresos, ingresos);	
			
			return resumen; 
		}

		private ResultResumenMes elaborarResumen(List<CompraProducto> compraProducto, List<Egreso> egreso, List<Ingreso> ingreso)
		{
			decimal montoCompraProducto = 0; decimal  montoEgreso = 0; decimal montoIngreso = 0; 
			foreach (var x in compraProducto)
			{
				montoCompraProducto += x.PrecioTotal.Value;
			}

			foreach (var x in egreso)
			{
				montoEgreso += x.Monto.Value;
			}

			foreach (var x in ingreso)
			{
				montoIngreso += x.Monto.Value;
			}

			ResultResumenMes result=  new ResultResumenMes();
			result.montoToralEgreso = montoCompraProducto + montoEgreso; 
			result.montoTotalIngresos = montoIngreso;
			result.montoComprasProductos = montoCompraProducto;
			result.montoEgresos = montoEgreso;

			return result;
		}
	}

	public class ResultResumenMes
	{
		public decimal montoTotalIngresos {get;set; }
		public decimal montoToralEgreso { get;set; }
		public decimal montoComprasProductos { get;set; }
		public decimal montoEgresos { get; set; }
	}

	public class PeridoResumen
	{
		public string mes { get; set; }
		public string anio { get; set; }	
	}
}
