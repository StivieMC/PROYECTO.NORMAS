using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Normas.Api.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Normas");

            migrationBuilder.CreateTable(
                name: "TipoRequisitos",
                schema: "Normas",
                columns: table => new
                {
                    TipoRequisitoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRequisitos", x => x.TipoRequisitoId);
                });

            migrationBuilder.CreateTable(
                name: "Normas",
                schema: "Normas",
                columns: table => new
                {
                    NormaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    TipoRequisitoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Normas", x => x.NormaID);
                    table.ForeignKey(
                        name: "FK_Normas_TipoRequisitos_TipoRequisitoId",
                        column: x => x.TipoRequisitoId,
                        principalSchema: "Normas",
                        principalTable: "TipoRequisitos",
                        principalColumn: "TipoRequisitoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Normas",
                table: "TipoRequisitos",
                columns: new[] { "TipoRequisitoId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Norma Oficial Mexicana (NOM)" },
                    { 2, "Especificacion CFE" },
                    { 3, "Reglamento Federal" },
                    { 4, "Reglamento Estatal" },
                    { 5, "Reglamento Municipal" },
                    { 6, "Estandar Internacional" },
                    { 7, "Norma de Referencia" },
                    { 8, "Norma Mexicana" }
                });

            migrationBuilder.InsertData(
                schema: "Normas",
                table: "Normas",
                columns: new[] { "NormaID", "Activo", "Clave", "Descripcion", "FechaPublicacion", "TipoRequisitoId" },
                values: new object[] { 1, true, "abc123", "descripcion", new DateTime(2021, 5, 14, 18, 30, 45, 163, DateTimeKind.Local).AddTicks(2021), 1 });

            migrationBuilder.InsertData(
                schema: "Normas",
                table: "Normas",
                columns: new[] { "NormaID", "Activo", "Clave", "Descripcion", "FechaPublicacion", "TipoRequisitoId" },
                values: new object[] { 2, true, "abc123", "descripcion", new DateTime(2021, 5, 14, 18, 30, 45, 167, DateTimeKind.Local).AddTicks(4797), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Normas_TipoRequisitoId",
                schema: "Normas",
                table: "Normas",
                column: "TipoRequisitoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Normas",
                schema: "Normas");

            migrationBuilder.DropTable(
                name: "TipoRequisitos",
                schema: "Normas");
        }
    }
}
