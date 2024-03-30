using Biblioteca.BL.Contract;
using Biblioteca.BL.Dto.Libro;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService libroService;

        public LibroController(ILibroService libroService)
        {
            this.libroService = libroService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.libroService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this.libroService.GetById(id);
            return Ok(result);
        }

        [HttpGet("searchTerm")]
        public async Task<IActionResult> GetByNameOrAuthor(string searchTerm)
        {
            var result = await this.libroService.GetByNameOrAuthor(searchTerm);
            return Ok(result);
        }

        [HttpPost("CrearLibro")]
        public async Task<IActionResult> Post(CrearLibroDto libro)
        {
            var result = await this.libroService.CrearLibro(libro);
            return Ok(result);
        }

        [HttpPost("ActualizarLibro")]
        public async Task<IActionResult> Put(ActualizarLibroDto libro)
        {
            var result = await this.libroService.ActualizarLibro(libro);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.libroService.EliminarLibro(id);
            return Ok(result);
        }
        [HttpGet("LibrosRecomendados")]
        public async Task<IActionResult> GetLibrosRecomendados(int UsuarioID) 
        {
            var result = await this.libroService.RecomendarLibros(UsuarioID);
            return Ok(result);
        }
    }
}
