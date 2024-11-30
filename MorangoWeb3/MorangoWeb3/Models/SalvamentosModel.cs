namespace MorangoWeb3.Models
{
    // Modelo que representa os salvamentos de receitas feitas pelos usuários.
    public class SalvamentosModel
    {
        // Propriedade que representa o identificador único do salvamento.
        public int Id { get; set; }

        // Propriedade que armazena a data e hora em que a receita foi salva.
        public DateTime DataSalvamento { get; set; } = DateTime.Now;

        // Propriedade que armazena o identificador da receita salva.
        public int IdReceita { get; set; }

        // Propriedade que armazena o identificador do usuário que realizou o salvamento.
        public int IdUsuario { get; set; }

        // Propriedade de navegação que representa a receita associada ao salvamento.
        public ReceitasModel receitasModel { get; set; }

        // Propriedade de navegação que representa o usuário que realizou o salvamento.
        public UsuariosModel usuariosModel { get; set; }
    }
}
