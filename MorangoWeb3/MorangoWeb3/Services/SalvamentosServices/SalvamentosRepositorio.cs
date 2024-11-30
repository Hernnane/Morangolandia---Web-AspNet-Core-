using MorangoWeb3.Data;
using MorangoWeb3.Models;

namespace MorangoWeb3.Services.SalvamentosServices
{
    // Implementação da interface ISalvamentosRepositorio
    public class SalvamentosRepositorio : ISalvamentosRepositorio
    {
        private readonly ApplicationDbContext _db;

        // Construtor da classe SalvamentosRepositorio, recebendo o contexto do banco de dados (ApplicationDbContext)
        public SalvamentosRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        // Método para adicionar um salvamento no banco de dados
        public SalvamentosModel Adicionar(SalvamentosModel salvamento)
        {
            // Adiciona o salvamento à tabela 'Salvamentos'
            _db.Salvamentos.Add(salvamento);

            // Salva as alterações no banco de dados
            _db.SaveChanges();

            // Retorna o objeto 'salvamento' com os dados salvos
            return salvamento;
        }
    }
}
