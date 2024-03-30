
using Biblioteca.BL.Contract;
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Usuario;
using Biblioteca.BL.Extentions.Usuarios;
using Biblioteca.BL.Validations;
using Biblioteca.DAL.Context;
using Biblioteca.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Biblioteca.BL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly BibliotecaContext context;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILogger<UsuarioService> logger;

        public UsuarioService(BibliotecaContext context, IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger)
        {
            this.context = context;
            this.usuarioRepository = usuarioRepository;
            this.logger = logger;
        }
        public async Task<ServiceResult> GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.usuarioRepository.GetAll();
                result.Success = true;
                result.Message = "Usuarios Encontrados Exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Obteniendo los Usuarios";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.usuarioRepository.GetByID(id);
                result.Success = true;
                result.Message = "Usuario Encontrado Exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Obteniendo el Usuario ";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> LoginUsuario(UsuarioLoginDto loginDto)
        {
            ServiceResult result = new ServiceResult();
            var valida = new Validaciones(context);
            try
            {
                //ValidarVACIO//
                var ValidarVaciosNombreUsuario = valida.ValidarVacios(loginDto.NombreUsuario, "Nombre");
                if (!ValidarVaciosNombreUsuario.Success)
                    return ValidarVaciosNombreUsuario;
                var ValidarVaciosContraseña = valida.ValidarVacios(loginDto.Contraseña, "Contraseña");
                if (!ValidarVaciosContraseña.Success)
                    return ValidarVaciosContraseña;
                //ValidarCredenciales//
                var ValidarCredenciales = valida.ValidarCredenciales(loginDto.NombreUsuario, loginDto.Contraseña);
                if (!ValidarCredenciales.Success)
                    return ValidarCredenciales;

                var loginExitoso = await this.usuarioRepository.LoginUsuario(loginDto.ConvertUsuarioLoginDtoToEntity());
                result.Success = true;
                result.Message = "Inicio de sesión exitoso";
                result.Data = loginExitoso;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Logeando el Usuario";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> RegistroUsuario(UsuarioResgistroDto resgistroDto)
        {
            ServiceResult result = new ServiceResult();
            var valida = new Validaciones(context);
            try
            {
                //VALIDACIONES//
                var ValidarNombreUsuario = valida.ValidarNombreUsuario(resgistroDto.NombreUsuario);
                if (!ValidarNombreUsuario.Success)
                    return ValidarNombreUsuario;
                var ValidarLongitud = valida.ValidarLongitud(resgistroDto.Contraseña, "Contraseña", 15, 5);
                if (!ValidarLongitud.Success)
                    return ValidarLongitud;
                var ValidarCorreo = valida.ValidarCorreo(resgistroDto.CorreoElectronico);
                if (!ValidarCorreo.Success)
                    return ValidarCorreo;
                var ValidarNombreReal = valida.SoloLetras(resgistroDto.Nombre, "Nombre");
                if (!ValidarNombreReal.Success)
                    return ValidarNombreReal;
                var ValidarApellidoReal = valida.SoloLetras(resgistroDto.Nombre, "Apellido");
                if (!ValidarApellidoReal.Success)
                    return ValidarApellidoReal;
                //REGISTRO//

                if (await this.usuarioRepository.RegistroUsuario(resgistroDto.ConvertUsuarioRegistroDtoToEntity()))
                {
                    result.Success = true;
                    result.Message = "Usuario Registrado Exitosamente";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Ocurrio un Error Registrando el Usuario";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Registrando el Usuario";
                logger.LogError(ex.Message);
            }
            return result;
        }
    }
}
