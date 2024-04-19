
using Biblioteca.BL.Dto.Prestamo;
using Biblioteca.DAL.Entities;

namespace Biblioteca.BL.Extentions.Prestamos
{
    public static class PrestamoExtension
    {
        public static Prestamo ConvertPrestamoGestionDtoToEntity(this PrestamoGestionDto prestamoGestionDto)
        {
            return new Prestamo()
            {
                IDUsuario = prestamoGestionDto.IDUsuario,
                IDLibro = prestamoGestionDto.IDLibro
            };
        }
        public static Prestamo ConvertPrestamoDevolucionDtoToEntity(this PrestamoDevolucionDto devolucionDto)
        {
            return new Prestamo()
            {
                IDUsuario = devolucionDto.IDUsuario,
                IDLibro = devolucionDto.IDLibro
            };
        }
        public static Prestamo ConvertActualizarCodigoAleatorioDtoToEntity(this ActualizarCodigoDto actualizarCodigoAleatorioDto)
        {
            return new Prestamo()
            {
                IDUsuario = actualizarCodigoAleatorioDto.IDUsuario,
                IDLibro = actualizarCodigoAleatorioDto.IDLibro,
                CodigoAleatorio = actualizarCodigoAleatorioDto.CodigoAleatorio
            };
        }
    }
}
