using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subastas.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatedecimalcount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "subastas",
                table: "usuarios",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<decimal>(
                name: "monto_transaccion",
                schema: "subastas",
                table: "transacciones",
                type: "decimal(26,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "monto_inicial",
                schema: "subastas",
                table: "subastas",
                type: "decimal(26,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "monto_puja",
                schema: "subastas",
                table: "pujas",
                type: "decimal(26,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "saldo",
                schema: "subastas",
                table: "cuenta",
                type: "decimal(26,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "subastas",
                table: "usuarios",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "monto_transaccion",
                schema: "subastas",
                table: "transacciones",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "monto_inicial",
                schema: "subastas",
                table: "subastas",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "monto_puja",
                schema: "subastas",
                table: "pujas",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "saldo",
                schema: "subastas",
                table: "cuenta",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(26,2)");
        }
    }
}
