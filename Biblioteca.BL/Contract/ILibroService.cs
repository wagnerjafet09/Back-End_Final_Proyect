
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Libro;

namespace Biblioteca.BL.Contract
{
    public interface ILibroService : IBaseService
    {
        Task<ServiceResult> ActualizarLibro(ActualizarLibroDto libroActualizarDto);
        Task<ServiceResult> CrearLibro(CrearLibroDto libroCrearDto);
        Task<ServiceResult> GetByNameOrAuthor(string searchTerm);
        Task<ServiceResult> EliminarLibro(int id);
        Task<ServiceResult> RecomendarLibros(int UsuarioID);
        Task<ServiceResult> LibrosReservados(int usuarioId);
    }
}
