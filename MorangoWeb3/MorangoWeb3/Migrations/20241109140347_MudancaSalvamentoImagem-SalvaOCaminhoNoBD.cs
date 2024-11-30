using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorangoWeb3.Migrations
{
    /// <inheritdoc />
    public partial class MudancaSalvamentoImagemSalvaOCaminhoNoBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfilCaminho",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagemCaminho",
                table: "Receitas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPerfilCaminho",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ImagemCaminho",
                table: "Receitas");
        }
    }
}
