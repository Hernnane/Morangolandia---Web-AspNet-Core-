using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorangoWeb3.Migrations
{
    /// <inheritdoc />
    public partial class MudandoNomeTabelaBDLoginModelUsuarioLoginSenhaLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "Login",
                newName: "UsuarioLogin");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Login",
                newName: "SenhaLogin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioLogin",
                table: "Login",
                newName: "Usuario");

            migrationBuilder.RenameColumn(
                name: "SenhaLogin",
                table: "Login",
                newName: "Senha");
        }
    }
}
