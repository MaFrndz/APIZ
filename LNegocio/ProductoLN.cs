
using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
	public class ProductoLN
	{
		SitemaZContext bd = new SitemaZContext();

		private static int verStockProducto(int idProducto, int idSede)
		{
			SitemaZContext bd = new SitemaZContext();
			var pedido = (from x in bd.Pedido where x.IdSede.Value == idSede select x).ToList();
			int total = 0;

			foreach (var item in pedido)
			{
				var productos = (from x in bd.CompraProducto where x.IdPedido.Value == item.IdPedido select x).ToList(); //item.CompraProducto.ToList();
				foreach (var prod in productos)
				{
					if (prod.IdProducto.Value == idProducto) total += prod.Cantidad.Value;
				}
			}
			return total;
		}
		private static int verConsumoProducto(int idProducto, int idSede)
		{
			SitemaZContext bd = new SitemaZContext();
			var consumo = (from x in bd.Consumo where x.IdSede.Value == idSede select x).ToList();
			int total = 0;

			foreach (var c in consumo)
			{
				var detalle = (from x in bd.DetalleConsumo where x.IdConsumo == c.IdConsumo select x).ToList();
				//var detalle = c.DetalleConsumo.ToList();

				foreach (var d in detalle)
				{
					if (d.IdProducto.Value == idProducto) total += d.Cantidad.Value;
				}
			}


			return total;
		}
		private static decimal verComprasPorProducto(int idProd, int idSede)
		{
			SitemaZContext bd = new SitemaZContext();
			decimal total = 0;
			// ver los pedidos de la sede
			var pedidos = (from x in bd.Pedido where x.IdSede == idSede select x).ToList();

			foreach (var item in pedidos)
			{
				total += (from x in bd.CompraProducto
						  where x.IdProducto == idProd && x.IdPedido == item.IdPedido
						  select x).Sum(x => x.PrecioTotal.Value);
			}

			return total;
		}

		#region almacen

		public List<unidaMedida> ListarUnidadMedida()
		{
			//List<unidaMedida> result = new List<unidaMedida>();

			var result = (from x in bd.UnidadMedida
						  select (new unidaMedida { idUnidadMedida = x.IdUnidadMedida, UnidadMedida = x.Nombre })
					   ).ToList();
			return result;
		}

		public List<moneda> listarTipoMoneda()
		{

			var result = (from x in bd.Moneda
						  select (new moneda { codigo = x.Codigo, nombre = x.Nombre, simbolo = x.Codigo, idMoneda = x.IdMoneda })
						  ).ToList();
			return result;
		}

		public List<producto> listarProducto(string tipo)
		{

			var result = (from x in bd.Producto
						  where tipo == "compra" ? x.ProductoCompra == true : x.ProductoVenta == true
						  select (new producto
						  {
							  idProducto = x.IdProducto,
							  nomProducto = x.Nombre,
							  precioUnidad = x.UltimoPrecio.Value,
							  stock = x.Stock.Value,
							  CostoCompras = x.CostoCompras.Value,
							  CostoGasto = x.CostoGasto.Value,
							  simboloMoneda = x.IdMonedaNavigation.Simbolo,
							  NombreCategoria = x.IdCategoriaProductoNavigation.Nombre,
							  idCategoriaProducto = x.IdCategoriaProducto.Value,
							  IdUnidadMedida = x.IdUnidadMedida.Value,
							  idMoneda = x.IdMoneda.Value,
							  nombreUnidadMedida = x.IdUnidadMedidaNavigation.Nombre
						  })).ToList();



			return result;
		}


		public List<categoriaProducto> listaCategoriaProducto()
		{
			var result = (from x in bd.CategoriaProducto
						  select (new categoriaProducto { idCategoriaProducto = x.IdCategoriaProducto, Nombre = x.Nombre })
						  ).ToList();

			return result;
		}

		public bool insertarProducto(producto param)
		{
			bool insert = false;
			// existe 
			var edit = (from x in bd.Producto
						where x.IdProducto == param.idProducto
						select x).SingleOrDefault();
			if (edit != null)
			{
				edit.IdUnidadMedida = param.IdUnidadMedida;

				edit.Nombre = param.nomProducto;
				//edit.Stock = param.stock;
				edit.UltimoPrecio = param.precioUnidad;
				edit.IdCategoriaProducto = param.idCategoriaProducto;
				edit.IdMoneda = param.idMoneda;
				edit.ProductoCompra = param.productoCompra;
				edit.ProductoVenta = param.productoVenta;
				bd.SaveChanges();
				insert = true;
			}
			else
			{
				Producto nuevo = new Producto();
				nuevo.IdUnidadMedida = param.IdUnidadMedida;

				nuevo.Nombre = param.nomProducto;
				nuevo.IdMoneda = param.idMoneda;
				nuevo.UltimoPrecio = param.precioUnidad;
				nuevo.Stock = 0;
				nuevo.IdCategoriaProducto = param.idCategoriaProducto;
				nuevo.ProductoCompra = param.productoCompra;
				nuevo.ProductoVenta = param.productoVenta;

				bd.Producto.Add(nuevo);
				bd.SaveChanges();
				insert = true;
			}
			bd.Dispose();
			return insert;
		}

		public bool insertarCompra(producto param)
		{
			bool actualizar = false;

			// insertar en compra
			CompraProducto compra = new CompraProducto();
			compra.IdProducto = param.idProducto;
			compra.PrecioUnidad = param.precioUnidad;
			compra.PrecioTotal = param.precioTotal;
			compra.Cantidad = param.stock;
			compra.IdMoneda = param.idMoneda;
			compra.IdPedido = param.idPedido;
			compra.FechaCompra = param.fechaCompra;
			compra.Agotado = false;
			compra.CantidadConsumida = 0;
			bd.CompraProducto.Add(compra);

			// actualizar stock 
			var producto = (from x in bd.Producto where x.IdProducto == param.idProducto select x).SingleOrDefault();
			producto.Stock = producto.Stock + param.stock;
			producto.UltimoPrecio = param.precioUnidad;
			producto.CostoCompras = producto.CostoCompras + param.precioTotal;

			//var pedido = (from x in bd.Pedido where x.IdPedido == param.idPedido select x).SingleOrDefault();
			//producto.CostoCompras = verComprasPorProducto(param.idProducto, pedido.IdSede.Value) + param.precioTotal;
			try
			{
				bd.SaveChanges();
				actualizar = true;
			}

			catch (Exception e) { throw e; }
			return actualizar;
		}

		public List<pedido> listarPedidos()
		{
			var result = (from x in bd.Pedido
						  orderby x.IdPedido descending
						  select (new pedido
						  {
							  idPedido = x.IdPedido,
							  FechaPedido = x.Fecha.Value.ToString("dd/MM/yyyy"),
							  NombrePedido = x.Nombre
						  })
								).ToList();

			foreach (var item in result)
			{

				var compras = (from x in bd.CompraProducto
							   where x.IdPedido == item.idPedido
							   select x).ToList();
				item.costoTotal = "S/. " + Math.Round(compras.Sum(x => x.PrecioTotal.Value), 2).ToString();
			}

			return result;
		}

		public int insertarPedido(pedido param)
		{
			int idPedido = 0;

			var existe = (from x in bd.Pedido
						  where x.IdPedido == param.idPedido
						  select x).SingleOrDefault();
			if (existe != null)
			{
				existe.Nombre = param.NombrePedido;
				existe.Fecha = (param.FechaPedidoDT);

				bd.SaveChanges();
				idPedido = param.idPedido;
			}
			else
			{
				Pedido nuevo = new Pedido();
				nuevo.Fecha = (param.FechaPedidoDT);
				nuevo.Nombre = param.NombrePedido;
				nuevo.IdSede = param.idSede;

				bd.Pedido.Add(nuevo);
				bd.SaveChanges();
				idPedido = nuevo.IdPedido;
			}

			return idPedido;
		}

		public List<compraProducto> listaCompraProductoPorPedido(int param)
		{
			List<compraProducto> result = new List<compraProducto>();
			result = (from x in bd.CompraProducto
					  where x.IdPedido.Value == param
					  orderby x.IdCompraProducto descending
					  select (new compraProducto
					  {
						  idCompraProducto = x.IdCompraProducto,
						  Cantidad = x.Cantidad.Value,
						  Precio = x.PrecioTotal.Value,
						  nombreProducto = x.IdProductoNavigation.Nombre
					  })
					  ).ToList();

			return result;
		}
		#endregion


	}

	//produccion
	public class detalleConsumo
	{
		public int idProducto { get; set; }
		public int idConsumo { get; set; }
		public int cantidad { get; set; }

		public string producto { get; set; }
		//public string costoTotal { get; set; }
	}

	public class consumo
	{
		public int IdConsumo { get; set; }
		public DateTime Fecha { get; set; }
		public string Nombre { get; set; }
		public string Sede { get; set; }
		public int idSede { get; set; }
	}


	// almacen
	public class compraProducto
	{
		public int idCompraProducto { get; set; }
		public int Cantidad { get; set; }
		public decimal Precio { get; set; }
		public string nombreProducto { get; set; }
	}
	public class pedido
	{
		public int idPedido { get; set; }
		public int idSede { get; set; }

		public string NombrePedido { get; set; }
		public string FechaPedido { get; set; }
		public DateTime FechaPedidoDT { get; set; }
		public string costoTotal { get; set; }
	}
	public class moneda
	{
		public int idMoneda { get; set; }
		public string codigo { get; set; }
		public string nombre { get; set; }
		public string simbolo { get; set; }
	}
	public class producto
	{
		public int IdUnidadMedida { get; set; }
		public int IdRazonSocial { get; set; }
		public int idMoneda { get; set; }
		public int idProducto { get; set; }
		public int idPedido { get; set; }
		public int idCategoriaProducto { get; set; }

		public string nomProducto { get; set; }
		public string simboloMoneda { get; set; }
		public int stock { get; set; }
		public decimal precioTotal { get; set; }
		public decimal precioUnidad { get; set; }

		public decimal CostoCompras { get; set; }
		public decimal CostoGasto { get; set; }
		public string NombreCategoria { get; set; }
		public string nombreUnidadMedida { get; set; }
		public DateTime fechaCompra { get; set; }

		public bool productoCompra { get; set; }
		public bool productoVenta { get; set; }

	}
	public class productoBuscar
	{
		public int idCategoriaProducto { get; set; }
		public string nomProducto { get; set; }
		public bool ordeby { get; set; }
		public int idSede { get; set; }

		//public int numPag { get; set; }
		//public int tamPag { get; set; }
	}

	public class unidaMedida
	{
		public int idUnidadMedida { get; set; }
		public string UnidadMedida { get; set; }
	}
	public class categoriaProducto
	{
		public int idCategoriaProducto { get; set; }
		public string Nombre { get; set; }
	}
}
