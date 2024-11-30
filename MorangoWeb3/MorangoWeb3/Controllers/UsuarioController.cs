using FluentValidation; // Importa o namespace do FluentValidation, usado para validação de modelos.
using Microsoft.AspNetCore.Authentication.Cookies; // Importa a autenticação com cookies.
using Microsoft.AspNetCore.Authentication; // Importa recursos de autenticação.
using Microsoft.AspNetCore.Hosting; // Importa o ambiente de hospedagem para acessar arquivos do servidor.
using Microsoft.AspNetCore.Mvc; // Importa o ASP.NET Core MVC para trabalhar com controladores e views.
using Microsoft.IdentityModel.Tokens; // Importa recursos de autenticação e tokens.
using MorangoWeb3.Data; // Importa o namespace de acesso aos dados.
using MorangoWeb3.Models; // Importa os modelos utilizados no projeto.
using MorangoWeb3.Services.LoginServices; // Importa os serviços para login.
using MorangoWeb3.Services.PerfilServices; // Importa os serviços para o perfil do usuário.
using MorangoWeb3.Services.UsuarioServices; // Importa os serviços para a gestão de usuários.
using System.Data.Entity; // Importa recursos de acesso a banco de dados (não utilizado diretamente no código).
using System.Security.Claims; // Importa classes para a gestão de claims (informações sobre o usuário).

namespace MorangoWeb3.Controllers
{
    public class UsuarioController : Controller
    {
        // Injeção de dependências no controlador para acesso aos repositórios e serviços necessários
        private readonly ApplicationDbContext _db;
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IValidator<MeuPerfilModel> _validator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoginRepositorio _loginRepositorio;
        private readonly IPerfilRepositorio _perfilRepositorio;

        // Construtor do controlador, inicializando as dependências.
        public UsuarioController(ApplicationDbContext db, IUsuariosRepositorio usuariosRepositorio, IValidator<MeuPerfilModel> validator,
                                                            IWebHostEnvironment webHostEnvironment, ILoginRepositorio loginRepositorio,
                                                            IPerfilRepositorio perfilRepositorio)
        {
            _db = db;
            _validator = validator;
            _usuariosRepositorio = usuariosRepositorio;
            _perfilRepositorio = perfilRepositorio;
            _loginRepositorio = loginRepositorio;
            _webHostEnvironment = webHostEnvironment;
        }

        // Ação que exibe o perfil do usuário logado.
        public async Task<IActionResult> IndexAsync()
        {
            // Obtém o usuário logado com base no nome de usuário.
            UsuariosModel user = await _usuariosRepositorio.BuscarPorUsuarioAsync(User.Identity.Name.ToUpper());

            // Caso o usuário não seja encontrado, redireciona para a página inicial.
            if (user == null)
            {
                return RedirectToAction("Index", "SimplePages");
            }

            // Busca o perfil associado ao usuário.
            MeuPerfilModel perfil = _db.Perfil.FirstOrDefault(x => x.UsuarioId == user.Id);

            // Se o perfil não existir, redireciona para a página "Sobre Nós".
            if (perfil == null)
            {
                return RedirectToAction("SobreNos", "SimplePages");
            }

            // Exibe a view com os dados do perfil.
            return View(perfil);
        }

        // Ação GET para exibir o formulário de edição do perfil.
        [HttpGet]
        public async Task<IActionResult> EditarPerfilAsync(int Perfil_)
        {
            // Verifica se o ID do perfil é válido.
            if (Perfil_ <= 0)
            {
                return RedirectToAction("Contato", "SimplePages");
            }

            // Busca o perfil pelo ID.
            MeuPerfilModel perfilConfirm = _perfilRepositorio.BuscarPorId(Perfil_);

            // Se o perfil não existir, redireciona para a página "Sobre Nós".
            if (perfilConfirm == null)
            {
                return RedirectToAction("SobreNos", "SimplePages");
            }

            // Exibe a view de edição do perfil com os dados carregados.
            return View(perfilConfirm);
        }

        // Ação POST para salvar as alterações no perfil do usuário.
        [HttpPost]
        public async Task<IActionResult> EditarPerfilAsync(MeuPerfilModel perfil_, IFormFile Imagem)
        {
            // Obtém os dados do perfil, usuário e login para atualização.
            MeuPerfilModel perfil_2 = perfil_;
            UsuariosModel attUsuario = await _usuariosRepositorio.BuscarPorUsuarioAsync(User.Identity.Name.ToUpper());
            LoginModel attLogin = _loginRepositorio.BuscarPorLogin(User.Identity.Name.ToUpper());
            MeuPerfilModel attPerfil = _perfilRepositorio.BuscarPorUsuario(User.Identity.Name.ToUpper());

            // Se algum dos dados do usuário ou perfil estiver incorreto, redireciona para a página "Sobre Nós".
            if (attUsuario == null || attLogin == null || attPerfil == null)
            {
                return RedirectToAction("SobreNos", "SimplePages");
            }

            // Se os dados estiverem corretos, começa o processo de atualização.
            else if (attUsuario != null && attLogin != null && attPerfil != null)
            {
                // Exibe os dados recebidos para debug.
                Console.WriteLine("---------- Dados recebidos ----------");
                Console.WriteLine($"Id: {perfil_.Id}");
                Console.WriteLine($"Usuário: {perfil_.Usuario}");
                Console.WriteLine($"Apelido: {perfil_.Apelido}");
                Console.WriteLine($"Email: {perfil_.Email}");
                Console.WriteLine($"FotoPerfilCaminho: {perfil_.FotoPerfilCaminho}");
                Console.WriteLine("---------- Fim dos dados ----------");

                // Valida o modelo de perfil com FluentValidation.
                var validationResult = await _validator.ValidateAsync(perfil_);
                if (!validationResult.IsValid)
                {
                    // Se a validação falhar, exibe os erros na view.
                    foreach (var failure in validationResult.Errors)
                    {
                        ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                    }
                    return View();
                }

                // Verifica se já existe um perfil com o mesmo nome de usuário.
                MeuPerfilModel perfilConfirm = _perfilRepositorio.BuscarPorUsuario(perfil_.Usuario.ToUpper());
                if (perfilConfirm != null)
                {
                    // Se o usuário já existir, exibe erro.
                    ModelState.AddModelError("Usuario", "Este usuário já está em uso.");
                    return View();
                }
                else
                {
                    // Se o perfil não existir, realiza as alterações necessárias.

                    // Define o diretório para salvar a imagem de perfil.
                    var imagensDiretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img", "Receitas");

                    // Verifica se foi enviado um novo arquivo de imagem para o perfil.
                    if (Imagem != null)
                    {
                        var caminhoImagemAntiga = Path.Combine(imagensDiretorio, perfil_.FotoPerfilCaminho);

                        // Exclui a imagem antiga, caso exista.
                        if (System.IO.File.Exists(caminhoImagemAntiga))
                        {
                            System.IO.File.Delete(caminhoImagemAntiga);
                        }

                        // Cria um novo nome único para a imagem com base no GUID.
                        var novoNomeArquivo = Guid.NewGuid().ToString() + Imagem.FileName;
                        var caminhoImagemNova = Path.Combine(imagensDiretorio, novoNomeArquivo);

                        // Salva o novo arquivo de imagem.
                        using (var stream = new FileStream(caminhoImagemNova, FileMode.Create))
                        {
                            await Imagem.CopyToAsync(stream);
                        }

                        // Atualiza o caminho da imagem no banco de dados.
                        perfil_.FotoPerfilCaminho = Path.Combine("img", "Receitas", novoNomeArquivo);
                    }

                    // Atualiza os dados do usuário no banco de dados.
                    attUsuario.Usuario = perfil_.Usuario;
                    attUsuario.Apelido = perfil_.Apelido;
                    _usuariosRepositorio.Atualizar(attUsuario);

                    // Atualiza os dados do perfil no banco de dados.
                    attPerfil.Usuario = perfil_2.Usuario;
                    attPerfil.Apelido = perfil_2.Apelido;
                    _perfilRepositorio.Atualizar(attPerfil);

                    // Atualiza os dados de login no banco de dados.
                    attLogin.UsuarioLogin = perfil_.Usuario;
                    _loginRepositorio.Atualizar(attLogin);

                    // Cria os claims para atualizar a autenticação do usuário.
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, attLogin.Id.ToString()),
                        new Claim(ClaimTypes.Name, attLogin.UsuarioLogin) // Atualiza o nome do usuário.
                    };

                    // Cria a identidade e o principal com base nos claims.
                    var identidade = new ClaimsIdentity(claims, "Custom");
                    var principal = new ClaimsPrincipal(identidade);

                    // Atualiza a identidade do usuário no contexto de autenticação.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redireciona para a página inicial do perfil.
                    return RedirectToAction("Index", "Usuario");
                }
            }

            // Se algo falhar, retorna para a view de edição.
            return View();
        }

        // Ação para exibir o formulário de alteração de senha.
        [HttpGet]
        public async Task<IActionResult> AlterarSenha()
        {
            return View();
        }

        // Ação para processar a alteração de senha.
        [HttpPost]
        public async Task<IActionResult> AlterarSenhaAsync(string SenhaAntiga, string NovaSenha1, string NovaSenha2)
        {
            // Obtém os dados do usuário e login para validação.
            UsuariosModel usuarioAtual = await _usuariosRepositorio.BuscarPorUsuarioAsync(User.Identity.Name.ToUpper());
            LoginModel attLogin = _loginRepositorio.BuscarPorLogin(User.Identity.Name.ToUpper());

            // Se os dados do usuário ou login não forem encontrados, redireciona para a página de contato.
            if (usuarioAtual == null || attLogin == null)
            {
                return RedirectToAction("Contato", "SimplePages");
            }

            // Se os dados estiverem corretos, valida a alteração de senha.
            else if (usuarioAtual != null)
            {
                // Verifica se a senha antiga está correta.
                if (SenhaAntiga == usuarioAtual.Senha)
                {
                    // Verifica se as novas senhas são iguais.
                    if (NovaSenha1 == NovaSenha2)
                    {
                        // Atualiza a senha na tabela de usuários.
                        usuarioAtual.Senha = NovaSenha1;
                        _usuariosRepositorio.Atualizar(usuarioAtual);

                        // Atualiza a senha na tabela de login.
                        attLogin.SenhaLogin = NovaSenha1;
                        _loginRepositorio.Atualizar(attLogin);

                        // Redireciona para a página do perfil.
                        return RedirectToAction("Index", "Usuario");
                    }
                    else
                    {
                        return View(); // Se as senhas não coincidirem, retorna à view.
                    }
                }
                else
                {
                    return View(); // Se a senha antiga estiver errada, retorna à view.
                }
            }

            return View(); // Se algo falhar, retorna à view.
        }
    }
}
