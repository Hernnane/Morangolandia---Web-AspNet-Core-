using Microsoft.AspNetCore.Mvc; // Usado para criar controladores e lidar com requisições HTTP.
using MorangoWeb3.Models; // Namespace contendo os modelos usados na aplicação.
using MorangoWeb3.Services.UsuarioServices; // Serviços relacionados ao gerenciamento de usuários.
using Microsoft.AspNetCore.Authentication.Cookies; // Necessário para trabalhar com autenticação baseada em cookies.
using System.Security.Claims; // Usado para criar e manipular informações de identidade do usuário.
using Microsoft.AspNetCore.Authentication; // Usado para realizar a autenticação.
using FluentValidation; // Biblioteca para validação de modelos.
using MorangoWeb3.Services.LoginServices; // Serviços relacionados ao gerenciamento de login.

namespace MorangoWeb3.Controllers
{
    public class LoginController : Controller
    {
        // Dependências para o repositório de usuários, validação de login e repositório de login.
        private readonly IUsuariosRepositorio _usuarioRepositorio; // Repositório para operações com usuários.
        private readonly IValidator<LoginModel> _validator; // Validador de modelo de login.
        private readonly ILoginRepositorio _loginRepositorio; // Repositório para operações com login.

        // Construtor para injeção de dependência.
        public LoginController(IUsuariosRepositorio usuarioRepositorio, IValidator<LoginModel> validator, ILoginRepositorio loginRepositorio)
        {
            _validator = validator; // Inicializa o validador.
            _usuarioRepositorio = usuarioRepositorio; // Inicializa o repositório de usuários.
            _loginRepositorio = loginRepositorio; // Inicializa o repositório de login.
        }

        // Retorna a página de login.
        public IActionResult Index()
        {
            return View();
        }

        // Retorna a página para realizar login (usado para exibição de um formulário, por exemplo).
        public IActionResult Logar()
        {
            return View();
        }

        // Método POST para realizar o login do usuário.
        [HttpPost]
        public async Task<IActionResult> LogarAsync(LoginModel usuarioLogin_)
        {
            // Verifica se o usuário existe no repositório de login.
            var userConfirm = _loginRepositorio.BuscarPorLogin(usuarioLogin_.UsuarioLogin);

            if (userConfirm == null)
            {
                // Se o login for inválido, adiciona erro ao ModelState.
                ModelState.AddModelError("UsuarioLogin", "Usuário e/ou senha incorretos. Tente novamente.");
                ModelState.AddModelError("SenhaLogin", "Usuário e/ou senha incorretos. Tente novamente.");
                return View("Index"); // Retorna à tela de login.
            }

            // Busca o usuário completo para verificar a senha e obter o Id.
            UsuariosModel usuario_ = await _usuarioRepositorio.BuscarPorUsuarioAsync(userConfirm.UsuarioLogin);
            usuarioLogin_.UsuarioId = usuario_.Id; // Associa o ID do usuário ao modelo de login.
            usuarioLogin_.usuariosModel = usuario_; // Associa o modelo de usuário.

            // Verifica se a senha fornecida é válida.
            if (usuario_.SenhaValida(usuarioLogin_.SenhaLogin))
            {
                // Se a senha estiver correta, cria as claims para o usuário.
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario_.Id.ToString()), // Identificador único do usuário.
                    new Claim(ClaimTypes.Name, usuario_.Usuario) // Nome de usuário.
                };

                var authScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Define o esquema de autenticação baseado em cookies.
                var identity = new ClaimsIdentity(claims, authScheme); // Cria a identidade do usuário.
                var principal = new ClaimsPrincipal(identity); // Cria o principal (usuário autenticado).

                // Realiza a autenticação do usuário.
                await HttpContext.SignInAsync(authScheme, principal);

                // Redireciona o usuário para a página inicial do "MorangoCheff" após o login.
                return RedirectToAction("Index", "MorangoCheff");
            }
            else
            {
                // Se a senha estiver incorreta, adiciona erro ao ModelState.
                ModelState.AddModelError("UsuarioLogin", "Usuário e/ou senha incorretos. Tente novamente.");
                ModelState.AddModelError("SenhaLogin", "Usuário e/ou senha incorretos. Tente novamente.");
                return View("Index"); // Retorna à tela de login.
            }
        }

        // Método para fazer logout e redirecionar o usuário para uma página simples.
        public async Task<IActionResult> Logout()
        {
            // Realiza o logout do usuário.
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "SimplePages"); // Redireciona para uma página simples após o logout.
        }
    }
}
