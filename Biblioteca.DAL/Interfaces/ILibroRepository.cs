
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;

namespace Biblioteca.DAL.Interfaces
{
    public interface ILibroRepository : IBaseRepository<Libro>
    {
        Task CrearLibro(Libro libro);
        Task ActualizarLibro(Libro libro);
        Task EliminarLibro(int libroID);
        Task<List<Libro>> GetByNameOrAuthor(string searchTerm);
        Task<List<Libro>> RecomendarLibros(int usuarioID);
    }
}
