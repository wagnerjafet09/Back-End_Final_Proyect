using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Entities
{
    public class Usuario
    {
        public int ID { get; set; }
        public string? Nombre { get; set; }
        public string? Contraseña { get; set; }
        public string? CorreoElectronico { get; set; }
        public bool? EsAdmin { get; set; }
    }
}
