using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<bool> RegistroUsuario(Usuario usuarios);

        Task<Usuario> LoginUsuario(Usuario loginRequest);
    }
}
