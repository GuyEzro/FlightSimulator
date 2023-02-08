using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using FlightController.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Context;

public partial class FlightsContext : DbContext
{
    public FlightsContext()
    {
    }

    public FlightsContext(DbContextOptions<FlightsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=Flights;Persist Security Info=False;User ID=sa;Password=guyEzro@20;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PLANE");

            entity.ToTable("plane");

            entity.Property(e => e.Id).HasMaxLength(255).IsUnicode(false).HasColumnName("ID");
            entity.Property(e => e.Brand).HasMaxLength(255).IsUnicode(false).HasColumnName("brand");
            entity.Property(e => e.FlightCode).HasMaxLength(255).IsUnicode(false).HasColumnName("flight_code");
            entity.Property(e => e.Model).HasMaxLength(255).IsUnicode(false).HasColumnName("model");
            entity.Property(e => e.PassengerCount).HasColumnName("passenger_count");
            entity.Property(e => e.Speed).HasColumnName("speed");
            entity.Property(e => e.Leg).HasColumnName("Leg");
            entity.Property(e => e.T1Time).HasColumnType("datetime").HasColumnName("T1_Time");
            entity.Property(e => e.T2Time).HasColumnType("datetime").HasColumnName("T2_Time");
            entity.Property(e => e.T3Time).HasColumnType("datetime").HasColumnName("T3_Time");
            entity.Property(e => e.T4Time).HasColumnType("datetime").HasColumnName("T4_Time");
            entity.Property(e => e.T5Time).HasColumnType("datetime").HasColumnName("T5_Time");
            entity.Property(e => e.T6Time).HasColumnType("datetime").HasColumnName("T6_Time");
            entity.Property(e => e.T7Time).HasColumnType("datetime").HasColumnName("T7_Time");
            entity.Property(e => e.T8Time).HasColumnType("datetime").HasColumnName("T8_Time");
            entity.Property(e => e.IsInLanding).HasColumnName("IsInLanding");
            entity.Property(e => e.IsReady).HasColumnName("IsReady");
            entity.Property(e => e.origin).HasMaxLength(255).IsUnicode(false).HasColumnName("origin");
            entity.Property(e => e.destination).HasMaxLength(255).IsUnicode(false).HasColumnName("destination");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
