using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Models
{
    public class ReservaConDetalle
    {
        public ReservaConDetalle()
        {
            this.FechaHoraReserva = DateTime.Now;
        }
        public int IDReserva { get; set; }
        public int IDUsuario { get; set; }
        public int IDLibro { get; set; }
        public DateTime? FechaHoraReserva { get; set; }
        public string? Estado { get; set; }
        public string? CodigoAleatorio { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? UrlImagen { get; set; }
    }
}
