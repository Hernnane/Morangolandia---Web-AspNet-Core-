using Microsoft.AspNetCore.Mvc; // Importa o namespace do ASP.NET Core MVC para trabalhar com controladores e views.
using MorangoWeb3.Models; // Importa o namespace dos modelos da aplica��o.
using System.Diagnostics; // Importa o namespace para funcionalidades de depura��o (n�o utilizado no c�digo, mas pode ser �til para logs).

namespace MorangoWeb3.Controllers
{
    // Controlador para p�ginas simples do site, como a p�gina inicial, contato e sobre n�s.
    public class SimplePagesController : Controller
    {
        // A��o para exibir a p�gina inicial.
        public IActionResult Index()
        {
            return View(); // Retorna a view correspondente � p�gina inicial.
        }

        // A��o para exibir a p�gina de contato.
        public IActionResult Contato()
        {
            return View(); // Retorna a view correspondente � p�gina de contato.
        }

        // A��o para exibir a p�gina "Sobre N�s".
        public IActionResult SobreNos()
        {
            return View(); // Retorna a view correspondente � p�gina "Sobre N�s".
        }
    }
}
