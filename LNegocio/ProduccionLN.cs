using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
	public class ProduccionLN
	{
		SitemaZContext bd = new SitemaZContext();

		public List<ProductoResult> lsitaProductosConStock()
		{
			var result = (from x in bd.Producto
						  where x.Stock > 0
						  select (new ProductoResult
						  {
							  IdProducto = x.IdProducto,
							  Nombre = x.Nombre + " - stock:" + x.Stock+ "("+ x.IdUnidadMedidaNavigation.Nombre + ")",
							  Precio = x.UltimoPrecio.Value,
							  SimboloMoneda = x.IdMonedaNavigation.Simbolo,
							  Stock = x.Stock.Value,
							  UnidadMedida = x.IdUnidadMedidaNavigation.Nombre
						  }
						  )).ToList();
			return result;
		}

		#region produccion
		public List<consumo> listaConsumo()
		{
			var result = (from x in bd.Consumo
						  orderby x.IdConsumo descending 
						  select (new consumo
						  {
							  IdConsumo = x.IdConsumo,
							  Fecha = x.Fecha.Value,
							  Nombre = x.Nombre,
							  Sede = (from s in bd.Sede where s.IdSede == x.IdSede
									  select s.Nombre).FirstOrDefault()
						  })).ToList();

			//foreach (var item in result)
			//{
			//	var detalleConsumo = (from x in bd.DetalleConsumo where x.IdConsumo.Value == item.IdConsumo select x).ToList();
			//	decimal cant = 0;

			//	for (int i = 0; i < detalleConsumo.Count; i++)
			//	{
			//		var prodd = (from x in bd.Producto where x.IdProducto == detalleConsumo[i].IdProducto.Value select x).SingleOrDefault();
			//		cant += Math.Round(detalleConsumo[i].Cantidad.Value * prodd.UltimoPrecio.Value, 2);
			//		item.costoTotal = cant.ToString();
			//	}
			//	if (item.costoTotal == null) item.costoTotal = "S/. 0";
			//}

			return result;
		}
		public int insertarConsumo(consumo param)
		{
			int idgen = 0;
			if (param.IdConsumo == 0)
			{
				Consumo nuevo = new Consumo();
				nuevo.Nombre = param.Nombre;
				nuevo.Fecha = param.Fecha;
				nuevo.IdSede = param.idSede;
				bd.Consumo.Add(nuevo);
				bd.SaveChanges();
				idgen = nuevo.IdConsumo;
			}
			else
			{
				var obj = (from x in bd.Consumo where x.IdConsumo == param.IdConsumo select x).SingleOrDefault();
				obj.Nombre = param.Nombre;
				bd.SaveChanges();
				idgen = param.IdConsumo;
			}
			bd.Dispose();
			return idgen;
		}

		public List<detalleConsumo> obtenerDetalleConsumo(consumo param)
		{
			var result = (from x in bd.DetalleConsumo
						  where x.IdConsumo == param.IdConsumo
						  orderby x.IdDetalleConsumo descending
						  select (new detalleConsumo
						  {
							  cantidad = x.Cantidad.Value,
							  producto = x.IdProductoNavigation.Nombre,
							  idProducto = x.IdProducto.Value,
							  idConsumo = x.IdConsumo.Value,
						  })).ToList();

			//foreach (var item in result)
			//{
			//	var prodd = (from x in bd.Producto where x.IdProducto == item.idProducto select x).SingleOrDefault();
			//	item.costoTotal = Math.Round(item.cantidad * prodd.UltimoPrecio.Value, 2).ToString();
			//}

			return result;
		}

		public bool insertarDetalleConsumo(detalleConsumo param)
		{
			bool result = false;

			try
			{
				DetalleConsumo nuevo = new DetalleConsumo();
				nuevo.Cantidad = param.cantidad;
				nuevo.IdProducto = param.idProducto;
				nuevo.IdConsumo = param.idConsumo;
				// restar stock
				var prod = (from x in bd.Producto where x.IdProducto == param.idProducto select x).SingleOrDefault();
				prod.Stock = prod.Stock - param.cantidad;
				// hacer calculo en monto gastado 

				bd.DetalleConsumo.Add(nuevo);
				bd.SaveChanges();

				result = true;

			}
			catch (Exception e) { throw e; }

			return result;
		}
		#endregion
	}

}
