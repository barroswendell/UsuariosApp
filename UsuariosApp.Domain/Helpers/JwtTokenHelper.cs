using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Helpers
{
    /// <summary>
    /// Classe auxilizar para geração dos TOKENS JWT de autenticação dos usuários
    /// </summary>
    public class JwtTokenHelper
    {
        /// <summary>
        /// Método para retornar a data e hora de expiração da autenticação do usuário
        /// </summary>
        public static DateTime? GetExpirationDate()
        {
            return DateTime.UtcNow.AddHours(1); //1 hora a partir da data atual
        }

        /// <summary>
        /// Método para gerar e retornar o TOKEN JWT do usuário autenticado
        /// </summary>
        public static string? GetAccessToken(string email)
        {
            //gerando a chave para criptografar / assinar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("0854E15A-A4D7-4F5B-BC06-9BF9DC68F6A2");

            //escrevendo o conteúdo do TOKEN
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //identificação do usuário autenticado (email)
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, email) }),

                //definir a data de expiração do token
                Expires = GetExpirationDate(),

                //chave para criptografia / assinatura do token
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //gerando e retornando o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}



