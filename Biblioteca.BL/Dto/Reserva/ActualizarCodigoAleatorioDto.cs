using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.BL.Dto.Reserva
{
    public class ActualizarCodigoAleatorioDto
    {
        public int IDUsuario { get; set; }
        public int IDLibro { get; set; }
        public string? CodigoAleatorio { get; set; }
    }
}
