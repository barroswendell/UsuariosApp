using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Dtos;

namespace UsuariosApp.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface para aplicação dos métodos de serviço de domínio de usuário
    /// </summary>
    public interface IUsuarioDomainService

    {
        CriarUsuarioResponseDto CriarUsuario(CriarUsuarioResquetDto request);

        AutenticarUsuarioResponseDto AutenticarUsuario(AutenticarUsuarioRequestDto request);

        MinhaContaResponseDto MinhaConta(string email);

    }
}
