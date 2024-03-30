
using Biblioteca.BL.Contract;
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Libro;
using Biblioteca.BL.Extentions.Libros;
using Biblioteca.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Biblioteca.BL.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository libroRepository;
        private readonly ILogger<LibroService> logger;

        public LibroService(ILibroRepository libroRepository, ILogger<LibroService> logger)
        {
            this.libroRepository = libroRepository;
            this.logger = logger;
        }
        public async Task<ServiceResult> ActualizarLibro(ActualizarLibroDto libroActualizarDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                if (libroActualizarDto == null)
                {
                    throw new Exception("Libro no puede ser nulo");
                }
                if (libroActualizarDto.InventarioLibroDto != null)
                {
                    await this.libroRepository.ActualizarLibro(libroActualizarDto.ConvertActualizarLibroDtoToEntity());

                    result.Success = true;
                    result.Message = "Libro actualizado correctamente";
                }
                else
                {
                    throw new Exception("InventarioLibro no puede ser nulo");
                }

            }

            catch (Exception ex)
            {
                result.Message = "Ocurrio un error actualizado el Libro";
                result.Success = false;
                logger.LogError($"{result.Message},{ex.Message}");
            }
            return result;
        }

        public async Task<ServiceResult> CrearLibro(CrearLibroDto libroCrearDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                if (libroCrearDto == null)
                {
                    throw new Exception("Libro no puede ser nulo");
                }
                if (libroCrearDto.InventarioLibroDto == null)
                {
                    throw new Exception("Libro no puede ser nulo");
                }
                await this.libroRepository.CrearLibro(libroCrearDto.ConvertCrearLibroDtoToEntity());

                result.Success = true;
                result.Message = "Libro agregado correctamente";
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error creando el Libro";
                result.Success = false;
                logger.LogError($"{result.Message}, {ex.Message}");
            }
            return result;
        }

        public async Task<ServiceResult> EliminarLibro(int id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                await this.libroRepository.EliminarLibro(id);

                result.Success = true;
                result.Message = "Libro removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error al intentar remover el libro";
            }

            return result;
        }

        public async Task<ServiceResult> GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.libroRepository.GetAll();
                result.Success = true;
                result.Message = "Libros encontrados Exitosamente ✅";
            }
            catch (Exception ex)
            {
                result.Success = false;
                logger.LogError($"Ocurrio un error obteniendo los Libros {ex.Message}");
            }
            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.libroRepository.GetByID(id);
                result.Success = true;
                result.Message = "Libro encontrado Exitosamente";
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error obteniendo el Libro";
                result.Success = false;
                logger.LogError($"{result.Message}, {ex.Message}");
            }
            return result;
        }

        public async Task<ServiceResult> GetByNameOrAuthor(string searchTerm)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var libros = await this.libroRepository.GetByNameOrAuthor(searchTerm);

                if (libros == null || !libros.Any())
                {
                    result.Message = "No hay libros o autores con ese nombre ❌";
                    result.Success = false;
                }
                else
                {
                    result.Data = libros;

                    result.Success = true;
                    result.Message = "Busqueda realizada correctamente ✅";
                }


            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio un error encontrando los libros";
                result.Success = false;
                logger.LogError($"{result.Message}, {ex.Message}");
            }

            return result;
        }

        public async Task<ServiceResult> RecomendarLibros(int UsuarioID)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await this.libroRepository.RecomendarLibros(UsuarioID);
                result.Success = true;
                result.Message = "Libros Recomendados encontrados Exitosamente ✅";
            }
            catch (Exception ex)
            {
                result.Success = false;
                logger.LogError($"Ocurrio un error obteniendo los Libros Recomendados {ex.Message}");
            }
            return result;
        }
    }
}
