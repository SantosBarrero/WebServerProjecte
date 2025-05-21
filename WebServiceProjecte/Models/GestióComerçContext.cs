using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebServiceProjecte.Models;

public partial class GestióComerçContext : DbContext
{
    public GestióComerçContext()
    {
    }

    public GestióComerçContext(DbContextOptions<GestióComerçContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comerç> Comerçs { get; set; }

    public virtual DbSet<Encarrec> Encarrecs { get; set; }

    public virtual DbSet<Producte> Productes { get; set; }

    public virtual DbSet<ProducteEncarrec> ProducteEncarrecs { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Usuari> Usuaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=GestióComerç");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comerç>(entity =>
        {
            entity.HasKey(e => e.ComerçId).HasName("PK__Comerç__7BE1AC4169B7E99E");

            entity.ToTable("Comerç");

            entity.Property(e => e.ComerçId).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nif)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Encarrec>(entity =>
        {
            entity.HasKey(e => e.EncarrecId).HasName("PK__Encarrec__E85CC01FE450EFBA");

            entity.ToTable("Encarrec");

            entity.Property(e => e.EncarrecId).ValueGeneratedNever();
            entity.Property(e => e.Estat).HasMaxLength(50);
            entity.Property(e => e.PreuTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Encarrecs)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK__Encarrec__Sucurs__4316F928");

            entity.HasOne(d => d.Usu).WithMany(p => p.Encarrecs)
                .HasForeignKey(d => d.UsuId)
                .HasConstraintName("FK__Encarrec__UsuId__440B1D61");
        });

        modelBuilder.Entity<Producte>(entity =>
        {
            entity.HasKey(e => e.CodiDeBarres).HasName("PK__Producte__9EEDE07129083725");

            entity.ToTable("Producte");

            entity.Property(e => e.CodiDeBarres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcio)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Imatge)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Preu).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProducteEncarrec>(entity =>
        {
            entity.HasKey(e => new { e.CodiDeBarres, e.EncarrecId }).HasName("PK__Producte__70682C70046F06AB");

            entity.ToTable("ProducteEncarrec");

            entity.Property(e => e.CodiDeBarres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CodiDeBarresNavigation).WithMany(p => p.ProducteEncarrecs)
                .HasForeignKey(d => d.CodiDeBarres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProducteE__CodiD__4AB81AF0");

            entity.HasOne(d => d.Encarrec).WithMany(p => p.ProducteEncarrecs)
                .HasForeignKey(d => d.EncarrecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProducteE__Encar__4BAC3F29");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.CodiDeBarres, e.SucursalId }).HasName("PK__Stock__9826A85F5826F24B");

            entity.ToTable("Stock");

            entity.Property(e => e.CodiDeBarres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock1).HasColumnName("stock");

            entity.HasOne(d => d.CodiDeBarresNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.CodiDeBarres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__CodiDeBar__46E78A0C");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__SucursalI__47DBAE45");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.SucursalId).HasName("PK__Sucursal__6CB482E100F027EB");

            entity.ToTable("Sucursal");

            entity.Property(e => e.SucursalId).ValueGeneratedNever();
            entity.Property(e => e.Direccio)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Comerç).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.ComerçId)
                .HasConstraintName("FK__Sucursal__Comerç__398D8EEE");
        });

        modelBuilder.Entity<Usuari>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK__Usuari__68526383FF728BCA");

            entity.ToTable("Usuari");

            entity.Property(e => e.UsuId).ValueGeneratedNever();
            entity.Property(e => e.Contrasenya)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correu)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomUsuari)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Comerç).WithMany(p => p.Usuaris)
                .HasForeignKey(d => d.ComerçId)
                .HasConstraintName("FK__Usuari__ComerçId__3D5E1FD2");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Usuaris)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK__Usuari__Sucursal__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
