using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceCare.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaTelemetrias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ATIVO",
                table: "SC_TURISTAS",
                type: "CHAR(1)",
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)",
                oldDefaultValue: "1");

            migrationBuilder.CreateTable(
                name: "SC_TELEMETRIAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TURISTA_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BATIMENTOS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TEMPERATURA = table.Column<decimal>(type: "NUMBER(4,2)", nullable: false),
                    PRESSAO_ARTERIAL = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    DT_LEITURA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_TELEMETRIAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SC_TELEMETRIAS_SC_TURISTAS_TURISTA_ID",
                        column: x => x.TURISTA_ID,
                        principalTable: "SC_TURISTAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SC_TELEMETRIAS_TURISTA_ID",
                table: "SC_TELEMETRIAS",
                column: "TURISTA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_TELEMETRIAS");

            migrationBuilder.AlterColumn<bool>(
                name: "ATIVO",
                table: "SC_TURISTAS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(string),
                oldType: "CHAR(1)",
                oldDefaultValue: "1");
        }
    }
}
