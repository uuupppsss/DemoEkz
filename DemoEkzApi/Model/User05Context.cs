using System;
using System.Collections.Generic;
using DemoEkzApi.Model;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DemoEkzApi;

public partial class User05Context : DbContext
{
    public User05Context()
    {
    }

    public User05Context(DbContextOptions<User05Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cleaning> Cleanings { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<GuestsRegister> GuestsRegisters { get; set; }

    public virtual DbSet<Otchet> Otchets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<НомернойФонд> НомернойФондs { get; set; }

    public virtual DbSet<Роли> Ролиs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.35;user=user05;password=44084;database=user05", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Cleaning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Cleaning");

            entity.HasIndex(e => e.RoomId, "FK_Cleaning_Номерной фонд_Номер");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cleaner)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.RoomId)
                .HasColumnType("int(11)")
                .HasColumnName("room_id");

            entity.HasOne(d => d.Room).WithMany(p => p.Cleanings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cleaning_Номерной фонд_Номер");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Guest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FirstnameName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.SecondName).HasMaxLength(50);
        });

        modelBuilder.Entity<GuestsRegister>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("GuestsRegister");

            entity.HasIndex(e => e.GuestId, "FK_GuestsRegister_Guest_id");

            entity.HasIndex(e => e.RoomId, "FK_GuestsRegister_Номерной фонд_Номер");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.GuestId)
                .HasColumnType("int(11)")
                .HasColumnName("guest_id");
            entity.Property(e => e.LeavingDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasPrecision(19, 2);
            entity.Property(e => e.Receipt)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.RoomId)
                .HasColumnType("int(11)")
                .HasColumnName("room_id");

            entity.HasOne(d => d.Guest).WithMany(p => p.GuestsRegisters)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GuestsRegister_Guest_id");

            entity.HasOne(d => d.Room).WithMany(p => p.GuestsRegisters)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GuestsRegister_Номерной фонд_Номер");
        });

        modelBuilder.Entity<Otchet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("otchet");

            entity.HasIndex(e => e.Номер, "FK_otchet_Номерной фонд_Номер");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Price).HasPrecision(19, 2);
            entity.Property(e => e.Номер).HasColumnType("int(11)");
            entity.Property(e => e.Статус)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.НомерNavigation).WithMany(p => p.Otchets)
                .HasForeignKey(d => d.Номер)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.RoleId, "FK_User_Роли_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Роли_id");
        });

        modelBuilder.Entity<НомернойФонд>(entity =>
        {
            entity.HasKey(e => e.Номер).HasName("PRIMARY");

            entity.ToTable("Номерной фонд");

            entity.Property(e => e.Номер)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Категория).HasMaxLength(100);
            entity.Property(e => e.Этаж).HasMaxLength(50);
        });

        modelBuilder.Entity<Роли>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Роли");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Название)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
