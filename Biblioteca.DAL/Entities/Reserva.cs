

namespace Biblioteca.DAL.Entities
{
    public class Reserva
    {
        public Reserva() { 
            this.FechaHoraReserva = DateTime.Now;
        }
        public int ID { get; set; }
        public int IDUsuario { get; set; }
        public int IDLibro { get; set; }
        public DateTime? FechaHoraReserva { get; set; }
        public string? Estado {  get; set; }
        public string? CodigoAleatorio {  get; set; }
    }
}
