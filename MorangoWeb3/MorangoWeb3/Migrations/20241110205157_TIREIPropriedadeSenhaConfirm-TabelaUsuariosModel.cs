using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorangoWeb3.Migrations
{
    /// <inheritdoc />
    public partial class TIREIPropriedadeSenhaConfirmTabelaUsuariosModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenhaConfirm",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenhaConfirm",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
