using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Data.Mappings;

namespace UsuariosApp.Infra.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para configuração do EntityFramework
    /// </summary>
    public class DataContext : DbContext
    {
        //Método para configurarmos a connectionstring do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //definir o caminho do banco de dados
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;User ID=sa;Password=Coti@2025;Encrypt=False");
        }

        //Método para configurarmos cada classe de mapeamento do projeto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //incluir cada classe de mapeamento do projeto
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}



