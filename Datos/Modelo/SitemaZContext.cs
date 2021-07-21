using System;
using APIZ.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Datos.Modelo
{
    public partial class SitemaZContext : DbContext
    {
        public SitemaZContext()
        {
        }

        public SitemaZContext(DbContextOptions<SitemaZContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public virtual DbSet<CompraProducto> CompraProducto { get; set; }
        public virtual DbSet<Consumo> Consumo { get; set; }
        public virtual DbSet<DetalleConsumo> DetalleConsumo { get; set; }
        public virtual DbSet<Egreso> Egreso { get; set; }
        public virtual DbSet<EntidadEgreso> EntidadEgreso { get; set; }
        public virtual DbSet<EvidenciaEgreso> EvidenciaEgreso { get; set; }
        public virtual DbSet<Ingresos> Ingresos { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuPerfil> MenuPerfil { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Sede> Sede { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new conexion().getConexionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaProducto);

                entity.Property(e => e.IdCategoriaProducto).HasColumnName("idCategoriaProducto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompraProducto>(entity =>
            {
                entity.HasKey(e => e.IdCompraProducto);

                entity.Property(e => e.IdCompraProducto).HasColumnName("idCompraProducto");

                entity.Property(e => e.Agotado).HasDefaultValueSql("((0))");

                entity.Property(e => e.FechaCompra).HasColumnType("date");

                entity.Property(e => e.IdMoneda).HasColumnName("idMoneda");

                entity.Property(e => e.IdPedido).HasColumnName("idPedido");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.PrecioTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrecioUnidad)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.CompraProducto)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK_CompraProducto_Moneda");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.CompraProducto)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_CompraProducto_Pedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.CompraProducto)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_CompraProducto_Producto");
            });

            modelBuilder.Entity<Consumo>(entity =>
            {
                entity.HasKey(e => e.IdConsumo);

                entity.Property(e => e.IdConsumo).HasColumnName("idConsumo");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdSede).HasColumnName("idSede");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleConsumo>(entity =>
            {
                entity.HasKey(e => e.IdDetalleConsumo);

                entity.Property(e => e.IdDetalleConsumo).HasColumnName("idDetalleConsumo");

                entity.Property(e => e.IdConsumo).HasColumnName("idConsumo");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.HasOne(d => d.IdConsumoNavigation)
                    .WithMany(p => p.DetalleConsumo)
                    .HasForeignKey(d => d.IdConsumo)
                    .HasConstraintName("FK_DetalleConsumo_Consumo");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleConsumo)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_DetalleConsumo_Producto");
            });

            modelBuilder.Entity<Egreso>(entity =>
            {
                entity.HasKey(e => e.IdEgreso);

                entity.Property(e => e.IdEgreso).HasColumnName("idEgreso");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdEntidadEgreso).HasColumnName("idEntidadEgreso");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdEntidadEgresoNavigation)
                    .WithMany(p => p.Egreso)
                    .HasForeignKey(d => d.IdEntidadEgreso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Egreso_EntidadEgreso");
            });

            modelBuilder.Entity<EntidadEgreso>(entity =>
            {
                entity.HasKey(e => e.IdEntidadEgreso);

                entity.Property(e => e.IdEntidadEgreso).HasColumnName("idEntidadEgreso");

                entity.Property(e => e.ApMaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApPaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MontoPagoPeriodico).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EvidenciaEgreso>(entity =>
            {
                entity.HasKey(e => e.IdEgreso);

                entity.Property(e => e.IdEgreso)
                    .HasColumnName("idEgreso")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.B64).IsUnicode(false);

                entity.HasOne(d => d.IdEgresoNavigation)
                    .WithOne(p => p.EvidenciaEgreso)
                    .HasForeignKey<EvidenciaEgreso>(d => d.IdEgreso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvidenciaEgreso_Egreso");
            });

            modelBuilder.Entity<Ingresos>(entity =>
            {
                entity.HasKey(e => e.IdIngresos);

                entity.Property(e => e.IdIngresos).HasColumnName("idIngresos");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu);

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.NomMenu)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuPerfil>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK_MenuPerfil_Menu");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_MenuPerfil_Perfil");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.HasKey(e => e.IdMoneda);

                entity.Property(e => e.IdMoneda).HasColumnName("idMoneda");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Simbolo)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.Property(e => e.IdPedido).HasColumnName("idPedido");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdSede).HasColumnName("idSede");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil);

                entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SesionUnica).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.CostoCompras)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CostoGasto)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdCategoriaProducto).HasColumnName("idCategoriaProducto");

                entity.Property(e => e.IdMoneda).HasColumnName("idMoneda");

                entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Stock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UltimoPrecio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdCategoriaProductoNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdCategoriaProducto)
                    .HasConstraintName("FK_Producto_CategoriaProducto");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK_Producto_Moneda");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_Producto_UnidadMedida");
            });

            modelBuilder.Entity<Sede>(entity =>
            {
                entity.HasKey(e => e.IdSede)
                    .HasName("PK_RazonSocial");

                entity.Property(e => e.IdSede).HasColumnName("idSede");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida);

                entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.ApMaterno)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ApPaterno)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");

                entity.Property(e => e.IdSede).HasColumnName("idSede");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioSistema)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_Usuario_Perfil");

                entity.HasOne(d => d.IdSedeNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdSede)
                    .HasConstraintName("FK_Usuario_RazonSocial");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
