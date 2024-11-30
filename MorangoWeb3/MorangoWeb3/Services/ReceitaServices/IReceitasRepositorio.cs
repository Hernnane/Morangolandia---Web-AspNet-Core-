using MorangoWeb3.Models;

namespace MorangoWeb3.Services.ReceitaServices
{
    // Definição da interface IReceitasRepositorio
    public interface IReceitasRepositorio
    {
        // Método para adicionar uma receita
        ReceitasModel Adicionar(ReceitasModel receita);

        // Método para buscar uma receita pelo ID
        ReceitasModel BuscarPorId(int id);
    }
}
