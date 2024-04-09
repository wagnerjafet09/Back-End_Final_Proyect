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
        Task<List<ReservaConDetalles>> ObtenerReservasConDetalle(int usuarioId);
    }
}
