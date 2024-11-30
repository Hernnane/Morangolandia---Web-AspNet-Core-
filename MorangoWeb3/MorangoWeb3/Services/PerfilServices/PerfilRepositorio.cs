using MorangoWeb3.Data;
using MorangoWeb3.Models;

namespace MorangoWeb3.Services.PerfilServices
{
    public class PerfilRepositorio : IPerfilRepositorio
    {
        // Definindo o contexto de banco de dados
        private readonly ApplicationDbContext _db;

        // Construtor da classe, recebe o contexto de banco de dados
        public PerfilRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        // Método para adicionar um novo perfil ao banco de dados
        public MeuPerfilModel Adicionar(MeuPerfilModel perfil)
        {
            // Adiciona o perfil ao banco de dados
            _db.Perfil.Add(perfil);
            // Salva as alterações no banco
            _db.SaveChanges();

            // Retorna o perfil adicionado
            return perfil;
        }

        // Método para atualizar um perfil existente no banco de dados
        public MeuPerfilModel Atualizar(MeuPerfilModel perfil)
        {
            // Atualiza o perfil no banco de dados
            _db.Perfil.Update(perfil);
            // Salva as alterações no banco
            _db.SaveChanges();

            // Retorna o perfil atualizado
            return perfil;
        }

        // Método para buscar um perfil pelo nome de usuário
        public MeuPerfilModel BuscarPorUsuario(string usuario)
        {
            // Busca o perfil do usuário, ignorando diferença de maiúsculas e minúsculas
            return _db.Perfil.FirstOrDefault(x => x.Usuario.ToUpper() == usuario);
        }

        // Método para buscar um perfil pelo ID
        public MeuPerfilModel BuscarPorId(int id)
        {
            // Busca o perfil pelo ID
            return _db.Perfil.FirstOrDefault(x => x.Id == id);
        }
    }
}
