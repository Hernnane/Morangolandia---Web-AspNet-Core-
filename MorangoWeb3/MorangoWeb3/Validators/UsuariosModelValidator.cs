using FluentValidation;
using MorangoWeb3.Models;
using MorangoWeb3.Services.UsuarioServices;

namespace MorangoWeb3.Validators
{
    // Validador para o modelo UsuariosModel usando FluentValidation
    public class UsuariosModelValidator : AbstractValidator<UsuariosModel>
    {
        // Repositório de usuários injetado para interagir com os dados de usuários
        private readonly IUsuariosRepositorio _usuarioRepositorio;

        // Construtor que recebe o repositório de usuários
        public UsuariosModelValidator(IUsuariosRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;

            // Validação para o campo Nome
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.") // Garante que o nome não esteja vazio
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres."); // Garante que o nome tenha entre 3 e 100 caracteres

            // Validação para o campo Usuario
            RuleFor(x => x.Usuario)
                .NotEmpty().WithMessage("O usuário é obrigatório.") // Garante que o usuário não esteja vazio
                .Length(3, 50).WithMessage("O nome de usuário deve ter entre 3 e 50 caracteres."); // Garante que o nome de usuário tenha entre 3 e 50 caracteres

            // Validação para o campo Idade
            RuleFor(x => x.Idade)
                .NotEmpty().WithMessage("A idade é obrigatória.") // Garante que a idade não esteja vazia
                .LessThan(120).WithMessage("Insira uma idade válida."); // Garante que a idade seja menor que 120

            // Validação para o campo Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O E-mail é obrigatório.") // Garante que o e-mail não esteja vazio
                .EmailAddress().WithMessage("Insira um E-mail válido."); // Garante que o e-mail seja válido

            // Validação para o campo Senha
            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.") // Garante que a senha não esteja vazia
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.") // Garante que a senha tenha pelo menos 8 caracteres
                .MaximumLength(20).WithMessage("A senha deve ter no máximo 20 caracteres.") // Garante que a senha tenha no máximo 20 caracteres
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.") // Garante que a senha tenha pelo menos uma letra maiúscula
                .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.") // Garante que a senha tenha pelo menos uma letra minúscula
                .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.") // Garante que a senha tenha pelo menos um número
                .WithMessage("No mínimo 8 caracteres contendo: letra minúscula, letra maiúscula e número"); // Mensagem geral se a senha não cumprir as regras
        }
    }
}
