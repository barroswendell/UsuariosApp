using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// interface para aplicação dos métodos de repositório de usuário
    /// </summary>
    public interface IUsuarioRepository

    {
        //Método para gravar um novo usuário
        void insert (Usuario usuario);

        //Método para buscar um usuário pelo email e senha
        Usuario? Find(string email, string senha);

        //Método para consultar se existe um usuário com o email informado
        bool Exists(string email);

        //Método para consultar um usuario pelo email
        Usuario? Find(string email);


    }
}
