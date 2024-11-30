using FluentValidation;  // Importa o FluentValidation, usado para valida��o de modelos
using FluentValidation.AspNetCore;  // Importa as extens�es do FluentValidation para ASP.NET Core
using Microsoft.AspNetCore.Authentication.Cookies;  // Importa a autentica��o baseada em cookies
using Microsoft.EntityFrameworkCore;  // Importa o Entity Framework Core, usado para interagir com o banco de dados
using Microsoft.Extensions.Options;  // Importa para configurar op��es de servi�os
using MorangoWeb3.Data;  // Importa o contexto de dados da aplica��o
using MorangoWeb3.Models;  // Importa os modelos de dados da aplica��o
using MorangoWeb3.Services.CurtidasServices;  // Importa os servi�os para gerenciar curtidas
using MorangoWeb3.Services.LoginServices;  // Importa os servi�os para gerenciar login
using MorangoWeb3.Services.PerfilServices;  // Importa os servi�os para gerenciar perfis de usu�rios
using MorangoWeb3.Services.ReceitaServices;  // Importa os servi�os para gerenciar receitas
using MorangoWeb3.Services.SalvamentosServices;  // Importa os servi�os para gerenciar salvamentos
using MorangoWeb3.Services.UsuarioServices;  // Importa os servi�os para gerenciar usu�rios
using MorangoWeb3.Validators;  // Importa os validadores personalizados

var builder = WebApplication.CreateBuilder(args);  // Cria um construtor para a aplica��o web

// Adiciona os servi�os ao container de inje��o de depend�ncia
builder.Services.AddControllersWithViews();  // Registra os controladores e as views na aplica��o

// Adiciona os servi�os de valida��o com FluentValidation
builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<UsuariosModelValidator>();  // Registra validadores para o modelo de usu�rios
});

builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<ReceitasModelValidator>();  // Registra validadores para o modelo de receitas
});

builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>();  // Registra validadores para o modelo de login
});

// Adiciona o servi�o de autentica��o por cookie
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  // Define o esquema de autentica��o baseado em cookie
    .AddCookie((options) =>
    {
        options.LoginPath = "/login";  // Define o caminho para login quando a autentica��o for necess�ria
    });

// Configura��o do banco de dados com string de conex�o
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  // Configura o uso do SQL Server com a string de conex�o
});

// Inje��o de Depend�ncia para as interfaces e seus reposit�rios
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();  // Inje��o do reposit�rio de usu�rios
builder.Services.AddScoped<IReceitasRepositorio, ReceitasRepositorio>();  // Inje��o do reposit�rio de receitas
builder.Services.AddScoped<ICurtidasRepositorio, CurtidasRepositorio>();  // Inje��o do reposit�rio de curtidas
builder.Services.AddScoped<ISalvamentosRepositorio, SalvamentosRepositorio>();  // Inje��o do reposit�rio de salvamentos
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();  // Inje��o do reposit�rio de login
builder.Services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();  // Inje��o do reposit�rio de perfis

var app = builder.Build();  // Cria a aplica��o a partir do builder

// Configura o pipeline de requisi��es HTTP
if (!app.Environment.IsDevelopment())  // Verifica se n�o est� em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error");  // Define um manipulador de exce��es para produ��o
    // O valor padr�o do HSTS (Strict Transport Security) � 30 dias. Voc� pode querer alterar isso para cen�rios de produ��o.
    app.UseHsts();  // Habilita o HSTS (seguran�a em conex�es HTTPS)
}

app.UseHttpsRedirection();  // Redireciona todas as requisi��es HTTP para HTTPS
app.UseStaticFiles();  // Habilita o uso de arquivos est�ticos (como imagens, CSS, JavaScript)

app.UseRouting();  // Configura o roteamento de URLs para os controladores

app.UseAuthorization();  // Adiciona a autoriza��o para controlar o acesso aos recursos
app.UseAuthentication();  // Adiciona a autentica��o para identificar os usu�rios

// Mapeia a rota padr�o
app.MapControllerRoute(
    name: "default",  // Nome da rota
    pattern: "{controller=SimplePages}/{action=Index}/{id?}");  // Padr�o para a URL da aplica��o (controlador, a��o, id opcional)

app.Run();  // Executa a aplica��o
