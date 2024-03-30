
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;

namespace Biblioteca.DAL.Interfaces
{
    public interface IPrestamoRepository : IBaseRepository<Prestamo>
    {
        Task<bool> GestionPrestamo(Prestamo prestamo);
        Task<bool> DevolucionLibro(Prestamo prestamo);
    }
}
