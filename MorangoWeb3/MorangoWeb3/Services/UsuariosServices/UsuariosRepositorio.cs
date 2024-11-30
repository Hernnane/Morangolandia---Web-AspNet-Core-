using Microsoft.EntityFrameworkCore;
using MorangoWeb3.Data;
using MorangoWeb3.Models;
using MorangoWeb3.Services.ReceitaServices;

namespace MorangoWeb3.Services.UsuarioServices
{
    // Implementação da interface IUsuariosRepositorio para gerenciar as operações de usuários
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        // Contexto de banco de dados para operações com a tabela de usuários
        private readonly ApplicationDbContext _db;

        // Construtor que recebe a instância de ApplicationDbContext
        public UsuariosRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        // Método síncrono para adicionar um novo usuário
        public UsuariosModel Adicionar(UsuariosModel usuario)
        {
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();

            return usuario;
        }

        // Método síncrono para atualizar as informações de um usuário
        public UsuariosModel Atualizar(UsuariosModel usuario)
        {
            _db.Usuarios.Update(usuario);
            _db.SaveChanges();

            return usuario;
        }

        // Método assíncrono para buscar um usuário pelo email
        public async Task<UsuariosModel?> BuscarPorEmailAsync(string email)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        // Método assíncrono para buscar um usuário pelo ID
        public Task<UsuariosModel?> BuscarPorIdAsync(int id)
        {
            return _db.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Método assíncrono para buscar um usuário pelo nome de usuário
        public async Task<UsuariosModel?> BuscarPorUsuarioAsync(string usuario)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(x => x.Usuario.ToUpper() == usuario);
        }

        // Método síncrono para buscar um usuário pelo nome de usuário (caso o nome de usuário já esteja presente no parâmetro)
        public UsuariosModel BuscarUsuario(UsuariosModel usuario)
        {
            return _db.Usuarios.FirstOrDefault(x => x.Usuario == usuario.Usuario);
        }
    }
}
