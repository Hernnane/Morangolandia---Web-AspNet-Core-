using FluentValidation;
using MorangoWeb3.Models;
using MorangoWeb3.Services.PerfilServices;

namespace MorangoWeb3.Validators
{
    // Validador para o modelo MeuPerfilModel usando FluentValidation
    public class PerfilModelValidator : AbstractValidator<MeuPerfilModel>
    {
        // Repositório de perfil injetado para interagir com os dados de perfil
        private readonly IPerfilRepositorio _perfilRepositorio;

        // Construtor que recebe o repositório de perfil
        public PerfilModelValidator(IPerfilRepositorio perfilRepositorio)
        {
            _perfilRepositorio = perfilRepositorio;

            // Validação para o campo Usuario
            RuleFor(x => x.Usuario)
                .NotEmpty().WithMessage("Insira um novo nome de usuário para fazer a alteração.") // Garante que o nome de usuário não seja vazio
                .Length(3, 100).WithMessage("Nome de usuário inválido."); // Garante que o nome de usuário tenha entre 3 e 100 caracteres
        }
    }
}
