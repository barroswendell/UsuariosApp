using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configuração para formatar os endpoints da API em caixa baixa
builder.Services.AddRouting(config => config.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configurar as injeções de dependência do projeto
builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

//configuração de autenticação para que o projeto API seja capaz de
//receber e validar tokens no padrão JWT (JSON WEB TOKEN)
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //JWT Bearer
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //JWT Bearer
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true, //verificar se o token não expirou
                                 //comparando se o TOKEN que foi recebido pela API usa a mesma chave de criptografia
                                 //(assinatura) utilizada para criação dos tokens desta API
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes("0854E15A-A4D7-4F5B-BC06-9BF9DC68F6A2"))
    };
});

//Criando a configuração do CORS para dar permissão ao projeto Angular
builder.Services.AddCors(
    config => config.AddPolicy("DefaultPolicy", builder => {
        builder.WithOrigins("http://localhost:4200") //servidor do angular
            .AllowAnyMethod() //permissão para todos os métodos (POST, PUT, DELETE, GET etc)
            .AllowAnyHeader(); //permissão para enviar parametros de cabeçalho das requisições
    })
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Aplicando a política de CORS
app.UseCors("DefaultPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//Tornando a classe Program pública. Para que o projeto de testes do XUnit
//possa executar a API e testar os seus ENDPOINTS.
public partial class Program { }


