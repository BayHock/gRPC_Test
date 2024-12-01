using System;
using System.Collections.Generic;
using System.Configuration;
using GrpcServer.ClassesDb;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer;

public partial class TestTrainsContext : DbContext
{
    public TestTrainsContext()
    {
    }

    public TestTrainsContext(DbContextOptions<TestTrainsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Epc> Epcs { get; set; }

    public virtual DbSet<EpcEvent> EpcEvents { get; set; }

    public virtual DbSet<EventAdd> EventAdds { get; set; }

    public virtual DbSet<EventArrival> EventArrivals { get; set; }

    public virtual DbSet<EventDeparture> EventDepartures { get; set; }

    public virtual DbSet<EventSub> EventSubs { get; set; }

    public virtual DbSet<Park> Parks { get; set; }

    public virtual DbSet<ClassesDb.Path> Paths { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Epc>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Epc");

            entity.Property(e => e.Number)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<EpcEvent>(entity =>
        {
            entity.HasKey(e => e.Time);

            entity.ToTable("EpcEvent");

            entity
                .HasOne(e => e.Path)
                .WithMany(e => e.EpcEvents)
                .HasForeignKey(e => e.IdPath);

            entity
                .HasOne(e => e.Epc)
                .WithOne(e => e.EpcEvent)
                .HasForeignKey<EpcEvent>(e => e.IdEpc);
        });

        modelBuilder.Entity<EventAdd>(entity =>
        {
            entity.HasKey(e => e.Time);

            entity.ToTable("EventAdd");

            entity
                .HasOne(e => e.EpcEvent)
                .WithOne(e => e.EventAdd)
                .HasForeignKey<EventAdd>(e => e.Time);
        });

        modelBuilder.Entity<EventArrival>(entity =>
        {
            entity.HasKey(e => e.Time);
            entity.ToTable("EventArrival");

            entity
                .HasOne(e => e.EventAdd)
                .WithOne(e => e.EventArrival)
                .HasForeignKey<EventArrival>(e => e.Time);

            entity.Property(e => e.TrainIndex)
                .HasMaxLength(17)
                .IsFixedLength();
            entity.Property(e => e.TrainNumber)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<EventDeparture>(entity =>
        {
            entity.HasKey(e => e.Time);
            entity.ToTable("EventDeparture");

            entity
                .HasOne(e => e.EventSub)
                .WithOne(e => e.EventDeparture)
                .HasForeignKey<EventDeparture>(e => e.Time);

            entity.Property(e => e.TrainIndex)
                .HasMaxLength(17)
                .IsFixedLength();
            entity.Property(e => e.TrainNumber)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<EventSub>(entity =>
        {
            entity.HasKey(e => e.Time);
            entity.ToTable("EventSub");

            entity
                .HasOne(e => e.EpcEvent)
                .WithOne(e => e.EventSub)
                .HasForeignKey<EventSub>(e => e.Time);
        });

        modelBuilder.Entity<Park>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Park_pkey");

            entity.ToTable("Park");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AsuNumber)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<ClassesDb.Path>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Path_pkey");

            entity.ToTable("Path");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AsuNumber)
                .HasMaxLength(2)
                .IsFixedLength();

            entity.HasOne(d => d.IdParkNavigation).WithMany(p => p.Paths)
                .HasForeignKey(d => d.IdPark)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Path_Park");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
