using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Entities
{
    public class Prestamo
    {
        public int ID { get; set; }
        public int? IDUsuario { get; set; }
        public int? IDLibro { get; set; }
        public DateTime? FechaHoraPrestamo { get; set; }
        public DateTime? FechaHoraDevolucion { get; set; }
        public string? Estado { get; set; }

    }
}
