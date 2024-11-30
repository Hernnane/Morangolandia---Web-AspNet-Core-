using MorangoWeb3.Models; // Importa o modelo LoginModel.

namespace MorangoWeb3.Services.LoginServices
{
    // Interface que define os métodos para o repositório de login.
    public interface ILoginRepositorio
    {
        // Método para adicionar um novo login.
        LoginModel Adicionar(LoginModel login);

        // Método para atualizar os dados de um login existente.
        LoginModel Atualizar(LoginModel login);

        // Método para buscar um login pelo nome de usuário.
        LoginModel BuscarPorLogin(string usuario);
    }
}
