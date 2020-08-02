using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LNegocio
{
    public class ProductoLN
    {
        // general 
        public List<unidaMedida> ListarUnidadMedida()
        {
            //List<unidaMedida> result = new List<unidaMedida>();
            SitemaZContext bd = new SitemaZContext();
            var result = (from x in bd.UnidadMedida 
                       select (new unidaMedida { idUnidadMedida = x.IdUnidadMedida, UnidadMedida = x.Nombre })
                       ).ToList();
            return result;
        }

        // general 
        public List<moneda> listarTipoMoneda()
        {
            SitemaZContext bd = new SitemaZContext();
            var result = (from x in bd.Moneda
                          select (new moneda {codigo= x.Codigo, nombre=x.Nombre, simbolo = x.Codigo, idMoneda =x.IdMoneda })
                          ).ToList();
            return result;
        }

        public List<producto> listarProducto()
        {
            SitemaZContext bd = new SitemaZContext();
            var result = (from x in bd.Producto
                          select (new producto {idProducto = x.IdProducto, nomProducto = x.Nombre, 
                                                precio = x.Precio.Value, stock = x.Stock.Value, 
                                                simboloMoneda = x.IdMonedaNavigation.Simbolo
                                                })
                          ).ToList();
            return result;
        }

        public bool insertarProducto(producto param)
        {
            bool insert = false;
            SitemaZContext bd = new SitemaZContext();
            Producto nuevo = new Producto();
            nuevo.IdUnidadMedida = param.IdUnidadMedida;
            nuevo.IdUsuario = param.IdUsuario;
            nuevo.IdRazonSocial = param.IdRazonSocial;
            nuevo.Nombre = param.nomProducto;
            nuevo.Stock = param.stock;
            nuevo.Precio = param.precio;
            try { 
                bd.Producto.Add(nuevo);
                bd.SaveChanges();
                insert = true;
            }
            catch(Exception e) { throw e; }
            return insert;
        }

        public bool insertarCompra(producto param)
        {
            SitemaZContext bd = new SitemaZContext();
            bool actualizar = false;

            // actualziar producto
            var producto = (from x in bd.Producto
                            where x.IdProducto  == param.idProducto
                            select x
                            ).SingleOrDefault();
            producto.Stock = producto.Stock + param.stock;
            producto.Precio = param.precio;
            producto.IdMoneda = param.idMoneda;
            //bd.SaveChanges();

            // insertar en compra
            CompraProducto compra = new CompraProducto();
            compra.IdProducto = param.idProducto;
            compra.Precio = param.precio;
            compra.Cantidad = param.stock;
            compra.IdMoneda = param.idMoneda;
            bd.CompraProducto.Add(compra);

            try {
                bd.SaveChanges();
                actualizar = true;
            }
            catch (Exception e) { actualizar = false; }
            

            return actualizar;
        }
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
        public int IdUsuario { get; set; }
        public int IdRazonSocial { get; set; }
        public int idMoneda { get; set; }
        public int idProducto { get; set; }

        public string simboloMoneda { get; set; }

        public string nomProducto { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
    }
    public class unidaMedida
    {
        public int idUnidadMedida { get; set; }
        public string UnidadMedida { get; set; }
    }
}
