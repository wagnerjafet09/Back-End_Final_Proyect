
using Biblioteca.DAL.Context;
using Biblioteca.DAL.Core;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.DAL.Repositories
{
    public class LibroRepository : BaseRepository<Libro>, ILibroRepository
    {
        private readonly BibliotecaContext context;

        public LibroRepository(BibliotecaContext context) : base(context)
        {
            this.context = context;
        }
        public async override Task<List<Libro>> GetAll()
        {
            List<Libro> libros = new List<Libro>();
            libros = context.Set<Libro>().Include(l => l.InvetarioLibro).Where(cd => cd.Eliminado != true).ToList();
            return libros;
        }

        public async override Task<Libro> GetByID(int id)
        {
            var libro = this.context.Libros.Include(l => l.InvetarioLibro).First(libro => libro.ID == id && libro.Eliminado != true);

            return libro;
        }

        public async override Task SaveChanges()
        {
            await base.SaveChanges();
        }
        public async Task ActualizarLibro(Libro libro)
        {
            var existingLibro = await GetByID(libro.ID);

            if (existingLibro != null)
            {

                if (existingLibro.Eliminado != true)
                {
                    existingLibro.Titulo = libro.Titulo;
                    existingLibro.Autor = libro.Autor;
                    existingLibro.UrlImagen = libro.UrlImagen;
                    existingLibro.Eliminado = false;
                    existingLibro.InvetarioLibro = libro.InvetarioLibro;
                    existingLibro.Descripcion = libro.Descripcion;
                    existingLibro.Genero = libro.Genero;
                    context.Libros.Update(existingLibro);

                    await this.SaveChanges();
                }
            }
        }

        public async Task CrearLibro(Libro libro)
        {
            if (libro != null)
            {
                if (libro.Eliminado != true)
                {
                    await context.Libros.AddAsync(libro);
                    await context.InventarioLibros.AddAsync(libro.InvetarioLibro);
                    await this.SaveChanges();
                }
            }
        }

        public async Task EliminarLibro(int libroID)
        {
            var existingLibro = await this.GetByID(libroID);

            if (existingLibro != null)
            {
                existingLibro.Eliminado = true;
                await this.SaveChanges();
            }
        }

        public async Task<List<Libro>> GetByNameOrAuthor(string searchTerm)
        {
            var termNormalized = searchTerm.ToLower();

            var libros = await context.Libros.Include(l => l.InvetarioLibro)
                .Where(libro => libro.Titulo.ToLower().Contains(termNormalized) ||
                        libro.Autor.ToLower().Contains(termNormalized))
                .Where(libro => libro.Eliminado != true)
                .ToListAsync();
            return libros;
        }

        public async Task<List<Libro>> RecomendarLibros(int usuarioID)
        {
            var librosReservadosUsuario = await context.Reservas
                .Where(r => r.IDUsuario == usuarioID)
                .Select(r => r.IDLibro)
                .ToListAsync();

            var otrosUsuarios = await context.Reservas
                .Where(r => librosReservadosUsuario.Contains(r.IDLibro) && r.IDUsuario != usuarioID)
                .Select(r => r.IDUsuario)
                .Distinct()
                .ToListAsync();

            var librosRecomendados = await context.Reservas
                .Where(r => otrosUsuarios.Contains(r.IDUsuario) && !librosReservadosUsuario.Contains(r.IDLibro))
                .Select(r => r.IDLibro)
                .ToListAsync();

            var libros = await context.Libros
                .Include(l => l.InvetarioLibro)
                .Where(l => librosRecomendados.Contains(l.ID))
                .ToListAsync();

            return libros;
        }

        public async Task<List<Libro>> LibrosReservados(int usuarioID)
        {
            var reservas = await context.Reservas
                                    .Where(r => r.IDUsuario == usuarioID && r.Estado == "Confirmada")
                                    .Select(r => r.IDLibro)
                                    .ToListAsync();
            var librosReservados = await context.Libros.Include(l => l.InvetarioLibro).Where(l => reservas.Contains(l.ID)).ToListAsync();
            return librosReservados;
        }
    }
}
