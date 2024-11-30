using FluentValidation;
using MorangoWeb3.Models;
using MorangoWeb3.Services.ReceitaServices;

namespace MorangoWeb3.Validators
{
    // Validador para o modelo ReceitasModel usando FluentValidation
    public class ReceitasModelValidator : AbstractValidator<ReceitasModel>
    {
        // Repositório de receitas injetado para interagir com os dados de receita
        private readonly IReceitasRepositorio _receitasRepositorio;

        // Construtor que recebe o repositório de receitas
        public ReceitasModelValidator(IReceitasRepositorio receitasRepositorio)
        {
            _receitasRepositorio = receitasRepositorio;

            // Validação para o campo Titulo
            RuleFor(x => x.Titulo)
                .Length(3, 30).WithMessage("O título deve ter entre 3 e 30 caracteres.");

            // Validação para o campo Tipo
            RuleFor(x => x.Tipo)
                .Length(3, 20).WithMessage("O tipo da receita deve ter entre 3 a 20 caracteres.");

            // Validação para o campo Nivel
            RuleFor(x => x.Nivel)
                .NotEmpty().WithMessage("Insira o nível da receita (de 0 a 10).") // Garante que o nível não esteja vazio
                .LessThan(11).WithMessage("Digite um número de 0 a 10."); // Garante que o nível seja menor que 11

            // Validação para o campo Ingredientes
            RuleFor(x => x.Ingredientes)
                .Length(3, 200).WithMessage("Os ingredientes devem ter entre 3 a 200 caracteres");

            // Validação para o campo Descricao
            RuleFor(x => x.Descricao)
                .MinimumLength(10).WithMessage("A descrição da receita deve ter pelo menos 10 caracteres.");
        }
    }
}
