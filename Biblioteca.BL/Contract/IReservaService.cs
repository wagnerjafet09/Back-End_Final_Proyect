using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Prestamo;
using Biblioteca.BL.Dto.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.BL.Contract
{
    public interface IReservaService : IBaseService
    {
        Task<ServiceResult> NuevaReserva(NuevaReservaDto nuevaReservaDto);
        Task<ServiceResult> VencimientoReserva(int usuarioId);
        Task<ServiceResult> ActualizarCodigoAleatorio(ActualizarCodigoAleatorioDto actualizarCodigoDto);
        Task<ServiceResult> ObtenerReservasConDetalle(int usuarioId);
        Task<ServiceResult> CancelarReserva(int usuarioId);
    }
}
