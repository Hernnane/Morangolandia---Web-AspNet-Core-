using FluentValidation; // Biblioteca para validação de modelos.
using Microsoft.AspNetCore.Hosting; // Necessário para acessar informações do ambiente de hospedagem.
using Microsoft.AspNetCore.Mvc; // Usado para criar controladores e lidar com requisições HTTP.
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // Usado para validações de modelos em Views.
using MorangoWeb3.Data; // Namespace contendo o contexto do banco de dados.
using MorangoWeb3.Models; // Namespace contendo os modelos usados na aplicação.
using MorangoWeb3.Services.LoginServices; // Serviços relacionados ao gerenciamento de login.
using MorangoWeb3.Services.PerfilServices; // Serviços relacionados ao gerenciamento de perfis.
using MorangoWeb3.Services.UsuarioServices; // Serviços relacionados ao gerenciamento de usuários.

namespace MorangoWeb3.Controllers
{
    public class CadastroController : Controller
    {
        // Interfaces e dependências necessárias.
        private readonly IUsuariosRepositorio _usuarioRepositorio; // Repositório para operações com usuários.
        private readonly ILoginRepositorio _loginRepositorio; // Repositório para operações com login.
        private readonly IPerfilRepositorio _perfilRepositorio; // Repositório para operações com perfis.
        private readonly IWebHostEnvironment _webHostEnvironment; // Permite acessar informações do ambiente de hospedagem.
        private readonly IValidator<UsuariosModel> _validator; // Validador de modelo de usuário usando FluentValidation.
        private readonly ApplicationDbContext _db; // Contexto do banco de dados.

        // Construtor para injeção de dependência.
        public CadastroController(IUsuariosRepositorio usuarioRepositorio, IWebHostEnvironment webHostEnvironment,
                                    IValidator<UsuariosModel> validator, ApplicationDbContext db,
                                    ILoginRepositorio loginRepositorio, IPerfilRepositorio perfilRepositorio)
        {
            // Inicializa as dependências.
            _db = db;
            _loginRepositorio = loginRepositorio;
            _perfilRepositorio = perfilRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _webHostEnvironment = webHostEnvironment;
            _validator = validator;
        }

        // Retorna a página inicial do cadastro.
        public IActionResult Index()
        {
            return View();
        }

        // Método POST para cadastrar um novo usuário.
        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)] // Limita o tamanho da requisição para 10 MB.
        public async Task<IActionResult> CadastrarAsync(UsuariosModel usuario_, IFormFile Imagem)
        {
            // Verifica se o nome de usuário já existe no banco de dados.
            var usuarioConfirm = await _usuarioRepositorio.BuscarPorUsuarioAsync(usuario_.Usuario);
            if (usuarioConfirm != null)
            {
                // Adiciona erro ao ModelState se o nome de usuário já estiver em uso.
                ModelState.AddModelError("Usuario", "Este nome de usuário já está em uso.");
            }

            // Verifica se o email já existe no banco de dados.
            var emailConfirm = await _usuarioRepositorio.BuscarPorEmailAsync(usuario_.Email);
            if (emailConfirm != null)
            {
                // Adiciona erro ao ModelState se o email já estiver em uso.
                ModelState.AddModelError("Email", "Este email já está em uso.");
            }

            // Valida o modelo de usuário usando FluentValidation.
            var validationResult = await _validator.ValidateAsync(usuario_);
            if (!validationResult.IsValid)
            {
                // Se houver falhas na validação, adiciona os erros ao ModelState.
                foreach (var failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                // Retorna à página de cadastro com os erros.
                return View("Index");
            }

            // Se o nome de usuário e email não existirem no banco.
            if (usuarioConfirm == null && emailConfirm == null)
            {
                // Se o apelido for nulo, define o nome de usuário como apelido.
                if (usuario_.Apelido == null)
                {
                    usuario_.Apelido = usuario_.Usuario;
                }

                // Se nenhuma imagem for enviada, define uma imagem padrão.
                if (Imagem == null)
                {
                    usuario_.FotoPerfilCaminho = "img\\Perfil\\perfil-padrao.png";
                }

                // Se uma imagem for enviada.
                if (Imagem != null && Imagem.Length > 0)
                {
                    // Define o caminho onde a imagem será salva.
                    var caminhoDaPasta = Path.Combine(_webHostEnvironment.WebRootPath, "img", "Perfil");
                    var caminhoCompleto = Path.Combine(caminhoDaPasta, Imagem.FileName);

                    // Cria a pasta, caso não exista.
                    if (!Directory.Exists(caminhoDaPasta))
                    {
                        Directory.CreateDirectory(caminhoDaPasta);
                    }

                    // Salva a imagem no caminho especificado.
                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await Imagem.CopyToAsync(stream);
                    }

                    // Armazena o caminho relativo da imagem na model.
                    usuario_.FotoPerfilCaminho = Path.Combine("img", "Perfil", Imagem.FileName);
                }

                // Adiciona o usuário ao banco.
                _usuarioRepositorio.Adicionar(usuario_);

                // Cria um login para o novo usuário.
                LoginModel userLogin = new LoginModel();
                userLogin.UsuarioLogin = usuario_.Usuario; // Define o nome de usuário.
                userLogin.SenhaLogin = usuario_.Senha; // Define a senha.
                userLogin.UsuarioId = usuario_.Id; // Associa ao ID do usuário.
                userLogin.usuariosModel = usuario_; // Associa ao modelo de usuário.

                // Adiciona o login ao banco.
                _loginRepositorio.Adicionar(userLogin);

                // Cria um perfil para o novo usuário.
                MeuPerfilModel perfil = new MeuPerfilModel();
                perfil.Usuario = usuario_.Usuario; // Define o nome de usuário.
                perfil.Apelido = usuario_.Apelido; // Define o apelido.
                perfil.Email = usuario_.Email; // Define o email.
                perfil.FotoPerfilCaminho = usuario_.FotoPerfilCaminho; // Define o caminho da foto.
                perfil.UsuarioId = usuario_.Id; // Associa ao ID do usuário.
                perfil.usuariosModel = usuario_; // Associa ao modelo de usuário.

                // Adiciona o perfil ao banco.
                _perfilRepositorio.Adicionar(perfil);

                // Redireciona o usuário para a página de login após o cadastro.
                return RedirectToAction("Index", "Login");
            }

            // Retorna à página de cadastro se algo falhar.
            return View("Index");
        }
    }
}
