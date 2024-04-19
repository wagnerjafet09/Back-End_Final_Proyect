using Biblioteca.BL.Contract;
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Prestamo;
using Biblioteca.BL.Dto.Reserva;
using Biblioteca.BL.Extentions.Reservas;
using Biblioteca.DAL.Interfaces;
using Biblioteca.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.BL.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository reservaRepository;
        private readonly ILogger<ReservaService> logger;
        public ReservaService(IReservaRepository reservaRepository, ILogger<ReservaService> logger)
        {
            this.reservaRepository = reservaRepository;
            this.logger = logger;
        }
        public async Task<ServiceResult> NuevaReserva(NuevaReservaDto nuevaReservaDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (await reservaRepository.NuevaReserva(nuevaReservaDto.ConvertNuevaReservaDtoToEntity()))
                {
                    result.Success = true;
                    result.Message = "Libro Reservado Correctamente ✅";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Ocurrio un error haciendo la Reserva 👾👾";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error haciendo la Reserva";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> VencimientoReserva(int usuarioId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (await reservaRepository.VencimientoReserva(usuarioId))
                {
                    result.Success = true;
                    result.Message = "El estado de la reserva fue actualizado con exito ✅";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Ocurrio un error al intentar cambiar el estado de la Reserva 👾👾";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error al intentar cambiar el estado de la Reserva";
                logger.LogError(ex.Message);
            }
            return result;
        }
        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.reservaRepository.GetByID(id);
                result.Success = true;
                result.Message = "Reserva encontrada exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo la Reserva";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.reservaRepository.GetAll();
                result.Success = true;
                result.Message = "Reservas encontradas exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo las Reservas";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> ObtenerReservasConDetalle(int usuarioId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.reservaRepository.ObtenerReservasConDetalle(usuarioId);
                result.Success = true;
                result.Message = "Reservas con detalle encontradas exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo las Reservas con detalle";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> CancelarReserva(int reservaId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.reservaRepository.CancelarReserva(reservaId);
                result.Success = true;
                result.Message = "Reserva cancelada exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error cancelando la Reserva";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> ActualizarCodigoAleatorio(ActualizarCodigoAleatorioDto actualizarCodigoDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Success = true;
                result.Message = "El codigo aleatorio de la reserva fue actualizado con exito ✅";
                result.Data = await reservaRepository.ActualizarCodigoAleatorio(actualizarCodigoDto.ConvertActualizarCodigoAleatorioDtoToEntity());
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error al intentar actualizar el codigo de la Reserva";
                logger.LogError(ex.Message);
            }
            return result;
        }
    }
}
