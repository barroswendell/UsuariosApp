using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Validations
{
    /// <summary>
    /// Classe para validação de dados do usuário
    /// </summary>
    public class UsuarioValidator : AbstractValidator<Usuario>

    {
        /// <summary>
        /// Método Construtor da classe
        /// </summary>
        public UsuarioValidator()

        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("O Id é obrigatório");

            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(6, 150).WithMessage("O Nome deve ter entre 6 e 150 caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O Email do usuário é obrigatório")
                .EmailAddress().WithMessage("O Email deve ser um endereço válido");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A Senha é obrigatória")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("A Senha deve conter pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial");

            RuleFor(u => u.DataHoraCriacao)
                .NotEmpty().WithMessage("A Data e Hora de Criação é obrigatória");
        }
    }
}
