using Biblioteca.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Models
{
    public class PrestamoConDetalle
    {
        public int IDPrestamo { get; set; }
        public int? IDUsuario { get; set; }
        public int? IDLibro { get; set; }
        public DateTime? FechaHoraPrestamo { get; set; }
        public DateTime? FechaHoraDevolucion { get; set; }
        public string? Estado { get; set; }
        public string? CodigoAleatorio { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public bool? Eliminado { get; set; }
    }
}
