using Biblioteca.BL.Core;
using Biblioteca.DAL.Context;
using System.Text.RegularExpressions;

namespace Biblioteca.BL.Validations
{
    public class Validaciones
    {
        private readonly BibliotecaContext context;

        public Validaciones(BibliotecaContext context)
        {
            this.context = context;
        }

        public ServiceResult ValidarVacios(string campo, string nombreCampo)
        {
            var result = new ServiceResult();

            if (string.IsNullOrWhiteSpace(campo))
            {
                result.Success = false;
                result.Message = $"El campo {nombreCampo} no puede estar vacío";
            }

            return result;
        }

        public ServiceResult ValidarLongitud(string datos, string nombreCampo, int maximo, int minimo)
        {
            var result = new ServiceResult();

            if (!string.IsNullOrEmpty(datos))
            {
                if (datos.Length > maximo || datos.Length < minimo)
                {
                    result.Success = false;
                    result.Message = $"Debe ser entre {minimo} y {maximo} caracteres para {nombreCampo}";
                }
            }

            return result;
        }

        public ServiceResult ValidarCorreo(string datos)
        {
            var result = new ServiceResult();
            var regex = new Regex("^[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,4}$");

            if (!string.IsNullOrEmpty(datos))
            {
                if (!regex.IsMatch(datos))
                {
                    result.Success = false;
                    result.Message = "La dirección de correo es incorrecta";
                }

                var correosElectronicosExiste = context.Usuarios.Any(b => b.CorreoElectronico == datos);
                if (correosElectronicosExiste)
                {
                    result.Success = false;
                    result.Message = "La dirección de correo ya está en uso";
                }
            }

            return result;
        }

        public ServiceResult SoloLetras(string datos, string nombreCampo)
        {
            var result = new ServiceResult();
            var regex = new Regex("^[a-zA-Z]*$");

            if (!string.IsNullOrEmpty(datos))
            {
                if (!regex.IsMatch(datos))
                {
                    result.Success = false;
                    result.Message = $"Solo se admiten letras para el campo de {nombreCampo}";
                }
            }

            return result;
        }

        public ServiceResult SoloNumeros(string datos)
        {
            var result = new ServiceResult();
            var regex = new Regex("^[0-9]*$");

            if (!string.IsNullOrEmpty(datos))
            {
                if (!regex.IsMatch(datos))
                {
                    result.Success = false;
                    result.Message = "Solo se admiten números para la contraseña";
                }
            }

            return result;
        }

        public ServiceResult ValidarNombreUsuario(string nombreUsuario)
        {
            var result = new ServiceResult();

            var nombreUsuarioExiste = context.Usuarios.FirstOrDefault(b => b.NombreUsuario == nombreUsuario);
            if (nombreUsuarioExiste != null)
            {
                result.Success = false;
                result.Message = "El nombre de usuario ya está en uso";
            }

            return result;
        }
        public ServiceResult ValidarCredenciales(string nombreusuario, string contraseña)
        {
            var result = new ServiceResult();
            var contraseñaExiste = context.Usuarios.FirstOrDefault(b => b.Contraseña == contraseña);
            if (contraseñaExiste == null)
            {
                result.Success = false;
                result.Message = "Contraseña Incorrecta";
            }
            var nombreUsuarioExiste = context.Usuarios.FirstOrDefault(b => b.NombreUsuario == nombreusuario);
            if (nombreUsuarioExiste == null)
            {
                result.Success = false;
                result.Message = "Nombre de Usuario Incorrecto";
            }
            return result;
        }
    }
}
