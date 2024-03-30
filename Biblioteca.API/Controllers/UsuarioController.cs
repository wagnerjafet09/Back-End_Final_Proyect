using Biblioteca.BL.Contract;
using Biblioteca.BL.Core;
using Biblioteca.BL.Dto.Usuario;
using Biblioteca.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.usuarioService.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this.usuarioService.GetById(id);
            return Ok(result);
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Registro(UsuarioResgistroDto usuarioResgistroDto)
        {
            if (usuarioResgistroDto != null)
            {
                var result = await this.usuarioService.RegistroUsuario(usuarioResgistroDto);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("InicioDeSesion")]
        public async Task<IActionResult> InicioSesion(UsuarioLoginDto usuarioLoginDto)
        {
            if (usuarioLoginDto != null)
            {
                var result = await this.usuarioService.LoginUsuario(usuarioLoginDto);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
