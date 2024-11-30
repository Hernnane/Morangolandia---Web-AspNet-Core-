using MorangoWeb3.Models; // Importa os modelos de dados, especificamente o modelo CurtidasModel.

namespace MorangoWeb3.Services.CurtidasServices
{
    // Interface que define os métodos para o repositório de curtidas.
    public interface ICurtidasRepositorio
    {
        // Método para adicionar uma nova curtida.
        CurtidasModel Adicionar(CurtidasModel curtida);
    }
}
