using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using UsuariosApp.Domain.Dtos;

namespace UsuariosApp.Tests
{
    /// <summary>
    /// Classe de testes para os serviços de usuario da API
    /// </summary>
    public class UsuariosTest

    {
        [Fact]
        public void Criar_Usuario_Com_Sucesso_Test()
        {
            //Instanciando a classe para geração dos dados FAKE (bogus)
            var faker = new Faker("pt_BR");

            //Criando um Dto para enviar a requisição de cadastro de usuario 
            var resquest = new CriarUsuarioResquetDto
            {
                Nome = faker.Person.FullName, //Gerando um nome completo do usuario
                Email = faker.Person.Email, //Gerando um email do usuario
                Senha = "@Teste123" //Gerando uma senha para o usuario
            };

            //Serializando os dados do objeto DTO para o formato JSON
            var jsonResquest = new StringContent(JsonConvert.SerializeObject(resquest),
                Encoding.UTF8, "application/json");

            //Executar a API para criar um novo usuario
            var client = new WebApplicationFactory<Program>().CreateClient();

            //Enviando uma requisição POST para o ENDPOINT de criação de usuario
            var result = client.PostAsync("/api/usuarios/criar", jsonResquest).Result;

            //Verificando se o resultado obteve status HTTP 201 (Created)
            result.StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            //Ler o conteudo da resposta em json
            var json = result.Content.ReadAsStringAsync().Result;

            //Deserializar o conteudo da resposta para um objeto Dto
            var response = JsonConvert.DeserializeObject<CriarUsuarioResponseDto>(json);

            //Aplicando as verificações de teste
            response.Id.Should().NotBeEmpty();//verificando se o ID do usuario foi gerado
            response.Nome.Should().Be(resquest.Nome);//verificando se o nome do usuario foi retornado corretamente
            response.Email.Should().Be(resquest.Email);//verificando se o email do usuario foi retornado corretamente
            response.DataHoraCriacao.Should().NotBeNull();//verificando se a data de criação do usuario foi gerada corretamente


        }

        [Fact]
        public void Auntenticar_Usuario_Com_Sucesso_Test()

        {
            #region Criar um usuário na API
            //Instanciando a classe para geração dos dados FAKE (bogus)
            var faker = new Faker("pt_BR");

            //Criando um Dto para enviar a requisição de cadastro de usuario
            var criarUsuarioResques = new CriarUsuarioResquetDto
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Senha = "@Teste123"
            };

            //Serializando os dados do objeto DTO para o formato JSON
            var jsonCriarUsuarioResquest = new StringContent(JsonConvert.SerializeObject(criarUsuarioResques),
                Encoding.UTF8, "application/json");

            //Executar a API para criar um novo usuario
            var client = new WebApplicationFactory<Program>().CreateClient();
            var resultCriarUsuario = client.PostAsync("/api/usuarios/criar", jsonCriarUsuarioResquest).Result;
            #endregion

            #region Autenticar o usuário

            //Criando um Dto para enviar a requisição de autenticação de usuario
            var autenticarUsuarioResquest = new AutenticarUsuarioRequestDto
            {
                Email = criarUsuarioResques.Email,
                Senha = criarUsuarioResques.Senha
            };

            //Serializando os dados do objeto DTO para o formato JSON
            var jsonAutenticarUsuarioResquest = new StringContent(JsonConvert.SerializeObject(autenticarUsuarioResquest),
                Encoding.UTF8, "application/json");

            //Enviando uma requisição POST para o ENDPOINT de autenticação de usuario
            var resultAutenticarUsuario = client.PostAsync("/api/usuarios/autenticar", jsonAutenticarUsuarioResquest).Result;

            //Verificando se o resultado obteve status HTTP 200 (OK)
            resultAutenticarUsuario.StatusCode.Should().Be(HttpStatusCode.OK);

            //Ler o conteudo da resposta em json
            var json = resultAutenticarUsuario.Content.ReadAsStringAsync().Result;

            //Deserializar o conteudo da resposta para um objeto Dto
            var response = JsonConvert.DeserializeObject<AutenticarUsuarioResponseDto>(json);

            //Aplicando as verificações de teste
            response.Token.Should().NotBeNull();//verificando se o token de autenticação foi gerado
            response.Id.Should().NotBeNull();//verificando se o ID do usuario foi retornado corretamente
            response.Nome.Should().Be(criarUsuarioResques.Nome);//verificando se o nome do usuario foi retornado corretamente
            response.Email.Should().Be(criarUsuarioResques.Email);//verificando se o email do usuario foi retornado corretamente
            response.DataHoraAcesso.Should().NotBeNull();//verificando se a data de acesso do usuario foi gerada corretamente
            response.DataHoraExpiracao.Should().NotBeNull();//verificando se a data de expiração do token foi gerada corretamente


            #endregion
        }
    }
}
