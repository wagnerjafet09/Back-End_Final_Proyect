using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Entities
{
    public class InventarioLibro
    {
        public int ID { get; set; }
        public int? LibroID { get; set; }
        public int? CantidadTotal { get; set; }

        public int? CantidadFuera { get; set; }

        public int? CantidadDisponible { get; set; }

    }
}
