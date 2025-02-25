﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos
{
    /// <summary>
    /// DTO para a resposta (saída de dados) do serviço de autenticação de usuário
    /// </summary>
    public class AutenticarUsuarioResponseDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAcesso { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
        public string? Token { get; set; }
    }
}



