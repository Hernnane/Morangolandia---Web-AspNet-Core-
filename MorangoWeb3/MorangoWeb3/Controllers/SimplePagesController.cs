using Microsoft.AspNetCore.Mvc; // Importa o namespace do ASP.NET Core MVC para trabalhar com controladores e views.
using MorangoWeb3.Models; // Importa o namespace dos modelos da aplicação.
using System.Diagnostics; // Importa o namespace para funcionalidades de depuração (não utilizado no código, mas pode ser útil para logs).

namespace MorangoWeb3.Controllers
{
    // Controlador para páginas simples do site, como a página inicial, contato e sobre nós.
    public class SimplePagesController : Controller
    {
        // Ação para exibir a página inicial.
        public IActionResult Index()
        {
            return View(); // Retorna a view correspondente à página inicial.
        }

        // Ação para exibir a página de contato.
        public IActionResult Contato()
        {
            return View(); // Retorna a view correspondente à página de contato.
        }

        // Ação para exibir a página "Sobre Nós".
        public IActionResult SobreNos()
        {
            return View(); // Retorna a view correspondente à página "Sobre Nós".
        }
    }
}
