using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos
{
    /// <summary>
    /// DTO para requisição de autenticação de usuario.
    /// </summary>
    public class AutenticarUsuarioRequestDto

    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
