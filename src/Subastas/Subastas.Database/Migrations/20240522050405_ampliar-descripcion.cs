using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subastas.Database.Migrations
{
    /// <inheritdoc />
    public partial class ampliardescripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descripcion_producto",
                schema: "subastas",
                table: "productos",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descripcion_producto",
                schema: "subastas",
                table: "productos",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500);
        }
    }
}
