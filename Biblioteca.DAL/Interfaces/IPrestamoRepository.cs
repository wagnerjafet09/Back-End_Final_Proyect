
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Models;

namespace Biblioteca.DAL.Interfaces
{
    public interface IPrestamoRepository : IBaseRepository<Prestamo>
    {
        Task<bool> GestionPrestamo(Prestamo prestamo);
        Task<bool> DevolucionLibro(Prestamo prestamo);
        Task<bool> VencimientoPrestamo(int usuarioId);
        Task<string> ActualizarCodigoAleatorio(Prestamo prestamo);
        Task<List<PrestamoConDetalle>> ObtenerPrestamosConDetalle(int usuarioId);
    }
}
