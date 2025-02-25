﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Entities
{
    /// <summary>
    /// Modelo de Entidade de Usuário
    /// </summary>
    public class Usuario

    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataHoraCriacao { get; set; }

    }
}
