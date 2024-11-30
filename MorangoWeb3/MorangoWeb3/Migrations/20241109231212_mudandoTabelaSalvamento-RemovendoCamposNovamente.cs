using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorangoWeb3.Migrations
{
    /// <inheritdoc />
    public partial class mudandoTabelaSalvamentoRemovendoCamposNovamente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Salvamentos");

            migrationBuilder.DropColumn(
                name: "ImagemCaminho",
                table: "Salvamentos");

            migrationBuilder.DropColumn(
                name: "Ingredientes",
                table: "Salvamentos");

            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Salvamentos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Salvamentos");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Salvamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Salvamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagemCaminho",
                table: "Salvamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ingredientes",
                table: "Salvamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                table: "Salvamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Salvamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Salvamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
