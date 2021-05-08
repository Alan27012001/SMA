using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SMA.Modelos
{
    public partial class smafacpyaContext : DbContext
    {
        public smafacpyaContext()
        {
        }

        public smafacpyaContext(DbContextOptions<smafacpyaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estatus> Estatus { get; set; }
        public virtual DbSet<Evidencia> Evidencia { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<ModuloPantalla> ModuloPantalla { get; set; }
        public virtual DbSet<Motivo> Motivo { get; set; }
        public virtual DbSet<Pantalla> Pantalla { get; set; }
        public virtual DbSet<PantallaPermiso> PantallaPermiso { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Reporte> Reporte { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolApp> RolApp { get; set; }
        public virtual DbSet<RolModuloPantallaPermiso> RolModuloPantallaPermiso { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioCodigo> UsuarioCodigo { get; set; }
        public virtual DbSet<UsuarioContraseña> UsuarioContraseña { get; set; }
        public virtual DbSet<UsuarioLogin> UsuarioLogin { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=den1.mssql8.gear.host;Database=smafacpya;User=smafacpya;Password=Rg291I~G2_mL;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estatus>(entity =>
            {
                entity.ToTable("Estatus", "Configuracion");

                entity.Property(e => e.Llave)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evidencia>(entity =>
            {
                entity.ToTable("Evidencia", "Reporte");

                entity.Property(e => e.Extension)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdReporteNavigation)
                    .WithMany(p => p.Evidencia)
                    .HasForeignKey(d => d.IdReporte);
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("Modulo", "Seguridad");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModuloPantalla>(entity =>
            {
                entity.HasKey(e => new { e.IdModulo, e.IdPantalla })
                    .HasName("PK_Seguridad.ModuloPantalla");

                entity.ToTable("ModuloPantalla", "Seguridad");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.ModuloPantalla)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuloPantalla_Modulo");
            });

            modelBuilder.Entity<Motivo>(entity =>
            {
                entity.ToTable("Motivo", "Catalogo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Motivo1)
                    .HasColumnName("Motivo")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pantalla>(entity =>
            {
                entity.ToTable("Pantalla", "Seguridad");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PantallaPermiso>(entity =>
            {
                entity.HasKey(e => new { e.IdPantalla, e.IdPermiso });

                entity.ToTable("PantallaPermiso", "Seguridad");

                entity.HasOne(d => d.IdPantallaNavigation)
                    .WithMany(p => p.PantallaPermiso)
                    .HasForeignKey(d => d.IdPantalla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PantallaPermiso_Pantalla");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.PantallaPermiso)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PantallaPermiso_Permiso");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Permiso", "Seguridad");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Llave)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.ToTable("Proyecto", "Catalogo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.ToTable("Reporte", "Reporte");

                entity.Property(e => e.ComentarioAsignacion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ComentarioFinalizacion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ComentarioReporte)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEdicion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaReporte).HasColumnType("date");

                entity.Property(e => e.Folio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstatusReporteNavigation)
                    .WithMany(p => p.Reporte)
                    .HasForeignKey(d => d.IdEstatusReporte);

                entity.HasOne(d => d.IdMotivoNavigation)
                    .WithMany(p => p.Reporte)
                    .HasForeignKey(d => d.IdMotivo);

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Reporte)
                    .HasForeignKey(d => d.IdProyecto);

                entity.HasOne(d => d.IdUsuarioAsignacionNavigation)
                    .WithMany(p => p.Reporte)
                    .HasForeignKey(d => d.IdUsuarioAsignacion);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol", "Seguridad");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEdicion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.RolUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.UsuarioCreacion)
                    .HasConstraintName("FK_Rol_UsuarioCreacion");

                entity.HasOne(d => d.UsuarioEliminacionNavigation)
                    .WithMany(p => p.RolUsuarioEliminacionNavigation)
                    .HasForeignKey(d => d.UsuarioEliminacion)
                    .HasConstraintName("FK_Rol_UsuarioEliminacion");
            });

            modelBuilder.Entity<RolApp>(entity =>
            {
                entity.ToTable("RolApp", "Seguridad");

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolModuloPantallaPermiso>(entity =>
            {
                entity.HasKey(e => new { e.IdRol, e.IdModulo, e.IdPantalla, e.IdPermiso });

                entity.ToTable("RolModuloPantallaPermiso", "Seguridad");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.RolModuloPantallaPermiso)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolModuloPantallaPermiso_Modulo");

                entity.HasOne(d => d.IdPantallaNavigation)
                    .WithMany(p => p.RolModuloPantallaPermiso)
                    .HasForeignKey(d => d.IdPantalla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolModuloPantallaPermiso_Pantalla");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.RolModuloPantallaPermiso)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolModuloPantallaPermiso_Permiso");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolModuloPantallaPermiso)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolModuloPantallaPermiso_Rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario", "Seguridad");

                entity.Property(e => e.ApellidoMaterno)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña).IsRequired();

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEdicion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolAppNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRolApp)
                    .HasConstraintName("FK_Usuario_RolApp");

                entity.HasOne(d => d.UsuarioCreacionNavigation)
                    .WithMany(p => p.InverseUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.UsuarioCreacion)
                    .HasConstraintName("FK_Usuario_UsuarioCreacion");
            });

            modelBuilder.Entity<UsuarioCodigo>(entity =>
            {
                entity.ToTable("UsuarioCodigo", "Seguridad");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaVigencia).HasColumnType("datetime");
            });

            modelBuilder.Entity<UsuarioContraseña>(entity =>
            {
                entity.ToTable("UsuarioContraseña", "Seguridad");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaVigencia).HasColumnType("datetime");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioContraseña)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioContraseña_Usuario");
            });

            modelBuilder.Entity<UsuarioLogin>(entity =>
            {
                entity.ToTable("UsuarioLogin", "Seguridad");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaVigencia).HasColumnType("datetime");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdRol });

                entity.ToTable("UsuarioRol", "Seguridad");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRol_Rol");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRol_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
