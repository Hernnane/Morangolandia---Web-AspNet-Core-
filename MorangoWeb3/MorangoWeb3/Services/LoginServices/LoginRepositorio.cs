using MorangoWeb3.Data; // Importa o contexto de dados (ApplicationDbContext).
using MorangoWeb3.Models; // Importa o modelo LoginModel.

namespace MorangoWeb3.Services.LoginServices
{
    // Implementação da interface ILoginRepositorio que define as operações de repositório de login.
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly ApplicationDbContext _db; // Referência para o contexto do banco de dados.

        // Construtor da classe LoginRepositorio, recebe uma instância do ApplicationDbContext.
        public LoginRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        // Método para adicionar um novo login ao banco de dados.
        public LoginModel Adicionar(LoginModel login)
        {
            _db.Login.Add(login); // Adiciona o login ao contexto.
            _db.SaveChanges(); // Salva as mudanças no banco de dados.

            return login; // Retorna o login adicionado.
        }

        // Método para atualizar um login existente no banco de dados.
        public LoginModel Atualizar(LoginModel login)
        {
            _db.Login.Update(login); // Atualiza o login no contexto.
            _db.SaveChanges(); // Salva as mudanças no banco de dados.

            return login; // Retorna o login atualizado.
        }

        // Método para buscar um login no banco de dados baseado no nome de usuário.
        public LoginModel BuscarPorLogin(string usuario)
        {
            // Se o usuário for nulo, substitui por uma string vazia para evitar erro no ToUpper.
            string usuarioNormalized = usuario?.ToUpper() ?? string.Empty;

            // Retorna o primeiro login que tenha o nome de usuário correspondente (sem diferenciação de maiúsculas/minúsculas).
            return _db.Login.FirstOrDefault(x => x.UsuarioLogin.ToUpper() == usuarioNormalized);
        }
    }
}
