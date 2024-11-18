using Microsoft.EntityFrameworkCore;
using Subastas.Domain;

namespace Subastas.Database;

public partial class SubastasContext : DbContext
{
    public SubastasContext()
    {
    }
    public SubastasContext(DbContextOptions<SubastasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<ParticipantesSubasta> ParticipantesSubasta { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Puja> Pujas { get; set; }

    public virtual DbSet<RolPermiso> RolPermisos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subasta> Subastas { get; set; }

    public virtual DbSet<Transaccion> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }
    
    public virtual DbSet<LogEntry> LogEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.IdCuenta);

            entity.ToTable("cuenta", "subastas");

            entity.HasIndex(e => e.IdUsuario, "unique_id_usuario").IsUnique();

            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(26, 2)")
                .HasColumnName("saldo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Cuentum)
                .HasForeignKey<Cuenta>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_cuenta_usuarios");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu);

            entity.ToTable("menus", "subastas");

            entity.Property(e => e.IdMenu).HasColumnName("id_menu");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.IdMenuPadre).HasColumnName("id_menu_padre");
            entity.Property(e => e.NombreMenu)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_menu");

            entity.HasOne(d => d.IdMenuPadreNavigation).WithMany(p => p.InverseIdMenuPadreNavigation)
                .HasForeignKey(d => d.IdMenuPadre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_menus_menus");
        });

        modelBuilder.Entity<ParticipantesSubasta>(entity =>
        {
            entity.HasKey(e => new { e.IdSubasta, e.IdUsuario });

            entity.ToTable("participantes_subasta", "subastas");

            entity.Property(e => e.IdSubasta).HasColumnName("id_subasta");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

            entity.HasOne(d => d.IdSubastaNavigation).WithMany(p => p.ParticipantesSubasta)
                .HasForeignKey(d => d.IdSubasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participantes_subasta_subastas");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ParticipantesSubasta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participantes_subasta_usuarios");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso);

            entity.ToTable("permisos", "subastas");

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.IdMenu).HasColumnName("id_menu");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_permiso");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_permisos_menus");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("productos", "subastas");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion_producto");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.EstaSubastado).HasColumnName("esta_subastado");
            entity.Property(e => e.ImagenProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("imagen_producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
        });

        modelBuilder.Entity<Puja>(entity =>
        {
            entity.HasKey(e => e.IdPuja);

            entity.ToTable("pujas", "subastas");

            entity.Property(e => e.IdPuja).HasColumnName("id_puja");
            entity.Property(e => e.FechaPuja).HasColumnName("fecha_puja");
            entity.Property(e => e.IdSubasta).HasColumnName("id_subasta");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.MontoPuja)
                .HasColumnType("decimal(26, 2)")
                .HasColumnName("monto_puja");

            entity.HasOne(d => d.IdSubastaNavigation).WithMany(p => p.Pujas)
                .HasForeignKey(d => d.IdSubasta)
                .OnDelete(DeleteBehavior.Cascade) // Cambiado a DeleteBehavior.Cascade
                .HasConstraintName("FK_pujas_subastas");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pujas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pujas_usuarios");
        });

        modelBuilder.Entity<RolPermiso>(entity =>
        {
            entity.HasKey(e => new { e.IdRol, e.IdPermiso });

            entity.ToTable("rol_permiso", "subastas");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_rol_permiso_permisos");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_rol_permiso_roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("roles", "subastas");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<Subasta>(entity =>
        {
            entity.HasKey(e => e.IdSubasta);

            entity.ToTable("subastas", "subastas");

            entity.HasIndex(e => e.IdProducto, "unique_id_producto").IsUnique();

            entity.Property(e => e.IdSubasta).HasColumnName("id_subasta");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.FechaSubasta).HasColumnName("fecha_subasta");
            entity.Property(e => e.FechaSubastaFin).HasColumnName("fecha_subasta_fin");
            entity.Property(e => e.Finalizada).HasColumnName("finalizada");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.MontoInicial)
                .HasColumnType("decimal(26, 2)")
                .HasColumnName("monto_inicial");
            entity.Property(e => e.TituloSubasta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo_subasta");

            entity.HasOne(d => d.IdProductoNavigation).WithOne(p => p.Subasta)
                .HasForeignKey<Subasta>(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_subastas_producto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_subastas_usuarios");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion);

            entity.ToTable("transacciones", "subastas");

            entity.Property(e => e.IdTransaccion).HasColumnName("id_transaccion");
            entity.Property(e => e.EsAFavor).HasColumnName("es_a_favor");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.FechaTransaccion).HasColumnName("fecha_transaccion");
            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.MontoTransaccion)
                .HasColumnType("decimal(26, 2)")
                .HasColumnName("monto_transaccion");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transacciones_cuenta");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuarios", "subastas");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("apellido_usuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo_usuario");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdRol });

            entity.ToTable("usuario_rol", "subastas");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.EstaActivo).HasColumnName("esta_activo");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuario_rol_roles");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_usuario_rol_usuarios");
        });
        modelBuilder.Entity<LogEntry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
