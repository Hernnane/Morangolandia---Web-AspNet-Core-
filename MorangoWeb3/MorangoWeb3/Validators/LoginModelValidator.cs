using FluentValidation;
using MorangoWeb3.Models;
using MorangoWeb3.Services.LoginServices;

namespace MorangoWeb3.Validators
{
    // Validador para o modelo LoginModel usando FluentValidation
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        // Repositório de login injetado para interagir com os dados de login
        private readonly ILoginRepositorio _loginRepositorio;

        // Construtor que recebe o repositório de login
        public LoginModelValidator(ILoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;

            // Validação para o campo UsuarioLogin
            RuleFor(x => x.UsuarioLogin)
                .NotEmpty().WithMessage("Insira seu usuário para fazer o login.") // Valida que o campo não pode ser vazio
                .Length(3, 100).WithMessage("Usuário inválido."); // Valida o comprimento mínimo e máximo do campo

            // Validação para o campo SenhaLogin
            RuleFor(x => x.SenhaLogin)
                .NotEmpty().WithMessage("Insira a sua senha.") // Valida que o campo não pode ser vazio
                .Length(3, 100).WithMessage("Usuário inválido."); // Valida o comprimento mínimo e máximo do campo
        }
    }
}
