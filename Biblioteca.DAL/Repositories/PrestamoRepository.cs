
using Biblioteca.DAL.Context;
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
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

            InventarioLibro inventario = context.InventarioLibros.First(i => i.LibroID == prestamo.IDLibro);
            try
            {
                if (inventario.CantidadDisponible > 0)
                {
                    inventario.CantidadFuera += 1;
                    inventario.CantidadDisponible -= 1;
                    prestamo.FechaHoraPrestamo = DateTime.Now;
                    prestamo.FechaHoraDevolucion = DateTime.Now.AddDays(30);
                    prestamo.Estado = "Prestado";
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
            Prestamo prestamoExistente = context.Prestamos.First(e => e.IDLibro == prestamo.IDLibro && e.Estado == "Prestado");

            try
            {
                if (inventarioLibro.CantidadFuera > 0 && prestamoExistente.Estado == "Prestado")
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
    }
}
