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

        public async Task<string> ActualizarCodigoAleatorio(Reserva reserva)
        {
            Reserva reservaExistente = new Reserva();
            try
            {
                reservaExistente = await this.context.Reservas.FirstAsync(r => r.IDUsuario == reserva.IDUsuario && r.IDLibro == reserva.IDLibro && r.Estado == "Confirmada");
                reservaExistente.CodigoAleatorio = reserva.CodigoAleatorio;
                await SaveChanges();
                return reservaExistente.CodigoAleatorio;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error actualizando el codigo aleatorio {ex.Message}");
                return reservaExistente.CodigoAleatorio;
            }
        }

        public async Task<bool> CancelarReserva(int reservaId)
        {
            Reserva reserva = new Reserva();
            try
            {
                reserva = await this.context.Reservas.FirstAsync(r => r.ID == reservaId);
                reserva.Estado = "Cancelada";
                await SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error obteniendo la Reserva a cancelar {ex.Message}");
                return false;
            }
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
            bool reservaExistente = context.Reservas.Any(r => r.IDUsuario == reserva.IDUsuario && r.IDLibro == reserva.IDLibro && (r.Estado == "Confirmada" || r.Estado == "Retirada"));
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

        public async Task<List<ReservaConDetalle>> ObtenerReservasConDetalle(int usuarioId)
        {
            await this.VencimientoReserva(usuarioId);
            using (this.context)
            {
                var reservasConDetalle = await context.Reservas
                                    .Where(reserva => reserva.IDUsuario == usuarioId && reserva.Estado == "Confirmada")
                                    .Join(context.Libros,
                                        reserva => reserva.IDLibro,
                                        libro => libro.ID,
                                        (reserva, libro) => new ReservaConDetalle
                                        {
                                            IDReserva = reserva.ID,
                                            FechaHoraReserva = reserva.FechaHoraReserva,
                                            Autor = libro.Autor,
                                            Titulo = libro.Titulo,
                                            UrlImagen = libro.UrlImagen,
                                            IDLibro = reserva.IDLibro,
                                            IDUsuario = reserva.IDUsuario,
                                            Estado = reserva.Estado,
                                            CodigoAleatorio = reserva.CodigoAleatorio,
                                        })
                                    .ToListAsync();

                return reservasConDetalle;
            }
            
        }

        public async Task<bool> VencimientoReserva(int idUsuario)
        {
            try
            {
                /*InventarioLibro inventario = context.InventarioLibros.First(i => i.LibroID == reserva.IDLibro);*/
                var reservasUsuario = await context.Reservas.Where(r => r.IDUsuario == idUsuario && r.Estado != "Retirada" && r.Estado != "Cancelada").ToListAsync();

                foreach (var reserva in reservasUsuario)
                {
                    // Calcular la diferencia de días entre la fecha de reserva y la fecha actual

                    TimeSpan? diasTranscurridos = DateTime.Now - reserva.FechaHoraReserva;
                    int diasTranscurridosInt = diasTranscurridos.HasValue ? (int)diasTranscurridos.Value.TotalDays : 0;

                    if (diasTranscurridosInt >= 30) // Cancelar la reserva si han pasado más de 30 días
                    {
                        InventarioLibro inventario = context.InventarioLibros.First(i => i.LibroID == reserva.IDLibro);

                        inventario.CantidadFuera -= 1;
                        inventario.CantidadDisponible += 1;
                        reserva.Estado = "Vencida";
                        /*context.Reservas.Update(reserva);*/
                    }


                }
                
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrió un error al procesar el vencimiento de la reserva: {ex.Message}");
                return false;
            }
        }

    }
}

