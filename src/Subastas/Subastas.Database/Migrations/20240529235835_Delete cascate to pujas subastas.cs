using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subastas.Database.Migrations
{
    /// <inheritdoc />
    public partial class Deletecascatetopujassubastas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pujas_subastas",
                schema: "subastas",
                table: "pujas");

            migrationBuilder.AddForeignKey(
                name: "FK_pujas_subastas",
                schema: "subastas",
                table: "pujas",
                column: "id_subasta",
                principalSchema: "subastas",
                principalTable: "subastas",
                principalColumn: "id_subasta",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pujas_subastas",
                schema: "subastas",
                table: "pujas");

            migrationBuilder.AddForeignKey(
                name: "FK_pujas_subastas",
                schema: "subastas",
                table: "pujas",
                column: "id_subasta",
                principalSchema: "subastas",
                principalTable: "subastas",
                principalColumn: "id_subasta");
        }
    }
}
