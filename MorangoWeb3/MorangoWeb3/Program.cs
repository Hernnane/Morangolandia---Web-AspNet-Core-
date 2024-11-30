using FluentValidation;  // Importa o FluentValidation, usado para validação de modelos
using FluentValidation.AspNetCore;  // Importa as extensões do FluentValidation para ASP.NET Core
using Microsoft.AspNetCore.Authentication.Cookies;  // Importa a autenticação baseada em cookies
using Microsoft.EntityFrameworkCore;  // Importa o Entity Framework Core, usado para interagir com o banco de dados
using Microsoft.Extensions.Options;  // Importa para configurar opções de serviços
using MorangoWeb3.Data;  // Importa o contexto de dados da aplicação
using MorangoWeb3.Models;  // Importa os modelos de dados da aplicação
using MorangoWeb3.Services.CurtidasServices;  // Importa os serviços para gerenciar curtidas
using MorangoWeb3.Services.LoginServices;  // Importa os serviços para gerenciar login
using MorangoWeb3.Services.PerfilServices;  // Importa os serviços para gerenciar perfis de usuários
using MorangoWeb3.Services.ReceitaServices;  // Importa os serviços para gerenciar receitas
using MorangoWeb3.Services.SalvamentosServices;  // Importa os serviços para gerenciar salvamentos
using MorangoWeb3.Services.UsuarioServices;  // Importa os serviços para gerenciar usuários
using MorangoWeb3.Validators;  // Importa os validadores personalizados

var builder = WebApplication.CreateBuilder(args);  // Cria um construtor para a aplicação web

// Adiciona os serviços ao container de injeção de dependência
builder.Services.AddControllersWithViews();  // Registra os controladores e as views na aplicação

// Adiciona os serviços de validação com FluentValidation
builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<UsuariosModelValidator>();  // Registra validadores para o modelo de usuários
});

builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<ReceitasModelValidator>();  // Registra validadores para o modelo de receitas
});

builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>();  // Registra validadores para o modelo de login
});

// Adiciona o serviço de autenticação por cookie
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  // Define o esquema de autenticação baseado em cookie
    .AddCookie((options) =>
    {
        options.LoginPath = "/login";  // Define o caminho para login quando a autenticação for necessária
    });

// Configuração do banco de dados com string de conexão
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  // Configura o uso do SQL Server com a string de conexão
});

// Injeção de Dependência para as interfaces e seus repositórios
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();  // Injeção do repositório de usuários
builder.Services.AddScoped<IReceitasRepositorio, ReceitasRepositorio>();  // Injeção do repositório de receitas
builder.Services.AddScoped<ICurtidasRepositorio, CurtidasRepositorio>();  // Injeção do repositório de curtidas
builder.Services.AddScoped<ISalvamentosRepositorio, SalvamentosRepositorio>();  // Injeção do repositório de salvamentos
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();  // Injeção do repositório de login
builder.Services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();  // Injeção do repositório de perfis

var app = builder.Build();  // Cria a aplicação a partir do builder

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())  // Verifica se não está em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error");  // Define um manipulador de exceções para produção
    // O valor padrão do HSTS (Strict Transport Security) é 30 dias. Você pode querer alterar isso para cenários de produção.
    app.UseHsts();  // Habilita o HSTS (segurança em conexões HTTPS)
}

app.UseHttpsRedirection();  // Redireciona todas as requisições HTTP para HTTPS
app.UseStaticFiles();  // Habilita o uso de arquivos estáticos (como imagens, CSS, JavaScript)

app.UseRouting();  // Configura o roteamento de URLs para os controladores

app.UseAuthorization();  // Adiciona a autorização para controlar o acesso aos recursos
app.UseAuthentication();  // Adiciona a autenticação para identificar os usuários

// Mapeia a rota padrão
app.MapControllerRoute(
    name: "default",  // Nome da rota
    pattern: "{controller=SimplePages}/{action=Index}/{id?}");  // Padrão para a URL da aplicação (controlador, ação, id opcional)

app.Run();  // Executa a aplicação
