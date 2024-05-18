using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subastas.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "subastas");

            migrationBuilder.CreateTable(
                name: "menus",
                schema: "subastas",
                columns: table => new
                {
                    id_menu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_menu = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    id_menu_padre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus", x => x.id_menu);
                    table.ForeignKey(
                        name: "FK_menus_menus",
                        column: x => x.id_menu_padre,
                        principalSchema: "subastas",
                        principalTable: "menus",
                        principalColumn: "id_menu");
                });

            migrationBuilder.CreateTable(
                name: "productos",
                schema: "subastas",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_producto = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    descripcion_producto = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    imagen_producto = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    esta_subastado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id_producto);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "subastas",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_rol = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "subastas",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_usuario = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    apellido_usuario = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    correo_usuario = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    password = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                schema: "subastas",
                columns: table => new
                {
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_permiso = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    id_menu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id_permiso);
                    table.ForeignKey(
                        name: "FK_permisos_menus",
                        column: x => x.id_menu,
                        principalSchema: "subastas",
                        principalTable: "menus",
                        principalColumn: "id_menu");
                });

            migrationBuilder.CreateTable(
                name: "cuenta",
                schema: "subastas",
                columns: table => new
                {
                    id_cuenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saldo = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuenta", x => x.id_cuenta);
                    table.ForeignKey(
                        name: "FK_cuenta_usuarios",
                        column: x => x.id_usuario,
                        principalSchema: "subastas",
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "subastas",
                schema: "subastas",
                columns: table => new
                {
                    id_subasta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo_subasta = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    monto_inicial = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    fecha_subasta = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_subasta_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    finalizada = table.Column<bool>(type: "bit", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subastas", x => x.id_subasta);
                    table.ForeignKey(
                        name: "FK_subastas_producto",
                        column: x => x.id_producto,
                        principalSchema: "subastas",
                        principalTable: "productos",
                        principalColumn: "id_producto");
                    table.ForeignKey(
                        name: "FK_subastas_usuarios",
                        column: x => x.id_usuario,
                        principalSchema: "subastas",
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "usuario_rol",
                schema: "subastas",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_rol", x => new { x.id_usuario, x.id_rol });
                    table.ForeignKey(
                        name: "FK_usuario_rol_roles",
                        column: x => x.id_rol,
                        principalSchema: "subastas",
                        principalTable: "roles",
                        principalColumn: "id_rol");
                    table.ForeignKey(
                        name: "FK_usuario_rol_usuarios",
                        column: x => x.id_usuario,
                        principalSchema: "subastas",
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "rol_permiso",
                schema: "subastas",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    id_permiso = table.Column<int>(type: "int", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol_permiso", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK_rol_permiso_permisos",
                        column: x => x.id_permiso,
                        principalSchema: "subastas",
                        principalTable: "permisos",
                        principalColumn: "id_permiso");
                    table.ForeignKey(
                        name: "FK_rol_permiso_roles",
                        column: x => x.id_rol,
                        principalSchema: "subastas",
                        principalTable: "roles",
                        principalColumn: "id_rol");
                });

            migrationBuilder.CreateTable(
                name: "transacciones",
                schema: "subastas",
                columns: table => new
                {
                    id_transaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    monto_transaccion = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    fecha_transaccion = table.Column<DateOnly>(type: "date", nullable: false),
                    es_a_favor = table.Column<bool>(type: "bit", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    id_cuenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacciones", x => x.id_transaccion);
                    table.ForeignKey(
                        name: "FK_transacciones_cuenta",
                        column: x => x.id_cuenta,
                        principalSchema: "subastas",
                        principalTable: "cuenta",
                        principalColumn: "id_cuenta");
                });

            migrationBuilder.CreateTable(
                name: "participantes_subasta",
                schema: "subastas",
                columns: table => new
                {
                    id_subasta = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participantes_subasta", x => new { x.id_subasta, x.id_usuario });
                    table.ForeignKey(
                        name: "FK_participantes_subasta_subastas",
                        column: x => x.id_subasta,
                        principalSchema: "subastas",
                        principalTable: "subastas",
                        principalColumn: "id_subasta");
                    table.ForeignKey(
                        name: "FK_participantes_subasta_usuarios",
                        column: x => x.id_usuario,
                        principalSchema: "subastas",
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "pujas",
                schema: "subastas",
                columns: table => new
                {
                    id_puja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    monto_puja = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    fecha_puja = table.Column<DateOnly>(type: "date", nullable: false),
                    id_subasta = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pujas", x => x.id_puja);
                    table.ForeignKey(
                        name: "FK_pujas_subastas",
                        column: x => x.id_subasta,
                        principalSchema: "subastas",
                        principalTable: "subastas",
                        principalColumn: "id_subasta");
                    table.ForeignKey(
                        name: "FK_pujas_usuarios",
                        column: x => x.id_usuario,
                        principalSchema: "subastas",
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "unique_id_usuario",
                schema: "subastas",
                table: "cuenta",
                column: "id_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_menus_id_menu_padre",
                schema: "subastas",
                table: "menus",
                column: "id_menu_padre");

            migrationBuilder.CreateIndex(
                name: "IX_participantes_subasta_id_usuario",
                schema: "subastas",
                table: "participantes_subasta",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_permisos_id_menu",
                schema: "subastas",
                table: "permisos",
                column: "id_menu");

            migrationBuilder.CreateIndex(
                name: "IX_pujas_id_subasta",
                schema: "subastas",
                table: "pujas",
                column: "id_subasta");

            migrationBuilder.CreateIndex(
                name: "IX_pujas_id_usuario",
                schema: "subastas",
                table: "pujas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_rol_permiso_id_permiso",
                schema: "subastas",
                table: "rol_permiso",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_subastas_id_usuario",
                schema: "subastas",
                table: "subastas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "unique_id_producto",
                schema: "subastas",
                table: "subastas",
                column: "id_producto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_id_cuenta",
                schema: "subastas",
                table: "transacciones",
                column: "id_cuenta");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_rol_id_rol",
                schema: "subastas",
                table: "usuario_rol",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participantes_subasta",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "pujas",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "rol_permiso",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "transacciones",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "usuario_rol",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "subastas",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "permisos",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "cuenta",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "productos",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "menus",
                schema: "subastas");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "subastas");
        }
    }
}
