using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceCare.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaTuristas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SC_TURISTAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NR_PASSAPORTE_ESPACIAL = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    HISTORICO_MEDICO = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: true),
                    DT_CADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_TURISTAS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SC_TURISTAS_NR_PASSAPORTE_ESPACIAL",
                table: "SC_TURISTAS",
                column: "NR_PASSAPORTE_ESPACIAL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_TURISTAS");
        }
    }
}
