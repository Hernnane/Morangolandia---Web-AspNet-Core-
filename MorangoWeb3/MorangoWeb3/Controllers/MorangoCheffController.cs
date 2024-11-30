using Microsoft.AspNetCore.Mvc; // Usado para criar controladores e lidar com requisições HTTP.
using Microsoft.EntityFrameworkCore.Metadata.Internal; // Referência para metadados internos do Entity Framework.
using MorangoWeb3.Data; // Namespace contendo o contexto de dados da aplicação (ApplicationDbContext).
using MorangoWeb3.Models; // Namespace contendo os modelos usados na aplicação.
using MorangoWeb3.Services.CurtidasServices; // Serviços relacionados a curtidas de receitas.
using MorangoWeb3.Services.ReceitaServices; // Serviços relacionados a receitas.
using MorangoWeb3.Services.SalvamentosServices; // Serviços relacionados ao salvamento de receitas.
using System.Data.Entity; // Referência para manipulação de entidades no contexto de dados.

namespace MorangoWeb3.Controllers
{
    public class MorangoCheffController : Controller
    {
        // Injeção de dependências para repositórios e contexto de banco de dados.
        private readonly IReceitasRepositorio _receitasRepositorio; // Repositório para operações com receitas.
        private readonly ApplicationDbContext _db; // Contexto de dados para acessar o banco de dados.
        private readonly IWebHostEnvironment _webHostEnvironment; // Ambiente da Web para acessar o diretório de arquivos estáticos.
        private readonly ICurtidasRepositorio _curtidasRepositorio; // Repositório para operações com curtidas de receitas.
        private readonly ISalvamentosRepositorio _salvamentosRepositorio; // Repositório para operações com salvamentos de receitas.

        // Construtor que inicializa as dependências.
        public MorangoCheffController(IReceitasRepositorio receitasRepositorio,
                                        ApplicationDbContext db,
                                        IWebHostEnvironment webHostEnvironment,
                                        ICurtidasRepositorio curtidasRepositorio,
                                        ISalvamentosRepositorio salvamentosRepositorio)
        {
            _receitasRepositorio = receitasRepositorio;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _curtidasRepositorio = curtidasRepositorio;
            _salvamentosRepositorio = salvamentosRepositorio;
        }

        // Ação que retorna a lista de receitas para a página inicial.
        public IActionResult Index()
        {
            IEnumerable<ReceitasModel> receitas = _db.Receitas; // Obtém todas as receitas da base de dados.
            return View(receitas); // Retorna a lista de receitas para a view.
        }

        // Ação para exibir a página quando o usuário não está logado.
        public IActionResult IndexDeslogado()
        {
            return View(); // Retorna a view para usuários não logados.
        }

        // Redireciona para o perfil do usuário.
        public IActionResult MeuPerfil()
        {
            return RedirectToAction("Index", "Usuario"); // Redireciona para o controlador de usuário.
        }

        // Exibe as receitas do usuário logado.
        public IActionResult MinhasReceitas()
        {
            UsuariosModel user = _db.Usuarios.FirstOrDefault(x => x.Usuario == User.Identity.Name); // Obtém o usuário logado.
            IEnumerable<ReceitasModel> receitas = _db.Receitas.Where(z => z.UsuarioId == user.Id); // Obtém as receitas do usuário.
            return View(receitas); // Retorna as receitas do usuário para a view.
        }

        // Exibe as receitas que o usuário salvou.
        public IActionResult ReceitasSalvas()
        {
            UsuariosModel user = _db.Usuarios.FirstOrDefault(y => y.Usuario == User.Identity.Name); // Obtém o usuário logado.
            var receitasSalvas = _db.Salvamentos.Where(x => x.IdUsuario == user.Id) // Obtém as receitas salvas pelo usuário.
                                                .Include(x => x.receitasModel) // Inclui as receitas associadas ao salvamento.
                                                .Select(x => x.receitasModel) // Seleciona apenas as receitas.
                                                .ToList(); // Converte para uma lista.
            return View(receitasSalvas); // Retorna as receitas salvas para a view.
        }

        // Método para salvar uma receita no perfil do usuário.
        [HttpPost]
        public IActionResult Salvar(SalvamentosModel salvamento_, int iDReceita)
        {
            ReceitasModel receita = _db.Receitas.FirstOrDefault(x => x.Id == iDReceita); // Busca a receita pelo ID.
            UsuariosModel usuario = _db.Usuarios.FirstOrDefault(y => y.Usuario == User.Identity.Name); // Obtém o usuário logado.

            if (usuario != null && receita != null)
            {
                salvamento_.IdReceita = iDReceita; // Define o ID da receita.
                salvamento_.IdUsuario = usuario.Id; // Define o ID do usuário.
                salvamento_.receitasModel = receita; // Associa a receita ao salvamento.
                salvamento_.usuariosModel = usuario; // Associa o usuário ao salvamento.

                _salvamentosRepositorio.Adicionar(salvamento_); // Adiciona o salvamento ao banco de dados.
                return RedirectToAction("Index", "MorangoCheff"); // Redireciona para a página inicial.
            }

            return View("Index"); // Caso o salvamento não tenha sido realizado, retorna para a página inicial.
        }

        // Exibe o formulário para adicionar uma nova receita.
        public IActionResult AdicionarReceita()
        {
            return View(); // Retorna a view para adicionar uma nova receita.
        }

        // Método para adicionar uma nova receita.
        [HttpPost]
        public async Task<IActionResult> AdicionarReceitaAsync(ReceitasModel receita_, IFormFile Imagem)
        {
            UsuariosModel usuario = _db.Usuarios.FirstOrDefault(x => x.Usuario == User.Identity.Name); // Obtém o usuário logado.

            // Tratamento de imagens
            if (Imagem != null && Imagem.Length > 0)
            {
                var imagensDiretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img", "Receitas"); // Define o diretório de imagens.

                // Criação de nome único para o arquivo
                var nomeOriginal = Path.GetFileName(Imagem.FileName); // Obtém o nome original do arquivo.
                var extensao = Path.GetExtension(nomeOriginal); // Obtém a extensão da imagem (JPG, PNG, etc.)
                var nomeUnico = Guid.NewGuid().ToString() + extensao; // Gera um nome único usando GUID.

                var caminhoCompleto = Path.Combine(imagensDiretorio, nomeUnico); // Define o caminho completo da imagem.

                // Criação do diretório, caso não exista
                if (!Directory.Exists(imagensDiretorio))
                {
                    Directory.CreateDirectory(imagensDiretorio); // Cria o diretório se não existir.
                }

                // Salva a imagem no diretório especificado
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await Imagem.CopyToAsync(stream); // Copia a imagem para o diretório.
                }

                // Armazena o caminho relativo da imagem na receita
                receita_.ImagemCaminho = Path.Combine("img", "Receitas", nomeUnico);
            }

            if (usuario != null)
            {
                receita_.UsuarioId = usuario.Id; // Define o ID do usuário na receita.
                receita_.usuariosModel = usuario; // Associa o usuário à receita.
                _receitasRepositorio.Adicionar(receita_); // Adiciona a receita ao banco de dados.

                return RedirectToAction("Index", "MorangoCheff"); // Redireciona para a página inicial.
            }

            return View("Index"); // Caso o usuário não esteja logado, retorna para a página inicial.
        }

        // Método para editar uma receita existente.
        [HttpPost]
        public async Task<IActionResult> EditarReceitaAsync(ReceitasModel receita, IFormFile? Imagem)
        {
            var imagensDiretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img", "Receitas"); // Define o diretório de imagens.

            // Se houver uma nova imagem
            if (Imagem != null)
            {
                var caminhoImagemAntiga = Path.Combine(imagensDiretorio, receita.ImagemCaminho); // Caminho da imagem antiga.

                // Exclui a imagem antiga
                if (System.IO.File.Exists(caminhoImagemAntiga))
                {
                    System.IO.File.Delete(caminhoImagemAntiga); // Exclui a imagem antiga do sistema de arquivos.
                }

                // Criação de nome único para a nova imagem
                var novoNomeArquivo = Guid.NewGuid().ToString() + Imagem.FileName; // Gera nome único para o novo arquivo.
                var caminhoImagemNova = Path.Combine(imagensDiretorio, novoNomeArquivo); // Caminho completo para a nova imagem.

                // Salva a nova imagem
                using (var stream = new FileStream(caminhoImagemNova, FileMode.Create))
                {
                    await Imagem.CopyToAsync(stream); // Copia a nova imagem para o diretório.
                }

                // Atualiza o caminho da nova imagem no banco de dados
                receita.ImagemCaminho = Path.Combine("img", "Receitas", novoNomeArquivo);
            }

            UsuariosModel usuario = _db.Usuarios.FirstOrDefault(x => x.Usuario == User.Identity.Name); // Obtém o usuário logado.

            receita.UsuarioId = usuario.Id; // Define o ID do usuário.
            receita.usuariosModel = usuario; // Associa o usuário à receita.

            _db.Receitas.Update(receita); // Atualiza a receita no banco de dados.
            _db.SaveChanges(); // Salva as mudanças no banco de dados.

            return RedirectToAction("MinhasReceitas", "MorangoCheff"); // Redireciona para a página de receitas do usuário.
        }

        // Ação para exibir o formulário de edição de uma receita.
        [HttpGet]
        public IActionResult EditarReceita(int id)
        {
            ReceitasModel receita_ = _receitasRepositorio.BuscarPorId(id); // Busca a receita pelo ID.
            return View(receita_); // Retorna a receita para a view de edição.
        }

        // Método para curtir uma receita.
        [HttpPost]
        public IActionResult Curtir(CurtidasModel curtida_, int iDReceita)
        {
            ReceitasModel receita = _db.Receitas.FirstOrDefault(x => x.Id == iDReceita); // Busca a receita pelo ID.
            UsuariosModel usuario = _db.Usuarios.FirstOrDefault(y => y.Usuario == User.Identity.Name); // Obtém o usuário logado.

            if (usuario != null && receita != null)
            {
                curtida_.IdReceita = iDReceita; // Define o ID da receita.
                curtida_.IdUsuario = usuario.Id; // Define o ID do usuário.
                curtida_.receitasModel = receita; // Associa a receita à curtida.
                curtida_.usuariosModel = usuario; // Associa o usuário à curtida.

                _curtidasRepositorio.Adicionar(curtida_); // Adiciona a curtida ao banco de dados.
                return RedirectToAction("Index", "MorangoCheff"); // Redireciona para a página inicial.
            }

            return RedirectToAction("Contato", "SimplePages"); // Caso não consiga curtir, redireciona para a página de contato.
        }
    }
}
