using System;

namespace Biblioteca.BL.Dto.Libro
{
    public class ActualizarLibroDto
    {
        public int ID { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? UrlImagen { get; set; }
        public string? Genero { get; set; }
        public string? Descripcion { get; set; }
        public bool? Eliminado { get; set; }
        public InventarioLibroDto? InventarioLibroDto { get; set; }
    }
}
