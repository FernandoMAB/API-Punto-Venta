using API_Punto_Venta.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace API_Punto_Venta.Context
{
    public class DocumentosContext
    {
        public DocumentosContext() { }

        public static void getDocumentos(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Documento>( entity =>
            {
                entity.ToTable("DOCUMENTOS");

                entity.HasKey(e => e.DocId).IsClustered(false);

                entity.Property(e => e.DocId).HasColumnName("DOC_ID").HasColumnType("int").ValueGeneratedOnAdd();

                entity.HasOne(e => e.Cliente).WithMany(e => e.Documentos).HasForeignKey(e => e.DocIdClient).OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.DocIdClient).HasColumnName("DOC_ID_CLIENT");

                entity.HasOne(e => e.Usuario).WithMany(e => e.Documentos).HasForeignKey(e => e.DocIdUploader).OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.DocIdUploader).HasColumnName("DOC_ID_UPLOADER");

                entity.Property(e => e.DocName).HasColumnName("DOC_NAME").HasColumnType("varchar").HasMaxLength(100).IsRequired();

                entity.Property(e => e.DocExtension).HasColumnName("DOC_EXTENSION").HasColumnType("varchar").HasMaxLength(10).IsRequired();

                entity.Property(e => e.DocBase64).HasColumnName("DOC_BASE64").HasColumnType("varchar").IsRequired();

                entity.Property(e => e.DocStatus)
                                .HasMaxLength(1)
                                .HasColumnType("char")
                                .IsUnicode(false)
                                .HasColumnName("DOC_STATUS")
                                .IsFixedLength()
                                .IsRequired();
            });
        }
    }
}
