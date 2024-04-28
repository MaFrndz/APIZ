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

				bd.EntidadEgreso.Remove(item);
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
					  orderby x.Fecha descending, x.IdEgreso descending
					  select (new egresoFront
					  {
						  idEgreso = x.IdEgreso,
						  nombreEntEgreso = x.IdEntidadEgresoNavigation.Nombres,
						  apPaterno = x.IdEntidadEgresoNavigation.ApPaterno,
						  apMaterno = x.IdEntidadEgresoNavigation.ApMaterno,
						  monto = x.Monto.Value,
						  descripcion = x.Descripcion,
						  fecha = x.Fecha.Value,
						  evidencia = x.EvidenciaEgreso.B64,
						  celular = x.IdEntidadEgresoNavigation.Celular
					  })).ToList();
			return result;
		}

		public resultEgresosContar listarEgresosContar(paramsBusquedaEgresos param)
		{
			resultEgresosContar result = new resultEgresosContar();

			if (param.montoHasta == null) { param.montoHasta = 9999999; }
			if (param.montoDesde == null) { param.montoDesde = 0; }

			if (param.idEntEgre != 0)
			{
				result.totalRegitros = (from x in bd.Egreso
										where x.Monto >= param.montoDesde && x.Monto <= param.montoHasta && x.IdEntidadEgreso == param.idEntEgre
										&& x.Fecha.Value >= param.fDesde && x.Fecha.Value <= param.fHasta
										select x
						  ).Count();

				result.TotalMonto += (from x in bd.Egreso
									  where x.Monto >= param.montoDesde && x.Monto <= param.montoHasta && x.IdEntidadEgreso == param.idEntEgre
									  && x.Fecha.Value >= param.fDesde && x.Fecha.Value <= param.fHasta
									  select x
						  ).Sum(x => x.Monto.Value);
			}
			else
			{
				result.totalRegitros = (from x in bd.Egreso
										where x.Monto >= param.montoDesde && x.Monto <= param.montoHasta
										&& x.Fecha.Value >= param.fDesde && x.Fecha.Value <= param.fHasta
										orderby x.Fecha descending
										select x).Count();

				result.TotalMonto = (from x in bd.Egreso
									 where x.Monto >= param.montoDesde && x.Monto <= param.montoHasta
									 && x.Fecha.Value >= param.fDesde && x.Fecha.Value <= param.fHasta
									 select x
						 ).Sum(x => x.Monto.Value);
			}

			return result;
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
						  orderby x.DiaPagoPeriodico descending
						  select x).ToList();



			return result;
		}

		public bool insertarEgreso(insertarEgreso param)
		{
			bool result = false;
			Egreso eg = new Egreso();
			eg.IdUsuario = param.idUsuario;
			eg.IdEntidadEgreso = param.idEntidadEgreso;
			eg.Fecha = param.fehca;
			eg.Monto = param.monto;
			eg.Descripcion = param.descripcion;
			bd.Egreso.Add(eg);
			//
			EvidenciaEgreso ev = new EvidenciaEgreso();
			ev.B64 = param.evidenciaB64;
			ev.IdEgreso = eg.IdEgreso;
			bd.Add(ev);
			bd.SaveChanges();
			bd.Dispose();
			return result;
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


	public class insertarEgreso
	{
		public int idEntidadEgreso { get; set; }
		public int idUsuario { get; set; }
		public DateTime fehca { get; set; }
		public decimal monto { get; set; }
		public string descripcion { get; set; }
		public string evidenciaB64 { get; set; }
	}
	public class egresoFront
	{
		public int idEgreso { get; set; }
		public string nombreEntEgreso { get; set; }
		public string apPaterno { get; set; }
		public string apMaterno { get; set; }
		public decimal monto { get; set; }
		public string descripcion { get; set; }
		public DateTime fecha { get; set; }
		public string evidencia { get; set; }
		public string celular { get; set; }
	}

	public class resultEgresosContar
	{
		public int totalRegitros { get; set; }
		public decimal TotalMonto { get; set; }
	}
}

