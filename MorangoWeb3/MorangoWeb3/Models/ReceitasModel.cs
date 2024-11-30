using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MorangoWeb3.Models
{
    // Modelo que representa uma receita criada por um usuário.
    public class ReceitasModel
    {
        // Propriedade que representa o ID único da receita.
        public int Id { get; set; }

        // Propriedade que representa o título da receita.
        // A validação exige que o título seja preenchido.
        [Required(ErrorMessage = "Insira o título da receita.")]
        public string Titulo { get; set; }

        // Propriedade que representa o tipo de receita (ex: sobremesa, prato principal).
        // A validação exige que o tipo seja preenchido.
        [Required(ErrorMessage = "Insira o tipo da receita.")]
        public string Tipo { get; set; }

        // Propriedade que representa o nível de dificuldade da receita.
        // Esta propriedade pode ser um valor numérico que define a dificuldade (ex: 1 para fácil, 3 para difícil).
        public int Nivel { get; set; }

        // Propriedade que representa os ingredientes necessários para a receita.
        // A validação exige que os ingredientes sejam preenchidos.
        [Required(ErrorMessage = "Insira os ingredientes da receita.")]
        public string Ingredientes { get; set; }

        // Propriedade que representa a descrição detalhada do preparo da receita.
        // A validação exige que a descrição seja preenchida.
        [Required(ErrorMessage = "Insira a descrição da receita.")]
        public string Descricao { get; set; }

        // Propriedade que armazena o caminho da imagem associada à receita.
        // A validação exige que a imagem seja carregada.
        [Required(ErrorMessage = "Insira a imagem da receita.")]
        public string ImagemCaminho { get; set; }

        // Propriedade que representa a data em que a receita foi postada.
        // O valor padrão é a data e hora atuais.
        public DateTime DataPostagem { get; set; } = DateTime.Now;

        // Propriedade que representa o ID do usuário que postou a receita.
        public int UsuarioId { get; set; }

        // Propriedade de navegação que representa o usuário que criou a receita.
        public UsuariosModel usuariosModel { get; set; }
    }
}
