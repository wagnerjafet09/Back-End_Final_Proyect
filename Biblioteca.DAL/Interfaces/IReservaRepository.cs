using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Interfaces
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        Task<bool> NuevaReserva(Reserva reserva);
        Task<bool> VencimientoReserva(Reserva reserva);
        Task<bool> CancelarReserva(int reservaId);
        Task<string> ActualizarCodigoAleatorio(Reserva codigo);
        Task<List<ReservaConDetalle>> ObtenerReservasConDetalle(int usuarioId);
    }
}
