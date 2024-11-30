using SixLabors.ImageSharp.Memory; // Importa a biblioteca ImageSharp para manipulação de imagens (não utilizado diretamente neste modelo).
using System.ComponentModel.DataAnnotations; // Importa os atributos de validação de dados.
using System.ComponentModel.DataAnnotations.Schema; // Importa os atributos de mapeamento de banco de dados.

namespace MorangoWeb3.Models
{
    // Modelo que representa os dados de um usuário no sistema.
    public class UsuariosModel
    {
        // Identificador único do usuário.
        public int Id { get; set; }

        // Nome completo do usuário, obrigatório.
        [Required(ErrorMessage = "Insira seu nome completo.")]
        public string Nome { get; set; }

        // Nome de usuário, obrigatório.
        [Required(ErrorMessage = "Insira um nome de usuário.")]
        public string Usuario { get; set; }

        // Apelido do usuário, opcional.
        public string? Apelido { get; set; }

        // Caminho da foto de perfil, com valor padrão configurado para uma imagem padrão.
        public string? FotoPerfilCaminho { get; set; } = "wwwroot\\img\\Perfil\\perfil-padrao.png";

        // Idade do usuário, obrigatório.
        [Required(ErrorMessage = "Insira sua idade.")]
        public int Idade { get; set; }

        // E-mail do usuário, obrigatório.
        [Required(ErrorMessage = "Insira seu e-mail.")]
        public string Email { get; set; }

        // Senha do usuário, obrigatório.
        [Required(ErrorMessage = "Insira uma senha.")]
        public string Senha { get; set; }

        // Tipo de usuário, com valor padrão de 0.
        public int TipoUsuario = 0;

        // Método para validar a senha do usuário, comparando com a senha armazenada.
        public bool SenhaValida(string senha)
        {
            return Senha == senha; // Retorna true se a senha fornecida for igual à senha armazenada.
        }
    }
}
