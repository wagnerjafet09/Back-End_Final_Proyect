
using Biblioteca.BL.Dto.Libro;
using Biblioteca.DAL.Entities;

namespace Biblioteca.BL.Extentions.Libros
{
    public static class LibroExtention
    {
        public static Libro ConvertActualizarLibroDtoToEntity(this ActualizarLibroDto libroActualizarDto)
        {
            return new Libro
            {
                ID = libroActualizarDto.ID,
                Titulo = libroActualizarDto.Titulo,
                Autor = libroActualizarDto.Autor,
                UrlImagen = libroActualizarDto.UrlImagen,
                Descripcion = libroActualizarDto.Descripcion,
                Genero = libroActualizarDto.Genero,
                InvetarioLibro = ConvertInventarioLibroDtoToEntity(libroActualizarDto.InventarioLibroDto)
            };
        }
        public static Libro ConvertCrearLibroDtoToEntity(this CrearLibroDto libroCrearDto)
        {
            return new Libro
            {
                Titulo = libroCrearDto.Titulo,
                Autor = libroCrearDto.Autor,
                UrlImagen = libroCrearDto.UrlImagen,
                Genero = libroCrearDto.Genero,
                Descripcion = libroCrearDto.Descripcion,
                InvetarioLibro = ConvertInventarioLibroDtoToEntity(libroCrearDto.InventarioLibroDto)
            };
        }
        public static InventarioLibro ConvertInventarioLibroDtoToEntity(this InventarioLibroDto inventarioLibroDto)
        {
            return new InventarioLibro
            {
                CantidadDisponible = inventarioLibroDto.CantidadDisponible,
                CantidadFuera = inventarioLibroDto.CantidadFuera
            };
        }
    }
}
