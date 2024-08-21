using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LNegocio
{
	public class IngresosLN
	{
		SitemaZContext bd = new SitemaZContext();

		public List<IngresoFront> obtenerIngresos()
		{
			var result = (from x in bd.Ingreso
						  where x.Borrado == false || x.Borrado == null
						  orderby x.IdIngreso descending
						  select (new IngresoFront { 
							  idIngreso = x.IdIngreso,
							  producto = x.IdProductoNavigation.Nombre,
							  fecha = x.Fecha,
							  monto = x.Monto.Value,
							  cantidad =	x.Cantidad.Value,
							  precioUnidad = x.ProcioUnidad.Value,
							  descripcion = x.Descripcion
						  })
						  ).ToList();


			return result;
		}

		public int insertarIngreso(IngresoFront param)
		{
			int idGen = 0;
			var producto = (from x in bd.Producto where x.IdProducto == param.idProducto select x).SingleOrDefault();
			decimal monto = param.cantidad * producto.UltimoPrecio.Value; 


			if (param.idIngreso == 0)
			{
				Ingreso nuevo = new Ingreso();
				nuevo.IdUsuario = param.idUsuario;
				nuevo.IdProducto = param.idProducto;
				nuevo.Fecha = param.fecha; 
				nuevo.Monto = monto;
				nuevo.Cantidad = param.cantidad;
				nuevo.ProcioUnidad = producto.UltimoPrecio;
				nuevo.Descripcion = param.descripcion;

				bd.Ingreso.Add(nuevo);
				bd.SaveChanges();
				idGen = nuevo.IdIngreso;
			}
			else
			{
				var obj = (from x in bd.Ingreso where x.IdIngreso == param.idIngreso select x).SingleOrDefault();
				obj.IdUsuario = param.idUsuario;
				obj.IdProducto = param.idProducto;
				obj.Fecha = param.fecha;
				obj.Monto = monto;
				obj.Cantidad = param.cantidad;
				obj.ProcioUnidad = producto.UltimoPrecio;
				obj.Descripcion = param.descripcion;

				bd.SaveChanges();
				idGen = param.idIngreso;
			}
			bd.Dispose();

			return idGen;
		}

		public bool eliminar(int id)
		{
			bool result = false;
			try
			{
				var obj = (from x in bd.Ingreso where x.IdIngreso == id select x).SingleOrDefault();
				obj.Borrado = true;
				bd.SaveChanges();
				bd.Dispose();
				result = true;
			}catch (Exception ex)
			{
				result = false;
			}
			

			return result;
		}
	}

	public class IngresoFront
	{
		public int idIngreso { get; set; }
		public int idUsuario { get; set; }
		public int idProducto { get; set; }
		


		public string producto { get; set; }
		public string fecha { get; set; }
		public decimal monto { get; set; }
		public int cantidad { get; set; }
		public decimal precioUnidad { get; set; }
		public string descripcion { get; set; }
	}
}
