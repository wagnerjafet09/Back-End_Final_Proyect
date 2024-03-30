
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
