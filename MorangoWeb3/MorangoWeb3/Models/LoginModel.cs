using System.ComponentModel.DataAnnotations;

namespace MorangoWeb3.Models
{
    // Modelo que representa o login de um usuário no sistema.
    public class LoginModel
    {
        // Propriedade que representa o ID único de cada login.
        public int Id { get; set; }

        // Propriedade que representa o nome de usuário utilizado para o login.
        // É obrigatória, com uma mensagem de erro personalizada.
        [Required(ErrorMessage = "Insira o seu usuário para fazer login.")]
        public string UsuarioLogin { get; set; }

        // Propriedade que representa a senha do usuário para o login.
        // Também é obrigatória, com uma mensagem de erro personalizada.
        [Required(ErrorMessage = "Insira a sua senha para fazer login.")]
        public string SenhaLogin { get; set; }

        // Propriedade que armazena o ID do usuário relacionado a esse login.
        public int UsuarioId { get; set; }

        // Propriedade de navegação que representa o usuário associado a este login.
        public UsuariosModel usuariosModel { get; set; }
    }
}
