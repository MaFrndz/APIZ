﻿using Datos.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
	public class UsuarioLN
	{
		SitemaZContext bd = new SitemaZContext();

		public List<usuaio> listaUsuarios()
		{
			var result = (from x in bd.Usuario
						  where x.Borrado != true
						  select (new usuaio
						  {
							  idUsuario = x.IdUsuario,
							  idPerfil = x.IdPerfil.Value,
							  UsuarioSistema = x.UsuarioSistema,
							  Nombres = x.Nombres,
							  ApPaterno = x.ApPaterno,
							  ApMaterno = x.ApMaterno,
							  Correo = x.Correo,
							  nPerfil = x.IdPerfilNavigation.Nombre,
							  idSede = x.IdSede.Value,
							  nSede = x.IdSedeNavigation.Nombre
						  })
						  ).ToList();

			return result;
		}

		public List<perfil> lsitarPerfil()
		{
			var result = (from x in bd.Perfil
						  select (new perfil { idPerfil = x.IdPerfil, Nombre = x.Nombre })).ToList();

			return result;
		}

		public bool insertarEditarUsuario(usuaio param)
		{
			bool result = false;
			try
			{
				if (param.idUsuario != 0)
				{
					var usuario = (from x in bd.Usuario where x.IdUsuario == param.idUsuario select x).SingleOrDefault();
					usuario.Nombres = param.Nombres;
					usuario.ApPaterno = param.ApPaterno;
					usuario.ApMaterno = param.ApMaterno;
					usuario.Correo = param.Correo;
					usuario.UsuarioSistema = param.UsuarioSistema;
					if (param.ContraseniaAnt != null)
					{
						if (usuario.Contrasenia == Funciones.codificarB64(param.ContraseniaAnt))
						{
							usuario.Contrasenia = Funciones.codificarB64(param.ContraseniaNueva);
							result = true;
						}
					}

					usuario.IdPerfil = param.idPerfil;
					usuario.IdSede = param.idSede;
					bd.SaveChanges();
					bd.Dispose();
				}
				else
				{
					Usuario nuevo = new Usuario();
					nuevo.ApMaterno = param.ApMaterno;
					nuevo.ApPaterno = param.ApPaterno;
					nuevo.Nombres = param.Nombres;
					nuevo.IdSede = param.idSede;
					nuevo.IdPerfil = param.idPerfil;
					nuevo.UsuarioSistema = param.UsuarioSistema;

					if (param.ContraseniaNueva != null)
					{
						nuevo.Contrasenia = Funciones.codificarB64(param.ContraseniaNueva);
						result = true;
					}

					bd.Usuario.Add(nuevo);
					bd.SaveChanges(); bd.Dispose();
				}
			}
			catch (Exception e) { result = false; throw e; }
			return result;
		}
		public List<sede> listarSede()
		{
			var result = (from x in bd.Sede
						  select (new sede { idSede = x.IdSede, Nombre = x.Nombre, Detalle = x.Detalle })).ToList();
			return result;
		}

		public List<MenuPerfil> listMenuPerfil(int idPerfil)
		{

			var result = (from mp in bd.MenuPerfil
						  join m in bd.Menu
						  on mp.IdMenu equals m.IdMenu
						  where mp.IdPerfil == idPerfil
						  select (new MenuPerfil
						  {
							  idMenu = m.IdMenu,
							  idPerfil = mp.IdPerfil.Value,
							  nomMenu = m.NomMenu,
							  orden = m.Orden.Value,
							  visible = mp.Visible.Value
						  })).ToList();

			return result;
		}

		public bool actualizarMenuPerfil(List<MenuPerfil> perfil)
		{
			var result = false;

			try
			{
				foreach (var m in perfil)
				{
					var menu = (from e in bd.Menu where e.IdMenu == m.idMenu select e).SingleOrDefault();
					if (menu != null)
					{
						menu.Orden = m.orden;
					}
					bd.Database.ExecuteSqlRaw(
						"UPDATE MenuPerfil SET Visible = {0} WHERE IdMenu = {1} AND IdPerfil = {2}",
						m.visible,
						m.idMenu,
						m.idPerfil
					);
				}
				bd.SaveChanges();
				bd.Dispose();
				result = true;
			}
			catch (Exception e)
			{
				throw e;
			}

			return result;
		}


		public bool eliminarUsuario(int idUsuario) { 
		
			var result = false;
			try
			{
				var usuario = (from x in bd.Usuario 
							   where x.IdUsuario == idUsuario 
							   select x).SingleOrDefault(); 
				if (usuario != null) {
					usuario.Borrado = true;
				}
				bd.SaveChanges();
				result = true;

			}catch (Exception e) { throw e; }


			return result;
		}
	}

	public class MenuPerfil
	{
		public int idMenu { get; set; }
		public int idPerfil { get; set; }
		public string nomMenu { get; set; }
		public int orden { get; set; }
		public bool visible { get; set; }
	}

	public class perfil
	{
		public int idPerfil { get; set; }
		public string Nombre { get; set; }
	}
	public class sede
	{
		public int idSede { get; set; }
		public string Nombre { get; set; }
		public string Detalle { get; set; }
	}

	public class usuaio
	{
		public int idUsuario { get; set; }
		public int idPerfil { get; set; }
		public int idSede { get; set; }

		public string UsuarioSistema { get; set; }

		public string ContraseniaNueva { get; set; }
		public string ContraseniaAnt { get; set; }

		public string Nombres { get; set; }
		public string ApPaterno { get; set; }
		public string ApMaterno { get; set; }
		public string Correo { get; set; }
		public string nPerfil { get; set; }
		public string nSede { get; set; }
	}
}
