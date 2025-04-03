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

    public virtual DbSet<Sucurrsal> Sucurrsals { get; set; }

    public virtual DbSet<Usuari> Usuaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=GestióComerç");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comerç>(entity =>
        {
            entity.HasKey(e => e.ComerçId).HasName("PK__Comerç__7BE1AC41FEA970E5");

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
            entity.HasKey(e => e.EncarrecId).HasName("PK__Encarrec__E85CC01F7C84A8E6");

            entity.ToTable("Encarrec");

            entity.Property(e => e.EncarrecId).ValueGeneratedNever();
            entity.Property(e => e.Estat).HasMaxLength(50);
            entity.Property(e => e.PreuTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Sucurrsal).WithMany(p => p.Encarrecs)
                .HasForeignKey(d => d.SucurrsalId)
                .HasConstraintName("FK__Encarrec__Sucurr__4316F928");

            entity.HasOne(d => d.Usu).WithMany(p => p.Encarrecs)
                .HasForeignKey(d => d.UsuId)
                .HasConstraintName("FK__Encarrec__UsuId__440B1D61");
        });

        modelBuilder.Entity<Producte>(entity =>
        {
            entity.HasKey(e => e.CodiDeBarres).HasName("PK__Producte__9EEDE071027C44AF");

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

            entity.HasMany(d => d.Encarrecs).WithMany(p => p.CodiDeBarres)
                .UsingEntity<Dictionary<string, object>>(
                    "ProducteEncarrec",
                    r => r.HasOne<Encarrec>().WithMany()
                        .HasForeignKey("EncarrecId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProducteE__Encar__4BAC3F29"),
                    l => l.HasOne<Producte>().WithMany()
                        .HasForeignKey("CodiDeBarres")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProducteE__CodiD__4AB81AF0"),
                    j =>
                    {
                        j.HasKey("CodiDeBarres", "EncarrecId").HasName("PK__Producte__70682C7000BFF190");
                        j.ToTable("ProducteEncarrec");
                        j.IndexerProperty<string>("CodiDeBarres")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });

            entity.HasMany(d => d.Sucurrsals).WithMany(p => p.CodiDeBarres)
                .UsingEntity<Dictionary<string, object>>(
                    "Stock",
                    r => r.HasOne<Sucurrsal>().WithMany()
                        .HasForeignKey("SucurrsalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Stock__Sucurrsal__47DBAE45"),
                    l => l.HasOne<Producte>().WithMany()
                        .HasForeignKey("CodiDeBarres")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Stock__CodiDeBar__46E78A0C"),
                    j =>
                    {
                        j.HasKey("CodiDeBarres", "SucurrsalId").HasName("PK__Stock__86D861DEA487BEE1");
                        j.ToTable("Stock");
                        j.IndexerProperty<string>("CodiDeBarres")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Sucurrsal>(entity =>
        {
            entity.HasKey(e => e.SucurrsalId).HasName("PK__Sucurrsa__83581AF00CFD3F7E");

            entity.ToTable("Sucurrsal");

            entity.Property(e => e.SucurrsalId).ValueGeneratedNever();
            entity.Property(e => e.Direccio)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Comerç).WithMany(p => p.Sucurrsals)
                .HasForeignKey(d => d.ComerçId)
                .HasConstraintName("FK__Sucurrsal__Comer__398D8EEE");
        });

        modelBuilder.Entity<Usuari>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK__Usuari__68526383A0BB3996");

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

            entity.HasOne(d => d.Sucurrsal).WithMany(p => p.Usuaris)
                .HasForeignKey(d => d.SucurrsalId)
                .HasConstraintName("FK__Usuari__Sucurrsa__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
