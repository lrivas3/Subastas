﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subastas.Database;

#nullable disable

namespace Subastas.Database.Migrations
{
    [DbContext(typeof(SubastasContext))]
    [Migration("20240529235835_Delete cascate to pujas subastas")]
    partial class Deletecascatetopujassubastas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Subastas.Domain.Cuenta", b =>
                {
                    b.Property<int>("IdCuenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_cuenta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCuenta"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("saldo");

                    b.HasKey("IdCuenta");

                    b.HasIndex(new[] { "IdUsuario" }, "unique_id_usuario")
                        .IsUnique();

                    b.ToTable("cuenta", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Menu", b =>
                {
                    b.Property<int>("IdMenu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_menu");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMenu"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<int?>("IdMenuPadre")
                        .HasColumnType("int")
                        .HasColumnName("id_menu_padre");

                    b.Property<string>("NombreMenu")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("nombre_menu");

                    b.HasKey("IdMenu");

                    b.HasIndex("IdMenuPadre");

                    b.ToTable("menus", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.ParticipantesSubasta", b =>
                {
                    b.Property<int>("IdSubasta")
                        .HasColumnType("int")
                        .HasColumnName("id_subasta");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.HasKey("IdSubasta", "IdUsuario");

                    b.HasIndex("IdUsuario");

                    b.ToTable("participantes_subasta", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Permiso", b =>
                {
                    b.Property<int>("IdPermiso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_permiso");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPermiso"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<int>("IdMenu")
                        .HasColumnType("int")
                        .HasColumnName("id_menu");

                    b.Property<string>("NombrePermiso")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("nombre_permiso");

                    b.HasKey("IdPermiso");

                    b.HasIndex("IdMenu");

                    b.ToTable("permisos", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"));

                    b.Property<string>("DescripcionProducto")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("descripcion_producto");

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<bool>("EstaSubastado")
                        .HasColumnType("bit")
                        .HasColumnName("esta_subastado");

                    b.Property<string>("ImagenProducto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("imagen_producto");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("nombre_producto");

                    b.HasKey("IdProducto");

                    b.ToTable("productos", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Puja", b =>
                {
                    b.Property<int>("IdPuja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_puja");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPuja"));

                    b.Property<DateOnly>("FechaPuja")
                        .HasColumnType("date")
                        .HasColumnName("fecha_puja");

                    b.Property<int>("IdSubasta")
                        .HasColumnType("int")
                        .HasColumnName("id_subasta");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<decimal>("MontoPuja")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("monto_puja");

                    b.HasKey("IdPuja");

                    b.HasIndex("IdSubasta");

                    b.HasIndex("IdUsuario");

                    b.ToTable("pujas", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.RolPermiso", b =>
                {
                    b.Property<int>("IdRol")
                        .HasColumnType("int")
                        .HasColumnName("id_rol");

                    b.Property<int>("IdPermiso")
                        .HasColumnType("int")
                        .HasColumnName("id_permiso");

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.HasKey("IdRol", "IdPermiso");

                    b.HasIndex("IdPermiso");

                    b.ToTable("rol_permiso", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Role", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_rol");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("nombre_rol");

                    b.HasKey("IdRol");

                    b.ToTable("roles", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Subasta", b =>
                {
                    b.Property<int>("IdSubasta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_subasta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSubasta"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<DateTime>("FechaSubasta")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_subasta");

                    b.Property<DateTime>("FechaSubastaFin")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_subasta_fin");

                    b.Property<bool>("Finalizada")
                        .HasColumnType("bit")
                        .HasColumnName("finalizada");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<decimal>("MontoInicial")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("monto_inicial");

                    b.Property<string>("TituloSubasta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("titulo_subasta");

                    b.HasKey("IdSubasta");

                    b.HasIndex("IdUsuario");

                    b.HasIndex(new[] { "IdProducto" }, "unique_id_producto")
                        .IsUnique();

                    b.ToTable("subastas", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Transaccion", b =>
                {
                    b.Property<int>("IdTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_transaccion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaccion"));

                    b.Property<bool>("EsAFavor")
                        .HasColumnType("bit")
                        .HasColumnName("es_a_favor");

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<DateOnly>("FechaTransaccion")
                        .HasColumnType("date")
                        .HasColumnName("fecha_transaccion");

                    b.Property<int>("IdCuenta")
                        .HasColumnType("int")
                        .HasColumnName("id_cuenta");

                    b.Property<decimal>("MontoTransaccion")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("monto_transaccion");

                    b.HasKey("IdTransaccion");

                    b.HasIndex("IdCuenta");

                    b.ToTable("transacciones", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("ApellidoUsuario")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("apellido_usuario");

                    b.Property<string>("CorreoUsuario")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("correo_usuario");

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("nombre_usuario");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("password");

                    b.HasKey("IdUsuario");

                    b.ToTable("usuarios", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.UsuarioRol", b =>
                {
                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<int>("IdRol")
                        .HasColumnType("int")
                        .HasColumnName("id_rol");

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esta_activo");

                    b.HasKey("IdUsuario", "IdRol");

                    b.HasIndex("IdRol");

                    b.ToTable("usuario_rol", "subastas");
                });

            modelBuilder.Entity("Subastas.Domain.Cuenta", b =>
                {
                    b.HasOne("Subastas.Domain.Usuario", "IdUsuarioNavigation")
                        .WithOne("Cuentum")
                        .HasForeignKey("Subastas.Domain.Cuenta", "IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_cuenta_usuarios");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.Menu", b =>
                {
                    b.HasOne("Subastas.Domain.Menu", "IdMenuPadreNavigation")
                        .WithMany("InverseIdMenuPadreNavigation")
                        .HasForeignKey("IdMenuPadre")
                        .HasConstraintName("FK_menus_menus");

                    b.Navigation("IdMenuPadreNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.ParticipantesSubasta", b =>
                {
                    b.HasOne("Subastas.Domain.Subasta", "IdSubastaNavigation")
                        .WithMany("ParticipantesSubasta")
                        .HasForeignKey("IdSubasta")
                        .IsRequired()
                        .HasConstraintName("FK_participantes_subasta_subastas");

                    b.HasOne("Subastas.Domain.Usuario", "IdUsuarioNavigation")
                        .WithMany("ParticipantesSubasta")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_participantes_subasta_usuarios");

                    b.Navigation("IdSubastaNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.Permiso", b =>
                {
                    b.HasOne("Subastas.Domain.Menu", "IdMenuNavigation")
                        .WithMany("Permisos")
                        .HasForeignKey("IdMenu")
                        .IsRequired()
                        .HasConstraintName("FK_permisos_menus");

                    b.Navigation("IdMenuNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.Puja", b =>
                {
                    b.HasOne("Subastas.Domain.Subasta", "IdSubastaNavigation")
                        .WithMany("Pujas")
                        .HasForeignKey("IdSubasta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_pujas_subastas");

                    b.HasOne("Subastas.Domain.Usuario", "IdUsuarioNavigation")
                        .WithMany("Pujas")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_pujas_usuarios");

                    b.Navigation("IdSubastaNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.RolPermiso", b =>
                {
                    b.HasOne("Subastas.Domain.Permiso", "IdPermisoNavigation")
                        .WithMany("RolPermisos")
                        .HasForeignKey("IdPermiso")
                        .IsRequired()
                        .HasConstraintName("FK_rol_permiso_permisos");

                    b.HasOne("Subastas.Domain.Role", "IdRolNavigation")
                        .WithMany("RolPermisos")
                        .HasForeignKey("IdRol")
                        .IsRequired()
                        .HasConstraintName("FK_rol_permiso_roles");

                    b.Navigation("IdPermisoNavigation");

                    b.Navigation("IdRolNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.Subasta", b =>
                {
                    b.HasOne("Subastas.Domain.Producto", "IdProductoNavigation")
                        .WithOne("Subasta")
                        .HasForeignKey("Subastas.Domain.Subasta", "IdProducto")
                        .IsRequired()
                        .HasConstraintName("FK_subastas_producto");

                    b.HasOne("Subastas.Domain.Usuario", "IdUsuarioNavigation")
                        .WithMany("Subasta")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK_subastas_usuarios");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.Transaccion", b =>
                {
                    b.HasOne("Subastas.Domain.Cuenta", "IdCuentaNavigation")
                        .WithMany("Transacciones")
                        .HasForeignKey("IdCuenta")
                        .IsRequired()
                        .HasConstraintName("FK_transacciones_cuenta");

                    b.Navigation("IdCuentaNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.UsuarioRol", b =>
                {
                    b.HasOne("Subastas.Domain.Role", "IdRolNavigation")
                        .WithMany("UsuarioRols")
                        .HasForeignKey("IdRol")
                        .IsRequired()
                        .HasConstraintName("FK_usuario_rol_roles");

                    b.HasOne("Subastas.Domain.Usuario", "IdUsuarioNavigation")
                        .WithMany("UsuarioRols")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_usuario_rol_usuarios");

                    b.Navigation("IdRolNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Subastas.Domain.Cuenta", b =>
                {
                    b.Navigation("Transacciones");
                });

            modelBuilder.Entity("Subastas.Domain.Menu", b =>
                {
                    b.Navigation("InverseIdMenuPadreNavigation");

                    b.Navigation("Permisos");
                });

            modelBuilder.Entity("Subastas.Domain.Permiso", b =>
                {
                    b.Navigation("RolPermisos");
                });

            modelBuilder.Entity("Subastas.Domain.Producto", b =>
                {
                    b.Navigation("Subasta");
                });

            modelBuilder.Entity("Subastas.Domain.Role", b =>
                {
                    b.Navigation("RolPermisos");

                    b.Navigation("UsuarioRols");
                });

            modelBuilder.Entity("Subastas.Domain.Subasta", b =>
                {
                    b.Navigation("ParticipantesSubasta");

                    b.Navigation("Pujas");
                });

            modelBuilder.Entity("Subastas.Domain.Usuario", b =>
                {
                    b.Navigation("Cuentum");

                    b.Navigation("ParticipantesSubasta");

                    b.Navigation("Pujas");

                    b.Navigation("Subasta");

                    b.Navigation("UsuarioRols");
                });
#pragma warning restore 612, 618
        }
    }
}
