using System.ComponentModel.DataAnnotations;

namespace MorangoWeb3.Models
{
    // Modelo que representa as informações do perfil de um usuário.
    public class MeuPerfilModel
    {
        // Propriedade que representa o ID único do perfil.
        public int Id { get; set; }

        // Propriedade que representa o nome de usuário do perfil.
        // É obrigatória e, caso não seja preenchida, exibe uma mensagem de erro personalizada.
        [Required(ErrorMessage = "Insira um nome de usuário para fazer a alteração.")]
        public string Usuario { get; set; }

        // Propriedade que representa o apelido do usuário no perfil.
        // Pode ser deixada em branco (não obrigatória).
        public string Apelido { get; set; }

        // Propriedade que representa o e-mail associado ao perfil.
        // Também pode ser deixada em branco.
        public string Email { get; set; }

        // Propriedade que armazena o caminho da foto do perfil.
        // Este caminho será utilizado para buscar a imagem armazenada no servidor.
        public string FotoPerfilCaminho { get; set; }

        // Propriedade que representa o ID do usuário associado a este perfil.
        public int UsuarioId { get; set; }

        // Propriedade de navegação que representa o usuário associado a este perfil.
        public UsuariosModel usuariosModel { get; set; }
    }
}
