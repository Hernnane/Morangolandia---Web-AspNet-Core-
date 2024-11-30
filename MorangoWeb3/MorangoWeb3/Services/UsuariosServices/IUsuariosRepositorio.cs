using MorangoWeb3.Models;

namespace MorangoWeb3.Services.UsuarioServices
{
    // Interface que define os métodos a serem implementados para o repositório de usuários
    public interface IUsuariosRepositorio
    {
        // Método síncrono para buscar um usuário baseado no modelo 'UsuariosModel'
        UsuariosModel BuscarUsuario(UsuariosModel usuario);

        // Método síncrono para adicionar um novo usuário
        UsuariosModel Adicionar(UsuariosModel usuario);

        // Método síncrono para atualizar os dados de um usuário
        UsuariosModel Atualizar(UsuariosModel usuario);

        // Método assíncrono para buscar um usuário pelo nome de usuário
        Task<UsuariosModel?> BuscarPorUsuarioAsync(string usuario);

        // Método assíncrono para buscar um usuário pelo ID
        Task<UsuariosModel?> BuscarPorIdAsync(int id);

        // Método assíncrono para buscar um usuário pelo email
        Task<UsuariosModel?> BuscarPorEmailAsync(string email);
    }
}
