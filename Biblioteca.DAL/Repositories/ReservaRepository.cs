using Biblioteca.DAL.Context;
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
using Biblioteca.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Repositories
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        private readonly BibliotecaContext context;
        private readonly ILogger<ReservaRepository> logger;

        public ReservaRepository(BibliotecaContext context, ILogger<ReservaRepository> logger) : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public async override Task<List<Reserva>> GetAll()
        {
            List<Reserva> reservas = new List<Reserva>();
            try
            {
                reservas = await base.GetAll();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error obteniendo las Reservas  {ex.Message}");
            }
            return reservas;
        }
        public async override Task<Reserva> GetByID(int id)
        {
            Reserva reserva = new Reserva();
            try
            {
                reserva = await base.GetByID(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error obteniendo las Reservas {ex.Message}");
            }
            return reserva;
        }
        public async Task<bool> NuevaReserva(Reserva reserva)
        {
            InventarioLibro inventario = context.InventarioLibros.First(i => i.LibroID == reserva.IDLibro);
            // Verificar si el usuario ya tiene una reserva activa para el mismo libro
            bool reservaExistente = context.Reservas.Any(r => r.IDUsuario == reserva.IDUsuario && r.IDLibro == reserva.IDLibro && r.Estado == "Confirmada");
            try
            {
                if (reservaExistente == false && inventario.CantidadDisponible > 0)
                {
                    inventario.CantidadFuera += 1;
                    inventario.CantidadDisponible -= 1;
                    reserva.FechaHoraReserva = DateTime.Now;
                    /*reserva.FechaHoraDevolucion = DateTime.Now.AddDays(30);*/
                    reserva.Estado = "Confirmada";
                    await context.Reservas.AddAsync(reserva);
                    await this.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error Gestionando la reserva del libro {ex.Message}");
                return false;
            }
        }

        public async Task<List<ReservaConDetalles>> ObtenerReservasConDetalle()
        {
            using (this.context)
            {
                var reservasConDetalle = await context.Reservas
                    .Join(context.Libros,
                        reserva => reserva.IDLibro,
                libro => libro.ID,
                        (reserva, libro) => new ReservaConDetalles
                        {
                            ID = reserva.ID,
                            FechaHoraReserva = reserva.FechaHoraReserva,
                            Autor = libro.Autor,
                            Titulo = libro.Titulo,
                            UrlImagen = libro.UrlImagen
                        })
                    .ToListAsync();

                return reservasConDetalle;
            }
        }

        public async Task<bool> VencimientoReserva(Reserva reserva)
        {
            try
            {
                InventarioLibro inventario = context.InventarioLibros.First(i => i.LibroID == reserva.IDLibro);
                var reservaExistente = context.Reservas.First(r => r.ID == reserva.ID);
                // Calcular la diferencia de días entre la fecha de reserva y la fecha actual
                /*TimeSpan? diasTranscurridos = DateTime.Now - reserva.FechaHoraReserva;
                int diasTranscurridosInt = (int)diasTranscurridos?.TotalDays;*/
                TimeSpan? diasTranscurridos = DateTime.Now - reservaExistente.FechaHoraReserva;
                int diasTranscurridosInt = diasTranscurridos.HasValue ? (int)diasTranscurridos.Value.TotalDays : 0;

                if (diasTranscurridosInt >= 30) // Cancelar la reserva si han pasado más de 30 días
                {
                    inventario.CantidadFuera -= 1;
                    inventario.CantidadDisponible += 1;
                    reservaExistente.Estado = "Vencida";
/*                    context.Reservas.Update(reserva);*/
                    await context.SaveChangesAsync(); // Guardar el cambio de estado de la reserva
                    return true;
                }
                else
                {
                    return false; // La reserva no ha vencido aún
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrió un error al procesar el vencimiento de la reserva: {ex.Message}");
                return false;
            }
        }

    }
}

