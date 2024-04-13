
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
        public static Prestamo ConvertVencimientoPrestamoDtoToEntity(this VencimientoPrestamoDto vencimientoDto)
        {
            return new Prestamo()
            {
                ID = vencimientoDto.ID
            };
        }
    }
}
