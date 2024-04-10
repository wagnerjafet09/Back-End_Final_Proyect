using Biblioteca.BL.Contract;
using Biblioteca.BL.Dto.Prestamo;
using Biblioteca.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService prestamoService;
        private readonly IPrestamoRepository prestamoRepository;

        public PrestamoController(IPrestamoService prestamoService, IPrestamoRepository prestamoRepository)
        {
            this.prestamoService = prestamoService;
            this.prestamoRepository = prestamoRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await prestamoService.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await prestamoService.GetById(id);
            return Ok(result);
        }
        [HttpPost("GestionPrestamo")]
        public async Task<IActionResult> Post(PrestamoGestionDto prestamoGestionDto)
        {
            if (prestamoGestionDto != null)
            {
                var result = await this.prestamoService.GestionPrestamo(prestamoGestionDto);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("DevolucionLibro")]
        public async Task<IActionResult> Post(PrestamoDevolucionDto prestamoDevolucionDto)
        {
            if (prestamoDevolucionDto != null)
            {
                var result = await this.prestamoService.DevolucionLibro(prestamoDevolucionDto);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("VencimientoPrestamo")]
        public async Task<IActionResult> Post(VencimientoPrestamoDto vencimientoPrestamoDto)
        {
            if (vencimientoPrestamoDto != null)
            {
                var result = await this.prestamoService.VencimientoPrestamo(vencimientoPrestamoDto);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("PrestamosConDetalle")]
        public async Task<IActionResult> Post(int usuarioId)
        {
            var result = await this.prestamoService.ObtenerPrestamosConDetalle(usuarioId);
            return Ok(result);
        }

    }
}
