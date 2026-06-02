using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceCare.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaDeleteTabelaTurista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ATIVO",
                table: "SC_TURISTAS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: "1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATIVO",
                table: "SC_TURISTAS");
        }
    }
}
