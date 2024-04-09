using Biblioteca.BL.Contract;
using Biblioteca.BL.Dto.Prestamo;
using Biblioteca.BL.Dto.Reserva;
using Biblioteca.BL.Services;
using Biblioteca.DAL.Entities;
using Biblioteca.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService reservaService;
/*        private readonly IReservaRepository reservaRepository;*/

        public ReservaController(IReservaService reservaService, IReservaRepository reservaRepository)
        {
            this.reservaService = reservaService;
/*            this.reservaRepository = reservaRepository;*/
        }
        // GET: api/<ReservaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await reservaService.GetAll();
            return Ok(result);
        }

        // GET api/<ReservaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await reservaService.GetById(id);
            return Ok(result);
        }

        [HttpPost("VenciminetoReserva")]
        public async Task<IActionResult> Post(VencimientoReservaDto reserva)
        {
            if (reserva != null)
            {
                var result = await this.reservaService.VencimientoReserva(reserva);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("NuevaReserva")]
        public async Task<IActionResult> Post(NuevaReservaDto reserva)
        {
            if (reserva != null)
            {
                var result = await this.reservaService.NuevaReserva(reserva);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("ReservaConDetalles")]
        public async Task<IActionResult> GetReservasConDetalles()
        {
                var result = await this.reservaService.ObtenerReservasConDetalle();
                return Ok(result);
            
        }
    }
}
