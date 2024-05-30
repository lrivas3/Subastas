using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subastas.Database.Migrations
{
    /// <inheritdoc />
    public partial class SetDateOnlytoDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_puja",
                schema: "subastas",
                table: "pujas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha_puja",
                schema: "subastas",
                table: "pujas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
