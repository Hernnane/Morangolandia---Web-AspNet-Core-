using MorangoWeb3.Data;
using MorangoWeb3.Models;
using MorangoWeb3.Services.UsuarioServices;

namespace MorangoWeb3.Services.ReceitaServices
{
    // Implementação da interface IReceitasRepositorio
    public class ReceitasRepositorio : IReceitasRepositorio
    {
        private readonly ApplicationDbContext _db;

        // Construtor que recebe o contexto do banco de dados para injeção de dependência
        public ReceitasRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        // Método para adicionar uma nova receita no banco de dados
        public ReceitasModel Adicionar(ReceitasModel receita)
        {
            // Adiciona a receita no contexto
            _db.Receitas.Add(receita);
            // Salva as alterações no banco de dados
            _db.SaveChanges();

            return receita;
        }

        // Método para buscar uma receita pelo seu ID
        public ReceitasModel BuscarPorId(int id)
        {
            // Retorna a primeira receita que corresponder ao ID, ou null se não encontrado
            return _db.Receitas.FirstOrDefault(x => x.Id == id);
        }
    }
}
