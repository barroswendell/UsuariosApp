using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UsuariosApp.Domain.Dtos;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Validations;

namespace UsuariosApp.Domain.Services
{
    /// <summary>
    /// Classe para implementar os métodos da interface de serviço de domínio
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //Atributo
        private readonly IUsuarioRepository _usuarioRepository;


        //Metedo construtor para injeção de dependência
        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public AutenticarUsuarioResponseDto AutenticarUsuario(AutenticarUsuarioRequestDto request)
        {
            //Cripotografar a senha do usuário
            request.Senha = CryptoHelper.SHA256Encrypt(request.Senha);

            //Buscar o usuário no banco de dados
            var usuario = _usuarioRepository.Find(request.Email, request.Senha);

            //Verificar se o usuário foi encontrado
            if ( usuario == null )
                throw new ApplicationException("Acesso negado. Usuário não encontrado");

            //Retornar os dados do DTO de resposta
            var response = new AutenticarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraAcesso = DateTime.Now,
                DataHoraExpiracao = JwtTokenHelper.GetExpirationDate(),
                Token = JwtTokenHelper.GetAccessToken(usuario.Email)

            };

            return response;

        }

        public CriarUsuarioResponseDto CriarUsuario(CriarUsuarioResquetDto request)
        {
            //Criar um objeto da classe usuário
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),//Gerar um novo Guid
                Nome = request.Nome,//Capturando o nome do usuario
                Email = request.Email,//Capturando o email do usuario
                Senha = request.Senha,//Capturando a senha do usuario
                DataHoraCriacao = DateTime.Now
            };

            //Executar a validação do objeto usuário

            var validation = new UsuarioValidator();
            var result = validation.Validate(usuario);

            //Verificar se a validação falhou

            if (!result.IsValid)
            {
                //Caso a validação falhe, retornar uma exceção com a mensagem de erro
                throw new ValidationException(result.Errors);
            }

            //Verificar se ja existe um usuario com o email informado
            if (_usuarioRepository.Exists(usuario.Email))
            {
                //Caso exista, retornar uma exceção com a mensagem de erro
                throw new ApplicationException("Já existe um usuário com o email informado");
            }

            //Cripotografar a senha do usuário
            usuario.Senha = CryptoHelper.SHA256Encrypt(usuario.Senha);

            //Gravar o usuário no banco de dados
           _usuarioRepository.insert(usuario);

            //Montar o Dto com os dados de resposta
            var response = new CriarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCriacao = usuario.DataHoraCriacao
            };
            //Retornar o Dto de resposta
            return response;
        }

        public MinhaContaResponseDto MinhaConta(string email)
        {
            //Buscar os dados do usuário através do email
            var usuario = _usuarioRepository.Find(email);

            //Verificar se o usuário foi encontrado
            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado Verifique o email informado");

            //Montar o Dto de resposta
            var response = new MinhaContaResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };

            //Retornar o Dto de resposta
            return response;
        }
    }
}
