
namespace Biblioteca.BL.Dto.Libro
{
    public class CrearLibroDto
    {
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? UrlImagen { get; set; }
        public string? Genero { get; set; }
        public string? Descripcion { get; set; }
        public InventarioLibroDto? InventarioLibroDto { get; set; }
    }
}
