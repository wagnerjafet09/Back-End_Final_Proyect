

namespace Biblioteca.BL.Dto.Usuario
{
    public class UsuarioResgistroDto
    {
        public string? NombreUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Contraseña { get; set; }
        public bool? EsAdmin { get; set; }
    }
}
