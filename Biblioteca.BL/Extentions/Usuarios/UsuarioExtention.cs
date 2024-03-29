
using Biblioteca.BL.Dto.Usuario;
using Biblioteca.DAL.Entities;

namespace Biblioteca.BL.Extentions.Usuarios
{
    public static class UsuarioExtention
    {
        public static Usuario ConvertUsuarioLoginDtoToEntity(this UsuarioLoginDto usuarioLoginDto)
        {
            return new Usuario
            {
                NombreUsuario = usuarioLoginDto.NombreUsuario,
                Contraseña = usuarioLoginDto.Contraseña
            };
        }
        public static Usuario ConvertUsuarioRegistroDtoToEntity(this UsuarioResgistroDto usuarioResgistroDto)
        {
            return new Usuario
            {
                NombreUsuario = usuarioResgistroDto.NombreUsuario,
                Contraseña = usuarioResgistroDto.Contraseña,
                Apellido = usuarioResgistroDto.Apellido,
                CorreoElectronico = usuarioResgistroDto.CorreoElectronico,
                Nombre = usuarioResgistroDto.Nombre,
                EsAdmin = usuarioResgistroDto.EsAdmin

            };
        }
    }
}
