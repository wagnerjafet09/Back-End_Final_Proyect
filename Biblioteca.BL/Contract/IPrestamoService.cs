
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Prestamo;

namespace Biblioteca.BL.Contract
{
    public interface IPrestamoService : IBaseService
    {
        Task<ServiceResult> GestionPrestamo(PrestamoGestionDto prestamoGestionDto);
        Task<ServiceResult> DevolucionLibro(PrestamoDevolucionDto prestamoDto);
        Task<ServiceResult> VencimientoPrestamo(int usuarioId);
        Task<ServiceResult> ObtenerPrestamosConDetalle(int usuarioId);
    }
}
