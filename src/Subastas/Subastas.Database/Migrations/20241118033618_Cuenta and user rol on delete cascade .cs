using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subastas.Database.Migrations
{
    /// <inheritdoc />
    public partial class Cuentaanduserrolondeletecascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cuenta_usuarios",
                schema: "subastas",
                table: "cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_usuario_rol_usuarios",
                schema: "subastas",
                table: "usuario_rol");

            migrationBuilder.AddForeignKey(
                name: "FK_cuenta_usuarios",
                schema: "subastas",
                table: "cuenta",
                column: "id_usuario",
                principalSchema: "subastas",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_rol_usuarios",
                schema: "subastas",
                table: "usuario_rol",
                column: "id_usuario",
                principalSchema: "subastas",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cuenta_usuarios",
                schema: "subastas",
                table: "cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_usuario_rol_usuarios",
                schema: "subastas",
                table: "usuario_rol");

            migrationBuilder.AddForeignKey(
                name: "FK_cuenta_usuarios",
                schema: "subastas",
                table: "cuenta",
                column: "id_usuario",
                principalSchema: "subastas",
                principalTable: "usuarios",
                principalColumn: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_rol_usuarios",
                schema: "subastas",
                table: "usuario_rol",
                column: "id_usuario",
                principalSchema: "subastas",
                principalTable: "usuarios",
                principalColumn: "id_usuario");
        }
    }
}
