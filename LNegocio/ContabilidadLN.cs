using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LNegocio
{
	public class ContabilidadLN
	{
		SitemaZContext bd = new SitemaZContext();

		public bool eliminarEntidadEgreso(EntidadEgreso param) {
			try
			{
				var item = bd.EntidadEgreso.SingleOrDefault(x => x.IdEntidadEgreso == param.IdEntidadEgreso);
				item.Borrado = true;
				bd.SaveChanges();
				bd.Dispose();
				return true;
			}catch (Exception)
			{
				return false;
			}
		}

		public bool insertarEditarEntidad(EntidadEgreso param)
		{
			bool result = false;
			try
			{
				if (param.IdEntidadEgreso == 0)
				{
					EntidadEgreso nuevo = new EntidadEgreso();
					nuevo.Nombres = param.Nombres;
					nuevo.ApPaterno = param.ApPaterno;
					nuevo.ApMaterno = param.ApMaterno;
					nuevo.Celular = param.Celular;
					nuevo.Correo = param.Correo;
					nuevo.PagoPeriodico = param.PagoPeriodico;
					nuevo.DiaPagoPeriodico = param.DiaPagoPeriodico;
					nuevo.MontoPagoPeriodico = param.MontoPagoPeriodico;
					nuevo.Borrado = false;
					bd.EntidadEgreso.Add(nuevo);
					bd.SaveChanges();
					bd.Dispose();
				}
				else
				{
					var ent = (from x in bd.EntidadEgreso where x.IdEntidadEgreso == param.IdEntidadEgreso select x).SingleOrDefault();

					ent.Nombres = param.Nombres;
					ent.ApPaterno = param.ApPaterno;
					ent.ApMaterno = param.ApMaterno;
					ent.Celular = param.Celular;
					ent.Correo = param.Correo;
					ent.PagoPeriodico = param.PagoPeriodico;
					ent.DiaPagoPeriodico = param.DiaPagoPeriodico;
					ent.MontoPagoPeriodico = param.MontoPagoPeriodico;
					ent.Borrado = false;
					bd.SaveChanges();
					bd.Dispose();
				}
				result = true;
			}catch(Exception )
			{
				result = false;
			}
			


			return result;
		}

		public List<egresoFront> listarEgresos()
		{
			List<egresoFront> result = new List<egresoFront>();

			result = (from x in bd.Egreso
					  where x.Borrado == false
					  orderby x.IdEgreso descending
					  select (new egresoFront
					  {
						  idEgreso = x.IdEgreso,
						  idEntidadEgreso = x.IdEntidadEgresoNavigation.IdEntidadEgreso,
						  nombresEntEgreso = x.IdEntidadEgresoNavigation.Nombres + " " +
						  x.IdEntidadEgresoNavigation.ApPaterno + " " +
						  x.IdEntidadEgresoNavigation.ApMaterno,
						  monto = x.Monto.Value,
						  descripcion = x.Descripcion,
						  fecha = x.Fecha,
						  evidencia = x.EvidenciaEgreso.B64,
						  celular = x.IdEntidadEgresoNavigation.Celular
					  })).ToList();
			return result;
		}

		public bool eliminarEgreso(egresoFront param)
		{
			try
			{
				var item = bd.Egreso.SingleOrDefault(x => x.IdEgreso == param.idEgreso);
				item.Borrado = true;
				bd.SaveChanges();
				bd.Dispose();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		

		public string obtenerEvidencia(egresoFront param)
		{

			var result = (from x in bd.EvidenciaEgreso
						  where x.IdEgreso == param.idEgreso
						  select x.B64).SingleOrDefault();

			return result;
		}

		public List<EntidadEgreso> listarEntidadEgreso()
		{

			var result = (from x in bd.EntidadEgreso
						  where x.Borrado == false
						  orderby x.DiaPagoPeriodico descending
						  select x).ToList();



			return result;
		}

		public bool insertarEgreso(InsertarEgreso param)
		{
			try {
				if(param.idEgreso == 0)
				{
					Egreso eg = new Egreso();
					eg.IdUsuario = param.idUsuario;
					eg.IdEntidadEgreso = param.idEntidadEgreso;
					eg.Fecha = param.fecha;
					eg.Monto = param.monto;
					eg.Descripcion = param.descripcion;
					eg.Borrado = false;
					bd.Egreso.Add(eg);
				}
				else
				{
					var ent = (from x in bd.Egreso where x.IdEgreso == param.idEgreso select x).SingleOrDefault();

					ent.IdUsuario = param.idUsuario;
					ent.IdEntidadEgreso = param.idEntidadEgreso;
					ent.Fecha = param.fecha;
					ent.Monto = param.monto;
					ent.Descripcion = param.descripcion;
					bd.Update(ent);
				}
				
				bd.SaveChanges();
				bd.Dispose();

				return true;
			}
			catch (Exception) { return false; }
		}
	}
	public class paramsBusquedaEgresos
	{
		public int numPag { get; set; }
		public int tamPag { get; set; }
		public int idEntEgre { get; set; }
		public Nullable<int> montoDesde { get; set; }
		public Nullable<int> montoHasta { get; set; }

		public DateTime fDesde { get; set; }
		public DateTime fHasta { get; set; }
	}


	public class InsertarEgreso
	{
		public int idEgreso { get; set; }
		public int idEntidadEgreso { get; set; }
		public int idUsuario { get; set; }
		public string fecha { get; set; }
		public decimal monto { get; set; }
		public string descripcion { get; set; }

	}

	public class egresoFront
	{
		public int idEgreso { get; set; }
		public int idEntidadEgreso { get; set; }
		public string nombresEntEgreso { get; set; }
		public decimal monto { get; set; }
		public string descripcion { get; set; }
		public string fecha { get; set; }
		public string evidencia { get; set; }
		public string celular { get; set; }
	}

	public class resultEgresosContar
	{
		public int totalRegitros { get; set; }
		public decimal TotalMonto { get; set; }
	}
}

