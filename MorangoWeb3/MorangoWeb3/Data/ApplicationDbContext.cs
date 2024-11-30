using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core, usado para interagir com o banco de dados.
using MorangoWeb3.Models; // Importa os modelos utilizados no projeto.

namespace MorangoWeb3.Data
{
    // Classe que define o contexto de banco de dados para a aplicação.
    public class ApplicationDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define as tabelas do banco de dados como propriedades do DbContext.

        // Tabela de usuários, representada pela model UsuariosModel.
        public DbSet<UsuariosModel> Usuarios { get; set; }

        // Tabela de login, representada pela model LoginModel.
        public DbSet<LoginModel> Login { get; set; }

        // Tabela de perfis de usuário, representada pela model MeuPerfilModel.
        public DbSet<MeuPerfilModel> Perfil { get; set; }

        // Tabela de receitas, representada pela model ReceitasModel.
        public DbSet<ReceitasModel> Receitas { get; set; }

        // Tabela de curtidas nas receitas, representada pela model CurtidasModel.
        public DbSet<CurtidasModel> Curtidas { get; set; }

        // Tabela de receitas salvas pelos usuários, representada pela model SalvamentosModel.
        public DbSet<SalvamentosModel> Salvamentos { get; set; }
    }
}
