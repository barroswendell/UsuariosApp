using System;
using System.Security.Cryptography;
using System.Text;

namespace UsuariosApp.Domain.Helpers
{
    /// <summary>
    /// Classe para funções de criptografia de dados
    /// </summary>
    public class CryptoHelper
    {  
        public static string SHA256Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "O valor não pode ser nulo ou vazio.");

            using (var sha256 = SHA256.Create())
            {
                var valueBytes = Encoding.UTF8.GetBytes(value);
                var hashBytes = sha256.ComputeHash(valueBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}



