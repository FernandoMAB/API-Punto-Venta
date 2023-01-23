using API_Punto_Venta.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Punto_Venta.Context;

public static class EstadisticasContext
{
    public static void GetEstadisticas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.ToTable("ESTADISTICA_VENTAS");

            entity.HasNoKey();

            entity.Property(e => e.EstYear)
                .HasColumnName("EST_ANIO")
                .HasColumnType("int")
                .IsRequired();

            entity.Property(e => e.EstMonth)
                .HasColumnName("EST_MES")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsFixedLength()
                .IsRequired();

            entity.Property(e => e.EstWeek)
                .HasColumnName("EST_SEMANA")
                .HasColumnType("int")
                .IsRequired();

            entity.Property(e => e.EstTotalSold)
                .HasColumnName("EST_TOTAL_VENDIDO")
                .IsRequired(false);
        });
    }
}