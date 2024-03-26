using Biblioteca.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Context
{
    public class BibliotecaContext: DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {

        }


        #region "DbSets"
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<InventarioLibro> InventarioLibros { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<TransaccionPago> TransaccionesPago { get; set; }
        #endregion
    }
}
