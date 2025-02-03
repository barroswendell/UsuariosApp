using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe para mapeamento da entidade usuario no banco de dados
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Nome da tabela no banco de dados
            builder.ToTable("USUARIO");

            //Chave primária
            builder.HasKey(u => u.Id);

            //Mapeamento de cada campo individualmente
            builder.Property(u => u.Id)
                .HasColumnName("ID");
               
                builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            //Regra para não permitir email duplicado
            builder.HasIndex(u => u.Email)//indice para o campo Email
                .IsUnique();    //Tornar campo unico na tabela do banco de dados

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.DataHoraCriacao)
                .HasColumnName("DATAHORACRIACAO")
                .IsRequired();
        }

    }
}
