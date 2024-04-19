
using Biblioteca.DAL.Context;
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
using Biblioteca.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteca.DAL.Repositories
{
    public class PrestamoRepository : BaseRepository<Prestamo>, IPrestamoRepository
    {
        private readonly BibliotecaContext context;
        private readonly ILogger<PrestamoRepository> logger;

        public PrestamoRepository(BibliotecaContext context, ILogger<PrestamoRepository> logger) : base(context)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<bool> GestionPrestamo(Prestamo prestamo)
        {
            Reserva reservaEliminar = context.Reservas.First(r => r.IDLibro == prestamo.IDLibro && r.IDUsuario == prestamo.IDUsuario);
            bool prestamoExistente = context.Prestamos.Any(p => p.IDUsuario == prestamo.IDUsuario && p.IDLibro == prestamo.IDLibro && p.Estado == "Activo");

            try
            {
                if (prestamoExistente == false && prestamo != null)
                {
/*                    inventario.CantidadFuera += 1;
                    inventario.CantidadDisponible -= 1;*/
                    prestamo.FechaHoraPrestamo = DateTime.Now;
                    prestamo.FechaHoraDevolucion = DateTime.Now.AddDays(30);
                    prestamo.Estado = "Activo";
                    reservaEliminar.Estado = "Retirada";
                    await context.Prestamos.AddAsync(prestamo);
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
                logger.LogError($"Ocurrio un error Gestionando el prestamo del libro {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DevolucionLibro(Prestamo prestamo)
        {

            InventarioLibro inventarioLibro = context.InventarioLibros.First(i => i.LibroID == prestamo.IDLibro);
            Prestamo prestamoExistente = context.Prestamos.First(e => e.IDLibro == prestamo.IDLibro && e.Estado == "Activo");

            try
            {
                if (inventarioLibro.CantidadFuera > 0 && prestamoExistente.Estado == "Activo")
                {
                    inventarioLibro.CantidadDisponible += 1;
                    inventarioLibro.CantidadFuera -= 1;
                    prestamoExistente.Estado = "Devuelto";
                    await base.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante la devolución del préstamo");
                throw;
            }
        }

        public async override Task<List<Prestamo>> GetAll()
        {

            List<Prestamo> prestamos = new List<Prestamo>();
            try
            {
                prestamos = await base.GetAll();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error Obteniendo los Prestamos  {ex.Message}");
            }
            return prestamos;
        }
        public async override Task<Prestamo> GetByID(int id)
        {
            Prestamo prestamo = new Prestamo();
            try
            {
                prestamo = await base.GetByID(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error Obteniendo el Prestamo {ex.Message}");
            }
            return prestamo;
        }

        public async Task<bool> VencimientoPrestamo(int idUsuario)
        {
            try
            {
                var prestamosUsuario = context.Prestamos.Where(p => p.IDUsuario == idUsuario && DateTime.Now >= p.FechaHoraDevolucion && p.Estado != "Devuelto");

                foreach (var prestamo in prestamosUsuario)
                {
                    prestamo.Estado = "Vencido";
                    /*context.Prestamos.Update(prestamo);*/
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrió un error al procesar el vencimiento del prestamo: {ex.Message}");
                return false;
            }
        }
        public async Task<List<PrestamoConDetalle>> ObtenerPrestamosConDetalle(int usuarioId)
        {
            await this.VencimientoPrestamo(usuarioId);
            using (this.context)
            {
                var prestamosConDetalle = await context.Prestamos
                    .Where(prestamo => prestamo.IDUsuario == usuarioId && (prestamo.Estado == "Activo" || prestamo.Estado == "Vencido"))
                    .Join(context.Libros,
                        prestamo => prestamo.IDLibro,
                        libro => libro.ID,
                        (prestamo, libro) => new PrestamoConDetalle
                        {
                            IDPrestamo = prestamo.ID,
                            IDUsuario = prestamo.IDUsuario,
                            IDLibro = prestamo.IDLibro,
                            FechaHoraPrestamo = prestamo.FechaHoraPrestamo,
                            FechaHoraDevolucion = prestamo.FechaHoraDevolucion,
                            Estado = prestamo.Estado,
                            Titulo = libro.Titulo,
                            Autor = libro.Autor,
                            Genero = libro.Genero,
                            Descripcion = libro.Descripcion,
                            UrlImagen = libro.UrlImagen,
                            Eliminado = libro.Eliminado
                        })
                    .ToListAsync();

                return prestamosConDetalle;
            }
        }
        }
}
