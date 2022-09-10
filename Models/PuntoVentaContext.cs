using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_Punto_Venta.Models
{
    public partial class PuntoVentaContext : DbContext
    {
        public PuntoVentaContext()
        {
        }

        public PuntoVentaContext(DbContextOptions<PuntoVentaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Caja> Cajas { get; set; } = null!;
        public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; } = null!;
        public virtual DbSet<Kardex> Kardices { get; set; } = null!;
        public virtual DbSet<Parametrosg> Parametrosgs { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=FERNANDOMAB;Initial Catalog=PuntoVenta;Integrated Security=Tru;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caja>(entity =>
            {
                entity.HasKey(e => e.CajId)
                    .IsClustered(false);

                entity.ToTable("CAJA");

                entity.Property(e => e.CajId)
                    .ValueGeneratedNever()
                    .HasColumnName("CAJ_ID");

                entity.Property(e => e.CajBill100Dol).HasColumnName("CAJ_BILL_100_DOL");

                entity.Property(e => e.CajBill10Dol).HasColumnName("CAJ_BILL_10_DOL");

                entity.Property(e => e.CajBill1Dol).HasColumnName("CAJ_BILL_1_DOL");

                entity.Property(e => e.CajBill20Dol).HasColumnName("CAJ_BILL_20_DOL");

                entity.Property(e => e.CajBill2Dol).HasColumnName("CAJ_BILL_2_DOL");

                entity.Property(e => e.CajBill50Dol).HasColumnName("CAJ_BILL_50_DOL");

                entity.Property(e => e.CajBill5Dol).HasColumnName("CAJ_BILL_5_DOL");

                entity.Property(e => e.CajEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CAJ_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.CajFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("CAJ_FECHA");

                entity.Property(e => e.CajMon10C).HasColumnName("CAJ_MON_10_C");

                entity.Property(e => e.CajMon1C).HasColumnName("CAJ_MON_1_C");

                entity.Property(e => e.CajMon1Dol).HasColumnName("CAJ_MON_1_DOL");

                entity.Property(e => e.CajMon25C).HasColumnName("CAJ_MON_25_C");

                entity.Property(e => e.CajMon50C).HasColumnName("CAJ_MON_50_C");

                entity.Property(e => e.CajMon5C).HasColumnName("CAJ_MON_5_C");

                entity.Property(e => e.CajRegIngreso).HasColumnName("CAJ_REG_INGRESO");

                entity.Property(e => e.CajRegSalida).HasColumnName("CAJ_REG_SALIDA");

                entity.Property(e => e.CajTotal).HasColumnName("CAJ_TOTAL");
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => e.CaProId)
                    .IsClustered(false);

                entity.ToTable("CATEGORIA_PRODUCTO");

                entity.HasIndex(e => e.CatId, "FK_CATEGORIA_CA_PRO_FK");

                entity.HasIndex(e => e.ProId, "FK_CATEGORIA_PRODUCTO_FK");

                entity.Property(e => e.CaProId)
                    .ValueGeneratedNever()
                    .HasColumnName("CA_PRO_ID");

                entity.Property(e => e.CaProEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CA_PRO_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.CatId).HasColumnName("CAT_ID");

                entity.Property(e => e.ProId).HasColumnName("PRO_ID");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.CategoriaProductos)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_CATEGORI_FK_CATEGO_CATEGORI");

                entity.HasOne(d => d.Pro)
                    .WithMany(p => p.CategoriaProductos)
                    .HasForeignKey(d => d.ProId)
                    .HasConstraintName("FK_CATEGORI_FK_CATEGO_PRODUCTO");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .IsClustered(false);

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.CatId)
                    .ValueGeneratedNever()
                    .HasColumnName("CAT_ID");

                entity.Property(e => e.CatDescrip)
                    .HasColumnType("text")
                    .HasColumnName("CAT_DESCRIP");

                entity.Property(e => e.CatEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CAT_ESTADO")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CliId)
                    .IsClustered(false);

                entity.ToTable("CLIENTE");

                entity.Property(e => e.CliId)
                    .ValueGeneratedNever()
                    .HasColumnName("CLI_ID");

                entity.Property(e => e.CliDireccion)
                    .HasColumnType("text")
                    .HasColumnName("CLI_DIRECCION");

                entity.Property(e => e.CliEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLI_EMAIL");

                entity.Property(e => e.CliEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CLI_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.CliNumCelular)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CLI_NUM_CELULAR");

                entity.Property(e => e.CliNumeroIden)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CLI_NUMERO_IDEN");

                entity.Property(e => e.CliPApellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLI_P_APELLIDO");

                entity.Property(e => e.CliPNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLI_P_NOMBRE");

                entity.Property(e => e.CliSApellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLI_S_APELLIDO");

                entity.Property(e => e.CliSNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLI_S_NOMBRE");

                entity.Property(e => e.CliTelefono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CLI_TELEFONO");

                entity.Property(e => e.CliTipoIden)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CLI_TIPO_IDEN")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.ComId)
                    .IsClustered(false);

                entity.ToTable("COMPRA");

                entity.Property(e => e.ComId)
                    .ValueGeneratedNever()
                    .HasColumnName("COM_ID");

                entity.Property(e => e.ComFecIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("COM_FEC_INGRESO");

                entity.Property(e => e.ComIva).HasColumnName("COM_IVA");

                entity.Property(e => e.ComSubtotal).HasColumnName("COM_SUBTOTAL");

                entity.Property(e => e.ComTotal).HasColumnName("COM_TOTAL");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.FacId)
                    .IsClustered(false);

                entity.ToTable("FACTURA");

                entity.HasIndex(e => e.CliId, "FK_CLIENTE_FACTURA_FK");

                entity.HasIndex(e => e.UsuId, "FK_USUARIO_FACTURA_FK");

                entity.Property(e => e.FacId)
                    .ValueGeneratedNever()
                    .HasColumnName("FAC_ID");

                entity.Property(e => e.CliId).HasColumnName("CLI_ID");

                entity.Property(e => e.FacDescuen).HasColumnName("FAC_DESCUEN");

                entity.Property(e => e.FacEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FAC_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.FacFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FAC_FECHA");

                entity.Property(e => e.FacIva).HasColumnName("FAC_IVA");

                entity.Property(e => e.FacSubtotal).HasColumnName("FAC_SUBTOTAL");

                entity.Property(e => e.FacTotal).HasColumnName("FAC_TOTAL");

                entity.Property(e => e.UsuId).HasColumnName("USU_ID");

                entity.HasOne(d => d.Cli)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CliId)
                    .HasConstraintName("FK_FACTURA_FK_CLIENT_CLIENTE");

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.UsuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA_FK_USUARI_USUARIO");
            });

            modelBuilder.Entity<FacturaDetalle>(entity =>
            {
                entity.HasKey(e => e.FadId)
                    .IsClustered(false);

                entity.ToTable("FACTURA_DETALLE");

                entity.HasIndex(e => e.FacId, "FK_DETALLEFACTURA_FACTURA_FK");

                entity.Property(e => e.FadId)
                    .ValueGeneratedNever()
                    .HasColumnName("FAD_ID");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.FadCantidad).HasColumnName("FAD_CANTIDAD");

                entity.Property(e => e.FadDescuento).HasColumnName("FAD_DESCUENTO");

                entity.Property(e => e.FadEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FAD_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.FadFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FAD_FECHA");

                entity.Property(e => e.FadIva).HasColumnName("FAD_IVA");

                entity.Property(e => e.FadNumeracion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FAD_NUMERACION");

                entity.Property(e => e.FadPrecioUnit).HasColumnName("FAD_PRECIO_UNIT");

                entity.Property(e => e.FadSubtotal).HasColumnName("FAD_SUBTOTAL");

                entity.Property(e => e.FadTotal).HasColumnName("FAD_TOTAL");

                entity.HasOne(d => d.Fac)
                    .WithMany(p => p.FacturaDetalles)
                    .HasForeignKey(d => d.FacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA__FK_DETALL_FACTURA");
            });

            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.HasKey(e => e.KarId)
                    .IsClustered(false);

                entity.ToTable("KARDEX");

                entity.HasIndex(e => e.ComId, "FK_COMPRA_KARDEX_FK");

                entity.HasIndex(e => e.FacId, "FK_FACTURA_KARDEX_FK");

                entity.HasIndex(e => e.ProId, "FK_PRODUCTO_KARDEX_FK");

                entity.Property(e => e.KarId)
                    .ValueGeneratedNever()
                    .HasColumnName("KAR_ID");

                entity.Property(e => e.ComId).HasColumnName("COM_ID");

                entity.Property(e => e.FacId).HasColumnName("FAC_ID");

                entity.Property(e => e.KarBalCantidad).HasColumnName("KAR_BAL_CANTIDAD");

                entity.Property(e => e.KarBalPrecio).HasColumnName("KAR_BAL_PRECIO");

                entity.Property(e => e.KarBalPrecioTotal).HasColumnName("KAR_BAL_PRECIO_TOTAL");

                entity.Property(e => e.KarDetalle)
                    .HasColumnType("text")
                    .HasColumnName("KAR_DETALLE");

                entity.Property(e => e.KarEntCantidad).HasColumnName("KAR_ENT_CANTIDAD");

                entity.Property(e => e.KarEntPreTotal).HasColumnName("KAR_ENT_PRE_TOTAL");

                entity.Property(e => e.KarEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("KAR_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.KarFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("KAR_FECHA");

                entity.Property(e => e.KarSalCantidad).HasColumnName("KAR_SAL_CANTIDAD");

                entity.Property(e => e.KarSalPreTotal).HasColumnName("KAR_SAL_PRE_TOTAL");

                entity.Property(e => e.ProId).HasColumnName("PRO_ID");

                entity.HasOne(d => d.Com)
                    .WithMany(p => p.Kardices)
                    .HasForeignKey(d => d.ComId)
                    .HasConstraintName("FK_KARDEX_FK_COMPRA_COMPRA");

                entity.HasOne(d => d.Fac)
                    .WithMany(p => p.Kardices)
                    .HasForeignKey(d => d.FacId)
                    .HasConstraintName("FK_KARDEX_FK_FACTUR_FACTURA");

                entity.HasOne(d => d.Pro)
                    .WithMany(p => p.Kardices)
                    .HasForeignKey(d => d.ProId)
                    .HasConstraintName("FK_KARDEX_FK_PRODUC_PRODUCTO");
            });

            modelBuilder.Entity<Parametrosg>(entity =>
            {
                entity.HasKey(e => e.ParId)
                    .IsClustered(false);

                entity.ToTable("PARAMETROSG");

                entity.Property(e => e.ParId)
                    .ValueGeneratedNever()
                    .HasColumnName("PAR_ID");

                entity.Property(e => e.ParDescrip)
                    .HasColumnType("text")
                    .HasColumnName("PAR_DESCRIP");

                entity.Property(e => e.ParEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PAR_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.ParValor)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PAR_VALOR");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.PerId)
                    .IsClustered(false);

                entity.ToTable("PERMISOS");

                entity.HasIndex(e => e.RolId, "FK_ROL_PERMISOS_FK");

                entity.Property(e => e.PerId)
                    .ValueGeneratedNever()
                    .HasColumnName("PER_ID");

                entity.Property(e => e.PerEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PER_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.PerPantalla)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PER_PANTALLA");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_PERMISOS_FK_ROL_PE_ROL");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .IsClustered(false);

                entity.ToTable("PRODUCTO");

                entity.HasIndex(e => e.ComId, "FK_PRODUCTO_COMPRA_FK");

                entity.Property(e => e.ProId)
                    .ValueGeneratedNever()
                    .HasColumnName("PRO_ID");

                entity.Property(e => e.ComId).HasColumnName("COM_ID");

                entity.Property(e => e.ProCategoria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRO_CATEGORIA");

                entity.Property(e => e.ProCodBarras)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRO_COD_BARRAS");

                entity.Property(e => e.ProDescuento).HasColumnName("PRO_DESCUENTO");

                entity.Property(e => e.ProDetalle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRO_DETALLE");

                entity.Property(e => e.ProEstIva).HasColumnName("PRO_EST_IVA");

                entity.Property(e => e.ProEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRO_ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.ProMarca)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRO_MARCA");

                entity.Property(e => e.ProNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRO_NOMBRE");

                entity.Property(e => e.ProPrecio).HasColumnName("PRO_PRECIO");

                entity.HasOne(d => d.Com)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.ComId)
                    .HasConstraintName("FK_PRODUCTO_FK_PRODUC_COMPRA");

                entity.HasMany(d => d.Fads)
                    .WithMany(p => p.Pros)
                    .UsingEntity<Dictionary<string, object>>(
                        "FkProductoDetallefactura",
                        l => l.HasOne<FacturaDetalle>().WithMany().HasForeignKey("FadId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FK_PRODU_FK_PRODUC_FACTURA_"),
                        r => r.HasOne<Producto>().WithMany().HasForeignKey("ProId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FK_PRODU_FK_PRODUC_PRODUCTO"),
                        j =>
                        {
                            j.HasKey("ProId", "FadId");

                            j.ToTable("FK_PRODUCTO_DETALLEFACTURA");

                            j.HasIndex(new[] { "FadId" }, "FK_PRODUCTO_DETALLEFACTURA2_FK");

                            j.HasIndex(new[] { "ProId" }, "FK_PRODUCTO_DETALLEFACTURA_FK");

                            j.IndexerProperty<int>("ProId").HasColumnName("PRO_ID");

                            j.IndexerProperty<int>("FadId").HasColumnName("FAD_ID");
                        });
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .IsClustered(false);

                entity.ToTable("ROL");

                entity.Property(e => e.RolId)
                    .ValueGeneratedNever()
                    .HasColumnName("ROL_ID");

                entity.Property(e => e.RolDescrip)
                    .HasColumnType("text")
                    .HasColumnName("ROL_DESCRIP");

                entity.Property(e => e.RolEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ROL_ESTADO")
                    .IsFixedLength();

                entity.HasMany(d => d.Usus)
                    .WithMany(p => p.Rols)
                    .UsingEntity<Dictionary<string, object>>(
                        "FkRolUsuario",
                        l => l.HasOne<Usuario>().WithMany().HasForeignKey("UsuId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FK_ROL_U_FK_ROL_US_USUARIO"),
                        r => r.HasOne<Rol>().WithMany().HasForeignKey("RolId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FK_ROL_U_FK_ROL_US_ROL"),
                        j =>
                        {
                            j.HasKey("RolId", "UsuId");

                            j.ToTable("FK_ROL_USUARIO");

                            j.HasIndex(new[] { "UsuId" }, "FK_ROL_USUARIO2_FK");

                            j.HasIndex(new[] { "RolId" }, "FK_ROL_USUARIO_FK");

                            j.IndexerProperty<int>("RolId").HasColumnName("ROL_ID");

                            j.IndexerProperty<int>("UsuId").HasColumnName("USU_ID");
                        });
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuId)
                    .IsClustered(false);

                entity.ToTable("USUARIO");

                entity.Property(e => e.UsuId)
                    .ValueGeneratedNever()
                    .HasColumnName("USU_ID");

                entity.Property(e => e.UsuContrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_CONTRASENA");

                entity.Property(e => e.UsuDireccion)
                    .HasColumnType("text")
                    .HasColumnName("USU_DIRECCION");

                entity.Property(e => e.UsuEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_EMAIL");

                entity.Property(e => e.UsuEstCivil)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("USU_EST_CIVIL")
                    .IsFixedLength();

                entity.Property(e => e.UsuEstado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USU_ESTADO")
                    .HasDefaultValue("S")
                    .IsFixedLength();

                entity.Property(e => e.UsuFecNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("USU_FEC_NACIMIENTO");

                entity.Property(e => e.UsuNumCargas).HasColumnName("USU_NUM_CARGAS").HasDefaultValue(0);

                entity.Property(e => e.UsuNumCelular)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USU_NUM_CELULAR");

                entity.Property(e => e.UsuNumeroIden)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USU_NUMERO_IDEN");

                entity.Property(e => e.UsuPApellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_P_APELLIDO");

                entity.Property(e => e.UsuPNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_P_NOMBRE");

                entity.Property(e => e.UsuSApellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_S_APELLIDO");

                entity.Property(e => e.UsuSNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_S_NOMBRE");

                entity.Property(e => e.UsuTelefono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USU_TELEFONO");

                entity.Property(e => e.UsuTipoIden)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("USU_TIPO_IDEN")
                    .IsFixedLength();

                entity.Property(e => e.UsuUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USU_USERNAME")
                    .IsFixedLength();

                entity.HasMany(d => d.Cajs)
                    .WithMany(p => p.Usus)
                    .UsingEntity<Dictionary<string, object>>(
                        "FkUsuarioCaja",
                        l => l.HasOne<Caja>().WithMany().HasForeignKey("CajId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FK_USUAR_FK_USUARI_CAJA"),
                        r => r.HasOne<Usuario>().WithMany().HasForeignKey("UsuId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FK_USUAR_FK_USUARI_USUARIO"),
                        j =>
                        {
                            j.HasKey("UsuId", "CajId");

                            j.ToTable("FK_USUARIO_CAJA");

                            j.HasIndex(new[] { "CajId" }, "FK_USUARIO_CAJA2_FK");

                            j.HasIndex(new[] { "UsuId" }, "FK_USUARIO_CAJA_FK");

                            j.IndexerProperty<int>("UsuId").HasColumnName("USU_ID");

                            j.IndexerProperty<int>("CajId").HasColumnName("CAJ_ID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
