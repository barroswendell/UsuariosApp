using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação da interface de repositório do usuário
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {


        public Usuario? Find(string email, string senha)
        {
            //abrindo conexão com o banco de dados
            using (var dataContext = new DataContext())
            {
                //SQL: SELECT * FROM USUARIO WHERE EMAIL = @EMAIL AND SENHA = @SENHA
                return dataContext
                    .Set<Usuario>() //tabela de usuários do banco de dados
                    .Where(u => u.Email.Equals(email) && u.Senha.Equals(senha)) //filtro
                    .FirstOrDefault(); //retornar o primeiro registro encontrado ou vazio
            }
        }

        public bool Exists(string email)
        {
            //Abrindo conexão com o banco de dados
            using (var dataContext = new DataContext())
            {
                //SQL: SELECT COUNT(*) FROM USUARIO WHERE EMAIL = @EMAIL
                return dataContext
                    .Set<Usuario>() //tabela de usuários do banco de dados
                    .Any(u => u.Email.Equals(email)); //verificar se existe algum registro
            }
        }

        public void insert(Usuario usuario)
        {
            //abrindo conexão com o banco de dados
            using (var dataContext = new DataContext())
            {
                dataContext.Add(usuario); //inserir o usuário no banco de dados
                //dataContext.Update(usuario); //atualizar o usuário
                //dataContext.Remove(usuario); //excluir o usuário
                dataContext.SaveChanges(); //executando a operação
            }
        }

        public Usuario? Find(string email)
        {
            //abrindo conexão com o banco de dados
            using (var dataContext = new DataContext())
            {
                //SQL: SELECT * FROM USUARIO WHERE EMAIL = @EMAIL
                return dataContext
                    .Set<Usuario>()
                    .Where(u => u.Email.Equals(email))
                    .FirstOrDefault();
            }
        }
    }
}



