namespace MorangoWeb3.Models
{
    // Modelo que representa a tabela de Curtidas no banco de dados.
    public class CurtidasModel
    {
        // Propriedade que representa o ID da curtida.
        public int Id { get; set; }

        // Propriedade que representa a data em que a curtida foi realizada. 
        // O valor padrão é a data e hora atual.
        public DateTime DataCurtida { get; set; } = DateTime.Now;

        // Propriedade que representa o ID da receita que foi curtida.
        public int IdReceita { get; set; }

        // Propriedade que representa o ID do usuário que curtiu a receita.
        public int IdUsuario { get; set; }

        // Propriedade de navegação que representa a receita associada à curtida.
        public ReceitasModel receitasModel { get; set; }

        // Propriedade de navegação que representa o usuário associado à curtida.
        public UsuariosModel usuariosModel { get; set; }
    }
}
