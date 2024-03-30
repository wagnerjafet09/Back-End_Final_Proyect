
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Usuario;

namespace Biblioteca.BL.Contract
{
    public interface IUsuarioService : IBaseService
    {
        Task<ServiceResult> RegistroUsuario(UsuarioResgistroDto resgistroDto);
        Task<ServiceResult> LoginUsuario(UsuarioLoginDto loginDto);

    }
}
