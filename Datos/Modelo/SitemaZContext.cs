using System;
using System.Configuration;
using APIZ.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.Administration;


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

        public virtual DbSet<CompraProducto> CompraProducto { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuPerfil> MenuPerfil { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<RazonSocial> RazonSocial { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                conexion obj = new conexion();

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=MIGUELPC; Initial Catalog=SitemaZ; User ID=sa;Password=.");
                optionsBuilder.UseSqlServer(obj.getConexionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompraProducto>(entity =>
            {
                entity.HasKey(e => e.IdCompraProducto);

                entity.Property(e => e.IdCompraProducto).HasColumnName("idCompraProducto");

                entity.Property(e => e.IdMoneda).HasColumnName("idMoneda");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.CompraProducto)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK_CompraProducto_Moneda");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.CompraProducto)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_CompraProducto_Producto");
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

                entity.Property(e => e.IdMoneda).HasColumnName("idMoneda");

                entity.Property(e => e.IdRazonSocial).HasColumnName("idRazonSocial");

                entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK_Producto_Moneda");

                entity.HasOne(d => d.IdRazonSocialNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdRazonSocial)
                    .HasConstraintName("FK_Producto_RazonSocial");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_Producto_UnidadMedida");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Producto_Usuario");
            });

            modelBuilder.Entity<RazonSocial>(entity =>
            {
                entity.HasKey(e => e.IdRazonSocial);

                entity.Property(e => e.IdRazonSocial).HasColumnName("idRazonSocial");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ruc)
                    .HasColumnName("RUC")
                    .HasMaxLength(100)
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

                entity.Property(e => e.IdRazonSocial).HasColumnName("idRazonSocial");

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

                entity.HasOne(d => d.IdRazonSocialNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRazonSocial)
                    .HasConstraintName("FK_Usuario_RazonSocial");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
