using MorangoWeb3.Data; // Importa o contexto do banco de dados.
using MorangoWeb3.Models; // Importa os modelos de dados, como CurtidasModel.

namespace MorangoWeb3.Services.CurtidasServices
{
    // Implementação do repositório de Curtidas, que contém a lógica para interagir com a tabela Curtidas no banco de dados.
    public class CurtidasRepositorio : ICurtidasRepositorio
    {
        private readonly ApplicationDbContext _db; // Contexto do banco de dados para interagir com as tabelas.

        // Construtor que recebe a instância de ApplicationDbContext para interagir com o banco de dados.
        public CurtidasRepositorio(ApplicationDbContext db)
        {
            _db = db; // Inicializa o contexto de banco de dados.
        }

        // Método para adicionar uma curtida ao banco de dados.
        public CurtidasModel Adicionar(CurtidasModel curtida)
        {
            _db.Curtidas.Add(curtida); // Adiciona o objeto CurtidasModel na tabela Curtidas.
            _db.SaveChanges(); // Salva as alterações no banco de dados.

            return curtida; // Retorna o objeto CurtidasModel que foi adicionado.
        }
    }
}
