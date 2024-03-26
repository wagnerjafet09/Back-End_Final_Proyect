using Biblioteca.DAL.Context;
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly BibliotecaContext context;
        private readonly ILogger<UsuarioRepository> logger;

        public UsuarioRepository(BibliotecaContext context, ILogger<UsuarioRepository> logger) : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public override Task<List<Usuario>> GetAll()
        {
            return base.GetAll();
        }

        public async Task<bool> RegistroUsuario(Usuario usuarios)
        {
            try
            {
                await context.Usuarios.AddAsync(usuarios);
                await this.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error Registrando el Usuario {ex.Message}");
                return false;
            }
        }

        public async Task<Usuario> LoginUsuario(Usuario loginRequest)
        {
            Usuario loginexitoso = new Usuario();
            if (loginRequest.Nombre != null && loginRequest.Contraseña != null)
            {
                var usuarios = await context.Usuarios.FirstOrDefaultAsync(U => U.Nombre == loginRequest.Nombre && U.Contraseña == loginRequest.Contraseña);

                if (usuarios != null)
                {
                    loginexitoso = usuarios;
                    return loginexitoso;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
