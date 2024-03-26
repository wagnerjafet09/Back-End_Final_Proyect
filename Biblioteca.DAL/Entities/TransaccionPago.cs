using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Entities
{
    public class TransaccionPago
    {
        public int ID { get; set; }
        public int? IDUsuario { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaHoraTransaccion { get; set; }
        public bool? Estado { get; set; }
    }
}
