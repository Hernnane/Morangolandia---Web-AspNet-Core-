using MorangoWeb3.Models; // Importa o modelo MeuPerfilModel.

namespace MorangoWeb3.Services.PerfilServices
{
    // Define a interface IPerfilRepositorio, que descreve as operações do repositório de perfis.
    public interface IPerfilRepositorio
    {
        // Método para adicionar um novo perfil ao banco de dados.
        MeuPerfilModel Adicionar(MeuPerfilModel perfil);

        // Método para atualizar um perfil existente no banco de dados.
        MeuPerfilModel Atualizar(MeuPerfilModel perfil);

        // Método para buscar um perfil com base no nome de usuário.
        MeuPerfilModel BuscarPorUsuario(string usuario);

        // Método para buscar um perfil com base no ID do usuário.
        MeuPerfilModel BuscarPorId(int id);
    }
}
