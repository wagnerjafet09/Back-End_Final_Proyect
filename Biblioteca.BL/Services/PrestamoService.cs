
using Biblioteca.BL.Contract;
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Prestamo;
using Biblioteca.BL.Extentions.Prestamos;
using Biblioteca.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Biblioteca.BL.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository prestamoRepository;
        private readonly ILogger<PrestamoService> logger;

        public PrestamoService(IPrestamoRepository prestamoRepository, ILogger<PrestamoService> logger)
        {
            this.prestamoRepository = prestamoRepository;
            this.logger = logger;
        }
        public async Task<ServiceResult> GestionPrestamo(PrestamoGestionDto prestamoGestionDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (await prestamoRepository.GestionPrestamo(prestamoGestionDto.ConvertPrestamoGestionDtoToEntity()))
                {
                    result.Success = true;
                    result.Message = "Libro Prestado Correctamente";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Ocurrio un error Haciendo el Prestamo";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Haciendo el Prestamo";
                logger.LogError(ex.Message);
            }
            return result;
        }
        public async Task<ServiceResult> DevolucionLibro(PrestamoDevolucionDto prestamoDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (await prestamoRepository.DevolucionLibro(prestamoDto.ConvertPrestamoDevolucionDtoToEntity()))
                {
                    result.Success = true;
                    result.Message = "Libro Devuelto Correctamente ✅";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Ocurrio un error en en el proceso de Devolucion 👾👾";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Haciendo la Devolucion";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> VencimientoPrestamo(VencimientoPrestamoDto prestamoDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (await prestamoRepository.VencimientoPrestamo(prestamoDto.ConvertVencimientoPrestamoDtoToEntity()))
                {
                    result.Success = true;
                    result.Message = "Estado del prestamo actualizado correctamente ✅";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Ocurrio un error en en el proceso de actualizacion de estado del prestamo 👾👾";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error en en el proceso de actualizacion de estado del prestamo";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.prestamoRepository.GetByID(id);
                result.Success = true;
                result.Message = "Prestamo Encontrado Exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Obteniendo el Prestamo";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.prestamoRepository.GetAll();
                result.Success = true;
                result.Message = "Prestamos Encontrados Exitosamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error Obteniendo los Prestamos ";
                logger.LogError(ex.Message);
            }
            return result;
        }

        public Task<ServiceResult> VencimeintoPrestamo(VencimientoPrestamoDto vencimientoPrestamoDto)
        {
            throw new NotImplementedException();
        }
    }
}
