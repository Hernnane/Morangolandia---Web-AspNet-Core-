using MorangoWeb3.Models;

namespace MorangoWeb3.Services.SalvamentosServices
{
    // Definição da interface ISalvamentosRepositorio
    public interface ISalvamentosRepositorio
    {
        // Método para adicionar um salvamento ao banco de dados
        SalvamentosModel Adicionar(SalvamentosModel salvamento);
    }
}
