using Biblioteca.DAL.Context;
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public override async Task<List<Usuario>> GetAll()
        {

            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                usuarios = await base.GetAll();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error Obteniendo los usuarios {ex.Message}");
            }
            return usuarios;
        }
        public override async Task<Usuario> GetByID(int id)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario = await base.GetByID(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error Obteniendo el usuario {ex.Message}");
            }
            return usuario;
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
                var usuarios = await context.Usuarios.FirstOrDefaultAsync(U => U.NombreUsuario == loginRequest.NombreUsuario && U.Contraseña == loginRequest.Contraseña);

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
