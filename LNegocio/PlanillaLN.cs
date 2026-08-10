using Datos.Modelo;
using LNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
    public class PlanillaLN
    {
        SitemaZContext bd = new SitemaZContext();
        public List<PlanillaDto> obtenerPlanilla(string periodo)
        {
            int month = 0, year = 0;
            bool filterByPeriod = false;
            if (!string.IsNullOrEmpty(periodo))
            {
                var parts = periodo.Split(new[] { '-', '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0], out month) && int.TryParse(parts[1], out year))
                {
                    filterByPeriod = true;
                }
            }

            if (filterByPeriod)
            {
                var result = (from x in bd.Planilla
                              where x.Borrado == false
                              select (new PlanillaDto
                              {
                                  IdPlanilla = x.IdPlanilla,
                                  Nombres = x.Nombres,
                                  Apellidos = x.Apellidos,
                                  Cargo = x.Cargo,
                                  Dni = x.Dni,
                                  CelularCuenta = x.Celularcuenta,
                                  DiasTrabajados = (bd.AsistenciaPlanilla
                                      .Where(a => a.IdPlanilla == x.IdPlanilla && a.Borrado == false && a.Fecha.HasValue && a.Fecha.Value.Month == month && a.Fecha.Value.Year == year)
                                      .Sum(a => (decimal?)a.Asistencia) ?? 0m),
                                  TarifaDia = x.TarifaDia ?? 0m,
                                  MontoPago = ((bd.AsistenciaPlanilla
                                      .Where(a => a.IdPlanilla == x.IdPlanilla && a.Borrado == false && a.Fecha.HasValue && a.Fecha.Value.Month == month && a.Fecha.Value.Year == year)
                                      .Sum(a => (decimal?)a.Asistencia) ?? 0m) * (x.TarifaDia ?? 0m)),
                                  grupoPlanilla = x.IdGrupoPlanillaNavigation != null ? new GrupoPlanillaDto(x.IdGrupoPlanillaNavigation.IdGrupoPlanilla, x.IdGrupoPlanillaNavigation.Nombre) : null
                              })).ToList();
                return result;
            }

            var resultNoFilter = (from x in bd.Planilla
                          where x.Borrado == false
                          select (new PlanillaDto
                          {
                              IdPlanilla = x.IdPlanilla,
                              Nombres = x.Nombres,
                              Apellidos = x.Apellidos,
                              Cargo = x.Cargo,
                              Dni = x.Dni,
                              CelularCuenta = x.Celularcuenta,
                              DiasTrabajados = (bd.AsistenciaPlanilla
                                  .Where(a => a.IdPlanilla == x.IdPlanilla && a.Borrado == false)
                                  .Sum(a => (decimal?)a.Asistencia) ?? 0m),
                              TarifaDia = x.TarifaDia ?? 0m,
                              MontoPago = ((bd.AsistenciaPlanilla
                                  .Where(a => a.IdPlanilla == x.IdPlanilla && a.Borrado == false)
                                  .Sum(a => (decimal?)a.Asistencia) ?? 0m) * (x.TarifaDia ?? 0m)),
                              grupoPlanilla = x.IdGrupoPlanillaNavigation != null ? new GrupoPlanillaDto(x.IdGrupoPlanillaNavigation.IdGrupoPlanilla, x.IdGrupoPlanillaNavigation.Nombre) : null
                          })).ToList();
            return resultNoFilter;
        }

        public int insertarPlanilla(PlanillaDto param)
        {
            try
            {
                Planilla nueva = new Planilla
                {
                    Nombres = param.Nombres,
                    Apellidos = param.Apellidos,
                    Cargo = param.Cargo,
                    Dni = param.Dni,
                    Celularcuenta = param.CelularCuenta,
                    DiasTrabajados = param.DiasTrabajados,
                    TarifaDia = param.TarifaDia,
                    Borrado = false
                };
                bd.Planilla.Add(nueva);
                bd.SaveChanges();
                return nueva.IdPlanilla;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool actualizarPlanilla(int idPlanilla, PlanillaDto param)
        {
            try
            {
                var planilla = bd.Planilla.Find(idPlanilla);
                if (planilla == null) return false;

                planilla.Nombres = param.Nombres;
                planilla.Apellidos = param.Apellidos;
                planilla.Cargo = param.Cargo;
                planilla.Dni = param.Dni;
                planilla.Celularcuenta = param.CelularCuenta;
                planilla.DiasTrabajados = param.DiasTrabajados;
                planilla.TarifaDia = param.TarifaDia;
                planilla.IdGrupoPlanilla = param.grupoPlanilla.idGrupoPlanilla;

                bd.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GrupoPlanillaDto> obtenerGrupoPlanilla()
        {
            var result = (from x in bd.GrupoPlanilla
                          select (new GrupoPlanillaDto
                          {
                              idGrupoPlanilla = x.IdGrupoPlanilla,
                              nombre = x.Nombre
                          })).ToList();
            return result;
        }

        public bool eliminarPlanilla(int idPlanilla)
        {
            try
            {
                var planilla = bd.Planilla.Find(idPlanilla);
                if (planilla == null) return false;

                planilla.Borrado = true;
                bd.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool guardarAsistencias(List<AsistenciaPlanillaDto> asistencias)
        {
            try
            {
                foreach (var asistencia in asistencias)
                {
                    // Verificar si existe un registro con el mismo IdPlanilla y Fecha
                    var registroExistente = bd.AsistenciaPlanilla.FirstOrDefault(x =>
                        x.IdPlanilla == asistencia.IdPlanilla &&
                        x.Fecha == asistencia.Fecha &&
                        x.Borrado == false);

                    if (registroExistente != null)
                    {
                        // Actualizar el registro existente
                        registroExistente.Asistencia = asistencia.Asistencia;
                    }
                    else
                    {
                        // Crear un nuevo registro
                        AsistenciaPlanilla nueva = new AsistenciaPlanilla
                        {
                            IdPlanilla = asistencia.IdPlanilla,
                            Fecha = asistencia.Fecha,
                            Asistencia = asistencia.Asistencia,
                            Borrado = false
                        };
                        bd.AsistenciaPlanilla.Add(nueva);
                    }
                }
                bd.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AsistenciaPlanillaDto> obtenerAsistenciasPorFecha(DateTime fecha)
        {
            try
            {
                var result = (from x in bd.AsistenciaPlanilla
                              where x.Fecha == fecha && x.Borrado == false
                              select (new AsistenciaPlanillaDto
                              {
                                  IdAsistenciaPlanilla = x.IdAsistenciaPlanilla,
                                  IdPlanilla = x.IdPlanilla,
                                  Fecha = x.Fecha,
                                  Asistencia = x.Asistencia,
                                  Borrado = x.Borrado
                              })).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AsistenciaPlanillaDto> obtenerAsistenciasPorIdPlanilla(int idPlanilla)
        {
            try
            {
                var result = (from x in bd.AsistenciaPlanilla
                              where x.IdPlanilla == idPlanilla && x.Borrado == false
                              select (new AsistenciaPlanillaDto
                              {
                                  IdAsistenciaPlanilla = x.IdAsistenciaPlanilla,
                                  IdPlanilla = x.IdPlanilla,
                                  Fecha = x.Fecha,
                                  Asistencia = x.Asistencia,
                                  Borrado = x.Borrado
                              })).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
