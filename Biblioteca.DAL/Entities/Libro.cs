using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Entities
{
    public class Libro
    {
        public Libro() {
            this.Eliminado = false;
        }
        public int ID { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public bool? Eliminado { get; set; }
        public InventarioLibro InvetarioLibro { get; set; }

    }
}
