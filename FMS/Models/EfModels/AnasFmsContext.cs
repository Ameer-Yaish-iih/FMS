using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FMS.Models.EfModels;

public partial class AnasFmsContext : DbContext
{
    public AnasFmsContext()
    {
    }

    public AnasFmsContext(DbContextOptions<AnasFmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CircleGeofence> CircleGeofences { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Geofence> Geofences { get; set; }

    public virtual DbSet<PolygonGeofence> PolygonGeofences { get; set; }

    public virtual DbSet<RectangleGeofence> RectangleGeofences { get; set; }

    public virtual DbSet<RouteHistory> RouteHistories { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehiclesInformation> VehiclesInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Anas_FMS;Username=postgres;Password=0000");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CircleGeofence>(entity =>
        {
            entity.ToTable("CircleGeofence");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GeofenceId).HasColumnName("GeofenceID");

            entity.HasOne(d => d.Geofence).WithMany(p => p.CircleGeofences)
                .HasForeignKey(d => d.GeofenceId)
                .HasConstraintName("FK");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("Driver_pkey");

            entity.ToTable("Driver");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.DriverName).HasColumnType("character varying");
        });

        modelBuilder.Entity<Geofence>(entity =>
        {
            entity.HasKey(e => e.GeofenceId).HasName("Geofences_pkey");

            entity.Property(e => e.GeofenceId).HasColumnName("GeofenceID");
            entity.Property(e => e.FillColor).HasColumnType("character varying");
            entity.Property(e => e.GeofenceType).HasColumnType("character varying");
            entity.Property(e => e.StrockColor).HasColumnType("character varying");
        });

        modelBuilder.Entity<PolygonGeofence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PolygonGeofence ");

            entity.ToTable("PolygonGeofence");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GeofenceId).HasColumnName("GeofenceID");

            entity.HasOne(d => d.Geofence).WithMany(p => p.PolygonGeofences)
                .HasForeignKey(d => d.GeofenceId)
                .HasConstraintName("FK_PolygonGeofence ");
        });

        modelBuilder.Entity<RectangleGeofence>(entity =>
        {
            entity.ToTable("RectangleGeofence");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GeofenceId).HasColumnName("GeofenceID");

            entity.HasOne(d => d.Geofence).WithMany(p => p.RectangleGeofences)
                .HasForeignKey(d => d.GeofenceId)
                .HasConstraintName("FK_RectangleGeofence ");
        });

        modelBuilder.Entity<RouteHistory>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK");

            entity.ToTable("RouteHistory");

            entity.Property(e => e.VehicleId)
                .ValueGeneratedNever()
                .HasColumnName("VehicleID");
            entity.Property(e => e.RouteHistoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RouteHistoryID");
            entity.Property(e => e.Status).HasColumnType("char");
            entity.Property(e => e.VehicleSpeed).HasColumnType("character varying");

            entity.HasOne(d => d.Vehicle).WithOne(p => p.RouteHistory)
                .HasForeignKey<RouteHistory>(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("Vehicles_pkey");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.VehicleType).HasColumnType("character varying");
        });

        modelBuilder.Entity<VehiclesInformation>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.VehicleMake).HasColumnType("character varying");
            entity.Property(e => e.VehicleModel).HasColumnType("character varying");

            entity.HasOne(d => d.Driver).WithMany(p => p.VehiclesInformations)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK_VehiclesInformations_Driver ");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehiclesInformations)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehiclesInformations_Vehicles ");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
