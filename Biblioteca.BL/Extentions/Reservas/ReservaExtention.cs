using Biblioteca.BL.Dto.Reserva;
using Biblioteca.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.BL.Extentions.Reservas
{
    public static class ReservaExtention
    {
        public static Reserva ConvertNuevaReservaDtoToEntity(this NuevaReservaDto nuevaReservaDto)
        {
            return new Reserva()
            {
                IDUsuario = nuevaReservaDto.IDUsuario,
                IDLibro = nuevaReservaDto.IDLibro
            };
        }
        public static Reserva ConvertVencimientoReservaDtoToEntity(this VencimientoReservaDto vencimientoReservaDto)
        {
            return new Reserva()
            {
                ID = vencimientoReservaDto.ID,
                IDLibro = vencimientoReservaDto.IDLibro
            };
        }
        public static Reserva ConvertActualizarCodigoAleatorioDtoToEntity(this ActualizarCodigoAleatorioDto actualizarCodigoAleatorioDto)
        {
            return new Reserva()
            {
                IDUsuario = actualizarCodigoAleatorioDto.IDUsuario,
                IDLibro = actualizarCodigoAleatorioDto.IDLibro,
                CodigoAleatorio = actualizarCodigoAleatorioDto.CodigoAleatorio
            };
        }
    }
}
