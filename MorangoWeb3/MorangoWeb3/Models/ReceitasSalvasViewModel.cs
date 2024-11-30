namespace MorangoWeb3.Models
{
    // ViewModel que representa os dados de uma receita salva por um usuário.
    public class ReceitasSalvasViewModel
    {
        // Propriedade que representa o título da receita.
        public string Titulo { get; set; }

        // Propriedade que representa o tipo da receita (ex: sobremesa, prato principal).
        public string Tipo { get; set; }

        // Propriedade que representa o nível de dificuldade da receita (ex: 1 para fácil, 3 para difícil).
        public int Nivel { get; set; }

        // Propriedade que representa os ingredientes necessários para a receita.
        public string Ingredientes { get; set; }

        // Propriedade que representa a descrição detalhada da receita.
        public string Descricao { get; set; }

        // Propriedade que armazena o caminho da imagem associada à receita.
        public string ImagemCaminho { get; set; }
    }
}
